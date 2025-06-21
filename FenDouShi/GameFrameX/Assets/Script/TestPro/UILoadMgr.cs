// 引入迭代器相关命名空间，用于处理可迭代对象
using System.Collections;
// 引入泛型集合命名空间，用于使用泛型集合类
using System.Collections.Generic;
// 引入Unity引擎核心命名空间，提供Unity相关功能
using UnityEngine;
// 引入FairyGUI库的命名空间，用于创建和管理UI界面
using FairyGUI;
// 引入Unity资源管理异步操作命名空间，用于处理资源的异步加载操作
using UnityEngine.ResourceManagement.AsyncOperations;
// 引入基础系统命名空间，提供常用的类型和功能
using System;
using UnityEngine.AddressableAssets;

// UI加载管理类，负责UI资源的加载、卸载和对象池管理等操作
// UI加载管理类
public class UILoadMgr
{
    // 单例模式的私有静态实例，用于确保该类只有一个实例
    // 单例模式
    private static UILoadMgr instance = null;
    // 单例模式的公共静态属性，用于获取该类的唯一实例
    public static UILoadMgr Instance
    {
        get
        {
            // 如果实例为空，则创建一个新的实例并返回
            if (instance == null)
                return new UILoadMgr();
            // 否则直接返回已有的实例
            return instance;
        }
    }

    // UI资源的地址模板，使用格式化字符串指定资源路径
    private const string address = "Assets/ZBuild/Build/UI/{0}/{1}";

    // 私有构造函数，确保该类不能在外部被实例化，符合单例模式的要求
    private UILoadMgr()
    {
        // 此处注释掉的代码可能是用于设置UI默认字体，暂时未使用
        //UIConfig.defaultFont=""
    }

    // 自定义结构体，用于存储回调函数和是否为主题公园相关的标识
    // 结构体，用于存储回调函数和是否与主题公园相关的标识
    private struct HandleStruct
    {
        // 回调函数，用于在特定操作完成后执行相应逻辑
        public Action callback;
        // 标识是否与主题公园相关
        public bool isThemePark;
    }


    // 字典，用于存储异步操作句柄的队列，键为模型名称
    // 存储异步操作句柄队列的字典
    private Dictionary<string, Queue<AsyncOperationHandle>> dicHandle = new Dictionary<string, Queue<AsyncOperationHandle>>();
    // 字典，用于存储包加载回调函数的队列，键为模型名称
    // 存储包加载回调函数队列的字典
    private Dictionary<string, Queue<HandleStruct>> dicPkgCallback = new Dictionary<string, Queue<HandleStruct>>();
    // 字典，用于存储模型视图加载回调函数的队列，键为模型名称
    // 存储模型视图加载回调函数队列的字典
    private Dictionary<string, Queue<ModelViewStruct>> dicCallback = new Dictionary<string, Queue<ModelViewStruct>>();
    // 静态列表，存储需要预加载的包名列表
    // 需要预加载的包名列表
    private static List<string> comPkgList = new List<string> { "Icon" };
    // 静态字典，用于存储包的引用计数，键为包的ID，值为引用计数
    // 存储包引用计数的字典
    private static Dictionary<string, int> pkgRefCountDic = new Dictionary<string, int>();

    // 静态字典，用于存储GObject对象池，键为资源URL，值为对象池实例
    // 存储GObject对象池的字典
    private static Dictionary<string, GObjectPool> dicGObjectPool = new Dictionary<string, GObjectPool>();

