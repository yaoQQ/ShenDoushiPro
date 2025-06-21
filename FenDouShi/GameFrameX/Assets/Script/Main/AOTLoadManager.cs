using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using HybridCLR;

/// <summary>
/// 加载HyBird框架相关的AOTDLL
/// </summary>
public class AOTLoadManager:SingleMonobehaviour<AOTLoadManager>
{
    // Start is called before the first frame update
    private System.Reflection.Assembly gameAss;
    public string updateStr = "";
    public string cateLogPath;
    public void Load() {
        Logger.PrintColor("blue", "AOTLoadManager Start()");
        LoadMetadataForAOTAssembly();//注入裁剪AOT DLL
#if HY_EDITOR
        LoadHotPrefab();
#elif UNITY_EDITOR
        LoadHotPrefab();
#else //网络加载热更

        //网络加载
        string netCatalogPath = "https://outspace-catalog.oss-cn-guangzhou.aliyuncs.com/CataPath/cataPath.txt";
        WWWManager.Instance.load(netCatalogPath,
        (www) => {
            if (www == null || www.error != null || www.bytes == null) {
                Debug.LogError("加载出错 netCatalogPath=" + netCatalogPath);

            }
            else {
                cateLogPath = www.text;
                Debug.Log("netCatalogPath 加载完成！");
                NetCheckCatalog();
            }
        }, null, null, null, true, 0);
#endif

    }
    private void NetCheckCatalog() {

        var op = Addressables.CheckForCatalogUpdates();
        op.Completed += (resLocatorAopHandler) => {
            if (resLocatorAopHandler.Status == AsyncOperationStatus.Succeeded) {
                Logger.PrintColor("red", "=====NetCheckCatalog  CheckForCatalogUpdates 检查CateLog到更新了完成 ");
                getSizeDoloadSize();
            }
            else {
                Debug.LogError("NetCheckCatalog CheckForCatalogUpdates 检查CateLog到更新了失败！！！！！！");
            }
        };
    }
    private void getSizeDoloadSize() {

        var op = Addressables.GetDownloadSizeAsync(new List<string> { "Loading", "BasePackage" });
        op.Completed += (resLocatorAopHandler) => {

            if (resLocatorAopHandler.Status == AsyncOperationStatus.Succeeded) {
                long totalDownloadSize = resLocatorAopHandler.Result;

                Logger.PrintColor("red", "=====GetDownloadSizeAsync.BasePackage.Loading ");
                Debug.Log("下载大小：" + totalDownloadSize);

            }
            else {
                Debug.LogError("获取 BasePackage 失败！！！！！！");
            }
        };

        var UIWidget = Addressables.GetDownloadSizeAsync("SelectPanel");
        UIWidget.Completed += (resLocatorAopHandler) => {

            if (resLocatorAopHandler.Status == AsyncOperationStatus.Succeeded) {
                long totalDownloadSize = resLocatorAopHandler.Result;

                Logger.PrintColor("red", "=====GetDownloadSizeAsync.SelectPanel ");
                Debug.Log("下载大小：" + totalDownloadSize);

            }
            else {
                Debug.LogError("获取 SelectPanel 失败！！！！！！");
            }
        };
        var OutSpacePackage = Addressables.GetDownloadSizeAsync("OutSpacePackage");
        OutSpacePackage.Completed += (resLocatorAopHandler) => {

            if (resLocatorAopHandler.Status == AsyncOperationStatus.Succeeded) {
                long totalDownloadSize = resLocatorAopHandler.Result;

                Logger.PrintColor("red", "=====GetDownloadSizeAsync.OutSpacePackage ");
                Debug.Log("下载大小：" + totalDownloadSize);
                LoadToolBundleSuccess();
            }
            else {
                Debug.LogError("获取 OutSpacePackage 失败！！！！！！");
            }
        };
    }
    private void initCataLog() {
        string cataLogPath = "https://assetstreaming-content.unity.cn/client_api/v1/buckets/71718927-d561-450a-a576-eef283e91fe8/release_by_badge/latest/content/catalog_1.json";
        var downloadDependenciesOp = UnityEngine.AddressableAssets.Addressables.LoadContentCatalogAsync(cataLogPath);
        downloadDependenciesOp.Completed += (resLocatorAopHandler) => {
            if (resLocatorAopHandler.Status == AsyncOperationStatus.Succeeded) {
                Logger.PrintColor("red", "=====加载tools 项目 catalog_1.json boundle 成功=====catalogPath=" + cataLogPath);
                // Logger.PrintColor("green", "=====加载tools 项目 boundle 成功=====");
                NetCheckCatalog();
            }
            else {
                Logger.PrintColor("red", "=====加载tools 项目 catalog_1.json boundle 失败=====catalogPath=" + cataLogPath);
            }
        };
    }
    private void StartPreload() {
        Debug.Log("<color='red'>StartPreload()");
        //string baseUI = "https://assetstreaming-content.unity.cn/client_api/v1/buckets/c7c7bc3c-5da3-4985-b781-07ab57069a22/release_by_badge/latest/content/catalog_1.json";
        //string localURL = "E:/unity/unityProject/YaoYoYoSVN/Client/ServerData/StandaloneWindows/catalog_1.json";
#if UNITY_ANDROID

        string baseUI = "https://assetstreaming-content.unity.cn/client_api/v1/buckets/9ca5ac58-2823-4f2b-b664-cac83f8ea593/release_by_badge/latest/content/catalog_1.json"; //android3
#else
        string baseUI = "https://assetstreaming-content.unity.cn/client_api/v1/buckets/ba52ece8-89cc-4df3-b3cf-6be2422ca923/release_by_badge/latest/content/catalog_1.json";//pc2
#endif
       StartCoroutine(AddressableLoadManager.Instance.StartLoadCataLogsPreload(baseUI));
        // AddressableLoadManager.Instance.setLoadComplete(LoadToolBundleSuccess);
    }
    private void LoadToolBundleSuccess() {
        Debug.Log("<color='red'>LoadToolBundleSuccess()============================begain===========</color>");
        //  LoadMetadataForAOTAssembly();
        LoadUIPackageOne();

    }

