using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableLoadManager : SingleMonobehaviour<AddressableLoadManager>
{
    private bool isDownAddressable = false;
    AsyncOperationHandle downloadDependenciesOp = new AsyncOperationHandle();
    private float downLoadTotalSze = 0;
    private string _key;
    /// <summary>结束回调</summary>
    private Action m_finishCallback = null;
    private string UIBaseKey = "BasePackage";//基础包名
    private string ShengDouShiPackage = "ShengDouShiPackage";//游戏包
    private AsyncOperationHandle<GameObject> loadHandle = new AsyncOperationHandle<GameObject>();

    private Transform _loginCanvas;

    public IEnumerator StartLoadCataLogsPreload(string key)
    {
        downloadDependenciesOp = Addressables.InitializeAsync();
        isDownAddressable = true;
      
        LoadingBarController.SetLoadContent("初始化Addressables");
        Logger.PrintColor("blue", "初始化Addressables");
        yield return downloadDependenciesOp;

        LoadingBarController.SetLoadContent("初始化Addressable 成功");
        Logger.PrintColor("green", "AddressableLoadManager 初始化成功");

        _key = key;
        Logger.PrintColor("blue", "catalog path=" + key);
        //StartCoroutine(checkUpdate());
        isDownAddressable = false;
          StartCoroutine(LoadCataLogs(key));
      //   StartPreloadPrefabe(UIBaseKey);
       // StartCoroutine(LoadCataLogsResource(key));
    }
    private void StartPreloadPrefabe(string key)
    {
        _key = key;
        StartCoroutine(ShowLoadAddressProccess(_key));
        // StartCoroutine(LoadCataLogs(key));
    }
  
    //查看更新的文件名
    //IEnumerator startCataLogsUpdate(List<string> catalogs)
    //{
    //    //.这个时候可以看到服务器的输出,访问了这两个文件,现在客户端本地的catalog文件已经到最新了
    //    var updateHandle = Addressables.UpdateCatalogs(catalogs, false);

    //    yield return updateHandle;
    //  //  Debug.Log("download catalogs finish");
    //    //获取可更新资源key
    //    List<object> keys = new List<object>();
    //    foreach (var item in updateHandle.Result)
    //    {
    //        foreach (var key in item.Keys)
    //        {
    //           // Debug.Log("key1111:" + key);
    //            keys.Add(key);
    //        }
    //    }
    //    //Debug.Log("download bundle start");
    //    var sizeHandle = Addressables.GetDownloadSizeAsync(keys);
    //    yield return sizeHandle;
    //    long totalDownloadSize = sizeHandle.Result;
    //    Debug.Log("下载大小：" + totalDownloadSize);
    //}
    IEnumerator LoadCataLogs(string catalogPath)
    {
       
        AsyncOperationHandle<IResourceLocator> te = Addressables.LoadContentCatalogAsync(catalogPath, true);

        downloadDependenciesOp = Addressables.LoadContentCatalogAsync(catalogPath, false);
        //var downLoadTotalSzeHandle = Addressables.GetDownloadSizeAsync(UIBaseKey);
        //yield return downLoadTotalSzeHandle;
        //downLoadTotalSze = downLoadTotalSzeHandle.Result;
       
        Logger.PrintColor("blue", "开始加载catalog downloadDependenciesOp=" + downloadDependenciesOp.ToString());
       // Logger.PrintColor("blue", "开始加载 UIBaseKey=" + UIBaseKey+ " downLoadTotalSze="+ downLoadTotalSze);
        //test@@@
        //LoadingBarController.SetLoadContent("开始加载catalog");
        downloadDependenciesOp.Completed += (resLocatorAopHandler) =>
        {
           // Logger.PrintColor("blue", "resLocatorAopHandler.Status=" + resLocatorAopHandler.Status );
          //  Logger.PrintColor("blue", "resLocatorAopHandler.IsValid=" + resLocatorAopHandler.IsValid());
            if (resLocatorAopHandler.Status == AsyncOperationStatus.Succeeded)
            {
                Logger.PrintColor("red", "=====加载tools 项目 catalog_1.json boundle 成功=====catalogPath=" + catalogPath);
                // Logger.PrintColor("green", "=====加载tools 项目 boundle 成功=====");
                StartPreloadPrefabe(UIBaseKey);
                // StartPreloadPrefabe(UIBaseKey);//test 界面加载

            }
            else
            {
                Logger.PrintColor("red", "=====加载tools 项目 catalog_1.json boundle 失败=====catalogPath=" + catalogPath);
            }

        };
        yield return downloadDependenciesOp;
        if(downloadDependenciesOp.Status== AsyncOperationStatus.Failed)
        {
             //Logger.PrintColor("red", "=====加载tools 项目 catalog_1.json boundle 失败=====catalogPath=" + catalogPath);
             Logger.PrintColor("red", "erro=" + downloadDependenciesOp.OperationException.ToString());
        }
       
    }
    public async void checkUpdate()
    {
        //开始连接服务器检查更新
        var handle = Addressables.CheckForCatalogUpdates(false);
        await handle.Task;
        Debug.Log("check catalog status " + handle.Status);
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            List<string> catalogs = handle.Result;
            if (catalogs != null && catalogs.Count > 0)
            {
                foreach (var catalog in catalogs)
                {
                    Debug.Log("catalog  " + catalog);
                }
                Debug.Log("download catalog start ");
                var updateHandle = Addressables.UpdateCatalogs(catalogs, false);
                await updateHandle.Task;
                foreach (var item in updateHandle.Result)
                {
                    Debug.Log("catalog result " + item.LocatorId);
                    foreach (var key in item.Keys)
                    {
                        Debug.Log("catalog key " + key);
                       
                    }
                   
                }
                Debug.Log("download catalog finish " + updateHandle.Status);
            }
            else
            {
                Debug.Log("dont need update catalogs");
            }
        }
    }
   public IEnumerator ShowLoadAddressProccess(string key)
    {
        Debug.Log("LoadAddressableObj() 开始加载" + key);

        //test@@@
        //  LoadingBarController.SetLoadContent("更新包["+ key + "]");
        Debug.Log("更新包[" + key + "]");
        var getDownLoadSize = Addressables.GetDownloadSizeAsync(key);
        yield return getDownLoadSize;

        downLoadTotalSze = (getDownLoadSize.Result / 1024f / 1024f);
        string str = "下载总量=" + downLoadTotalSze + "M";
        Debug.Log(str);

        // StartCoroutine(loadPrefabeByAddressable(UIBaseKey));
        //  StartCoroutine(loadPrefabeByAddressable(UIBaseKey));
      //  initSceneButton();
        downloadDependenciesOp = Addressables.DownloadDependenciesAsync(key);
        isDownAddressable = true;
        Logger.PrintColor("blue", "准备加载"+key+" 包");

        downloadDependenciesOp.Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                if (this.m_finishCallback != null)
                    this.m_finishCallback();
            }
        };

        //test@@@
        //LoadingBarController.SetProgress(1, 0);
        //LoadingBarController.SetLoadContent("下载完成");
    }

    IEnumerator LoadAddressableScene(string key)
    {
        Debug.Log("LoadAddressableObj() 开始加载" + key);

        //test@@@
        //  LoadingBarController.SetLoadContent("更新包[" + key + "]");
        Debug.Log("更新包[" + key + "]");
        var getDownLoadSize = Addressables.GetDownloadSizeAsync(key);
        yield return getDownLoadSize;

        downLoadTotalSze = (getDownLoadSize.Result / 1024f / 1024f);
        string str = "下载总量=" + downLoadTotalSze + "M";
        Debug.Log(str);

        StartCoroutine(loadPrefabeByAddressable(UIBaseKey));

    }
    public void initSceneButton()
    {
        loadScene("SceneButton");
    }
    private void loadScene(string sceneName)
    {
        isDownAddressable = true;
        downloadDependenciesOp = Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
        Debug.LogFormat("init {0}", sceneName);
        downloadDependenciesOp.Completed += op =>
        {
            isDownAddressable = false;
        };
    }
    IEnumerator loadPrefabeByAddressable(string key)
    {
        Debug.Log("loadPrefabeByAddressable() loadKey=" + key);
        loadHandle = Addressables.LoadAssetAsync<GameObject>(key);
        yield return loadHandle;
        Debug.Log("loadPrefabeByAddressable() assetHandle.Result=" + loadHandle.Result);
        if (loadHandle.Result != null)
        {
            GameObject obj = GameObject.Instantiate(loadHandle.Result, _loginCanvas);
            obj.transform.SetAsFirstSibling();
        }
    }
    public void Update()
    {
        UpdateAddressableDependencies();
        updateLoadPrefabe();
        AddressableGameProcessManager.updateGameProcess();
    }
    private void updateLoadPrefabe()
    {
        if (loadHandle.IsValid() && loadHandle.Status != AsyncOperationStatus.None)
        {
            //test@@@
            //LoadingBarController.SetProgress(loadHandle.PercentComplete, 0);
            //LoadingBarController.SetLoadContent("正在下载[" + _key + "]版本：" + loadHandle.PercentComplete+"%");

        }
    }
    private void updateGameDependece()
    {

    }

    private float delayUpadateTime = 1;
    private void UpdateAddressableDependencies()
    {
        if (!isDownAddressable)
        {
            return;
        }
        
        if (!downloadDependenciesOp.IsValid())
        {
            return;
        }
        if (!downloadDependenciesOp.IsDone)
        {
            delayUpadateTime -= Time.deltaTime;
            if (delayUpadateTime > 0)
            {
                return;
            }
            delayUpadateTime = 1;
            Debug.Log("downloadDependenciesOp.PercentComplete=" + ((float)downloadDependenciesOp.PercentComplete).ToString()+"  time="+Time.time);

            //test@@@
            //LoadingBarController.SetProgress(downloadDependenciesOp.PercentComplete, 0); 
            ////   LoadingBarController.SetLoadContent("正在下载["+_key+"]版本：" + downloadDependenciesOp.PercentComplete.ToString("0.00") + "% 大小"+ downLoadTotalSze.ToString("0.00") + "M");
            //LoadingBarController.SetLoadContent("正在下载版本：" + downloadDependenciesOp.PercentComplete.ToString("0.00") + "% 大小" + downLoadTotalSze.ToString("0.00") + "M");

        }
        else if (downloadDependenciesOp.IsDone)
        {
    

            isDownAddressable = false;
            //test@@@
            //  LoadingBarController.SetProgress(downloadDependenciesOp.PercentComplete, 0);
        }
    }
    public void setLoadComplete(Action action)
    {
        m_finishCallback = action;
    }

}

