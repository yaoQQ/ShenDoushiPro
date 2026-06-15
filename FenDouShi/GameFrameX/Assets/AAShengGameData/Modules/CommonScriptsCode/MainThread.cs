using DataTableFrame;
using FairyGUI;
using FairyGUI.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class MainThread : MonoBehaviour
{

    static MainThread _instance;

    public static MainThread Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake() {
        _instance = this;
        Logger.PrintColor("blue", "MainThread Awake()");
        Init();
        GameObject.DontDestroyOnLoad(this.gameObject);
       
    }
    void Start()
    {
        //游戏初始，加载公共和登入热更程序集
        PreloadManager.Instance.PreLoadPackage(PackageEnum.CommonScriptCode);
        PreloadManager.Instance.PreLoadPackage(PackageEnum.LoginPackage);

    }
    void Init() {
        Logger.PrintColor("yellow", "MainThread Init()");
        PreloadManager.Instance.Init();
        UnityWebSocketManager.Instance.Init();
        FUIBinder.BindAll();
        ClassPoolManger.Instance.Init();
        RedPointManager.Instance.Init();
        GameAudioManager.Instance.InitializeAudio();
        ModuleManager.Instance.Init();
      //  LoaderManager.Instance.Innit(dontDestory);
        GlobalTimeManager.Instance.Init();
        ControlManager.Instance.Init();
        //InputManager.Instance.Init();
        //this.gameObject.AddComponent<DataTableManager>();
    }

    void Update() {
        UnityWebSocketManager.Instance.OnProcess();
      //  LoaderManager.Instance.Update();
         GlobalTimeManager.Instance.Execute();
      //  InputManager.Instance.Execute();
      //  AudioManager.Instance.AdjustVolume();
    }

    void OnApplicationQuit() {
        StopAllCoroutines();
        UnityWebSocketManager.Instance.Dispose();
        GameAudioManager.Instance.Detroy();

        UIPackage.RemoveAllPackages();//退出时清理所有FairyGUI的缓存东西,重复开启关闭场景有时报错问题

        ResReleaseManager.ReleaseAllPackage();
        PreloadManager.Instance.Destroy();
        FGUIOperatHandletManager.Instance.ReleaseAllAssets();
        FGUIAssetManager.Instance.Destroy();
        ModuleManager.Instance.Dispose();
        ConfigMgr.Instance.Dispose();
        UIViewManager.Instance.Dispose();
        RedPointManager.Instance.Dispose();
        SceneManager.Instance.Clear();
        ControlManager.Instance.Clear();
        Caching.ClearCache();
        Logger.PrintColor("red", "MainThread CleanAllCache()");

        Logger.PrintColor("red", "MainThread OnApplicationQuit()");
    }
    public static void CleanGameCache()
    { 
      

        ResReleaseManager.ReleaseAllPackageExceptViewPackage();

#if !UNITY_EDITOR
        Addressables.CleanBundleCache();
#endif
        Caching.ClearCache();
        Logger.PrintColor("red", "MainThread CleanGameCache()");
    }
}