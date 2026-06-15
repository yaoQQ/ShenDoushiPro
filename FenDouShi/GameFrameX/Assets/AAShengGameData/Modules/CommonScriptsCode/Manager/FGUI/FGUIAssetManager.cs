using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Dynamic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class FGUIAssetManager : Singleton<FGUIAssetManager>, IUIAssetManagerConfiguration
{
    public UIAssetManager UIAssetManager;

    public Dictionary<int, string> Locations;

    public IUIPackageHelper PackageHelper { get; set; }

    public IUIAssetLoader AssetLoader { get; set; }

    public bool UnloadUnusedUIPackageImmediately { get; set; } = false;
    private bool isInit = false;
    private UIPackageMapping uIPackageMapping;
    AddressablesAssetLoader addressableLoad;//开始时就全部加载byte包
    
    void LoadUIPackageAsyncHandler(string packageName, LoadUIPackageBytesCallback callback)
    {
        UniTask.Void(async () => { await LoadUIPackageAsyncInner(packageName, callback); });
    }

    void LoadTextureAsyncHandler(string packageName, string assetName, string extension,
        LoadTextureCallback callback)
    {
        UniTask.Void(async () => { await LoadTextureAsyncInner(packageName, assetName, callback); });
    }

    void LoadAudioClipAsyncHandler(string packageName, string assetName, string extension,
        LoadAudioClipCallback callback)
    {
        UniTask.Void(async () => { await LoadAudioClipAsyncInner(packageName, assetName, callback); });
    }

   
    public  async Task Init(Action callBack)
    {
        if (isInit)
        {
            return;
        }

        FGUIOperatHandletManager.Instance.Init();
        this.Locations = new Dictionary<int, string>();
        initAssetLoader();

        uIPackageMapping = await Addressables.LoadAssetAsync<UIPackageMapping>("UIPackageMapping").Task;
        Logger.PrintDebug($"=============处理的fairyGUI包资源列表总数【{uIPackageMapping.PackageNames.Length}】===================");
        for (int i = 0; i < uIPackageMapping.PackageNames.Length; i++)
        {
            Logger.PrintDebug($"PackageNames[{i}]={uIPackageMapping.PackageNames[i]}");
        }
        this.PackageHelper = uIPackageMapping;


        this.UIAssetManager = new UIAssetManager();
        this.UIAssetManager.Initialize(this);
        isInit = true;


        await InitAddressablesLoader(uIPackageMapping);//Addressables加载
        if (callBack != null)
        {
            callBack();
        }
        Logger.PrintColor("yellow", "==========FGUIAssetManager 初始化完成！====================");
    }
    private void initAssetLoader()
    {
        var assetLoader = new DelegateUIAssetLoader();
        assetLoader.LoadUIPackageBytesAsyncHandlerImpl = LoadUIPackageAsyncHandler;//LoadUIPackageBytesAsync
        assetLoader.LoadUIPackageBytesHandlerImpl = this.LoadUIPackageSyncInner;//LoadUIPackageBytes

        assetLoader.LoadUIPackageBytesHandlerImpl2 = this.LoadUIPackageSyncInnerAsync;//LoadUIPackageBytes

        assetLoader.LoadTextureAsyncHandlerImpl = LoadTextureAsyncHandler;//LoadTextureAsync
        assetLoader.UnloadTextureHandlerImpl = this.UnloadAssetInner;//UnloadTexture
        assetLoader.LoadAudioClipAsyncHandlerImpl = LoadAudioClipAsyncHandler;//LoadAudioClipAsync
        assetLoader.UnloadAudioClipHandlerImpl = this.UnloadAssetInner;//UnloadAudioClip

        this.AssetLoader = assetLoader;
    }


    public async UniTask InitAddressablesLoader(UIPackageMapping mapping)
    {
        if (addressableLoad == null)
        {
            addressableLoad = new AddressablesAssetLoader();
        }
        await addressableLoad.PreLoadAsync(mapping);
    }
    public async void EnterGameAddressablesLoader()
    {
        if (addressableLoad == null)
        {
            addressableLoad = new AddressablesAssetLoader();
        }
        await addressableLoad.PreEnterGameLoadAsync(uIPackageMapping);
    }
    public UniTask<GObject> CreateObjectAsync(string pkgName, string resName)
    {
        var utcs = new UniTaskCompletionSource<GObject>();

        UIPackage.CreateObjectAsync(pkgName, resName, result =>
        {
            bool succ = utcs.TrySetResult(result);
        });
        return utcs.Task;
    }

    /// <summary>
    /// 动态创建UI对象回调方法
    /// </summary>
    /// <param name="pkgName"></param>
    /// <param name="resName"></param>
    /// <param name="callback"></param>
    public void CreateObjectAsync(string pkgName, string resName, Action<GObject> callback)
    {
        UIPackage.CreateObjectAsync(pkgName, resName, result =>
        {
            callback(result);
        });
    }
    public async void CreateObjectAsync(string pkgName, string resName, IBaseView uiView, PreloadOrder order = null)
    {
        GComponent gComponent = (await CreateObjectAsync(pkgName, resName)).asCom;
        if (gComponent != null)
        {
            if (uiView != null)
                uiView.FinishLoad(gComponent);
            if (order != null)
                order.onPreloadStepEnd(resName);
        }
    }

    public GObject CreateObject(string pkgName, string resName)
    {
        return UIPackage.CreateObject(pkgName, resName);
    }

    public void UnloadUnusedUIPackages()
    {
        UIPackage.RemoveUnusedPackages();
    }

    public void Destroy()
    {
        isInit = false;
        this.UIAssetManager.Dispose();
        this.AssetLoader.UnloadAllPackage();
        addressableLoad.UnloadAllPackage();
        addressableLoad = null;
        this.UIAssetManager = null;
        this.AssetLoader = null;
        this.Locations = null;
        PackageHelper = null;

    }

    private void LoadUIPackageSyncInner(string packageName, out byte[] bytes, out string assetNamePrefix)
    {

        string location = Utility.Platform.ConnectStrs(packageName, "_fui");

        Debug.Log($"<color='red'>====LoadUIPackageSyncInner 同步加载依赖资源packageName={packageName} location={location}=====</color>");
        // var textAsset = FGUIOperatHandletManager.Instance.LoadAsset<TextAsset>(packageName, location);
        bytes = addressableLoad.GetPackageBytes(packageName);
        if (bytes == null)
        {
            Logger.PrintError($"加载的packageName={packageName} 数据为空！");
        }
        assetNamePrefix = packageName;
    }



    private async UniTask<(byte[] bytes, string assetNamePrefix)> LoadUIPackageSyncInnerAsync(string packageName)
    {
        string location = Utility.Platform.ConnectStrs(packageName, "_fui");

        Logger.PrintColor("red", $"====LoadUIPackageSyncInner 异步加载资源 packageName={packageName} location={location}=====");
        var textAsset = await FGUIOperatHandletManager.Instance.LoadAssetAsync<TextAsset>(packageName, location);
        byte[] bytes = textAsset.bytes;
        return (bytes, packageName);
    }

    private async UniTask LoadUIPackageAsyncInner(string packageName, LoadUIPackageBytesCallback callback)
    {

        string location = Utility.Platform.ConnectStrs(packageName, "_fui");
        //byte[] descData = await ResManager.Instance.LoadRawFileDataAsync(location);
        Logger.PrintColor("white", $"开始加载 packageName={location}");
        var op = await FGUIOperatHandletManager.Instance.LoadAssetAsync<TextAsset>(packageName, location);
        byte[] descData = op.bytes;
        Logger.PrintColor("yellow", $"加载完成 packageName={location}");
        callback(descData, packageName);
    }

    private async UniTask LoadTextureAsyncInner(string packageName, string assetName, LoadTextureCallback callback)
    {
        Logger.PrintColor("red", $"@@@@@@开始加载贴图 packageName={packageName} assetName={assetName}");
        Texture res = await FGUIOperatHandletManager.Instance.LoadAssetAsync<Texture>(packageName, assetName);

        if (res != null)
        {

            this.Locations[res.GetInstanceID()] = Utility.Text.ConnectStrs(packageName, ":", assetName);
            Logger.PrintColor("yellow", $"加载完成贴图 packageName={packageName} assetName={assetName}");
        }

        callback(res);
    }

    private async UniTask LoadAudioClipAsyncInner(string packageName, string assetName, LoadAudioClipCallback callback)
    {
        Logger.PrintColor("red", $"LoadAudioClipAsyncInner packageName={packageName} assetName={assetName}");
        AudioClip res = await FGUIOperatHandletManager.Instance.LoadAssetAsync<AudioClip>(packageName, assetName);

        if (res != null)
            this.Locations[res.GetInstanceID()] = Utility.Text.ConnectStrs(packageName, ":", assetName); ;

        callback(res);
    }
    private void UnloadAssetInner(string packageName, UnityEngine.Object obj)
    {
        if (obj == null)
            return;

        int instanceId = obj.GetInstanceID();
        if (!this.Locations.TryGetValue(instanceId, out string location))
            return;
        string[] strs = location.Split(':');
        string texturePackageName = strs[0];
        string assetName = strs[1];
        this.Locations.Remove(instanceId);
        FGUIOperatHandletManager.Instance.ReleaseAsset(texturePackageName, assetName);
    }
}
