using FairyGUI;
using FairyGUI.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using static FairyGUI.UIPackage;
public class TestAddressableLoad2 : MonoBehaviour
{
    private readonly Dictionary<string, AsyncOperationHandle<Object>> AssetsList=new Dictionary<string, AsyncOperationHandle<Object>>();
    private readonly Dictionary<string, List<string>> KeyList = new Dictionary<string, List<string>>();
    void Start()
    {

    }
    /// <summary>
    /// 预载入并生成包名对应的所有资源地址的列表
    ///通过Addressables.LoadResourceLocationsAsync("UI")获取到Addressables资源内该标签的所有资源
    /// </summary>
    /// <returns></returns>
    private IEnumerator Preload()
    {
        AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync("UI");
        yield return handle;
        IList<IResourceLocation> locations = handle.Result;

        //获得所有Label为UI的资源的地址
        foreach (IResourceLocation location in locations)
        {
            string key = location.PrimaryKey.Substring(3);
            key = key.Substring(0, key.IndexOf('_'));
            //key为FairyGUI的包名
            List<string> addresses;
            if (!KeyList.ContainsKey(key))
            {
                addresses = new List<string>();
                KeyList.Add(key, addresses);
            }
            else
            {
                addresses = KeyList[key];
            }

            //将资源地址添加到包名对应的地址列表中
            addresses.Add(location.PrimaryKey);
        }

        Addressables.Release(handle);
    }
    /// <summary>
    /// 加载所有包
    /// </summary>
    /// <returns></returns>
    private IEnumerator DoAddPackages()
    {
        if (KeyList.Count <= 0)
        {
            //预载入并生成包名对应的所有资源地址的列表
            yield return Preload();
        }

        //加载所有包
        foreach (var item in KeyList)
        {
            //报名对应的资源列表，进行遍历载入
            List<string> addresses = item.Value;
            foreach (string address in addresses)
            {
                if (AssetsList.ContainsKey(address))
                {
                    //目标资源已经缓存则不需要再次载入
                    continue;
                }

                AsyncOperationHandle<Object> handle = Addressables.LoadAssetAsync<UnityEngine.Object>(address);
                yield return handle;
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    //载入后缓存
                    AssetsList.Add(address, handle);
                }
            }

            UnityEngine.Debug.Log("key:" + item.Key);

            //执行FairyGUI的添加包函数
            UIPackage.AddPackage(item.Key, LoadFunc);
        }

    }
    /// <summary>
    /// 执行FairyGUI的添加包函数
    ///LoadFunc方法可以将资源中的.bytesUI源文件，加入到UIPackage中，当执行它完毕后，从Addressables中的UI资源就算加载成功
    /// </summary>
    /// <param name="name"></param>
    /// <param name="extension"></param>
    /// <param name="type"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    // 给FairyGUI的工具函数，用于提供实际的资源给FairyGUI系统
    private object LoadFunc(string name, string extension, System.Type type, out DestroyMethod method)
    {
        method = DestroyMethod.None;
        string key = $"UI/{name}{extension}";

        //从已载入并缓存的资源列表中查询并返回资源
        return AssetsList.ContainsKey(key) ? AssetsList[key].Result : null;
    }

    //UIPackage.Addpackage中的方法
    //public static UIPackage AddPackage(string assetPath, LoadResource loadFunc)
    //{
    //    if (_packageInstById.ContainsKey(assetPath))
    //        return _packageInstById[assetPath];

    //    DestroyMethod dm;
    //    TextAsset asset = (TextAsset)loadFunc(assetPath + "_fui", ".bytes", typeof(TextAsset), out dm);
    //    if (asset == null)
    //    {
    //        if (Application.isPlaying)
    //            throw new Exception("FairyGUI: Cannot load ui package in '" + assetPath + "'");
    //        else
    //            Debug.LogWarning("FairyGUI: Cannot load ui package in '" + assetPath + "'");
    //    }

    //    ByteBuffer buffer = new ByteBuffer(asset.bytes);

    //    UIPackage pkg = new UIPackage();
    //    pkg._loadFunc = loadFunc;
    //    pkg._assetPath = assetPath;
    //    if (!pkg.LoadPackage(buffer, assetPath))
    //        return null;

    //    _packageInstById[pkg.id] = pkg;
    //    _packageInstByName[pkg.name] = pkg;
    //    _packageInstById[assetPath] = pkg;
    //    _packageList.Add(pkg);
    //    return pkg;
    //}
}