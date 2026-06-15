/*
using FairyGUI;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class UILoadControl : Singleton<UILoadControl>
{
    #region FairyGUI 加载相关
    //保存UI的Package的Addressable的AsyncOperationHandle(资源包对象持有)
    private Dictionary<string, AsyncOperationHandle<TextAsset>> packageHandleDic = new Dictionary<string, AsyncOperationHandle<TextAsset>>();
    /// <summary>
    /// 默认的 [ FairyGUI ] 资源根目录
    /// </summary>
    public static string DEFAULT_PACKAGE_ROOT = "Assets/AssetsPackage/FairyGUI"; // TODO 保持与fgui资源的根地址一致 //
    /// <summary>
    /// 加载资源包
    /// </summary>
    /// <param name="package">[ FairyGUI ] 资源包名</param>
    /// <param name="assetPath">assetPath of the resource file name. The file name would be in format of 'assetNamePrefix_resFileName'. It can be empty.</param>
    /// <param name="isFullPath">包名参数是否为完整的 [ Addressables ] 路径</param>
    public static async Task<UIPackage> AddAddressablePackage(string package, string assetPath = "", bool isFullPath = false) {
        UIPackage pkg = UIPackage.GetByName(package);
        if (pkg != null) {
            return pkg;
        }

        if (Instance.packageHandleDic == null) {
            Instance.packageHandleDic = new Dictionary<string, AsyncOperationHandle<TextAsset>>();
        }
        AsyncOperationHandle<TextAsset> descHandle;
        if (Instance.packageHandleDic.ContainsKey(package)) {
            descHandle = Instance.packageHandleDic[package];
            if (descHandle.Status == AsyncOperationStatus.Failed) {
                Logger.PrintError($"[ FairyGUI ] 加载资源包失败 {package} {assetPath} {isFullPath} 重新加载");
                Addressables.Release(Instance.packageHandleDic[package]);
                Instance.packageHandleDic.Remove(package);
            }
        }
        descHandle = Addressables.LoadAssetAsync<TextAsset>(isFullPath
                ? $"{DEFAULT_PACKAGE_ROOT}/{package}_fui.bytes"
                : package);
        Instance.packageHandleDic[package] = descHandle;

        TextAsset fguiByte = await descHandle.Task;
        //addressables 加载fairyGUI资源包
        pkg = AddPackage(fguiByte, package, assetPath, isFullPath);

        return pkg;
    }
    private static UIPackage AddPackage(TextAsset desc, string package, string assetPath = "", bool isFullPath = false) {
        UIPackage pkg = UIPackage.AddPackage(desc.bytes, assetPath, async (name, extension, type, packageItem) => {
            Logger.PrintColor("blue", $"异步加载package={package} {assetPath}.bytes成功后 加载其他依赖资源回调 name={name}, extension={extension}, type={type.ToString()}, PackageItem={packageItem.ToString()}");
            if (type == typeof(Texture)) {
                string path = isFullPath ? $"{DEFAULT_PACKAGE_ROOT}/{name}{extension}" : name + extension;
                Logger.PrintColor("blue", $"开始加载{package}的Texture依赖 {name}{extension}");
                var handle = Addressables.LoadAssetAsync<Texture>(path);
                var texture = await handle.Task;
                Logger.PrintColor("yellow", $"加载{package}纹理address={path}成功t={texture}");
                // 谷大让加上这句(
                packageItem.owner.SetItemAsset(packageItem, texture, DestroyMethod.Custom);
            }
            else if (type == typeof(AudioClip)) {
                string path = isFullPath ? $"{DEFAULT_PACKAGE_ROOT}/{name}{extension}" : name + extension;
                Logger.PrintColor("blue", $"开始加载{package}声音依赖 {name}{extension}");
                var handle = Addressables.LoadAssetAsync<AudioClip>(path);
                var clip = await handle.Task;
                Logger.PrintColor("yellow", $"加载{package}address={path}声音成功t={clip}");
                // 谷大让加上这句(
                packageItem.owner.SetItemAsset(packageItem, clip, DestroyMethod.Custom);
            }
        });
        return pkg;
    }

    /// <summary>
    /// 在FGUI和Addressable中移除指定的包
    /// </summary>
    /// <param name="packageOrID">包名或者包ID</param>
    public static void RemoveAddressablePackage(string packageOrID) {
        if (Instance.packageHandleDic.ContainsKey(packageOrID)) {
            UIPackage.RemovePackage(packageOrID);
            Addressables.Release(Instance.packageHandleDic[packageOrID]);
            Instance.packageHandleDic.Remove(packageOrID);
        }
    }

    #endregion

    public void AsyncAddressablesCreateUI(string packageName, string name, IBaseView uiView, bool isInstantiation, PreloadOrder order = null) {

        // string abRelativePath = Utility.Text.ConnectStrs("ui/", packageName, "/prefab/", name, ".unity3d");//老版本
        string abRelativePath = name;
        Logger.PrintColor("blue", "abRelativePath=" + abRelativePath);
        ResLoadManager.LoadAsync(AssetType.FairyUI, packageName, abRelativePath, (relativePath, res) => {
            // GameObject go = GameObject.Instantiate(res as GameObject);
            AsyncOperationHandle handle = (AsyncOperationHandle)res;

            if (uiView != null)
                uiView.executeLoadUIEnd(Utility.Text.ConnectStrs(packageName, ":", abRelativePath), res);
            if (order != null)
                order.onPreloadStepEnd(abRelativePath);
        });
    }

    public void AsyncCreateUI(string packageName, string name, BaseView uiView, bool isInstantiation, PreloadOrder order = null) {
        // string abRelativePath = Utility.Text.ConnectStrs("ui/", packageName, "/prefab/", name, ".unity3d");//老版本
        string abRelativePath = name;
        Logger.PrintColor("blue", "abRelativePath=" + abRelativePath);
        ResLoadManager.LoadAsync(AssetType.UI, packageName, abRelativePath, (relativePath, res) => {
            GameObject go = GameObject.Instantiate(res as GameObject);

            if (uiView != null)
                uiView.executeLoadUIEnd(Utility.Text.ConnectStrs(packageName, ":", abRelativePath), go);
            if (order != null)
                order.onPreloadStepEnd(abRelativePath);
        });
    }
    public void AsyncCreateUI(string packageName, string name, IBaseView uiView, bool isInstantiation, PreloadOrder order = null) {
        // string abRelativePath = Utility.Text.ConnectStrs("ui/", packageName, "/prefab/", name, ".unity3d");//老版本
        string abRelativePath = name;
        Logger.PrintColor("blue", "abRelativePath=" + abRelativePath);
        ResLoadManager.LoadAsync(AssetType.UI, packageName, abRelativePath, (relativePath, res) => {
            GameObject go = GameObject.Instantiate(res as GameObject);

            if (uiView != null)
                uiView.executeLoadUIEnd(Utility.Text.ConnectStrs(packageName, ":", abRelativePath), go);
            if (order != null)
                order.onPreloadStepEnd(abRelativePath);
        });
    }
}
*/