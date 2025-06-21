using HybridCLR;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// 预加载管理类
/// </summary>
public class PreloadManager : Singleton<PreloadManager>
{
    bool preloadSign = false;
    //加载和保存程序集
    private PreloadAssembleManager preloadAssembleManager = new PreloadAssembleManager();
    //保存实例化的游戏项目对象
    public Dictionary<string, AbstractPackage> packageDic = new Dictionary<string, AbstractPackage>();

    /// <summary>
    /// 加载场景所需资源
    /// </summary>
    /// <param name="order">每个游戏定义的包预加载类 如：OutSpacePreload : AbstractPreloadOrder</param>
    public void ExecuteOrder(LuaPreloadOrder order) {
        if (preloadSign) {
            Loger.PrintError("预加载同一时间只能执行一个");
            return;
        }
        preloadSign = true;
        MainThread.Instance.StartCoroutine(AsynPreload(order));
    }

    IEnumerator AsynPreload(LuaPreloadOrder order) {
        List<BaseView> uiViewList = order.getUIPreload();
        LuaScene sceneObj = order.getScenePreload();
        if (uiViewList != null) {
            for (int i = 0; i < uiViewList.Count; i++) {
                if (uiViewList[i].getIsLoaded()) {
                    order.onPreloadStepEnd();
                    Logger.PrintColor("blue", uiViewList[i].getViewEnum() + " 已经加载过了");
                    continue;
                }
                List<string> loadOrders = uiViewList[i].getLoadOrders();
                foreach (var loadPath in loadOrders) {
                    string[] orderArr = loadPath.Split(':');
                    if (orderArr.Length != 2) {
                        orderArr = new string[2];
                        orderArr[0] = "BasePackage";
                        orderArr[1] = loadPath;
                    }
                    // 老版本 yield return MainThread.Instance.StartCoroutine(UILoadControl.Instance.AsyncCreateUI(orderArr[0], orderArr[1], uiViewList[i], false, order));
                    Loger.PrintDebug("@@@@@@@@@@@AsynPreload() 预加载 package=" + orderArr[0] + " name=" + orderArr[1]);
                    UILoadControl.Instance.AsyncCreateUI(orderArr[0], orderArr[1], uiViewList[i], false, order);
                }
                while (!uiViewList[i].getIsLoaded()) {
                    yield return 0;
                }
                Logger.PrintColor("blue", "加载单步UI完毕. name: {0}, state:{1}" + loadOrders[0]);
            }
        }
#if !TEST_SCENE //TEST_SCENE单独场景运行，不从开始一步步加载
        bool changeSign = true;
        if (sceneObj != null) {
            //if (sceneObj.getIsInit())
            //{

            //   order.onPreloadStepEnd();
            //}
            //else
            //{
            changeSign = false;
            SceneManager.Instance.ChangeScene(sceneObj, () => {
                changeSign = true;
                order.onPreloadStepEnd();

            });
            // }
        }

        while (!changeSign) {
            yield return 0;
        }
#endif
        order.onPreloadEnd();
        preloadSign = false;
    }

