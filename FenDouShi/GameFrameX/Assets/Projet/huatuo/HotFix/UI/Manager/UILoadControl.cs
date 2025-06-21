using UnityEngine;
using System;
using System.Collections;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
public class UILoadControl : Singleton<UILoadControl>
{
   // private UIresProxy resProxy = new UIresProxy();

   // [BlackList]
    public void CreateUI(string packName, string name, Action<string, GameObject> onLoadUIEnd, bool isInstantiation = true)
    {
        //   MainThread.Instance.StartCoroutine(AsyncCreateUI(packName,name, onLoadUIEnd, isInstantiation));
      
        addressableAsyncCreateUI(packName, name, onLoadUIEnd, isInstantiation);
    }

    public void AsyncCreateUI(string packageName, string name, BaseView uiView, bool isInstantiation, LuaPreloadOrder order = null)
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
    public void CreateUI(string packName, string name, BaseView uiView, bool isInstantiation = true, LuaPreloadOrder2 order = null)
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
}