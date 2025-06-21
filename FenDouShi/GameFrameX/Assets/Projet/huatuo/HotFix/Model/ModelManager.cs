using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ModelManager : Singleton<ModelManager>
{
    //private ModelResProxy resProxy = new ModelResProxy();

    private Dictionary<string, List<string>> m_dict = new Dictionary<string, List<string>>();

    public void DestroyModel(GameObject model)
    {
        if (model != null)
            GameObject.Destroy(model);
        
    }

    public void CreateModel(string packName, string modelName, Action<GameObject> onLoadend, Action<GameObject> callBackFun=null)
    {
        //packName = packName.ToLower();
        //modelName = modelName.ToLower();
        MainThread.Instance.StartCoroutine(AsyncCreateModel(packName, modelName, onLoadend));
    }
    public void CreateModel(string packName, string modelName, Action<GameObject> callBack, Action<GameObject, Action<GameObject>> onLoadend) {
        //packName = packName.ToLower();
        //modelName = modelName.ToLower();
        MainThread.Instance.StartCoroutine(AsyncCreateModelCallBack(packName, modelName, callBack, onLoadend));
    }

    //private IEnumerator AsyncLoadPrefab(string packageName, string relativePath, Action<GameObject> finishCallback) {
    //    while (resProxy.GetPackManifest(packageName) == null) {
    //        yield return 0;
    //    }

    //    string abRelativePath = CommonUtils.ConnectStrs("Prefab/", packageName, relativePath.ToLower(), ".unity3d");
    //    ResLoadManager.LoadAsync(AssetType.Model, packageName, abRelativePath, (path, res) => {
    //        GameObject go = res as GameObject;
    //        if (finishCallback != null)
    //            finishCallback(go);
    //    });
    //}

    private IEnumerator AsyncCreateModel(string packName, string modelName, Action<GameObject> onLoadend)
    {
        //旧版本
        //while (resProxy.GetPackManifest(packName) == null)
        //{
        //    yield return 0;
        //}

        //string abRelativePath = UtilMethod.ConnectStrs("model/prefab/", packName, "/", modelName, ".unity3d");//老版本
        //string abRelativePath = UtilMethod.ConnectStrs("model/", packName, "/prefab/", modelName);
        //string address = modelName;
        //string[] oriStr = address.Split('/');
        //if (oriStr.Length > 1)
        //{
        //    address = oriStr.GetValue(oriStr.Length - 1) as string;
        //}
        //Logger.PrintColor("red", "AsyncCreateModel packName=" + packName + " address=" + address);
        
        ResLoadManager.LoadAsync(AssetType.Model, packName, modelName, (relativePath, res) =>
        {
            if (!m_dict.ContainsKey(packName))
            {
                m_dict.Add(packName, new List<string>());
                m_dict[packName].Add(modelName);
            }
            else
            {
                if (!m_dict[packName].Contains(modelName))
                    m_dict[packName].Add(modelName);
            }
            if (onLoadend != null)
            {
                onLoadend.Invoke(res as GameObject);
            }
        });
        yield return null;
    }
    private IEnumerator AsyncCreateModelCallBack(string packName, string modelName, Action<GameObject> callBack, Action<GameObject, Action<GameObject>> onLoadend) {
        // string abRelativePath2 = UtilMethod.ConnectStrs("model/prefab/", packName, "/", modelName, ".unity3d");//老版本
        string abRelativePath2 = modelName;
        Debug.Log("@@@@@@@ ModelManager  abRelativePath=" + abRelativePath2);

        string loadRootDir = CommonPathUtils.getLoadRootDir(packName, abRelativePath2);

        string fullPath = CommonUtils.ConnectStrs(loadRootDir, CommonPathUtils.PathWithENcrypt(abRelativePath2));
        Debug.Log("111@@@@@@@ ModelManager  fullPath=" + fullPath);

        string getLoadRootDirStr = CommonPathUtils.getLoadRootDir(packName, abRelativePath2);
        string PathWithENcryptStr = CommonPathUtils.PathWithENcrypt(abRelativePath2);
        string fullPath2 = CommonUtils.ConnectStrs(getLoadRootDirStr, PathWithENcryptStr);
        Debug.Log("<color=blue> getLoadRootDirStr=" + getLoadRootDirStr + "</color>");
        Debug.Log("<color=blue> PathWithENcryptStr=" + PathWithENcryptStr + "</color>");
        Debug.Log("<color=blue> fullPath2=" + fullPath2 + "</color>");
        //while (resProxy.GetPackManifest(packName) == null) {
        //    yield return 0;
        //}

        // string abRelativePath = UtilMethod.ConnectStrs("model/", packName, "/prefab/", modelName, ".unity3d");//老版本
        string abRelativePath = modelName;
        ResLoadManager.LoadAsync(AssetType.Model, packName, abRelativePath, (relativePath, res) => {
            if (!m_dict.ContainsKey(packName)) {
                m_dict.Add(packName, new List<string>());
                m_dict[packName].Add(abRelativePath);
            }
            else {
                if (!m_dict[packName].Contains(abRelativePath))
                    m_dict[packName].Add(abRelativePath);
            }
            if (onLoadend != null) {
                onLoadend.Invoke(res as GameObject, callBack);
            }
            if (callBack != null) {
                callBack(res as GameObject);
            }
        
        });
        yield return null;
    }

    //[BlackList]
    /// <summary>
    /// 销毁一个包的所有模型
    /// </summary>
    /// <param name="packageName"></param>
    public void DestroyPackageModel(string packageName)
    {
        if (!m_dict.ContainsKey(packageName))
            return;
        for (int i = 0, count = m_dict[packageName].Count; i < count; ++i)
        {
            //Logger.PrintLog(CommonUtils.ConnectStrs("卸载模型：", m_dict[packageName][i]));
            AssetNodeManager.ReleaseNode(AssetType.Model, packageName, m_dict[packageName][i]);
        }
    }
}