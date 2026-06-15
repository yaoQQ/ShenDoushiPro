using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// 预加载管理类
/// </summary>
public class PreloadManager : Singleton<PreloadManager>
{
    bool preloadSign = false;
    //加载和保存程序集
    private PreloadAssembleManager preloadAssembleManager;
    //保存实例化的游戏项目对象
    private Dictionary<PackageEnum, AbstractPackage> packageDic;

    public void Init()
    {
        preloadAssembleManager = new PreloadAssembleManager();
        packageDic = new Dictionary<PackageEnum, AbstractPackage>();
    }
    /// <summary>
    /// 加载场景所需资源
    /// </summary>
    /// <param name="order">每个游戏定义的包预加载类 如：OutSpacePreload : AbstractPreloadOrder</param>
    public void ExecuteOrder(PreloadOrder order)
    {
        if (preloadSign)
        {
            Logger.PrintError("预加载同一时间只能执行一个");
            return;
        }
        preloadSign = true;
        MainThread.Instance.StartCoroutine(AsynPreload(order));
    }

    IEnumerator AsynPreload(PreloadOrder order)
    {
        List<IBaseView> uiViewList = order.getUIPreload();
        IScene sceneObj = order.getScenePreload();
        if (uiViewList != null)
        {
            for (int i = 0; i < uiViewList.Count; i++)
            {
                if (!uiViewList[i].IsPreload)
                {
                  //  Logger.PrintColor("red", uiViewList[i].getViewEnum() + " 不提前加载");
                    continue;
                }
                if (uiViewList[i].getIsLoaded())
                {
                    order.onPreloadStepEnd();
                    Logger.PrintColor("blue", uiViewList[i].getViewEnum() + " 已经加载过了");
                    continue;
                }

                Logger.PrintColor("white", "@@@@@@@@@@@AsynPreload() 预加载 package=" + uiViewList[i].PackageName + " name=" + uiViewList[i].ComponentName);
                FGUIAssetManager.Instance.CreateObjectAsync(uiViewList[i].PackageName, uiViewList[i].ComponentName, uiViewList[i], order);

                while (!uiViewList[i].getIsLoaded())
                {
                    yield return 0;
                }
                Logger.PrintColor("white", $"AsynPreload加载单步UI完毕. name: {0}, state:{1}" + uiViewList[i].ComponentName);
            }
        }
        bool isCompleted = true;
        if (sceneObj != null)
        {
            isCompleted = false;
            SceneManager.Instance.ChangeScene(sceneObj, () =>
            {
                isCompleted = true;
                order.onPreloadStepEnd();

            });
        }

        while (!isCompleted)
        {
            yield return 0;
        }
        order.onPreloadEnd();
        preloadSign = false;
    }

    /// <summary>
    /// 从游戏场景返回其他加载过的包场景
    /// </summary>
    /// <param name="package"></param>
    /// <param name="order"></param>
    public void BackToScene(PackageEnum package)
    {
        if (!packageDic.ContainsKey(package))
        {
            Logger.PrintError("没有加载过 " + package + "资源包！");
            return;
        }
        AbstractPackage packageClass = packageDic[package];
        PreloadManager.Instance.ExecuteOrder(packageClass.PreloadOrder);
    }

    public void PreLoadPackage(PackageEnum packageID)
    {

        Logger.PrintGreen($"PreLoadPackage({packageID}) 开始加载");
        LoadingBarController.Show();
        LoadingBarController.SetLoadContent("开始加载[" + packageID + "] 包");
        loadDllAndEnter(packageID);
    }


    /// <summary>
    /// 加载包的程序名
    /// </summary>
    /// <param name="packageName">AbstractPackage的对应包类名与对应DLL名相同</param>
    /// </param>
    /// 
    private void loadDllAndEnter(PackageEnum packageName)
    {
        preloadAssembleManager.LoadGameAssemble(packageName, (gameAss) =>
        {
            Logger.PrintColor("green", $"加载{packageName}类集合完成 gameAss={gameAss}");
            ChangePackageSceneManager.Instance.SetCurrPackage(packageName);
            initPackge(gameAss, packageName);
        });
    }

    //HyBirdCRL框架ObjectScript资源丢失bug，需重新加载catalog.json
    private void HyBirdObjectScriptBug()
    {
        Logger.PrintColor("red", "======HyBirdObjectScriptBug outspace======");

        string cataLogPath = $"{Addressables.RuntimePath}/catalog.json";
        Logger.PrintColor("red", "======HyBirdObjectScriptBug cataLogPath:" + cataLogPath);
        AsyncOperationHandle handle = Addressables.LoadContentCatalogAsync(cataLogPath);
        handle.Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                Logger.PrintColor("yellow", "（objectScript资源bug,无法加载）加载更新catalog.json op.Result=" + op.Result);
            }
            else
            {
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
    private void initPackge(Assembly gameAss, PackageEnum packageName)
    {

        Logger.PrintDebug($"gameAss={gameAss}initPackge() packageName={packageName}");
        Type pack = gameAss.GetType(packageName.ToString());
        if (pack == null)
        {
            return;
        }
        System.Object packageObj = Activator.CreateInstance(pack);
        if (packageObj == null)
        {
            Logger.PrintError(gameAss + "没有  " + packageName + ".cs ");
            return;
        }
        AbstractPackage currPackage = packageObj as AbstractPackage;
        packageDic[packageName] = currPackage;
        currPackage.init(() =>
        {
            PreloadManager.Instance.ExecuteOrder(currPackage.PreloadOrder);

        });
    }

    public bool isLoadedPackage(PackageEnum package)
    {
        return packageDic.ContainsKey(package);
    }
    public void ReleasePackage(PackageEnum packageName)
    {
        if (packageDic.ContainsKey(packageName))
        {
            packageDic[packageName].ReleaseAll();
            packageDic.Remove(packageName);
        }
    }
    public void ReleaseAllPackage()
    {
        foreach (var pair in packageDic)
        {
            packageDic[pair.Key].ReleaseAll();
        }
        packageDic.Clear();
    }

    public void Destroy()
    {
        preloadSign = false;
        packageDic.Clear();
        preloadAssembleManager.Destory();
        preloadAssembleManager = null;
        packageDic = null;
    }
}