    /// <summary>
    /// 加载进入游戏资源入口,HotUpdatePrefab
    /// </summary>
    private void LoadHotPrefab() {
        AsyncOperationHandle ProjectTestHandle = Addressables.LoadAssetAsync<GameObject>("HotUpdatePrefab");

        Logger.PrintDebug("load HotUpdatePrefab start");
        ProjectTestHandle.Completed += (op) => {
            // Debug.Log("3333op.Status=" + op.Status);
            if (op.Status == AsyncOperationStatus.Succeeded) {
                Debug.LogFormat("8.17  21:100=========={0}load HotUpdatePrefab complete========", updateStr);

                Logger.PrintDebug("load HotUpdatePrefab sucess");
                //Type mainCs = gameAss.GetType("Main");
                //Logger.PrintDebug("load HotUpdatePrefab  mainCs=" + mainCs);
                GameObject obj = (GameObject)op.Result;
                GameObject.Instantiate(obj);

            }
        };
    }

    private void LoadUIPackageOne() {


        string dllName = "UIWidgetTest";
        AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync(dllName);
        if (handle.Result != null) {
            if (handle.Result.Count > 0) {
                IResourceLocation location = handle.Result[0];
                string path = location.InternalId;

                Debug.LogFormat("begian====================key={0}==============", dllName);
                Debug.Log("XXX location.InternalId  = " + path);
                Debug.Log("location.HasDependencies  = " + location.HasDependencies);
                Debug.Log("location.HasDependencies  = " + location.Dependencies);
                IList<IResourceLocation> listRes = location.Dependencies;
                for (int i = 0; i < listRes.Count; i++) {
                    Debug.LogFormat("listRes[{0}].InternalId=" + listRes[i].InternalId, i);
                }
                Debug.Log("location.ToString()  = " + location.ToString());
                Debug.Log("location.ProviderId = " + location.ProviderId);
                Debug.Log("location.PrimaryKey = " + location.PrimaryKey);
                Debug.Log("location.ResourceType = " + location.ResourceType);
                Debug.LogFormat("end====================key={0}==============", dllName);

                //Debug.LogFormat("begian====================key={0} DownloadDependenciesAsync==============", dllName);
                //AsyncOperationHandle op = Addressables.DownloadDependenciesAsync(dllName);
                //op.WaitForCompletion();
                //Debug.Log("op.Result=" + op.Result);

            }
            else {
                Debug.LogError("@@@@@@@@@@@@@@@@@ dllName=" + dllName + "handle.Result==null");
            }
        }

        AsyncOperationHandle ProjectTestHandle = Addressables.LoadAssetAsync<TextAsset>(dllName);
        Logger.PrintDebug("load UIWidget begain");
        ProjectTestHandle.Completed += (op) => {
            //  Debug.Log("3333op.Status=" + op.Status);
            if (op.Status == AsyncOperationStatus.Succeeded) {
                //  Debug.LogFormat("=========={0}load ProjectTest complete========", updateStr);
                TextAsset textAsset = (TextAsset)op.Result;
                // Debug.Log("UIWidget = " + textAsset);
                gameAss = System.Reflection.Assembly.Load(textAsset.bytes);
                string typeStr = "AnimationWidget";
                Type test = gameAss.GetType(typeStr);
                Logger.PrintDebug("load UIWidget suceess");
                // Debug.Log("UIWidget  AnimationWidget= " + test);
                //MethodInfo methord= test.GetMethod("TestClip");
                //methord.Invoke(null, null);
                loadHuotTuoClassTwo();


            }
            else {

                Debug.LogError("@@@@@@@@@@@@@@@@@ load dllName=" + dllName + "加载失败");

            }
        };


    }

