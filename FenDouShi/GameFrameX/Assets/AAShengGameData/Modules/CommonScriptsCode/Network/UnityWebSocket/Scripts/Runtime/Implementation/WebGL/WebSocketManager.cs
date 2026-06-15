#if !UNITY_EDITOR && UNITY_WEBGL
// 引入 System 命名空间，提供常用的类型和基础功能
using System;
// 引入 System.Collections.Generic 命名空间，提供泛型集合类
using System.Collections.Generic;
// 引入 System.Runtime.InteropServices 命名空间，提供与非托管代码交互的功能
using System.Runtime.InteropServices;
// 引入 AOT 命名空间，提供 Ahead-Of-Time 编译相关的功能
using AOT;

namespace UnityWebSocket
{
    /// <summary>
    /// 提供静态访问方法以与 JSLIB WebSocket 进行交互的类
    /// </summary>
    internal static class WebSocketManager
    {
        // WebSocket 实例的映射表
        private static Dictionary<int, WebSocket> sockets = new Dictionary<int, WebSocket>();

        // 定义委托
        // 连接打开时的回调委托
        public delegate void OnOpenCallback(int instanceId);
        // 接收到消息（字节数组形式）时的回调委托
        public delegate void OnMessageCallback(int instanceId, IntPtr msgPtr, int msgSize);
        // 接收到消息（字符串形式）时的回调委托
        public delegate void OnMessageStrCallback(int instanceId, IntPtr msgStrPtr);
        // 发生错误时的回调委托
        public delegate void OnErrorCallback(int instanceId, IntPtr errorPtr);
        // 连接关闭时的回调委托
        public delegate void OnCloseCallback(int instanceId, int closeCode, IntPtr reasonPtr);

        // WebSocket JSLIB 函数
        // 连接到指定实例 ID 的 WebSocket
        [DllImport("__Internal")]
        public static extern int WebSocketConnect(int instanceId);

        // 关闭指定实例 ID 的 WebSocket 连接
        [DllImport("__Internal")]
        public static extern int WebSocketClose(int instanceId, int code, string reason);

        // 向指定实例 ID 的 WebSocket 发送字节数组数据
        [DllImport("__Internal")]
        public static extern int WebSocketSend(int instanceId, byte[] dataPtr, int dataLength);

        // 向指定实例 ID 的 WebSocket 发送字符串数据
        [DllImport("__Internal")]
        public static extern int WebSocketSendStr(int instanceId, string data);

        // 获取指定实例 ID 的 WebSocket 连接状态
        [DllImport("__Internal")]
        public static extern int WebSocketGetState(int instanceId);

        // WebSocket JSLIB 回调设置器和其他函数
        // 分配一个新的 WebSocket 实例
        [DllImport("__Internal")]
        public static extern int WebSocketAllocate(string url);

        // 为指定实例 ID 的 WebSocket 添加子协议
        [DllImport("__Internal")]
        public static extern int WebSocketAddSubProtocol(int instanceId, string protocol);

        // 释放指定实例 ID 的 WebSocket 资源
        [DllImport("__Internal")]
        public static extern void WebSocketFree(int instanceId);

        // 设置 WebSocket 连接打开时的回调函数
        [DllImport("__Internal")]
        public static extern void WebSocketSetOnOpen(OnOpenCallback callback);

        // 设置 WebSocket 接收到字节数组消息时的回调函数
        [DllImport("__Internal")]
        public static extern void WebSocketSetOnMessage(OnMessageCallback callback);

        // 设置 WebSocket 接收到字符串消息时的回调函数
        [DllImport("__Internal")]
        public static extern void WebSocketSetOnMessageStr(OnMessageStrCallback callback);

        // 设置 WebSocket 发生错误时的回调函数
        [DllImport("__Internal")]
        public static extern void WebSocketSetOnError(OnErrorCallback callback);

        // 设置 WebSocket 连接关闭时的回调函数
        [DllImport("__Internal")]
        public static extern void WebSocketSetOnClose(OnCloseCallback callback);
        
        // 设置对 6000 版本的支持
        [DllImport("__Internal")]
        public static extern void WebSocketSetSupport6000();

        // 标记回调函数是否已初始化并设置
        private static bool isInitialized = false;

