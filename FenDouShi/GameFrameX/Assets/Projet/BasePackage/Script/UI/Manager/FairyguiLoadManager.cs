using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
public class FairyguiLoadManager : Singleton<FairyguiLoadManager>
{
    private readonly Dictionary<string, AsyncOperationHandle< UnityEngine.Object>> AssetsList = new Dictionary<string, AsyncOperationHandle<UnityEngine.Object>>();
    // //KeyList:[common]={UI/common_fui.byte,UI/common_atlas0.png}
    private readonly Dictionary<string,List<string>> PackageKeyAndAddressesValueList = new Dictionary<string, List<string>>();

    private IEnumerator Preload()
    {
        AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync("UI");
        yield return handle;
        IList<IResourceLocation> locations = handle.Result;

        //获得所有Label为UI的资源的地址
        foreach (IResourceLocation location in locations)
        {
            //addressable的key地址格式为UI_资源名(addressKey)_资源类型
            //举例:Addressables的common资源包，包含UI/png和common_fui.bytes和bytes和common_atlas0.png
            // UI/common_fui.bytes-->common_fui.bytes
            string packageName = location.PrimaryKey.Substring(3);
            //common_fui--->common addressKey=common
            packageName = packageName.Substring(0, packageName.IndexOf('_'));
            //包含对应包的全部资源(png和common_fui.bytes和common_atlas0.png)
            List<string> addresses;
            if (!PackageKeyAndAddressesValueList.ContainsKey(packageName))
            {
                addresses = new List<string>();
                PackageKeyAndAddressesValueList.Add(packageName, addresses);
            }
            else
            {
                addresses = PackageKeyAndAddressesValueList[packageName];
            }

            //将资源地址添加到包名对应的地址列表中,
            //addresses={UI/common_fui.byte,UI/common_atlas0.png}
            //KeyList:[common]={UI/common_fui.byte,UI/common_atlas0.png}
            addresses.Add(location.PrimaryKey);//保存UI/launch_fui.bytes
        }

        Addressables.Release(handle);
    }

