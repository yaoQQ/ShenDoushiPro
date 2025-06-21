using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class UILoadControl : Singleton<UILoadControl>
{
    // private UIresProxy resProxy = new UIresProxy();

    #region fairygui
    public static async Task LoadFairyGUIPackage(string address, string packageName)
    {
        Logger.PrintDebug($"LoadFairyGUIPackage() {address} {packageName}");
        var fuiAsset = await Addressables.LoadAssetAsync<TextAsset>(address).Task;
        Logger.PrintDebug($"LoadFairyGUIPackage() 加载完成 {address} {packageName} pkgAsset.dataSize={fuiAsset.dataSize}");
        UIPackage.AddPackage(
            fuiAsset.bytes,
            packageName,
            async (string name, string extension, Type type, PackageItem ite) => {
                Logger.PrintDebug($"{name}, {extension}, {type.ToString()}, {ite.ToString()}");

                if (type == typeof(Texture))
                {
                    Texture t = await Addressables.LoadAssetAsync<Texture>(name + extension).Task;
                    ite.owner.SetItemAsset(ite, t, DestroyMethod.Custom);

                }
            });
        Addressables.Release(fuiAsset);

    }

    public static async Task LoadFairyGUIPackageByLabel(string address, string labelName)
    {
        Logger.PrintDebug($"LoadFairyGUIPackage() {address} {labelName}");
        var fuiAsset = await Addressables.LoadAssetAsync<TextAsset>(address).Task;
        Logger.PrintDebug($"LoadFairyGUIPackage() 加载完成 {address} {labelName} pkgAsset.dataSize={fuiAsset.dataSize}");
        UIPackage.AddPackage(
            fuiAsset.bytes,
            labelName,
            async (string name, string extension, Type type, PackageItem ite) => {
                Logger.PrintDebug($"{name}, {extension}, {type.ToString()}, {ite.ToString()}");

                if (type == typeof(Texture))
                {
                    Texture t = await Addressables.LoadAssetAsync<Texture>(name + extension).Task;
                    ite.owner.SetItemAsset(ite, t, DestroyMethod.Custom);

                }
            });
        Addressables.Release(fuiAsset);


        //Addressables.LoadAssetsAsync<GameObject>(new List<string> { addressStr, lable }, null, Addressables.MergeMode.Intersection).Completed += (res) =>
        //{
        //    if (res.Result.Count <= 0)
        //    {
        //        return;
        //    }
        //    GameObject go = GameObject.Instantiate(res.Result[0]);
        //    Debug.Log("<color='blue'>$$$$$$$$$$$$$$$go=" + go + " onLoadUIEnd=" + onLoadUIEnd + "</color>");
        //    if (onLoadUIEnd != null)
        //        onLoadUIEnd(UtilMethod.ConnectStrs(packageName, ":", name), go);
        //};

    }
    #endregion
    #region old UGUI
    public void CreateUI(string packName, string name, Action<string, GameObject> onLoadUIEnd, bool isInstantiation = true)
    {
        //   MainThread.Instance.StartCoroutine(AsyncCreateUI(packName,name, onLoadUIEnd, isInstantiation));
      
        addressableAsyncCreateUI(packName, name, onLoadUIEnd, isInstantiation);
    }

    public void AsyncCreateUI(string packageName, string name, BaseView uiView, bool isInstantiation, PreloadOrder order = null)
    {
        // string abRelativePath = UtilMethod.ConnectStrs("ui/", packageName, "/prefab/", name, ".unity3d");//老版本
        string abRelativePath = name;
        Logger.PrintColor("blue", "abRelativePath=" + abRelativePath);
        ResLoadManager.LoadAsync(AssetType.UI, packageName, abRelativePath, (relativePath, res) =>
        {
            GameObject go = GameObject.Instantiate(res as GameObject);
            
            if (uiView != null)
                uiView.executeLoadUIEnd(UtilMethod.ConnectStrs(packageName, ":", abRelativePath), go);
            if (order != null)
                order.onPreloadStepEnd(abRelativePath);
        });
    }

    public void AsyncCreateUI(string packageName, string name, IBaseView uiView, bool isInstantiation, PreloadOrder order = null)
    {
        // string abRelativePath = UtilMethod.ConnectStrs("ui/", packageName, "/prefab/", name, ".unity3d");//老版本
        string abRelativePath = name;
        Logger.PrintColor("blue", "abRelativePath=" + abRelativePath);
        ResLoadManager.LoadAsync(AssetType.UI, packageName, abRelativePath, (relativePath, res) =>
        {
            GameObject go = GameObject.Instantiate(res as GameObject);

            if (uiView != null)
                uiView.executeLoadUIEnd(UtilMethod.ConnectStrs(packageName, ":", abRelativePath), go);
            if (order != null)
                order.onPreloadStepEnd(abRelativePath);
        });
    }
    public void CreateUI(string packName, string name, BaseView uiView, bool isInstantiation = true, PreloadOrder order = null)
    {
        //MainThread.Instance.StartCoroutine(AsyncCreateUI(packName,name, uiView, isInstantiation, order));

       // addressableAsyncCreateUI(packName, name, uiView, isInstantiation, order);
    }

  
    public void addressableAsyncCreateUI(string packageName, string name, Action<string, GameObject> onLoadUIEnd, bool isInstantiation = true)
    {
        string address = name;
        string[] oriStr = name.Split('/');
        if (oriStr.Length > 1)
        {
            address = oriStr.GetValue(oriStr.Length - 1) as string;
        }
        string lable = packageName;
        string addressStr = address;
        Debug.Log("<color='blue'>!@!@!@!@!@!@!@!22222@addressableAsyncCreateUI lable=" + lable + " addressStr=" + addressStr + "</color>");

       Addressables.LoadAssetsAsync<GameObject>(new List<string> { addressStr, lable }, null, Addressables.MergeMode.Intersection).Completed += (res) =>
        {
            if (res.Result.Count <= 0)
            {
                return;
            }
            GameObject go = GameObject.Instantiate(res.Result[0]);
            Debug.Log("<color='blue'>$$$$$$$$$$$$$$$go=" + go + " onLoadUIEnd=" + onLoadUIEnd + "</color>");
            if (onLoadUIEnd != null)
                onLoadUIEnd(UtilMethod.ConnectStrs(packageName, ":", name), go);
        };
    }
    #endregion
}