    /// <summary>
    /// 从游戏场景返回其他加载过的包场景
    /// </summary>
    /// <param name="package"></param>
    /// <param name="order"></param>
    public void BackToScene(string package) {
        if (!packageDic.ContainsKey(package)) {
            Logger.PrintError("没有加载过 " + package + "资源包！");
            return;
        }
        AbstractPackage packageClass = packageDic[package];
        PreloadManager.Instance.ExecuteOrder(packageClass.PreloadOrder);
    }
    public void BackToScene(GameEnum package) {
        if (!packageDic.ContainsKey(package.ToString())) {
            Logger.PrintError("没有加载过 " + package + "资源包！");
            return;
        }
        AbstractPackage packageClass = packageDic[package.ToString()];
        PreloadManager.Instance.ExecuteOrder(packageClass.PreloadOrder);
    }
    public void PreLoadPackage(string packageName) {
        Logger.PrintColor("red", "PreLoadPackage 开始加载packageName=" + packageName);
        LoadingBarController.Show();
        LoadingBarController.SetLoadContent("开始加载[" + packageName + "] 包");
        // AbstractPackage currPackage;
        Debug.Log("PreLoadPackage（） 开始加载[" + packageName + "] 包");

        switch (packageName) {
            case ProjectControler.basePackage://basePackage包加载
                PreLoadPackage(GameEnum.BasePackage);
                break;
            case ProjectControler.littlePrincePackage:

                PreLoadPackage(GameEnum.LittlePrincePackage);
                break;
            case ProjectControler.shaderPro:
                PreLoadPackage(GameEnum.ShaderProPackage);
                break;
            case ProjectControler.cameraPostPro:
                PreLoadPackage(GameEnum.CameraPostPackage);
                break;
            case ProjectControler.OutSpacePro:
                PreLoadPackage(GameEnum.OutSpacePackage);
                break;
        }
    }
#if AR_SYSTEM
    private bool isLoadAR = false;
#endif
    public void PreLoadPackage(GameEnum gameID) {

        Logger.PrintColor("red", "PreLoadPackage 开始加载packageName=" + gameID);
        LoadingBarController.Show();
        LoadingBarController.SetLoadContent("开始加载[" + gameID + "] 包");
        // AbstractPackage currPackage;
        Debug.Log("PreLoadPackage（） 开始加载[" + gameID + "] 包");

        switch (gameID) {
            case GameEnum.BasePackage://basePackage包加载
                HyBirdObjectScriptBug();
                loadDllAndEnter(gameID.ToString(), "LoadBasePreload");
                break;
            case GameEnum.LittlePrincePackage:
                loadDllAndEnter(gameID.ToString(), "LittlePrincePreload");
                break;
            case GameEnum.ShaderProPackage:
                loadDllAndEnter(gameID.ToString(), "ShaderProPreload");
                break;
            case GameEnum.CameraPostPackage:
                loadDllAndEnter(gameID.ToString(), "CameraPostPreload");
                break;
            case GameEnum.OutSpacePackage:
                //OutSpacePackage+PhythmMusicScript 两个项目代码
                preloadAssembleManager.LoadGameAssemble(ProjectControler.PhythmMusicScript, (gameAss) => {
                    Logger.PrintColor("red", "加载PhythmMusic类集合完成");
                    Type IMusicInterface = gameAss.GetType("IMusicInterface");
                    Logger.PrintColor("red", "IMusicInterface=" + IMusicInterface);
                    Type MusicDataBase = gameAss.GetType("MusicDataBase");
                    Logger.PrintColor("red", "MusicDataBase=" + MusicDataBase);
                    Type SoundBar = gameAss.GetType("SoundBar");
                    Logger.PrintColor("red", "SoundBar=" + SoundBar);
                    LoadingBarController.SetLoadContent("加载[" + gameID + "] 脚本包完成");
                });
                loadDllAndEnter(gameID.ToString(), "OutSpacePreload");
                break;
            case GameEnum.ARProPackage:
                loadDllAndEnter(gameID.ToString(), "ARPreload");
                break;
            case GameEnum.ProceduralPlanetPackage:
                //OutSpacePackage+ProceduralPlanetPackage 两个项目代码
                preloadAssembleManager.LoadGameAssemble(ProjectControler.OutSpacePro, (gameAss) => {
                    loadDllAndEnter(gameID.ToString(), "ProceduralPlanetPreload");
                });

                break;
        }
    }
    private void LoadGameAssemble(string AssembleName, Action<Assembly> callBack) {
        string preLoadDll = AssembleName;//提前加集合类
        AsyncOperationHandle ProjectTestHandle = Addressables.LoadAssetAsync<TextAsset>(preLoadDll);
        ProjectTestHandle.Completed += (op) => {
            if (op.Status == AsyncOperationStatus.Succeeded) {
                TextAsset textAsset = (TextAsset)op.Result;
                Assembly gameAss = Assembly.Load(textAsset.bytes);
                Logger.PrintColor("red", "加载PhythmMusic类集合完成");
                Type IMusicInterface = gameAss.GetType("IMusicInterface");
                Logger.PrintColor("red", "IMusicInterface=" + IMusicInterface);
                Type MusicDataBase = gameAss.GetType("MusicDataBase");
                Logger.PrintColor("red", "MusicDataBase=" + MusicDataBase);
                Type SoundBar = gameAss.GetType("SoundBar");
                Logger.PrintColor("red", "SoundBar=" + SoundBar);
                if (callBack != null) {
                    callBack(gameAss);
                }
            }
            else {
                Logger.PrintError("@@@加载PhythmMusic类集合失败！！！");
            }
        };
    }

    /// <summary>
    /// 加载包的程序名
    /// </summary>
    /// <param name="packageName">AbstractPackage的对应包类名与对应DLL名相同</param>
    /// <param name="preLoadTypeName">AbstractPreloadOrder包的预加载类名</param>
    /// 


