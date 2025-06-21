using System;
using UnityEngine;

public class LoadingCtrl : MonoBehaviour
{
    private static LoadingCtrl m_instance = null;

    private FLoadingView m_loadingView;

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

    private static bool m_isInit = false;
    public static void Init(GameObject container)
    {
        if (m_isInit)
            return;
        
        container.AddComponent<LoadingCtrl>();
        m_instance.m_loadingView = new FLoadingView();
        m_instance.m_loadingView.Show();
        m_isShow = true;
        m_isInit = true;
    }

    void Awake()
    {
        m_instance = this;
    }

    void Update()
    {
        if (!m_isShow)
            return;
        float curTime = Time.realtimeSinceStartup;
        if (m_isNeedClose/* && curTime > m_closeTime*/)
        {
            Logger.PrintLog("现在关闭Loading界面");
            if (m_instance.m_finishCallback != null)
                m_instance.m_finishCallback();
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
        if (m_instance == null)
            return;
        Logger.PrintLog("打开Loading界面");
        m_instance.m_loadingView.Show(true);
        m_instance.m_isNeedClose = false;
        SetLoadContent("");
        SetProgress(0f, 0);
        m_isShow = true;
    }

    public static void Hide()
    {
        if (m_instance == null)
            return;
        Logger.PrintLog("关闭Loading界面");
        m_instance.m_loadingView.Show(false);
        m_instance.m_finishCallback = null;
        m_isShow = false;
    }

    public static void SetLoadContent(string content)
    {
        m_instance.m_loadingView.SetLoadContent(content);
    }

    public static void ShowProgressWindow()
    {
        m_instance.m_loadingView.ShowProgressWindow();
    }

    public static void HideProgressWindow()
    {
        m_instance.m_loadingView.HideProgressWindow();
    }

    public static void SetProgress(float value, int frame = 30)
    {
        if (m_instance == null)
            return;
        value = Mathf.Clamp(value, 0f, 100f);
        if (GetProgressValue() >= value || frame <= 0)
        {
            SetProgressValue(value);
            m_instance.m_progressRunValue = value;
            m_instance.m_progressRunFrame = 0;
            if (isAutoClose && value >= 1f)
                m_instance.m_isNeedClose = true;
        }
        else
        {
            m_instance.m_progressRunValue = value;
            m_instance.m_progressRunFrame = frame;
        }
    }

    private static float GetProgressValue()
    {
        return m_instance.m_loadingView.GetProgressValue();
    }

    private static void SetProgressValue(float value)
    {
        m_instance.m_loadingView.SetProgress(value);
    }

    public static void SetVersions(string str)
    {
        m_instance.m_loadingView.SetVersions(str);
    }
    
    public static void ShowAlert1(string msg, string btnName, Action onBtnfunc)
    {
        m_instance.m_loadingView.ShowAlert1(msg, btnName, onBtnfunc);
    }
    
    public static void ShowAlert2(string msg, string btnName1, Action onBtnfunc1, string btnName2, Action onBtnfunc2)
    {
        m_instance.m_loadingView.ShowAlert2(msg, btnName1, onBtnfunc1, btnName2, onBtnfunc2);
    }
}