    //private void loadProjectClass2()
    //{


    //    AsyncOperationHandle ProjectTestHandle = Addressables.LoadAssetAsync<TextAsset>("LittlePrince");

    //    ProjectTestHandle.Completed += (op) =>
    //    {
    //        Debug.Log("3333op.Status=" + op.Status);
    //        if (op.Status == AsyncOperationStatus.Succeeded)
    //        {
    //            Debug.Log("==========222load LittlePrince complete========");
    //            TextAsset textAsset = (TextAsset)op.Result;
    //            Debug.Log("3333textAsset = " + textAsset);
    //            gameAss = System.Reflection.Assembly.Load(textAsset.bytes);
    //            Type test = gameAss.GetType("Main");
    //            Debug.Log("3333textAsset  Main= " + test);
    //            loadHuotTuoClass();
    //        }
    //    };

    //}


    private void loadHuotTuoClassTwo() {
        AsyncOperationHandle HuotTuoHandle = Addressables.LoadAssetAsync<TextAsset>("HotFix");
        Logger.PrintDebug("load HotFix begain");
        HuotTuoHandle.Completed += (op) => {
            //  Debug.Log("3333 loadHuotTuoClass.Status=" + op.Status);
            if (op.Status == AsyncOperationStatus.Succeeded) {
                //   Debug.Log("==========222load HotFix complete========");
                TextAsset textAsset = (TextAsset)op.Result;
                //  Debug.Log("3333textAsset = " + textAsset);
                gameAss = System.Reflection.Assembly.Load(textAsset.bytes);
                Type test = gameAss.GetType("PrintHello");
                //  Debug.Log("3333textAsset  PrintHello= " + test);
                Logger.PrintDebug("load HotFix sucess");
                LoadHotPrefab();
            }
        };

    }
    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(Screen.width-100, 100, 100, 100), "showPreFab"))
    //    {
    //        showPreFabe();
    //    }


    //}
    //private void showPreFabe()
    //{
    //    AsyncOperationHandle PreFabHandle = Addressables.LoadAssetAsync<GameObject>("HotUpdatePrefab");

    //    PreFabHandle.Completed += (op) =>
    //    {
    //        Debug.Log("3333 showPreFabe.Status=" + op.Status);
    //        if (op.Status == AsyncOperationStatus.Succeeded)
    //        {
    //            Debug.Log("==========222load showPreFabe complete========");
    //            GameObject pre = (GameObject)op.Result;
    //            GameObject.Instantiate(pre);

    //        }
    //    };
    //}
    ///// <summary>
    ///// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
    ///// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
    ////使用 hybridclr_unity package中的
    ////HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly函数
    ////为AOT的assembly补充对应的元数据。 
    ////LoadMetadataForAOTAssembly函数可以在任何时机调用，
    ////另外既可以在AOT中调用，也可以在热更新中调用，
    ////你只要在使用AOT泛型前调用即可（只需要调用一次）。
    ///// </summary>
    private void LoadMetadataForAOTAssembly() {
        Debug.Log("LoadMetadataForAOTAssembly() 1111");
        /// 注意，补充元数据是给AOT dll补充元数据，而不是给热更新dll补充元数据。
        /// 热更新dll不缺元数据，不需要补充，如果调用LoadMetadataForAOTAssembly会返回错误
        List<string> aotDllList = new List<string>
        {
            "mscorlib",
            "System",
            "System.Core",
            // "UnityEngine.CoreModule",
               // "Newtonsoft.Json.dll", 
            
        };
        Debug.Log("LoadMetadataForAOTAssembly() 2222");
        foreach (var dllStr in aotDllList) {
            var cop = Addressables.LoadAssetAsync<TextAsset>(dllStr).WaitForCompletion();
            Debug.Log("LoadMetadataForAOTAssembly() 3333 dllStr=" + dllStr);
            byte[] dllBytes = cop.bytes;

            //  加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
            LoadImageErrorCode errocode = HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, HomologousImageMode.SuperSet);
            Debug.Log("LoadMetadataForAOTAssembly()" + "dllStr=" + dllStr + "  LoadImageErrorCode=" + errocode);

        }
        Debug.Log("<color='red'>LoadMetadataForAOTAssembly============================end===========</color>");
        //HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly()
        //foreach (var dllBytes in LoadDll.aotDllBytes)
        //{
        //    fixed (byte* ptr = dllBytes.bytes)
        //    {
        //        加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
        //  HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly(Assmblybyte, HomologousImageMode.SuperSet);
        //    }

        //}
    }
}
