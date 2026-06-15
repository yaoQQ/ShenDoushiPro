using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class LoadingBarController : MonoBehaviour 
{
    public static LoadingBarController Instance = null;  

    private LoadingView m_loadingView;
    public Texture2D loadingTexture;

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

    /// <summary>是否显示</summary>
    private static bool m_isShow = false;

    public static bool isInit = false;

    void Awake()
    {
        Instance = this;
        Logger.PrintColor("white", "LoadingBarController Awake()");
        //  StartCoroutine(CheckUpdate());
    }

    public void InitLoadingView(Action callBack)
    {
        //Logger.PrintColor("white", "@@@@@@@@LoadingBarController InitLoadingView()");
        Instance.m_loadingView = new LoadingView(callBack);
        //Logger.PrintColor("white", "@@@@@@@@LoadingBarController  Instance.m_loadingView" + Instance.m_loadingView);

    }
    void Start()
    {
        // m_instance.m_loadingView = new LoadingView(LoadCallBakc);
    }
    public static void ShowEnterBtn()
    {
        // Instance.m_loadingView.ShowEnterBtn();
    }
    void Update()
    {
        if (!m_isShow)
            return;
        float curTime = Time.realtimeSinceStartup;
        if (m_isNeedClose/* && curTime > m_closeTime*/)
        {
            //  Logger.PrintLog("现在关闭Loading界面");
            if (Instance.m_finishCallback != null)
                Instance.m_finishCallback();
            Hide();
            return;
        }
        if (m_progressRunFrame > 0 && m_progressRunValue > GetProgressValue())
        {
            SetProgressValue(GetProgressValue() + ((m_progressRunValue - GetProgressValue()) / ((float)m_progressRunFrame)));
            m_progressRunFrame--;
        }
        if (isAutoClose && !m_isNeedClose && GetProgressValue() >= 1f)
        {
              Logger.PrintLog("准备关闭Loading界面");
            m_isNeedClose = true;
        }
    }

    public static void Show()
    {
        if (Instance == null)
            return;
        Logger.PrintLog("打开Loading界面");
        Instance.m_loadingView.Show();
        Instance.m_isNeedClose = false;
        SetLoadContent("");
        SetProgress(0f, 0);
        m_isShow = true;
    }

    public static void Hide()
    {
        if (Instance == null)
            return;
        // Logger.PrintLog("关闭Loading界面");
        Instance.m_loadingView.Hide();
        Instance.m_finishCallback = null;
        m_isShow = false;
    }

    public static void SetLoadContent(string content)
    {
        Instance.m_loadingView.SetLoadContent(content);
    }

    public static void ShowProgressWindow()
    {
        Instance.m_loadingView.ShowProgressWindow();
    }

    public static void HideProgressWindow()
    {
        Instance.m_loadingView.HideProgressWindow();
    }

    public static void SetProgress(float percent, int frame = 30)
    {
        if (Instance == null)
            return;
        percent = Mathf.Clamp(percent, 0f, 100f);
        //Logger.PrintColor("yellow", "LoadingBarController SetProgress() percent=" + percent);
        //Logger.PrintColor("yellow", "LoadingBarController SetProgress() GetProgressValue()=" + GetProgressValue());
        //Logger.PrintColor("yellow", "LoadingBarController SetProgress() frame()=" + frame);
        if (GetProgressValue() >= percent || frame <= 0)
        {
            SetProgressValue(percent); 
            Instance.m_progressRunValue = percent;
            Instance.m_progressRunFrame = 0;
            if (isAutoClose && percent >= 1f)
                Instance.m_isNeedClose = true;
         //   Logger.PrintColor("yellow", "SetProgressValue percent=" + percent);
        }
        else
        {
            Instance.m_progressRunValue = percent;
            Instance.m_progressRunFrame = frame;
            //Logger.PrintColor("yellow", "LoadingBarController SetProgress() Instance.m_progressRunValue" + Instance.m_progressRunValue);
            //Logger.PrintColor("yellow", "LoadingBarController SetProgress() Instance.m_progressRunFrame" + Instance.m_progressRunFrame);
        }
    }

    private static float GetProgressValue()
    {
        return Instance.m_loadingView.GetProgressValue();
    }

    private static void SetProgressValue(float value)
    {
        Instance.m_loadingView.SetProgress(value);
    }

    public static void SetVersions(string str)
    {
        Instance.m_loadingView.SetVersions(str);
    }

    public static void ShowAlert1(string msg, string btnName, Action onBtnfunc)
    {
        Instance.m_loadingView.ShowAlert1(msg, btnName, onBtnfunc);
    }

    public static void ShowAlert2(string msg, string btnName1, Action onBtnfunc1, string btnName2, Action onBtnfunc2)
    {
        Instance.m_loadingView.ShowAlert2(msg, btnName1, onBtnfunc1, btnName2, onBtnfunc2);
    }
    private void OnDestroy()
    {
        isInit = false;
        m_isShow = false;
        Instance = null;
    }

    public static async Task WaitForResponse()
    {
        await Instance.m_loadingView.WaitForResponse();
    }
}
