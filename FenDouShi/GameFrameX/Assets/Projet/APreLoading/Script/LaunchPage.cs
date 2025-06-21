using FairyGUI;
using NiceTS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// 定义一个名为LaunchPage的类，继承自MonoBehaviour，可挂载到Unity的游戏对象上
public class LaunchPage : MonoBehaviour
{
    // 定义一个常量URL，用于指定UI的地址
    public const string URL = "ui://ynb47g4jpyg64t";
    private GComponent _mainView;
    private GTextField gTextField;
    private GProgressBar gProgress;
    [SerializeField]
    private LoginViewMono loginView;

    private LoginView LoginView;
    private LoginViewOnInit loginViewOnInit;
    // 当脚本实例被加载时调用该方法
    void Start()
    {
        Logger.PrintDebug("LaunchPage Start");
        // 获取当前游戏对象上的UIPanel组件，并获取其UI作为主视图
        _mainView = this.GetComponent<UIPanel>().ui;
        if (_mainView == null)
        {
            Logger.PrintError("LaunchPage _mainView is null");
            return;
        }

        // 从主视图中获取名为"updateTxt"的子组件，并将其转换为文本框组件
        gTextField = _mainView.GetChild("updateTxt").asTextField;
        // 从主视图中获取名为"updateProgress"的子组件，并将其转换为进度条组件
        gProgress = _mainView.GetChild("updateProgress").asProgress;
        // 设置进度条可见
        gProgress.visible = true;

        // 启动一个协程，用于检查资源更新
        StartCoroutine(CheckUpdate());
    }

    // 用于绘制GUI元素的方法，每帧都会调用
    private void OnGUI()
    {
        // 在屏幕上绘制一个按钮，点击该按钮会执行加载FairyGUI包的操作
        if (GUI.Button(new Rect(10, 10, 200, 200), "LoadBoundleTest"))
        {
            // 打印调试信息，表示点击了加载包的按钮
            Logger.PrintDebug("Click LoadBoundle");
            // 启动一个协程，用于加载FairyGUI包
            LoadFairyGUIPackage("login", "login_fui.bytes");
        }
        // 在屏幕上绘制一个按钮，点击该按钮会执行加载FairyGUI包的操作
        if (GUI.Button(new Rect(10, 200, 200, 200), "Show LonginView UIPanel"))
        {
            // 打印调试信息，表示点击了加载包的按钮
            Logger.PrintDebug("Click  LonginView UIPanel");
            loginView.Show();
        }
        if (GUI.Button(new Rect(10, 400, 200, 200), "LonginView windows"))
        {
            // 打印调试信息，表示点击了加载包的按钮
            Logger.PrintDebug("Click loginViewOnInit windows");
            if (loginViewOnInit == null)
            {
                loginViewOnInit = new LoginViewOnInit();
                loginViewOnInit.Show();
            }
            
        }
    }

    // 异步方法，用于加载FairyGUI包
    public async void LoadFairyGUIPackage(string packageName, string address)
    {
        Logger.PrintDebug($"22LoadFairyGUIPackage() {packageName} {address} ");
        // 异步加载指定地址的gui.bytes
        var pkgAsset = await Addressables.LoadAssetAsync<TextAsset>(address).Task;
        // 打印调试信息，显示加载完成的包名、地址和资源大小
        Logger.PrintDebug($"LoadFairyGUIPackage() 加载完成 {address} {packageName} pkgAsset.dataSize={pkgAsset.dataSize}");
        // 将加载的资源添加到FairyGUI包中
        UIPackage uipackage = UIPackage.AddPackage(
            pkgAsset.bytes,
            packageName,
            async (string name, string extension, Type type, PackageItem packageItem) =>
            {
                Logger.PrintColor("blue", $"异步加载回调 name={name}, extension={extension}, type={type.ToString()}, PackageItem={packageItem.ToString()}");
                // 检查资源类型是否为纹理
                if (type == typeof(Texture))
                {
                    Logger.PrintColor("yellow", $"@@@准备加载纹理资源name={name} extension={extension}");
                    // 异步加载纹理资源
                    Texture t = await Addressables.LoadAssetAsync<Texture>(name + extension).Task;
                    Logger.PrintColor("yellow", $"加载纹理成功t={t}");
                    // 将加载的纹理资源设置给FairyGUI包项,即加载资源成功
                    packageItem.owner.SetItemAsset(packageItem, t, DestroyMethod.Custom);
                }
            });
        // 打印调试信息，显示添加的FairyGUI包
        Logger.PrintDebug($"uipackage={uipackage}");
        uipackage.LoadAllAssets();
   
        // 注释掉的代码，用于释放加载的资源
          Addressables.Release(pkgAsset);
    }
  