    // 加载包的公共方法，根据模型名称加载对应的UI包，并可传入回调函数
    // 加载包
    public void LoadPackage(string modelName, Action callback = null)
    {
        // 用于存储从字典中获取的异步操作句柄队列
        Queue<AsyncOperationHandle> handles;
        // 尝试从字典中获取指定模型名称的异步操作句柄队列
        if (dicHandle.TryGetValue(modelName, out handles))
        {
            // 如果存在，则调用回调函数并返回
            callback?.Invoke();
            return;
        }
        // 用于存储从字典中获取的包加载回调函数队列
        Queue<HandleStruct> actions;
        // 尝试从字典中获取指定模型名称的包加载回调函数队列
        if (dicPkgCallback.TryGetValue(modelName, out actions))
        {
            // 如果存在，则将新的回调函数和标识加入队列并返回
            actions.Enqueue(new HandleStruct() { callback = callback, isThemePark = false });
            return;
        }
        else
        {
            // 如果不存在，则创建一个新的队列，并将新的回调函数和标识加入队列
            dicPkgCallback[modelName] = new Queue<HandleStruct>();
            dicPkgCallback[modelName].Enqueue(new HandleStruct() { callback = callback, isThemePark = false });
        }


        // 异步加载二进制文件，使用AddressableTools加载指定路径的字节文件
        // 使用Addressables进行异步加载二进制文件

        AsyncOperationHandle handleBin = Addressables.LoadAssetAsync<TextAsset>(string.Format(address, modelName, modelName) + "_fui.bytes");
        handleBin.Completed += (handle) =>
        {
            // 调用AddHandle方法，将异步操作句柄添加到字典中
            AddHandle(modelName, handleBin);
            // 将加载的字节数据添加到FairyGUI的包管理中
            //UIPackage.AddPackage(bytes, modelName, (string name, string extension, System.Type type, PackageItem item) => {
            //    // 异步加载包中的资源
            //    AddressableTools.Load(string.Format(address, modelName, name + extension), data => {
            //        // 设置包项的资源，并指定销毁方法为卸载
            //        item.owner.SetItemAsset(item, data, DestroyMethod.Unload);
            //    });
            //});
            // 依次执行存储在队列中的回调函数
            while (dicPkgCallback[modelName].Count > 0)
            {
                // 从队列中取出一个回调函数和标识
                var v = dicPkgCallback[modelName].Dequeue();
                // 调用回调函数
                v.callback?.Invoke();
            }
            // 清空队列
            dicPkgCallback[modelName].Clear();
            // 从字典中移除该模型名称对应的队列
            dicPkgCallback.Remove(modelName);
        };

    }

    // 预加载包的公共方法，调用LoadPackages方法预加载comPkgList中的包，并可传入回调函数
    // 预加载包
    public void PreLoadPackages(Action callback = null)
    {
        // 调用LoadPackages方法预加载指定列表中的包
        LoadPackages(comPkgList, callback);
    }

    // 加载包列表的公共方法，根据传入的包名列表加载对应的UI包，并可传入回调函数
    // 加载包列表
    public void LoadPackages(List<string> list, Action callback = null)
    {
        // 如果列表为空或不存在，则直接调用回调函数并返回
        if (list == null || list.Count == 0)
        {
            callback?.Invoke();
            return;
        }
        // 用于记录已加载完成的包的数量
        int count = 0;
        // 遍历包名列表，依次调用Load方法加载每个包
        for (int i = 0; i < list.Count; i++)
        {
            Load(list[i], null, com => {
                // 每加载完成一个包，计数器加1
                count++;
                // 如果所有包都加载完成，则调用回调函数
                if (count >= list.Count)
                    callback?.Invoke();
            }, false);
        }
    }

    // UI对象池的根Transform，用于管理对象池中的UI对象
    // UI对象池的根节点
    private Transform uiPoolTransformRoot;

    // 从对象池中获取GObject对象的公共方法，根据模型名称和视图名称获取对应的对象
    // 从对象池中获取对象
    public GObject GetPoolObject(string modelName, string viewName)
    {
        // 用于存储获取到的GObject对象
        GObject obj;
        // 用于存储获取到的GObject对象池
        GObjectPool pool;
        // 标准化资源URL，根据模型名称和视图名称生成资源URL
        string resourceURL = UIPackage.NormalizeURL(UIPackage.URL_PREFIX + modelName + "/" + viewName);
        // 如果资源URL为空，则输出错误日志并返回null
        if (resourceURL == null)
        {
            Debug.LogError(modelName + "/" + viewName + "没有有效的资源路径");
            return null;
        }
        // 尝试从字典中获取指定资源URL的对象池
        if (!dicGObjectPool.TryGetValue(resourceURL, out pool))
        {
            // 如果不存在，则创建一个新的对象池，并将其添加到字典中
            pool = new GObjectPool(uiPoolTransformRoot);
            dicGObjectPool[resourceURL] = pool;
        }
        // 判断是否需要创建新的对象
        bool isCreate = pool.count == 0;
        // 从对象池中获取对象
        obj = pool.GetObject(resourceURL);
        // 如果是新创建的对象，则更新包的引用计数
        if (isCreate)
        {
            // 获取对象所属包的ID
            string pkgId = obj.packageItem.owner.id;
            // 用于存储包的引用计数
            int count;
            // 尝试从字典中获取该包的引用计数
            if (!pkgRefCountDic.TryGetValue(pkgId, out count))
            {
                // 如果不存在，则将引用计数初始化为1
                pkgRefCountDic[pkgId] = 1;
            }
            else
            {
                // 如果存在，则将引用计数加1
                pkgRefCountDic[pkgId] = ++count;
            }
        }
        // 返回获取到的GObject对象
        return obj;
    }

