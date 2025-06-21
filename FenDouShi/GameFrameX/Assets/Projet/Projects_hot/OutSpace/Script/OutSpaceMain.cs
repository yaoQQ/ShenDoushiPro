using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class OutSpaceMain : MonoBehaviour
{
    // Start is called before the first frame update
    public GameHardLevelEnum gameHardLevel = GameHardLevelEnum.Easy;
    public int testNum2 = 2;
    void Awake()
    {
        Application.targetFrameRate = 60;
        Loger.PrintColor("red","Screen.width=" + Screen.width + " Screen.height=" + Screen.height);

        Logger.PrintColor("blue", "========OutSpaceMain Awake()==========");
        AddEventSystem();
    }
    private void Start()
    {

        string cataLogPath = Application.dataPath + $"/{Addressables.RuntimePath}/catalog.json";
       // StartCoroutine(AddressableLoadManager.Instance.StartLoadCataLogsPreload(cataLogPath));
        OutSpaceAudioManager.Instance.Init();

        // «∑Òø™∆Ù“Ù¿÷
#if SHOW_MUSIC_DATA
       // GetAudioDataManager.Instance.ToInit();
#endif
        OutSpaceCameraManager.Instance.Init();
        OutSpacePlayerInfoManager.Instance.Init();
        OutSpaceGameManager.Instance.Init();
        OutSpaceGameManager.Instance.GameHardLevel = gameHardLevel;

        GunManager.Instance.Init();
        MonsterManager.Instance.Init();
        BoidsMotionManager.Instance.InitFun();

#if TEST_SCENE
        this.gameObject.AddComponent<MainThread>();
        TestScene();
#endif
        Logger.PrintColor("blue", "========OutSpaceMain Start()==========");
        List<GameGunData> gameDaotaList = new List<GameGunData>();
        Logger.PrintColor("blue", "gameDaotaList="+ gameDaotaList);
        //  TestScene();
        UIManager.Instance.addUICamera(CameraManager.Instance.MainCamera);
      

       // OutSpaceGameManager.Instance.ShowLevel();
    }

   
    private void OnGUI()
    {
        //if (GUI.Button(new Rect(0, 0, 100, 50), "addExp"))
        //{
        //    // OutSpacePlayerInfoManager.Instance.AddExp(5);
        //    // BoidsMotionManager.Instance.UpdateCurrMotion();
        //    // currEnemyWave.gameObject.SetActive(true);
        //    OutSpacePlayerInfoManager.Instance.AddExp(5);
        //}
        //if (GUI.Button(new Rect(100, 0, 100, 50), "test resume"))
        //{
        //    OutSpaceGameManager.Instance.ResumeGame();
        //}
        //if (GUI.Button(new Rect(100, 50, 100, 50), "CircleMotionType2"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("CircleMotionType2", MotionType.CircleMotion);
        //}
        //if (GUI.Button(new Rect(100, 100, 100, 50), "CircleMotionType3"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("CircleMotionType3", MotionType.CircleMotion);
        //}
        //if (GUI.Button(new Rect(100, 150, 100, 50), "CircleMotionType4"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("CircleMotionType4", MotionType.CircleMotion);
        //}
        //if (GUI.Button(new Rect(100, 200, 100, 50), "CircleMotionType5"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("CircleMotionType5", MotionType.CircleMotion);
        //}
        //if (GUI.Button(new Rect(100, 250, 100, 50), "CircleMotionType6"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("CircleMotionType6", MotionType.CircleMotion);
        //}
        //if (GUI.Button(new Rect(100, 300, 100, 50), "CircleMotionType7"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("CircleMotionType7", MotionType.CircleMotion);
        //}
        //if (GUI.Button(new Rect(100, 350, 100, 50), "CircleMotionType8"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("CircleMotionType8", MotionType.CircleMotion);
        //}
        //if (GUI.Button(new Rect(100, 400, 100, 50), "CircleMotionType9"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("CircleMotionType9", MotionType.CircleMotion);
        //}

        //if (GUI.Button(new Rect(0, 100, 50, 50), "PowMotionType2D1"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("PowMotionType2D1", MotionType.Pow2);
        //}
        //if (GUI.Button(new Rect(0, 150, 50, 50), "PowMotionType2D2"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("PowMotionType2D2", MotionType.Pow2);
        //}
        //if (GUI.Button(new Rect(0, 200, 50, 50), "PowMotionType2D3"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("PowMotionType2D3", MotionType.Pow2);
        //}
        //if (GUI.Button(new Rect(0, 250, 50, 50), "PowMotionType2D4"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("PowMotionType2D4", MotionType.Pow2);
        //}
        //if (GUI.Button(new Rect(0, 300, 50, 50), "PowMotionType2D5"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("PowMotionType2D5", MotionType.Pow2);
        //}
        //if (GUI.Button(new Rect(0, 350, 50, 50), "PowMotionType2D6"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("PowMotionType2D6", MotionType.Pow2);
        //}
        //if (GUI.Button(new Rect(0, 400, 50, 50), "PowMotionType2D7"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("PowMotionType2D7", MotionType.Pow2);
        //}
        //if (GUI.Button(new Rect(0, 450, 50, 50), "PowMotionType2D8"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("PowMotionType2D8", MotionType.Pow2);
        //}
        //if (GUI.Button(new Rect(0, 500, 50, 50), "PowMotionType2D9"))
        //{
        //    BoidsMotionManager.Instance.ShowMotionType("PowMotionType2D9", MotionType.Pow2);
        //}
    }
    private void TestScene()
    {

#if TEST_SCENE
        OutSpacePackage packa = new OutSpacePackage();
        packa.init(() =>
        {
            OutSpacePreload pre = new OutSpacePreload(packa);
            packa.PreloadOrder = pre;
            PreloadManager.Instance.ExecuteOrder(pre);

        });
#endif

        Logger.PrintDebug("TestScene()");
        CommonView.showTopTips("test");
    }
    // Update is called once per frame
    void Update()
    {
        OutSpaceLevel.Instance.ToUpdate();

#if SHOW_MUSIC_DATA

       //GetAudioDataManager.Instance.ToUpdate();
#endif
    }
    private void LateUpdate()
    {
        //GetAudioDataManager.Instance.ToUpdate();
    }
    private void OnDestroy()
    {
        // GetAudioDataManager.Instance.ToDestory();
    }
    public void showMainMenue()
    {
        UIViewManager.Instance.Open(UIViewEnum.OutSpaceMainMenuView);
    }
    public void showPlayerInfo()
    {
        UIViewManager.Instance.Open(UIViewEnum.OutSpacePlayerInfoView);
    }
    public void showGunInfo()
    {
        UIViewManager.Instance.Open(UIViewEnum.OutSpaceGunTopInfoPanel);
    }
    private void AddEventSystem() {
      var eventSystem=  GameObject.FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
        if (eventSystem == null) {
            Logger.PrintColor("yellow", "eventSystem == null");
            GameObject evetObj = new GameObject("EventSystem");
             eventSystem= evetObj.AddComponent<UnityEngine.EventSystems.EventSystem>();
            evetObj.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
        }
    }
    private void OnApplicationQuit()
    {
      
        OutSpaceResourceManager.Instance.clearAll();
        Caching.ClearCache();
        Logger.PrintColor("red","OnApplicationQuit()");
    }
}
