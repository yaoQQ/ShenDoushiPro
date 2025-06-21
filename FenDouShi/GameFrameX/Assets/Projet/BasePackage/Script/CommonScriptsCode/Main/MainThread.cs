using FairyGUI;
using UnityEngine;

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
        Logger.PrintColor("blue", "Res MainThread Awake()");
        CommonUtils.Init(this,false, false, false);
        Init();

    }

    GameObject dontDestory;
    void Init() {
        //LoadingBarController.isAutoClose = true;
        //LoadingBarController.SetProgress(0.9f, 60);
        Logger.PrintColor("yellow", "8.17 Res MainThread Init()");
        AddDstObj();
        
     //   AudioManager.Instance.Init(dontDestory);
        GameAudioManager.Instance.InitializeAudio();
        ModuleManager.Instance.Init();
      //  LoaderManager.Instance.Innit(dontDestory);
        GlobalTimeManager.Instance.Init();
        UIManager.Instance.Init(dontDestory);

        //InputManager.Instance.Init();

        //test
        //MyTest.CXTest.Instance.init();

        //初始化C#接入的游戏
        Logger.PrintLog("初始化C#接入的游戏");
     //   GameProcessManager.InitGame(EnumGameID.Fishing3D);
        Logger.PrintLog("初始化C#接入的游戏完成");
    }

    private void SendConnectSocket() {
        //test@@@
        //ConnectSocketNotice notice = new ConnectSocketNotice() {
        //    ip = LuaManager.Instance.GetGlobalValue<string>("GameConfig_socketIP"),
        //    port = LuaManager.Instance.GetGlobalValue<int>("GameConfig_socketPort")
        //};
        //Debug.Log("SendConnectSocket");
        //test@@@
        //  NoticeManager.Instance.Dispatch(notice);
    }

    private void AddDstObj() {
        dontDestory = new GameObject("DST");
        dontDestory.tag = "DST";
        DontDestroyOnLoad(dontDestory);
    }


    float deltaTime_ms = 0f;
    void Update() {
        //test@@@
        NetworkManager.Instance.OnProcess();
      //  LoaderManager.Instance.Update();
        deltaTime_ms = GlobalTimeManager.Instance.Execute();
      //  //Debug.Log("deltaTime_ms    " + deltaTime_ms);
      //  InputManager.Instance.Execute();
      //  AudioManager.Instance.AdjustVolume();
    }

    public void LateUpdate() {
    }

    void OnGUI() {
    }

    void OnDestroy() {

    }

    void OnApplicationQuit() {
        //  NetworkManager.Instance.OnApplicationQuit();
        StopAllCoroutines();
        GameAudioManager.Instance.Detroy();
        UIManager.Instance.Destroy();
        LoadingBarController.Instance.OnDestroy();

        PreloadManager.Instance.Destroy();
        UIPackage.RemoveAllPackages();//退出时清理所有FairyGUI的缓存东西,重复开启关闭场景有时报错问题
        Caching.ClearCache();
        Logger.PrintColor("red","MainThread OnApplicationQuit()");
      
    }
}