    // 将GObject对象回收到对象池的公共方法
    // 回收对象
    public void RecyclePoolObject(GObject obj)
    {
        // 如果对象为空，则直接返回
        if (obj == null) return;
        // 获取对象的资源URL
        string resouceURL = obj.resourceURL;
        // 用于存储获取到的GObject对象池
        GObjectPool pool;
        // 尝试从字典中获取指定资源URL的对象池
        if (dicGObjectPool.TryGetValue(resouceURL, out pool))
        {
            // 如果存在，则将对象回收到对象池中
            pool.ReturnObject(obj);
        }
        else
        {
            // 如果不存在，则输出错误日志
            Debug.LogError(string.Format("回收对象{0},没有找到对象池", obj.name));
        }

    }

    // 清空指定资源URL的对象池的公共方法
    // 清空对象池中的对象
    public void ClearPoolObject(string resourceURL)
    {
        // 用于存储获取到的GObject对象池
        GObjectPool pool;
        // 尝试从字典中获取指定资源URL的对象池
        if (dicGObjectPool.TryGetValue(resourceURL, out pool))
        {
            // 如果存在，则清空对象池
            pool.Clear();
        }
    }

    // 自定义结构体，用于存储模型名称、视图名称、是否创建对象和回调函数
    // 结构体，用于存储模型名称、视图名称、是否创建对象和回调函数
    private struct ModelViewStruct
    {
        // 模型名称
        public string modelName;
        // 视图名称
        public string viewName;
        // 标识是否创建新的对象
        public bool isCreate;
        // 回调函数，用于在对象加载完成后执行相应逻辑
        public Action<GObject> callback;
    }

    // 获取未加载的依赖包列表的私有方法，根据传入的依赖包数组返回未加载的包名列表
    // 获取未加载的依赖包列表
    private List<string> GetNoLoadedDependenciesPackages(string[] dependenciesPackages)
    {
        // 用于存储未加载的依赖包名列表
        List<string> list = new List<string>();
        // 遍历依赖包数组，检查每个包是否已加载
        Array.ForEach<string>(dependenciesPackages, v => {
            // 如果该包不在预加载列表中，不在未加载列表中，且未被FairyGUI加载，则将其添加到未加载列表中
            if (!comPkgList.Contains(v) && !list.Contains(v) && UIPackage.GetByName(v) == null)
            {
                list.Add(v);
            }
        });
        // 返回未加载的依赖包名列表
        return list;
    }

    // 加载UI的公共方法，可指定模型名称、视图名称、回调函数、是否创建对象和依赖包数组
    // 加载UI
    public void Load(string modelName, string viewName = null, Action<GObject> callback = null, bool isCreate = false, string[] dependenciesPackages = null)
    {
        // 用于存储未加载的依赖包名列表
        List<string> pkgNames;
        // 如果存在依赖包数组，且有未加载的依赖包，则先加载这些依赖包
        if (dependenciesPackages != null && dependenciesPackages.Length > 0 && (pkgNames = GetNoLoadedDependenciesPackages(dependenciesPackages)).Count > 0)
        {
            LoadPackages(pkgNames, () => {
                // 依赖包加载完成后，调用LoadMain方法加载主UI
                LoadMain(modelName, viewName, callback, isCreate);
            });
        }
        else
        {
            // 如果没有依赖包需要加载，则直接调用LoadMain方法加载主UI
            LoadMain(modelName, viewName, callback, isCreate);
        }
    }

