using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
/// <summary>
/// 使fairyGUI的UIPnael支持Addressables的异步加载的管理类
/// UIPanel默认只支持本地加载
/// </summary>
public class FairyUIPanelLoadManager
{
    private static FairyUIPanelLoadManager _instance;
    public static FairyUIPanelLoadManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FairyUIPanelLoadManager();
            }
            return _instance;
        }
    }
    //保存UI的Package的Addressable的AsyncOperationHandle(资源包对象持有)
    private Dictionary<string, AsyncOperationHandle<TextAsset>> packageHandleDic = new Dictionary<string, AsyncOperationHandle<TextAsset>>();
    /// <summary>
    /// 默认的 [ FairyGUI ] 资源根目录
    /// </summary>
    public static string DEFAULT_PACKAGE_ROOT = "Assets/AssetsPackage/FairyGUI"; // TODO 保持与fgui资源的根地址一致 //
    /// <summary>
    /// 加载fgui的描述文件资源包bytes(package+"_fui")
    /// </summary>
    /// <param name="package">[ FairyGUI ] 资源包名 对应Addressables的Label</param>
    //利用协程加载fairyGUI资源  MonoBehaviour支持
    public static IEnumerator LoadFairyGuiPackage(string packageName, Action<UIPackage> callback)
    {
        string fguiBytePath = packageName + "_fui";
        // 使用Unity的Debug实现并添加红色
        Debug.Log($"<color=red>AddAddressablePackage() 加载package={packageName} fguiBytePath={fguiBytePath}</color>");
        UIPackage pkg = UIPackage.GetByName(packageName);
        if (pkg != null)
        {
            if (callback != null)
            {
                callback(pkg);
            }
            yield return pkg;
        }
        if (Instance.packageHandleDic == null)
        {
            Instance.packageHandleDic = new Dictionary<string, AsyncOperationHandle<TextAsset>>();
        }
        AsyncOperationHandle<TextAsset> descHandle;
        if (Instance.packageHandleDic.ContainsKey(packageName))
        {
            descHandle = Instance.packageHandleDic[packageName];
            if (descHandle.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError($"[ FairyGUI ] 加载资源包失败 {packageName} {fguiBytePath} 重新加载");
                RemoveFairyPackage(packageName);
            }
        }
        descHandle = Addressables.LoadAssetAsync<TextAsset>(fguiBytePath);
        yield return descHandle;
        if (descHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Instance.packageHandleDic[packageName] = descHandle;
            pkg= AddPackage(descHandle.Result, packageName);
            if (callback != null)
            {
                callback(pkg);
            }
        }
        
    }

    //利用异步async加载fairyGUI资源 
    public static async Task<UIPackage> LoadFairyGuiPackage(string package)
    {
        string fguiBytePath = package + "_fui";
        // 使用Unity的Debug实现并添加红色
        Debug.Log($"<color=red>AddAddressablePackage() 加载package={package} fguiBytePath={fguiBytePath}</color>");
        UIPackage pkg = UIPackage.GetByName(package);
        if (pkg != null)
        {
            return pkg;
        }

        if (Instance.packageHandleDic == null)
        {
            Instance.packageHandleDic = new Dictionary<string, AsyncOperationHandle<TextAsset>>();
        }
        AsyncOperationHandle<TextAsset> descHandle;
        if (Instance.packageHandleDic.ContainsKey(package))
        {
            descHandle = Instance.packageHandleDic[package];
            if (descHandle.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError($"[ FairyGUI ] 加载资源包失败 {package} {fguiBytePath} 重新加载");
                RemoveFairyPackage(package);
            }
        }
        descHandle = Addressables.LoadAssetAsync<TextAsset>(fguiBytePath);
        Instance.packageHandleDic[package] = descHandle;

        TextAsset fguiByte = await descHandle.Task;

        //addressables 加载fairyGUI资源包
        pkg = AddPackage(fguiByte, package);

        return pkg;
    }
    private static UIPackage AddPackage(TextAsset fguiByte, string package)
    {
        UIPackage pkg = UIPackage.AddPackage(fguiByte.bytes, package, async (assetKey, extension, type, packageItem) => {
            // 使用Unity的Debug实现并添加红色
            Debug.Log($"<color=red>异步加载package={package}.bytes成功后 加载其他依赖资源回调 assetKey={assetKey}, extension={extension}, type={type.ToString()}, PackageItem={packageItem.ToString()}</color>");
            if (type == typeof(Texture))
            {
                // 使用Unity的Debug实现并添加红色
                Debug.Log($"<color=red>开始加载{package}的Texture依赖 assetKey={assetKey}{extension}</color>");
                var handle = Addressables.LoadAssetAsync<Texture>(assetKey);
                var texture = await handle.Task;
                // 使用Unity的Debug实现并添加绿色
                Debug.Log($"<color=green>加载{package}纹理address={assetKey}成功t={texture}</color>");
                // 使用Unity的Debug实现并添加白色
                Debug.Log($"<color=white>LoadAllAssets() @packageItem.id={packageItem.id} @packageItem.name={packageItem.name}  @packageItem.file={packageItem.file}  @items.type={packageItem.type} @items.objectType={packageItem.objectType} @items.texture={packageItem.texture}</color>");
                // 谷大让加上这句(
                packageItem.owner.SetItemAsset(packageItem, texture, DestroyMethod.Custom);
            }
            else if (type == typeof(AudioClip))
            {
                // 使用Unity的Debug实现并添加红色
                Debug.Log($"<color=red>开始加载{package}声音依赖 {assetKey}</color>");
                var handle = Addressables.LoadAssetAsync<AudioClip>(assetKey);
                var clip = await handle.Task;
                // 使用Unity的Debug实现并添加绿色
                Debug.Log($"<color=green>加载{package}address={assetKey}声音成功t={clip}</color>");
                // 谷大让加上这句(
                packageItem.owner.SetItemAsset(packageItem, clip, DestroyMethod.Custom);
            }
        });
        return pkg;
    }
    /// <summary>
    /// 加载资源包的组件对象
    /// </summary>
    /// <param name="package">[ FairyGUI ] 资源包名 对应fairyGUI项目对应模块包名</param>
    /// <param name="assetPath">assetPath  addressables的key即加载对象</param>
    /// <param name="isFullPath">包名参数是否为完整的 [ Addressables ] 路径</param>
    public static async Task<GComponent> CreateFairyGuiComponent(string packageName, string componentName)
    {
        if (string.IsNullOrEmpty(packageName) || string.IsNullOrEmpty(componentName))
            return null;
        // 使用Unity的Debug实现并添加白色
        Debug.Log($"<color=white>LoadFairyGuiComponent() 加载fairy组件 package={packageName} fguiBytePath={packageName}</color>");
        UIPackage pkg = UIPackage.GetByName(packageName);
        if (pkg == null)
        {
            pkg = await LoadFairyGuiPackage(packageName);
        }

        GComponent ui = (GComponent)UIPackage.CreateObject(packageName, componentName);
        return ui;
    }
    /// <summary>
    /// 在FGUI和Addressable中移除指定的包
    /// </summary>
    /// <param name="packageOrID">包名或者包ID</param>
    public static void RemoveFairyPackage(string packageOrID)
    {
        UIPackage.RemovePackage(packageOrID);
        Addressables.Release(Instance.packageHandleDic[packageOrID]);
        Instance.packageHandleDic.Remove(packageOrID);

    }
    public static void RemoveAllFairyPackage()
    {
        UIPackage.RemoveAllPackages();
        foreach (var pir in Instance.packageHandleDic)
        {
            Addressables.Release(Instance.packageHandleDic[pir.Key]);
        }
        Instance.packageHandleDic.Clear();


    }

}