using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class LoadingBarController :MonoBehaviour
{
    //test
    private LoadingBarView m_loadingBar;

    private LoadingView loadingView;

    /// <summary>是否在进度条满时自动关闭</summary>
    public static bool isAutoClose = true;

    /// <summary>结束回调</summary>
    private Action m_finishCallback = null;
    /// <summary>目标进度值</summary>
    private float m_progressRunValue = 0f;
    /// <summary>达到目标进度值需要的帧数</summary>
    private int m_progressRunFrame = 0;
    /// <summary>是否需要关闭</summary>
    private bool m_isNeedClose = false;
    /// <summary>需要关闭的时间</summary>
    //private float m_closeTime = 0f;
    /// <summary>是否在播动画</summary>
    private bool m_isShowSpine = false;
    private static int m_spineIndex = 0;


    private static bool m_isInit = false;
    private AsyncOperationHandle loadHandle=new AsyncOperationHandle();
    private Action enterFunCallBack;

    public static LoadingBarController Instance;
    private void Awake() {
        Instance = this;
        Logger.PrintColor("red", "@@@@@@@@@@@@InitLoadingView Awake()" + m_isInit);
        DontDestroyOnLoad(this.gameObject);
    }


    public static void InitLoadingView(Transform target, GameObject container)
    {
        Logger.PrintColor("red", "@@@@@@@@@@@@InitLoadingView m_isInit="+ m_isInit);
        if (m_isInit)
            return;

        GameObject barGOInstantiate = GameObject.Instantiate(target.gameObject);
        barGOInstantiate.AddComponent<LoadingBarController>();
        Instance.loadingView = barGOInstantiate.AddComponent<LoadingView>();
        Instance.loadingView.Init(barGOInstantiate.transform, container);
        m_isInit = true;
        LoadingBarController.SetLoadContent("正在读取版本信息");
        LoadingBarController.setLoadValue(0);
     
    }

    public static void AddressableInit(Transform target, GameObject container)
    {

        GameObject barGOInstantiate = GameObject.Instantiate(target.gameObject);
        barGOInstantiate.AddComponent<LoadingBarController>();

        //test@@@
        //Instance.m_loadingBar = new LoadingBarView();
        //Instance.m_loadingBar.Init(barGOInstantiate, container);
        m_isInit = true;
        LoadingBarController.SetLoadContent("正在读取版本信息");
        LoadingBarController.SetProgress(0.1f, 30);
    }

    public static void SetContainer(GameObject container)
    {
        //test@@@
        //Instance.m_loadingBar.SetContainer(container);
    }



    void Update()
    {
        if (!gameObject.activeInHierarchy)
            return;
        if (loadHandle.IsValid())
        {
            if (!loadHandle.IsDone)
            {
               // Debug.LogFormat("loadHandle.PercentComplete=" + loadHandle.PercentComplete);
                SetLoadPercent(loadHandle.PercentComplete*100+"%");
            }
        }
      
        //float curTime = Time.realtimeSinceStartup;
        //if (m_isNeedClose/* && curTime > m_closeTime*/)
        //{
        //    Logger.PrintLog("现在关闭Loading界面");
        //    if (Instance.m_finishCallback != null)
        //        Instance.m_finishCallback();
        //    Hide();
        //    return;
        //}
        //if (m_progressRunFrame > 0 && m_progressRunValue > GetProgressValue())
        //{
        //    SetProgressValue(GetProgressValue() + ((m_progressRunValue - GetProgressValue()) / ((float)m_progressRunFrame)));
        //    m_progressRunFrame--;
        //}
        //if (isAutoClose && !m_isNeedClose && !m_isShowSpine && GetProgressValue() >= 1f)
        //{
        //    Logger.PrintLog("准备关闭Loading界面");
        //    if (m_spineIndex == 0)
        //    {
        //        m_isShowSpine = true;
        //        Instance.m_loadingBar.ShowSpine(() =>
        //        {
        //            Instance.m_isShowSpine = false;
        //            m_isNeedClose = true;
        //            m_spineIndex = 1;
        //            //m_closeTime = curTime + 0.3f;
        //        });
        //    }
        //    else
        //        m_isNeedClose = true;
        //}
        ////Instance.m_loadingBar.Update();
    }

    public static void Show()
    {
        if (Instance == null)
            return;
        Logger.PrintLog("打开Loading界面 Instance="+ Instance+ " Instance.loadingView="+ Instance.loadingView);
        Instance.loadingView.Show(true);
        SetLoadContent("");
        Instance.loadingView.SetLoadPrecent("");
        setLoadValue(0);
        Instance.enterFunCallBack = null;
       // NoticeManager.Instance.Dispatch(NoticeType.Loading_Bar_Show);
    }

    public static void Hide()
    {
        if (Instance == null)
            return;
        Logger.PrintLog("关闭Loading界面");
        if(Instance.loadingView)
        Instance.loadingView.Show(false);
        
    }
    public static void showEnterBtn()
    {
        Debug.Log("@@@@@@@@@@@@showEnterBtn() Instance.loadingView=" + Instance.loadingView);
        if (Instance.loadingView)
            Instance.loadingView.PlayButton.gameObject.SetActive(true);
    }
    public static void setLoadHandle(AsyncOperationHandle handle)
    {
        Instance.loadHandle = handle;
    }
    public static void SetLoadContent(string content)
    {
        //Instance.m_loadingBar.SetLoadContent(content);
        if(Instance.loadingView)
        Instance.loadingView.SetLoadContent(content);
    }
    public static void SetLoadPercent(string content)
    {
        if (Instance.loadingView)
            Instance.loadingView.SetLoadPrecent(content);
    }
    public static void SetLoadEnterFun(Action action)
    {
        Instance.enterFunCallBack = action;
    }
    public static void enterFun()
    {
        if (Instance.enterFunCallBack != null)
        {
            Instance.enterFunCallBack();
        }
    }
    public static void setLoadValue(float percent)
    {
        //Instance.m_loadingBar.SetLoadContent(content);
        if (Instance.loadingView)
        Instance.loadingView.setLoadValue(percent);
    }
    public static void ShowProgressWindow()
    {
        
        Instance.m_loadingBar.ShowProgressWindow();
    }

    public static void HideProgressWindow()
    {
       
        Instance.m_loadingBar.HideProgressWindow();
    }

    public static void SetProgress(float value, int frame = 30)
    {
        if (Instance == null)
            return;
        value = Mathf.Clamp(value, 0f, 100f);
        if (GetProgressValue() >= value || frame <= 0)
        {
            SetProgressValue(value);
            Instance.m_progressRunValue = value;
            Instance.m_progressRunFrame = 0;
            if (isAutoClose && !Instance.m_isShowSpine && value >= 1f)
            {
                if (m_spineIndex == 0)
                {
                    Instance.m_isShowSpine = true;

                    //test@@@
                    //Instance.m_loadingBar.ShowSpine(() =>
                    //{
                    //    Instance.m_isShowSpine = false;
                    //    Instance.m_isNeedClose = true;
                    //    //Instance.m_closeTime = Time.realtimeSinceStartup + 0.3f;
                    //});
                }
                else
                    Instance.m_isNeedClose = true;
            }
        }
        else
        {
            Instance.m_progressRunValue = value;
            Instance.m_progressRunFrame = frame;
        }
    }

    private static float GetProgressValue()
    {
       
        return Instance.m_loadingBar.GetProgressValue();
    }

    private static void SetProgressValue(float value)
    {
        Instance.m_loadingBar.SetProgress(value);
    }

    public static void SetVersions(string str)
    {
        Instance.m_loadingBar.SetVersions(str);
    }

   // [BlackList]
    public static void ShowNotice(string msg, string btnName, Action onBtnfunc)
    {
        Instance.m_loadingBar.ShowNotice(msg, btnName, onBtnfunc);
    }

   // [BlackList]
    public static void ShowNotice2(string msg, string btnName1, Action onBtnfunc1, string btnName2, Action onBtnfunc2)
    {
        Instance.m_loadingBar.ShowNotice2(msg, btnName1, onBtnfunc1, btnName2, onBtnfunc2);
    }
    public  void OnDestroy() {
        LoadingBarController.Instance.m_loadingBar = null;
        LoadingBarController.Instance.loadingView = null;
        m_isInit = false;
        Logger.PrintColor("red", "LoadingBarController OnDestroy()");
    }
}