    public IEnumerator DoAddPackages()
    {
        if (PackageKeyAndAddressesValueList.Count <= 0)
        {
            //预载入并生成包名对应的所有资源地址的列表
            yield return Preload();
        }

        //加载所有包
        foreach (var item in PackageKeyAndAddressesValueList)
        {
            Logger.PrintDebug($"包名:{item.Key} {item.Key}包的所有资源{item.Value}");
            //报名对应的资源列表，进行遍历载入
            List<string> addresses = item.Value;
            string PackageName = item.Key;
            foreach (string address in addresses)
            {
                if (AssetsList.ContainsKey(address))
                {
                    //目标资源已经缓存则不需要再次载入
                    Logger.PrintWarning($"{address}已经缓存则不需要再次载入");
                    continue;
                }

                AsyncOperationHandle<UnityEngine.Object> handle = Addressables.LoadAssetAsync<UnityEngine.Object>(address);
                yield return handle;
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Logger.PrintColor("green",$"加载资源成功:{address}");
                    //载入后缓存
                    AssetsList.Add(address, handle);
                }
            }

            Logger.PrintColor("blue", $"FairyGUI AddPackage:{item.Key} start");
            //执行FairyGUI的添加包函数
           
            UIPackage.AddPackage(PackageName, LoadFunc);
        }

    }
    // 给FairyGUI的工具函数，用于提供实际的资源给FairyGUI系统
    private object LoadFunc(string name, string extension, System.Type type, out DestroyMethod method)
    {
        Logger.PrintColor("blue", $"FairyGUI AddPackage  LoadFunc() name={name} extension={extension} type={type}");
        method = DestroyMethod.None;
        string key = $"UI/{name}{extension}";
        Logger.PrintColor("blue", $"FairyGUI AddPackage  LoadFunc() key={key}");
        //从已载入并缓存的资源列表中查询并返回资源
        if (!AssetsList.ContainsKey(key))
        {
            Logger.PrintError( $"FairyGUI AddPackage  LoadFunc()  没有加载到资源 adress={key}");
            return null;
        }
        return AssetsList[key].Result;
    }
   

    #region fairygui
  
    public static IEnumerator LoadFairyGUIPackage(string packageName,string address)
    {
        Logger.PrintDebug($"LoadFairyGUIPackage() {packageName} {address} ");
        var handle = Addressables.LoadAssetAsync<TextAsset>(address);
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var fuiAsset = handle.Result;
            Logger.PrintDebug($"LoadFairyGUIPackage() 加载完成 {address} {packageName} pkgAsset.dataSize={fuiAsset.dataSize}");
            UIPackage.AddPackage(
                fuiAsset.bytes,
                packageName,
                 (string name, string extension, System.Type type, PackageItem ite) =>
                {
                    Logger.PrintColor("blue",$"{name}, {extension}, {type.ToString()}, {ite.ToString()}");

                    if (type == typeof(Texture))
                    {
                        Logger.PrintColor("blue", $"type==Texture {name}, {extension} 准备加载！");
                        var t = Addressables.LoadAssetAsync<Texture>(name + extension);
                        t.Completed += (AsyncOperationHandle<Texture> tHandle) =>
                        {
                            if (tHandle.Status == AsyncOperationStatus.Succeeded)
                            {
                                ite.owner.SetItemAsset(ite, t, DestroyMethod.Custom);
                            }
                            else
                            {
                                Logger.PrintError($"Failed to load asset: {name}.");
                            }
                        };
                       

                    }
                }
                
                );
            Addressables.Release(fuiAsset);
        }
        else
        {

            Logger.PrintError($"LoadFairyGUIPackage() 加载失败 {address} {packageName} ");
        }
    }
    // 协程方法，用于加载FairyGUI包
    IEnumerator LoadFairyGUIPackage2(string packageName, string address)
    {
        // 打印调试信息，显示当前要加载的包名和地址
        Logger.PrintDebug($"LoadFairyGUIPackage() {packageName} {address} ");
        // 开始异步加载指定地址的文本资源
        var handle = Addressables.LoadAssetAsync<TextAsset>(address);
        // 等待异步加载完成
        yield return handle;
        // 检查异步加载是否成功
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            // 获取加载成功的资源
            var fuiAsset = handle.Result;
            // 打印调试信息，显示加载完成的包名、地址和资源大小
            Logger.PrintDebug($"LoadFairyGUIPackage() 加载完成 {address} {packageName} pkgAsset.dataSize={fuiAsset.dataSize}");
            // 将加载的资源添加到FairyGUI包中
            UIPackage uipackage = UIPackage.AddPackage(
                fuiAsset.bytes,
                packageName,
                 (string name, string extension, System.Type type, PackageItem ite) =>
                 {
                     // 打印蓝色调试信息，显示资源的相关信息
                     Logger.PrintColor("blue", $"{name}, {extension}, {type.ToString()}, {ite.ToString()}");

                     // 检查资源类型是否为纹理
                     if (type == typeof(Texture))
                     {
                         // 打印蓝色调试信息，表示准备加载纹理资源
                         Logger.PrintColor("blue", $"type==Texture {name}, {extension} 准备加载！");
                         // 开始异步加载纹理资源
                         var t = Addressables.LoadAssetAsync<Texture>(name + extension);
                         // 为异步加载操作添加完成事件处理程序
                         t.Completed += (AsyncOperationHandle<Texture> tHandle) =>
                         {
                             // 检查纹理资源是否加载成功
                             if (tHandle.Status == AsyncOperationStatus.Succeeded)
                             {
                                 // 将加载的纹理资源设置给FairyGUI包项
                                 ite.owner.SetItemAsset(ite, t, DestroyMethod.Custom);
                             }
                             else
                             {
                                 // 若加载失败，打印错误信息
                                 Logger.PrintError($"Failed to load asset: {name}.");
                             }
                         };
                     }
                 }
                 );
            // 注释掉的代码，用于释放加载的资源
            //Addressables.Release(fuiAsset);
        }
        else
        {
            // 若加载失败，打印错误信息
            Logger.PrintError($"LoadFairyGUIPackage() 加载失败 {address} {packageName} ");
        }
    }
    public delegate void LoadResourceAsync(string name, string extension, System.Type type, PackageItem item);
    private object LoadFunc2(string name, string extension, System.Type type, out DestroyMethod method)
    {
        Logger.PrintColor("blue", $"FairyGUI AddPackage  LoadFunc() name={name} extension={extension} type={type}");
        method = DestroyMethod.None;
        string key = $"UI/{name}{extension}";
        Logger.PrintColor("blue", $"FairyGUI AddPackage  LoadFunc() key={key}");
        //从已载入并缓存的资源列表中查询并返回资源
        if (!AssetsList.ContainsKey(key))
        {
            Logger.PrintError($"FairyGUI AddPackage  LoadFunc()  没有加载到资源 adress={key}");
            return null;
        }
        return AssetsList[key].Result;
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
        //if (packageName == "base" || packageName == "mahjonghul" || packageName == "marbles")
        //{
        //    while (resProxy.GetManifest(packageName) == null)
        //    {
        //        yield return 0;
        //    }
        //}

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