        // 初始化 WebSocket 到 JSLIB 的回调函数
        private static void Initialize()
        {
            // 设置连接打开时的回调函数
            WebSocketSetOnOpen(DelegateOnOpenEvent);
            // 设置接收到字节数组消息时的回调函数
            WebSocketSetOnMessage(DelegateOnMessageEvent);
            // 设置接收到字符串消息时的回调函数
            WebSocketSetOnMessageStr(DelegateOnMessageStrEvent);
            // 设置发生错误时的回调函数
            WebSocketSetOnError(DelegateOnErrorEvent);
            // 设置连接关闭时的回调函数
            WebSocketSetOnClose(DelegateOnCloseEvent);
#if UNITY_6000_0_OR_NEWER
            // 设置对 6000 版本的支持
            WebSocketSetSupport6000();
#endif

            // 标记回调函数已初始化
            isInitialized = true;
        }

        // 连接打开事件的代理方法
        [MonoPInvokeCallback(typeof(OnOpenCallback))]
        public static void DelegateOnOpenEvent(int instanceId)
        {
            // 尝试从映射表中获取指定实例 ID 的 WebSocket 实例
            if (sockets.TryGetValue(instanceId, out var socket))
            {
                // 调用实例的连接打开处理方法
                socket.HandleOnOpen();
            }
        }

        // 接收到字节数组消息事件的代理方法
        [MonoPInvokeCallback(typeof(OnMessageCallback))]
        public static void DelegateOnMessageEvent(int instanceId, IntPtr msgPtr, int msgSize)
        {
            // 尝试从映射表中获取指定实例 ID 的 WebSocket 实例
            if (sockets.TryGetValue(instanceId, out var socket))
            {
                // 创建指定大小的字节数组
                var bytes = new byte[msgSize];
                // 将非托管内存中的数据复制到字节数组中
                Marshal.Copy(msgPtr, bytes, 0, msgSize);
                // 调用实例的消息处理方法
                socket.HandleOnMessage(bytes);
            }
        }

        // 接收到字符串消息事件的代理方法
        [MonoPInvokeCallback(typeof(OnMessageStrCallback))]
        public static void DelegateOnMessageStrEvent(int instanceId, IntPtr msgStrPtr)
        {
            // 尝试从映射表中获取指定实例 ID 的 WebSocket 实例
            if (sockets.TryGetValue(instanceId, out var socket))
            {
                // 将非托管内存中的字符串转换为托管字符串
                string msgStr = Marshal.PtrToStringAuto(msgStrPtr);
                // 调用实例的字符串消息处理方法
                socket.HandleOnMessageStr(msgStr);
            }
        }

        // 发生错误事件的代理方法
        [MonoPInvokeCallback(typeof(OnErrorCallback))]
        public static void DelegateOnErrorEvent(int instanceId, IntPtr errorPtr)
        {
            // 尝试从映射表中获取指定实例 ID 的 WebSocket 实例
            if (sockets.TryGetValue(instanceId, out var socket))
            {
                // 将非托管内存中的错误信息转换为托管字符串
                string errorMsg = Marshal.PtrToStringAuto(errorPtr);
                // 调用实例的错误处理方法
                socket.HandleOnError(errorMsg);
            }
        }

        // 连接关闭事件的代理方法
        [MonoPInvokeCallback(typeof(OnCloseCallback))]
        public static void DelegateOnCloseEvent(int instanceId, int closeCode, IntPtr reasonPtr)
        {
            // 尝试从映射表中获取指定实例 ID 的 WebSocket 实例
            if (sockets.TryGetValue(instanceId, out var socket))
            {
                // 将非托管内存中的关闭原因转换为托管字符串
                string reason = Marshal.PtrToStringAuto(reasonPtr);
                // 调用实例的连接关闭处理方法
                socket.HandleOnClose((ushort)closeCode, reason);
            }
        }

        // 分配一个新的 WebSocket 实例
        internal static int AllocateInstance(string address)
        {
            // 如果回调函数未初始化，则进行初始化
            if (!isInitialized) Initialize();
            // 调用 JSLIB 函数分配新的 WebSocket 实例
            return WebSocketAllocate(address);
        }

        // 向映射表中添加 WebSocket 实例
        internal static void Add(WebSocket socket)
        {
            // 如果映射表中不包含该实例 ID，则添加该实例
            if (!sockets.ContainsKey(socket.instanceId))
            {
                sockets.Add(socket.instanceId, socket);
            }
        }

        // 从映射表中移除指定实例 ID 的 WebSocket 实例
        internal static void Remove(int instanceId)
        {
            // 如果映射表中包含该实例 ID，则移除该实例
            if (sockets.ContainsKey(instanceId))
            {
                sockets.Remove(instanceId);
            }
        }
    }
}
#endif
