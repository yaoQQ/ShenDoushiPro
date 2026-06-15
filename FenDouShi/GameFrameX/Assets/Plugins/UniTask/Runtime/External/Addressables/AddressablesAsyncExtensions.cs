// asmdef 版本定义，当导入 com.unity.addressables 时启用。

// 引入 Cysharp.Threading.Tasks 内部命名空间
using Cysharp.Threading.Tasks.Internal;
// 引入 System 命名空间
using System;
// 引入运行时编译器服务命名空间
using System.Runtime.CompilerServices;
// 引入异常处理服务命名空间
using System.Runtime.ExceptionServices;
// 引入线程命名空间
using System.Threading;
// 引入 Unity 可寻址资源命名空间
using UnityEngine.AddressableAssets;
// 引入 Unity 资源管理异步操作命名空间
using UnityEngine.ResourceManagement.AsyncOperations;

// 定义 Cysharp.Threading.Tasks 命名空间
namespace Cysharp.Threading.Tasks
{
    // 定义静态类 AddressablesAsyncExtensions，用于扩展 Addressables 异步操作
    public static class AddressablesAsyncExtensions
    {
#region AsyncOperationHandle

        // 为 AsyncOperationHandle 类型扩展 GetAwaiter 方法，用于获取 UniTask 的等待器
        public static UniTask.Awaiter GetAwaiter(this AsyncOperationHandle handle)
        {
            // 调用 ToUniTask 方法并获取其等待器
            return ToUniTask(handle).GetAwaiter();
        }

        // 为 AsyncOperationHandle 类型扩展 WithCancellation 方法，用于支持取消操作
        public static UniTask WithCancellation(this AsyncOperationHandle handle, CancellationToken cancellationToken, bool cancelImmediately = false, bool autoReleaseWhenCanceled = false)
        {
            // 调用 ToUniTask 方法并传入取消令牌等参数
            return ToUniTask(handle, cancellationToken: cancellationToken, cancelImmediately: cancelImmediately, autoReleaseWhenCanceled: autoReleaseWhenCanceled);
        }

        // 为 AsyncOperationHandle 类型扩展 ToUniTask 方法，将其转换为 UniTask
        public static UniTask ToUniTask(this AsyncOperationHandle handle, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false, bool autoReleaseWhenCanceled = false)
        {
            // 如果取消令牌已请求取消，则返回一个已取消的 UniTask
            if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled(cancellationToken);

            // 如果操作句柄无效，则返回一个已完成的 UniTask
            if (!handle.IsValid())
            {
                // autoReleaseHandle:true handle is invalid(immediately internal handle == null) so return completed.
                return UniTask.CompletedTask;
            }

            // 如果操作已完成
            if (handle.IsDone)
            {
                // 如果操作状态为失败，则返回一个包含异常的 UniTask
                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    return UniTask.FromException(handle.OperationException);
                }
                // 操作成功完成，返回一个已完成的 UniTask
                return UniTask.CompletedTask;
            }

            // 创建一个新的 UniTask 并返回
            return new UniTask(AsyncOperationHandleConfiguredSource.Create(handle, timing, progress, cancellationToken, cancelImmediately, autoReleaseWhenCanceled, out var token), token);
        }

        // 定义 AsyncOperationHandleAwaiter 结构体，实现 ICriticalNotifyCompletion 接口
        public struct AsyncOperationHandleAwaiter : ICriticalNotifyCompletion
        {
            // 存储异步操作句柄
            AsyncOperationHandle handle;
            // 存储延续操作
            Action<AsyncOperationHandle> continuationAction;

            // 构造函数，初始化异步操作句柄
            public AsyncOperationHandleAwaiter(AsyncOperationHandle handle)
            {
                this.handle = handle;
                this.continuationAction = null;
            }

            // 属性，判断操作是否已完成
            public bool IsCompleted => handle.IsDone;

            // 获取操作结果
            public void GetResult()
            {
                // 如果存在延续操作，则移除该操作
                if (continuationAction != null)
                {
                    handle.Completed -= continuationAction;
                    continuationAction = null;
                }

                // 如果操作状态为失败，则抛出异常
                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    var e = handle.OperationException;
                    handle = default;
                    ExceptionDispatchInfo.Capture(e).Throw();
                }

