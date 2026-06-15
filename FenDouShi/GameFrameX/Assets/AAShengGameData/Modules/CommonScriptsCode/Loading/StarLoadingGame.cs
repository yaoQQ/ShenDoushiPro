using DataTableFrame;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using FairyGUI;
using launch;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class StarLoadingGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FGUIOperatHandletManager.Instance.Init();
        UIPackage.AddPackage(G_LaunchPage.PACKAGE_NAME);
        LoadingBarController.Instance.InitLoadingView(() =>
        {

        });
        _ = CheckUpdate();

    }

    // 检查更新
    public async Task CheckUpdate()
    {
        try
        {
            Logger.PrintDebug("LaunchPage CheckUpdate()");
            // 记录开始时间
            var start = DateTime.Now;

            // 设置加载内容为初始化资源系统
            LoadingBarController.SetLoadContent("初始化资源系统...");
            // 初始化Addressables
            await Addressables.InitializeAsync();

            // 打印Addressables初始化完成日志
            Logger.PrintDebug("Addressables 初始化完成()");
            // 获取Addressables运行时路径
            var a = Addressables.RuntimePath;
            Logger.PrintDebug("Addressables.RuntimePath=" + Addressables.RuntimePath);
            // 检查目录更新
            var catalogs = await Addressables.CheckForCatalogUpdates(false);
            // 检查是否有更新
            // 获取更新目录列表

            // 打印检查更新耗时
            Logger.PrintDebug(string.Format("CheckIfNeededUpdate use {0}ms", (DateTime.Now - start).Milliseconds));
            // 打印目录数量和检查状态
            Logger.PrintDebug($"catalog count: {catalogs.Count} === check status: {catalogs}");

            // 如果有更新目录
            if (catalogs != null && catalogs.Count > 0)
            {
                Logger.PrintDebug($"=========================更新开始===================================");
                // 设置加载内容为更新资源
                LoadingBarController.SetLoadContent("更新资源...");
                // 设置进度为0.1
                LoadingBarController.SetProgress(0.1f);


                // 记录更新开始时间
                start = DateTime.Now;
                // 更新目录
                var locators = await Addressables.UpdateCatalogs(catalogs, false);
                // 处理更新结果

                // 打印定位器数量
                Logger.PrintDebug($"locator count: {locators.Count}");



                // 遍历定位器列表
                foreach (var v in locators)
                {
                    // 获取下载大小
                    var size = await Addressables.GetDownloadSizeAsync(v.Keys);
                    // 设置加载内容为显示下载大小
                    LoadingBarController.SetLoadContent($"下载资源...{UtilityBuiltin.Valuer.GetByteLengthString(size)}");
                    // 打印下载大小
                    Logger.PrintDebug($"download size:{size}");

                    // 如果有资源需要下载
                    if (size > 0)
                    {

                        LoadingBarController.ShowAlert1("检测到新版本资源，建议在WiFi环境下更新，是否继续？",
                          "确定", () =>
                          {
                              Logger.PrintDebug("用户点击确定");
                          });
                        await LoadingBarController.WaitForResponse();

                        // 开始下载依赖资源
                        var downloadHandle = Addressables.DownloadDependenciesAsync(v.Keys, Addressables.MergeMode.Union);
                        // 等待下载完成
                        while (!downloadHandle.IsDone)
                        {
                            // 获取下载进度
                            float percentage = downloadHandle.PercentComplete;
                            // 打印下载进度
                            Logger.PrintDebug($"download pregress: {percentage}");
                            // 设置加载进度
                            LoadingBarController.SetProgress(percentage);
                        }
                        // 释放下载句柄
                        Addressables.Release(downloadHandle);
                    }
                }

                // 打印更新完成耗时
                Logger.PrintDebug(string.Format("UpdateFinish use {0}ms", (DateTime.Now - start).Milliseconds));
                // 处理更新完成逻辑

                // 释放定位器
                Addressables.Release(locators);

                // 释放目录
                Addressables.Release(catalogs);
                Logger.PrintDebug($"=========================更新结束===================================");
            }
            else
            {
                // 打印没有更新的日志
                Logger.PrintDebug("没有资源需要更新");
            }
            LoadingBarController.SetLoadContent("初始化UI资源...");
            Logger.PrintDebug("初始化UI资源...");



            start = DateTime.Now;
            await FGUIAssetManager.Instance.Init(async () =>
            {
                Logger.PrintDebug("FGUIAssetManager.Instance.Init compelete!");
                LoadingBarController.SetLoadContent("初始化完成");
                LoadingBarController.SetProgress(1);
                Logger.PrintDebug(string.Format("FGUIAssetManager.Instance.Init use {0}ms", (DateTime.Now - start).Milliseconds));
                // Instance.m_loadingView = new LoadingView(LoadCallBakc);
                await LoadLoginPackage();
            });
             FGUIAssetManager.Instance.EnterGameAddressablesLoader();
        }
        catch (Exception error)
        {

            LoadingBarController.ShowAlert1("初始化过程中出现错误：" + error, "确定", () =>
            {
                Application.Quit();
            });
        }
    }
    private async Task LoadLoginPackage()
    {
        Logger.PrintDebug($" LoadLoginPackage() 1000");

        LoadingBarController.SetLoadContent("加载登录资源...");
        var HotUpdatePrefab = await Addressables.LoadAssetAsync<GameObject>("HotUpdatePrefab").Task;

      
        Logger.PrintDebug("TestLoadPackage() load BasePackage end");


        GameObject obj = GameObject.Instantiate(HotUpdatePrefab);
        Logger.PrintDebug("实例化BasePackage对象 obj=" + obj);
        LoadingBarController.SetLoadContent("实例化BasePackage对象");
        UpdateFinish();
    }


    void UpdateFinish()
    {
        // 设置进度为100%
        LoadingBarController.SetProgress(1);
        // 设置加载内容为加载完成
        LoadingBarController.SetLoadContent("加载完成!");
        LoadingBarController.ShowEnterBtn();
        // 销毁当前StarLoadingGame组件
        GameObject.Destroy(this);
    }
    //private void OnApplicationQuit()
    //{
    //    FGUIOperatHandletManager.Instance.ReleaseAllAssets();
    //    UIPackage.RemoveAllPackages();
    //    FGUIAssetManager.Instance.Destroy();
    //}
}