    // 协程方法，用于检查资源更新
    IEnumerator CheckUpdate()
    {
        Logger.PrintDebug("LaunchPage CheckUpdate()");
        // 记录当前时间，用于计算检查更新所花费的时间
        var start = DateTime.Now;

        // 设置文本框的文本内容，表示正在检查资源更新
        gTextField.text = "正在检查资源更新...";

        // 等待Addressables系统初始化完成
        yield return Addressables.InitializeAsync();
        // 打印调试信息，表示Addressables系统初始化完成
        Logger.PrintDebug("Addressables 初始化完成()");
        // 获取Addressables系统的运行时路径
        var a = Addressables.RuntimePath;
        // 检查是否有目录更新
        var catalogs = Addressables.CheckForCatalogUpdates(false);
        // 等待检查目录更新的操作完成
        yield return catalogs;
        // 获取检查目录更新的结果列表
        var catalogsList = catalogs.Result;

        // 打印调试信息，显示检查是否需要更新所花费的时间
        Logger.PrintDebug(string.Format("CheckIfNeededUpdate use {0}ms", (DateTime.Now - start).Milliseconds));
        // 打印调试信息，显示目录的数量和检查状态
        Logger.PrintDebug($"catalog count: {catalogsList.Count} === check status: {catalogsList}");

        // 检查是否有需要更新的目录
        if (catalogsList != null && catalogsList.Count > 0)
        {
            Logger.PrintDebug($"=========================资源更新Start===================================");
            // 设置文本框的文本内容，表示正在更新资源
            gTextField.text = "正在更新资源...";
            // 设置进度条可见
            gProgress.visible = true;
            // 将进度条的值设置为0
            gProgress.value = 0;

            // 记录当前时间，用于计算资源更新所花费的时间
            start = DateTime.Now;
            // 更新目录列表
            var locators = Addressables.UpdateCatalogs(catalogsList, false);
            // 等待目录更新操作完成
            yield return locators;

            // 打印调试信息，显示更新后的定位器数量
            Logger.PrintDebug($"locator count: {locators.Result.Count}");

            // 遍历更新后的定位器列表
            foreach (var v in locators.Result)
            {
                // 获取需要下载的资源大小
                var size = Addressables.GetDownloadSizeAsync(v.Keys);
                // 等待获取下载大小的操作完成
                yield return size;
                // 打印调试信息，显示需要下载的资源大小
                Logger.PrintDebug($"download size:{size}");

                // 检查是否有需要下载的资源
                if (size.Result > 0)
                {
                    // 获取UINoticeWin的实例
                    UINoticeWin notice = UINoticeWin.Inst;
                    // 显示一个带有确认按钮的提示窗口，提示用户本次更新的大小
                    notice.ShowOneButton($"本次更新大小：{size.Result}", () =>
                    {
                        // 点击确认按钮后隐藏提示窗口
                        notice.Hide();
                    });

                    // 等待用户点击确认按钮
                    yield return notice.WaitForResponse();

                    // 开始下载资源的依赖项
                    var downloadHandle = Addressables.DownloadDependenciesAsync(v.Keys, Addressables.MergeMode.Union);
                    // 循环检查下载操作是否完成
                    while (!downloadHandle.IsDone)
                    {
                        // 获取当前下载的进度百分比
                        float percentage = downloadHandle.PercentComplete;
                        // 打印调试信息，显示当前下载进度
                        Logger.PrintDebug($"download pregress: {percentage}");
                        // 根据下载进度更新进度条的值
                        gProgress.value = percentage * 100;
                    }
                    // 释放下载操作的句柄
                    Addressables.Release(downloadHandle);
                }
            }

            // 打印调试信息，显示资源更新完成所花费的时间
            Logger.PrintDebug(string.Format("UpdateFinish use {0}ms", (DateTime.Now - start).Milliseconds));
            // 调用更新完成的方法
            UpdateFinish();

            // 释放更新目录操作的句柄
            Addressables.Release(locators);

            // 释放检查目录更新操作的句柄
            Addressables.Release(catalogs);
            Logger.PrintDebug($"=========================资源更新End===================================");
        }
        else
        {
            // 若没有需要更新的资源，打印调试信息
            Logger.PrintDebug("没有需要更新的资源");
        }
    }

    // 私有方法，用于启动游戏
    private void StartGame()
    {
        // 打印调试信息，表示开始启动游戏
        Logger.PrintDebug("LaunchPage StartGame()");
        // 设置文本框的文本内容，表示正在进入游戏
        gTextField.text = "正在进入游戏...";
        // 注释掉的代码，用于调用JsManager的启动游戏方法
        //   JsManager.Instance.StartGame();
    }

    // 方法，用于处理资源更新完成后的操作
    void UpdateFinish()
    {
        // 将进度条的值设置为100，表示更新完成
        gProgress.value = 100;
        // 设置文本框的文本内容，表示正在准备资源
        gTextField.text = "正在准备资源...";

        // 注释掉的代码，用于调用JsManager的重启方法
        //     JsManager.Instance.Restart();
    }

    // 当应用程序退出时调用该方法
    void OnApplicationQuit()
    {
        // 移除所有FairyGUI的包，清理缓存
        UIPackage.RemoveAllPackages();//退出应用时清理所有FairyGUI的缓存东西,重复开启关闭场景有时报错Bug
        // 清除Unity的缓存
        Caching.ClearCache();
        // 打印红色调试信息，表示应用程序退出
        Logger.PrintColor("red", "OnApplicationQuit()");
    }
}