                // 获取操作结果
                var result = handle.Result;
                handle = default;
            }

            // 实现 OnCompleted 方法，调用 UnsafeOnCompleted 方法
            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            // 实现 UnsafeOnCompleted 方法，注册延续操作
            public void UnsafeOnCompleted(Action continuation)
            {
                // 检查延续操作是否已注册，如果已注册则抛出异常
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                // 创建一个池化的委托并赋值给延续操作
                continuationAction = PooledDelegate<AsyncOperationHandle>.Create(continuation);
                // 为操作完成事件添加延续操作
                handle.Completed += continuationAction;
            }
        }

        // 定义密封类 AsyncOperationHandleConfiguredSource，实现 IUniTaskSource、IPlayerLoopItem 和 ITaskPoolNode 接口
        sealed class AsyncOperationHandleConfiguredSource : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<AsyncOperationHandleConfiguredSource>
        {
            // 定义静态任务池
            static TaskPool<AsyncOperationHandleConfiguredSource> pool;
            // 指向下一个节点
            AsyncOperationHandleConfiguredSource nextNode;
            // 获取下一个节点的引用
            public ref AsyncOperationHandleConfiguredSource NextNode => ref nextNode;

            // 静态构造函数，注册任务池大小获取器
            static AsyncOperationHandleConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AsyncOperationHandleConfiguredSource), () => pool.Size);
            }

            // 存储操作完成时的回调
            readonly Action<AsyncOperationHandle> completedCallback;
            // 存储异步操作句柄
            AsyncOperationHandle handle;
            // 存储取消令牌
            CancellationToken cancellationToken;
            // 存储取消令牌的注册信息
            CancellationTokenRegistration cancellationTokenRegistration;
            // 存储进度报告对象
            IProgress<float> progress;
            // 标识取消时是否自动释放句柄
            bool autoReleaseWhenCanceled;
            // 标识是否立即取消
            bool cancelImmediately;
            // 标识操作是否已完成
            bool completed;

            // 存储 UniTask 完成源核心
            UniTaskCompletionSourceCore<AsyncUnit> core;

            // 构造函数，初始化完成回调
            AsyncOperationHandleConfiguredSource()
            {
                completedCallback = HandleCompleted;
            }

            // 静态方法，创建 AsyncOperationHandleConfiguredSource 实例
            public static IUniTaskSource Create(AsyncOperationHandle handle, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, bool autoReleaseWhenCanceled, out short token)
            {
                // 如果取消令牌已请求取消，则创建一个已取消的 UniTask 完成源
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                // 尝试从任务池获取实例，如果失败则创建一个新实例
                if (!pool.TryPop(out var result))
                {
                    result = new AsyncOperationHandleConfiguredSource();
                }

                // 初始化实例属性
                result.handle = handle;
                result.progress = progress;
                result.cancellationToken = cancellationToken;
                result.cancelImmediately = cancelImmediately;
                result.autoReleaseWhenCanceled = autoReleaseWhenCanceled;
                result.completed = false;
                
                // 如果需要立即取消且取消令牌可以被取消，则注册取消回调
                if (cancelImmediately && cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var promise = (AsyncOperationHandleConfiguredSource)state;
                        if (promise.autoReleaseWhenCanceled && promise.handle.IsValid())
                        {
                            Addressables.Release(promise.handle);
                        }
                        promise.core.TrySetCanceled(promise.cancellationToken);
                    }, result);
                }

                // 跟踪活动任务
                TaskTracker.TrackActiveTask(result, 3);

                // 将实例添加到玩家循环中
                PlayerLoopHelper.AddAction(timing, result);

                // 为操作完成事件添加回调
                handle.Completed += result.completedCallback;

                // 获取版本令牌
                token = result.core.Version;
                return result;
            }

            // 处理操作完成事件
            void HandleCompleted(AsyncOperationHandle _)
            {
                // 如果操作句柄有效，则移除完成回调
                if (handle.IsValid())
                {
                    handle.Completed -= completedCallback;
                }

                // 如果操作已完成，则直接返回
                if (completed)
                {
                    return;
                }
                
                // 标记操作已完成
                completed = true;
                // 如果取消令牌已请求取消
                if (cancellationToken.IsCancellationRequested)
                {
                    // 如果需要自动释放且操作句柄有效，则释放句柄
                    if (autoReleaseWhenCanceled && handle.IsValid())
                    {
                        Addressables.Release(handle);
                    }
                    // 设置任务状态为已取消
                    core.TrySetCanceled(cancellationToken);
                }
                // 如果操作状态为失败
                else if (handle.Status == AsyncOperationStatus.Failed)
                {
                    // 设置任务状态为异常
                    core.TrySetException(handle.OperationException);
                }
                else
                {
                    // 设置任务状态为成功
                    core.TrySetResult(AsyncUnit.Default);
                }
            }

            // 获取任务结果
            public void GetResult(short token)
            {
                try
                {
                    // 从核心获取结果
                    core.GetResult(token);
                }
                finally
                {
                    // 如果不是立即取消且取消令牌已请求取消的情况
                    if (!(cancelImmediately && cancellationToken.IsCancellationRequested))
                    {
                        // 尝试将实例返回到任务池
                        TryReturn();
                    }
                    else
                    {
                        // 移除任务跟踪
                        TaskTracker.RemoveTracking(this);
                    }
                }
            }

            // 获取任务状态
            public UniTaskStatus GetStatus(short token)
            {
                // 从核心获取任务状态
                return core.GetStatus(token);
            }

            // 不安全地获取任务状态
            public UniTaskStatus UnsafeGetStatus()
            {
                // 从核心不安全地获取任务状态
                return core.UnsafeGetStatus();
            }

            // 注册延续操作
            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                // 调用核心的 OnCompleted 方法
                core.OnCompleted(continuation, state, token);
            }

            // 执行玩家循环的下一步操作
            public bool MoveNext()
            {
                // 如果操作已完成，则返回 false
                if (completed)
                {
                    return false;
                }

                // 如果取消令牌已请求取消
                if (cancellationToken.IsCancellationRequested)
                {
                    // 标记操作已完成
                    completed = true;
                    // 如果需要自动释放且操作句柄有效，则释放句柄
                    if (autoReleaseWhenCanceled && handle.IsValid())
                    {
                        Addressables.Release(handle);
                    }
                    // 设置任务状态为已取消
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                // 如果存在进度报告对象且操作句柄有效，则报告进度
                if (progress != null && handle.IsValid())
                {
                    progress.Report(handle.GetDownloadStatus().Percent);
                }

                return true;
            }

            // 尝试将实例返回到任务池
            bool TryReturn()
            {
                // 移除任务跟踪
                TaskTracker.RemoveTracking(this);
                // 重置核心状态
                core.Reset();
                // 重置操作句柄
                handle = default;
                // 重置进度报告对象
                progress = default;
                // 重置取消令牌
                cancellationToken = default;
                // 释放取消令牌的注册信息
                cancellationTokenRegistration.Dispose();
                // 尝试将实例推送到任务池
                return pool.TryPush(this);
            }
        }