    private void loadDllAndEnter(string packageName, string preLoadTypeName) {
        preloadAssembleManager.LoadGameAssemble(packageName, (gameAss) => {
            initPackge(gameAss, packageName, preLoadTypeName);
        });
    }

    //HyBirdCRL框架ObjectScript资源丢失bug，需重新加载catalog.json
    private void HyBirdObjectScriptBug() {
        Logger.PrintColor("red", "======HyBirdObjectScriptBug outspace======");
        
        string cataLogPath = $"{Addressables.RuntimePath}/catalog.json";
        Logger.PrintColor("red", "======HyBirdObjectScriptBug cataLogPath:"+ cataLogPath);
        AsyncOperationHandle handle = Addressables.LoadContentCatalogAsync(cataLogPath);
        handle.Completed += (op) => {
            if (op.Status == AsyncOperationStatus.Succeeded) {
                Logger.PrintColor("yellow", "（objectScript资源bug,无法加载）加载更新catalog.json op.Result=" + op.Result);
                //Type createEnemyWave = gameAss.GetType("CreateEnemyWave");
                ////测试代码裁剪完整性
                //Type gameGunData2 = gameAss.GetType("GameGunData2");
                //Type gunInfo = gameAss.GetType("GunInfo");
                //Type attriInfo = gameAss.GetType("AttriInfo");
                //Type EnemyLevelWave = gameAss.GetType("EnemyLevelWave");
                //Type enemyLevelWave = gameAss.GetType("EnemyLevelWave");
                //Type enemyLevel = gameAss.GetType("EnemyLevel");
                //Logger.PrintColor("red", "==========GameGunData======== ：gameGunData2=" + gameGunData2);
                //Logger.PrintColor("red", "==========GunInfo======== ：GunInfo=" + gunInfo);
                //Logger.PrintColor("red", "==========attriInfo======== ：attriInfo=" + attriInfo);
                //////    Logger.PrintColor("red", "==========GameGunData========str=：" + gameGunData.ToString());
                //Logger.PrintColor("red", "==========CreateEnemyWave========：createEnemyWave=" + createEnemyWave);
                //Logger.PrintColor("red", "==========@@@@@@@@@@EnemyLevelWave========：EnemyLevelWave=" + EnemyLevelWave);
                //    Logger.PrintColor("red", "==========enemyLevelWave========：enemyLevelWave=" + enemyLevelWave);
                //    Logger.PrintColor("red", "==========enemyLevel========：enemyLevel=" + enemyLevel);
            }
            else {
                Logger.PrintError("bug huotuo框架（objectScript资源bug,无法加载）重新更新catalog.json错误！ op.Result=" + op.Result);
            }
        };
    }



    /// <summary>
    /// 初始化所加载的包
    /// </summary>
    /// <param name="gameAss">包的程序集</param>
    /// <param name="packageName">包名</param>
    /// <param name="preLoadTypeName">包的预加载类名</param>
    private void initPackge(Assembly gameAss, string packageName, string preLoadTypeName) {

        Logger.PrintDebug("initPackge() packageName=" + packageName);
        Type pack = gameAss.GetType(packageName);
        System.Object packageObj = Activator.CreateInstance(pack);
        if (packageObj == null) {
            Logger.PrintError(gameAss + "没有  " + packageName + ".cs ");
            return;
        }
        AbstractPackage currPackage = packageObj as AbstractPackage;
        packageDic[packageName] = currPackage;
        currPackage.init(() => {
            Type type = gameAss.GetType(preLoadTypeName);
            System.Object obj = Activator.CreateInstance(type, currPackage);
            Logger.PrintDebug("LoadPreloadObj=" + obj);
            if (obj == null) {
                Logger.PrintError(packageName + ".dll没有  " + preLoadTypeName + ".cs ");
                return;
            }
            LuaPreloadOrder preLoad = obj as LuaPreloadOrder;
            currPackage.PreloadOrder = preLoad;
            Logger.PrintDebug("  LoadBasePreload= " + preLoad);
            PreloadManager.Instance.ExecuteOrder(preLoad);

        });
    }

    public void ReleasePackage(string packageName) {
        if (packageDic.ContainsKey(packageName)) {
            packageDic[packageName].ReleaseAll();
            packageDic.Remove(packageName);
        }
    }
    public void Destroy() {
        packageDic.Clear();
        preloadAssembleManager.Destory();
        preloadSign = false;
    }

    public void ReleaseUIView(BaseView baseView) {
        string viewPackage = baseView.getPackage();
        if (packageDic.ContainsKey(viewPackage)) {
            packageDic[viewPackage].ReleaseView(baseView);
        }
    }


}
