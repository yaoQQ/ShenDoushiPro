using UnityEngine;
using UnityEditor;

public class testTemp 
{

    //IEnumerator checkUpdate()
    //{
    //    //开始连接服务器
    //    isDownAddressable = true;
    //    Debug.Log(" isDownAddressable = true;");
    //    AsyncOperationHandle<List<string>> checkHandle = Addressables.CheckForCatalogUpdates(false);
    //    LoadingBarController.SetLoadContent("开始连接服务器 获取资源文件");
    //    yield return checkHandle;
    //    if (checkHandle.Status == AsyncOperationStatus.Succeeded)
    //    {
    //        List<string> cataLogs = checkHandle.Result;
    //        if (cataLogs != null && cataLogs.Count > 0)
    //        {
    //            for (int i = 0; i < cataLogs.Count; i++)
    //            {
    //                Debug.LogFormat("checkUpdate() cataLogs[{0}]= " + cataLogs[i], i);
    //            }
    //            StartCoroutine(startUpdate(cataLogs));
    //        }
    //        else
    //        {
    //            LoadingBarController.SetLoadContent("不需要更新");
    //            Debug.Log("不需要更新 cataLogs ,初始加载UI依赖");
    //            StartPreloadPrefabe(UIBaseKey);
    //        }
    //    }

    //}
    //IEnumerator startUpdate(List<string> catalogs)
    //{
    //    //.这个时候可以看到服务器的输出,访问了这两个文件,现在客户端本地的catalog文件已经到最新了
    //    var updateHandle = Addressables.UpdateCatalogs(catalogs, true);

    //    yield return updateHandle;
    //    Debug.Log("download catalogs 更新本地的catalogs");
    //    //获取可更新资源key
    //    List<object> keys = new List<object>();
    //    foreach (var item in updateHandle.Result)
    //    {
    //        foreach (var key in item.Keys)
    //        {
    //            Debug.Log("key1111:" + key);
    //            keys.Add(key);
    //        }
    //    }
    //    //Debug.Log("download bundle start");
    //    var sizeHandle = Addressables.GetDownloadSizeAsync(keys);
    //    yield return sizeHandle;
    //    long totalDownloadSize = sizeHandle.Result;
    //    Debug.Log("下载大小：" + totalDownloadSize);
    //}
}