    // 加载主UI的私有方法，处理模型视图加载的主要逻辑
    // 加载主UI
    private void LoadMain(string modelName, string viewName = null, Action<GObject> callback = null, bool isCreate = false)
    {
        // 用于存储从字典中获取的模型视图加载回调函数队列
        Queue<ModelViewStruct> actions;
        // 尝试从字典中获取指定模型名称的模型视图加载回调函数队列
        if (dicCallback.TryGetValue(modelName, out actions) && actions != null)
        {
            // 如果存在，则将新的模型视图信息和回调函数加入队列并返回
            actions.Enqueue(new ModelViewStruct() { modelName = modelName, viewName = viewName, callback = callback, isCreate = isCreate });
            return;
        }
        else
        {
            // 如果不存在，则创建一个新的队列，并将新的模型视图信息和回调函数加入队列
            dicCallback[modelName] = new Queue<ModelViewStruct>();
            dicCallback[modelName].Enqueue(new ModelViewStruct() { modelName = modelName, viewName = viewName, callback = callback, isCreate = isCreate });
        }
        // 调用LoadPackage方法加载指定模型的包，并在加载完成后执行相应逻辑
        LoadPackage(modelName, () => {
            // 依次处理存储在队列中的模型视图信息和回调函数
            while (dicCallback[modelName].Count > 0)
            {
                // 从队列中取出一个模型视图信息和回调函数
                var v = dicCallback[modelName].Dequeue();
                // 如果不需要创建对象，则直接调用回调函数并传入null
                if (!v.isCreate)
                {
                    v.callback?.Invoke(null);
                    continue;
                }
                // 如果回调函数不为空，则从对象池中获取对象并调用回调函数
                if (v.callback != null)
                {
                    var com = GetPoolObject(v.modelName, v.viewName).asCom;
                    v.callback(com);
                }
            }
            // 从字典中移除该模型名称对应的队列
            dicCallback.Remove(modelName);
        });
    }

    // 加载纹理的公共方法，根据传入的地址异步加载纹理，并可传入回调函数
    // 加载纹理
    public void LoadTexture(int address, Action<Texture> callback = null)
    {
        // 异步加载纹理，使用AddressableTools加载指定地址的纹理
        AsyncOperationHandle handle = Addressables.LoadAssetAsync<Texture>(address.ToString());
        handle.Completed += (op) =>
        {
            // 调用回调函数并将加载的纹理作为参数传入
            callback(op.Result as Texture);
        };
    }

    // 释放纹理资源的公共方法，根据传入的地址释放对应的纹理资源
    // 释放纹理
    public void DisposeTexture(int address)
    {
        // 调用AddressableTools的Release方法释放指定地址的资源
        Addressables.Release(address.ToString());
    }

    // 添加异步操作句柄的私有方法，将指定模型名称的异步操作句柄添加到字典中
    // 添加句柄
    private void AddHandle(string modelName, AsyncOperationHandle handleBin)
    {
        // 用于存储从字典中获取的异步操作句柄队列
        Queue<AsyncOperationHandle> handles;
        // 尝试从字典中获取指定模型名称的异步操作句柄队列
        if (!dicHandle.TryGetValue(modelName, out handles))
        {
            // 如果不存在，则创建一个新的队列，并将异步操作句柄加入队列
            dicHandle[modelName] = new Queue<AsyncOperationHandle>();
            dicHandle[modelName].Enqueue(handleBin);
        }
    }

    // 移除包的私有方法，根据传入的模型名称移除对应的UI包并释放相关资源
    // 移除包
    private void RemovePackage(string modelName)
    {
        // 用于存储从字典中获取的异步操作句柄队列
        Queue<AsyncOperationHandle> handles;
        // 尝试从字典中获取指定模型名称的异步操作句柄队列
        if (dicHandle.TryGetValue(modelName, out handles))
        {
            // 依次释放队列中的异步操作句柄
            while (handles.Count > 0)
            {
                var v = handles.Dequeue();
                Addressables.Release(v);
            }
            // 清空队列
            handles.Clear();
            // 从字典中移除该模型名称对应的队列
            dicHandle.Remove(modelName);
            // 从FairyGUI中移除指定名称的包
            UIPackage.RemovePackage(modelName);
        }
    }

    // 释放GObject对象的公共方法，释放对象并更新包的引用计数，必要时移除包
    // 释放对象
    public void DisposeObject(GObject obj)
    {
        // 如果对象为空，则直接返回
        if (obj == null) return;
        // 用于存储包的引用计数
        int count;
        // 获取对象所属包的ID
        string pkgId = obj.packageItem.owner.id;
        // 尝试从字典中获取该包的引用计数
        if (pkgRefCountDic.TryGetValue(pkgId, out count))
        {
            // 如果存在，则将引用计数减1
            pkgRefCountDic[pkgId] = --count;
            // 如果引用计数小于等于0，则移除该包并从字典中移除该包的引用计数
            if (count <= 0)
            {
                var pkg = UIPackage.GetById(pkgId);
                RemovePackage(pkg.name);
                pkgRefCountDic.Remove(pkgId);
            }
        }
        // 释放GObject对象
        obj.Dispose();
    }

}