#endregion

#region AsyncOperationHandle_T

        // 为 AsyncOperationHandle<T> 类型扩展 GetAwaiter 方法，用于获取 UniTask<T> 的等待器
        public static UniTask<T>.Awaiter GetAwaiter<T>(this AsyncOperationHandle<T> handle)
        {
            // 调用 ToUniTask 方法并获取其等待器
            return ToUniTask(handle).GetAwaiter();
        }

        // 为 AsyncOperationHandle<T> 类型扩展 WithCancellation 方法，用于支持取消操作
        public static UniTask<T> WithCancellation<T>(this AsyncOperationHandle<T> handle, CancellationToken cancellationToken, bool cancelImmediately = false, bool autoReleaseWhenCanceled = false)
        {
            // 调用 ToUniTask 方法并传入取消令牌等参数
            return ToUniTask(handle, cancellationToken: cancellationToken, cancelImmediately: cancelImmediately, autoReleaseWhenCanceled: autoReleaseWhenCanceled);
        }

        // 为 AsyncOperationHandle<T> 类型扩展 ToUniTask 方法，将其转换为 UniTask<T>
        public static UniTask<T> ToUniTask<T>(this AsyncOperationHandle<T> handle, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false, bool autoReleaseWhenCanceled = false)
        {
            // 如果取消令牌已请求取消，则返回一个已取消的 UniTask<T>
            if (cancellationToken.IsCancellationRequested) return UniTask.FromCanceled<T>(cancellationToken);

            // 如果操作句柄无效，则抛出异常
            if (!handle.IsValid())
            {
                throw new Exception("Attempting to use an invalid operation handle");
            }

            // 如果操作已完成
            if (handle.IsDone)
            {
                // 如果操作状态为失败，则返回一个包含异常的 UniTask<T>
                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    return UniTask.FromException<T>(handle.OperationException);
                }
                // 操作成功完成，返回一个包含结果的 UniTask<T>
                return UniTask.FromResult(handle.Result);
            }

            // 创建一个新的 UniTask<T> 并返回
            return new UniTask<T>(AsyncOperationHandleConfiguredSource<T>.Create(handle, timing, progress, cancellationToken, cancelImmediately, autoReleaseWhenCanceled, out var token), token);
        }

        // 定义密封类 AsyncOperationHandleConfiguredSource<T>，实现 IUniTaskSource<T>、IPlayerLoopItem 和 ITaskPoolNode 接口
        sealed class AsyncOperationHandleConfiguredSource<T> : IUniTaskSource<T>, IPlayerLoopItem, ITaskPoolNode<AsyncOperationHandleConfiguredSource<T>>
        {
            // 定义静态任务池
            static TaskPool<AsyncOperationHandleConfiguredSource<T>> pool;
            // 指向下一个节点
            AsyncOperationHandleConfiguredSource<T> nextNode;
            // 获取下一个节点的引用
            public ref AsyncOperationHandleConfiguredSource<T> NextNode => ref nextNode;

            // 静态构造函数，注册任务池大小获取器
            static AsyncOperationHandleConfiguredSource()
            {
                TaskPool.RegisterSizeGetter(typeof(AsyncOperationHandleConfiguredSource<T>), () => pool.Size);
            }

            // 存储操作完成时的回调
            readonly Action<AsyncOperationHandle<T>> completedCallback;
            // 存储异步操作句柄
            AsyncOperationHandle<T> handle;
            // 存储取消令牌
            CancellationToken cancellationToken;
            // 存储取消令牌的注册信息
            CancellationTokenRegistration cancellationTokenRegistration;
            // 存储进度报告对象
            IProgress<float> progress;
            // 标识取消时是否自动释放句柄
            bool autoReleaseWhenCanceled;
            // 标识是否立即取消
            bool cancelImmediately;
            // 标识操作是否已完成
            bool completed;

            // 存储 UniTask 完成源核心
            UniTaskCompletionSourceCore<T> core;

            // 构造函数，初始化完成回调
            AsyncOperationHandleConfiguredSource()
            {
                completedCallback = HandleCompleted;
            }

            // 静态方法，创建 AsyncOperationHandleConfiguredSource<T> 实例
            public static IUniTaskSource<T> Create(AsyncOperationHandle<T> handle, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, bool autoReleaseWhenCanceled, out short token)
            {
                // 如果取消令牌已请求取消，则创建一个已取消的 UniTask 完成源
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource<T>.CreateFromCanceled(cancellationToken, out token);
                }

                // 尝试从任务池获取实例，如果失败则创建一个新实例
                if (!pool.TryPop(out var result))
                {
                    result = new AsyncOperationHandleConfiguredSource<T>();
                }

                // 初始化实例属性
                result.handle = handle;
                result.cancellationToken = cancellationToken;
                result.completed = false;
                result.progress = progress;
                result.autoReleaseWhenCanceled = autoReleaseWhenCanceled;
                result.cancelImmediately = cancelImmediately;
                
                // 如果需要立即取消且取消令牌可以被取消，则注册取消回调
                if (cancelImmediately && cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var promise = (AsyncOperationHandleConfiguredSource<T>)state;
                        if (promise.autoReleaseWhenCanceled && promise.handle.IsValid())
                        {
                            Addressables.Release(promise.handle);
                        }
                        promise.core.TrySetCanceled(promise.cancellationToken);
                    }, result);
                }

                // 跟踪活动任务
                TaskTracker.TrackActiveTask(result, 3);

                // 将实例添加到玩家循环中
                PlayerLoopHelper.AddAction(timing, result);

                // 为操作完成事件添加回调
                handle.Completed += result.completedCallback;

                // 获取版本令牌
                token = result.core.Version;
                return result;
            }

            // 处理操作完成事件
            void HandleCompleted(AsyncOperationHandle<T> argHandle)
            {
                // 如果操作句柄有效，则移除完成回调
                if (handle.IsValid())
                {
                    handle.Completed -= completedCallback;
                }

                // 如果操作已完成，则直接返回
                if (completed)
                {
                    return;
                }
                // 标记操作已完成
                completed = true;
                // 如果取消令牌已请求取消
                if (cancellationToken.IsCancellationRequested)
                {
                    // 如果需要自动释放且操作句柄有效，则释放句柄
                    if (autoReleaseWhenCanceled && handle.IsValid())
                    {
                        Addressables.Release(handle);
                    }
                    // 设置任务状态为已取消
                    core.TrySetCanceled(cancellationToken);
                }
                // 如果操作状态为失败
                else if (argHandle.Status == AsyncOperationStatus.Failed)
                {
                    // 设置任务状态为异常
                    core.TrySetException(argHandle.OperationException);
                }
                else
                {
                    // 设置任务状态为成功并返回结果
                    core.TrySetResult(argHandle.Result);
                }
            }

            // 获取任务结果
            public T GetResult(short token)
            {
                try
                {
                    // 从核心获取结果
                    return core.GetResult(token);
                }
                finally
                {
                    // 如果不是立即取消且取消令牌已请求取消的情况
                    if (!(cancelImmediately && cancellationToken.IsCancellationRequested))
                    {
                        // 尝试将实例返回到任务池
                        TryReturn();
                    }
                    else
                    {
                        // 移除任务跟踪
                        TaskTracker.RemoveTracking(this);
                    }
                }
            }

            // 实现 IUniTaskSource 接口的 GetResult 方法
            void IUniTaskSource.GetResult(short token)
            {
                // 调用自身的 GetResult 方法
                GetResult(token);
            }

            // 获取任务状态
            public UniTaskStatus GetStatus(short token)
            {
                // 从核心获取任务状态
                return core.GetStatus(token);
            }

            // 不安全地获取任务状态
            public UniTaskStatus UnsafeGetStatus()
            {
                // 从核心不安全地获取任务状态
                return core.UnsafeGetStatus();
            }

            // 注册延续操作
            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                // 调用核心的 OnCompleted 方法
                core.OnCompleted(continuation, state, token);
            }

            // 执行玩家循环的下一步操作
            public bool MoveNext()
            {
                // 如果操作已完成，则返回 false
                if (completed)
                {
                    return false;
                }

                // 如果取消令牌已请求取消
                if (cancellationToken.IsCancellationRequested)
                {
                    // 标记操作已完成
                    completed = true;
                    // 如果需要自动释放且操作句柄有效，则释放句柄
                    if (autoReleaseWhenCanceled && handle.IsValid())
                    {
                        Addressables.Release(handle);
                    }
                    // 设置任务状态为已取消
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                // 如果存在进度报告对象且操作句柄有效，则报告进度
                if (progress != null && handle.IsValid())
                {
                    progress.Report(handle.GetDownloadStatus().Percent);
                }

                return true;
            }

            // 尝试将实例返回到任务池
            bool TryReturn()
            {
                // 移除任务跟踪
                TaskTracker.RemoveTracking(this);
                // 重置核心状态
                core.Reset();
                // 重置操作句柄
                handle = default;
                // 重置进度报告对象
                progress = default;
                // 重置取消令牌
                cancellationToken = default;
                // 释放取消令牌的注册信息
                cancellationTokenRegistration.Dispose();
                // 尝试将实例推送到任务池
                return pool.TryPush(this);
            }
        }

#endregion
    }
}

