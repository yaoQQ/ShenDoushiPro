using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using FairyGUI;
using System.Reflection;

public class UIViewManager : Singleton<UIViewManager>
{
    private class UIView
    {
        public PackageEnum packageName;
        public BaseView uiView;
    }
    //视图列表旧的《视图枚举, 视图对象》
    private Dictionary<UIViewEnum, UIView> m_UIViewDict = new Dictionary<UIViewEnum, UIView>();
    //视图列表  <视图名称， 视图对象>
    private Dictionary<string, UIView> mUIViewDict = new Dictionary<string, UIView>();
    //视图映射表  <视图名称， 视图枚举>
    private Dictionary<Type, FGUIViewAttribute> mViewAttributeDict = new Dictionary<Type, FGUIViewAttribute>();

    //旧的
    public BaseView GetView(UIViewEnum viewEnum)
    {
        m_UIViewDict.TryGetValue(viewEnum, out var uiView);
        return uiView?.uiView;
    }

    /// <summary>
    /// 新的获取视图通过名称获取
    /// </summary>
    /// <param name="viewName"></param>
    /// <returns></returns>
    public BaseView GetView(string viewName)
    {
        if (mUIViewDict.TryGetValue(viewName, out var uiView))
        {
            return uiView?.uiView;
        }
        return null;
    }

    /// <summary>
    /// 注册界面
    /// </summary>
    public List<BaseView> RegisterViewByAttribute(PackageEnum packageName)
    {
        Logger.PrintGreen("UIVIewManager RegisterViewByAttribute() packageName=" + packageName);
        List<BaseView> viewList = new List<BaseView>();

        foreach (Type type in AttributeBindManger.Instance.GetTypes(typeof(FGUIViewAttribute)))
        {
            FGUIViewAttribute attr = type.GetCustomAttribute<FGUIViewAttribute>();
            if (attr == null) continue;
            // 获取 type 所在的程序集
            Assembly typeAssembly = type.Assembly;
            PackageEnum AssmblyPackageName = AssemblyHelper.GetPackageEnum(type, typeAssembly.GetName().Name);
            // 包名检查卫语句,测试模式可以加载全部界面
            if (AssmblyPackageName != packageName&&!UIConfig.isShowAllWinowDebug)
            {
               // Logger.PrintColor("White", $"{type} AssmblyPackageName={AssmblyPackageName} packageName={packageName}");
                continue;
            }
            // 视图创建与初始化
            var baseView = CreateBaseView(attr);
            if (baseView == null) continue;

            Logger.PrintColor("White", $"{type} AssmblyPackageName={AssmblyPackageName} packageName ={packageName}");
            UIView uiView = new UIView
            {
                packageName = AssmblyPackageName,
                uiView = baseView
            };
            // 注册逻辑分离
            RegisterUIView(attr, uiView);

            viewList.Add(uiView.uiView);
            uiView.uiView.SetHome(FGUILayoutManager.Instance.GetGRootLayer(UIViewLayerType.windowContent));

        }

        return viewList;
    }
    // 分离视图创建逻辑
    private BaseView CreateBaseView(FGUIViewAttribute attr)
    {
        try
        {
            var view = Activator.CreateInstance(attr.viewType) as BaseView;
            if (view == null)
            {
                Logger.PrintError($"生成界面失败：{attr.viewType}");
            }
            return view;
        }
        catch (Exception ex)
        {
            Logger.PrintError($"创建视图异常：{ex.Message}");
            return null;
        }
    }
    // 分离注册逻辑
    private void RegisterUIView(FGUIViewAttribute attr, UIView uiView)
    {
        // 旧版枚举注册
        if (attr.uiViewEnum != UIViewEnum.None)
        {
            m_UIViewDict.TryAdd(attr.uiViewEnum, uiView);
            Logger.PrintGreen($"{attr.uiViewEnum} view={uiView}注册成功");
        }

        // 视图名注册
        if (string.IsNullOrEmpty(attr.viewName))
        {
            Logger.PrintColor("red",$"viewName不能为空：{attr.viewType}");
            return;
        }

        mUIViewDict.TryAdd(attr.viewName, uiView);
        mViewAttributeDict.TryAdd(attr.viewType, attr);
    }

    /// <summary>
    /// 注册界面
    /// </summary>
    [Obsolete]
    public void RegisterView(PackageEnum packageName, BaseView view)
    {
        Logger.PrintDebug("RegisterView() packageName=" + packageName + " view=" + view.getViewEnum().ToString());
        UIViewEnum viewEnum = view.getViewEnum();
        if (m_UIViewDict.ContainsKey(viewEnum))
        {
            Logger.PrintColor("red","m_UIViewDict.ContainsKey() viewEnum=" + viewEnum);
            return;
        }

        UIView uiView = new UIView();
        uiView.packageName = packageName;
        uiView.uiView = view;
        m_UIViewDict.Add(viewEnum, uiView);
        uiView.uiView.SetHome(FGUILayoutManager.Instance.GetGRootLayer(UIViewLayerType.windowContent));

    }
    /// <summary>
    /// 打开界面
    /// </summary>
    public void Show(UIViewEnum viewEnum, System.Object msg = null, Action openCallback = null, bool isPush = true)
    {
        Logger.PrintDebug("尝试打开界面：" + viewEnum);
        if (!m_UIViewDict.TryGetValue(viewEnum, out var uiView))
        {
            Logger.PrintError($"{viewEnum} 窗口没有注册");
            return;
        }

        //打开界面时，如果是栈界面，就尝试入栈
        if (isPush && uiView.uiView.getIsStackView())
            UIViewStack.Push((int)viewEnum, msg, openCallback);

        if (!uiView.uiView.getIsLoaded())
        {
            if (!uiView.uiView.getIsLoading())
            {
                Logger.PrintColor("blue", viewEnum.ToString() + "startLoad（） 开启加载");
                uiView.uiView.StartLoad();
            }
        }
        uiView.uiView.setOpening(true);

        MainThread.Instance.StartCoroutine(AsynOpen(uiView.uiView, msg, openCallback));
    }

    /// <summary>
    /// 打开界面新
    /// </summary>
    public void Show(string viewName, System.Object msg = null, Action openCallback = null, bool isPush = true)
    {
        Logger.PrintLog("尝试打开界面" + viewName);
        if (!mUIViewDict.TryGetValue(viewName, out var uiView))
        {
            Logger.PrintError($"{viewName} 窗口没有注册");
            return;
        }

        //打开界面时，如果是栈界面，就尝试入栈
        // if (isPush && uiView.uiView.getIsStackView())
        //     UIViewStack.Push(viewName, msg, openCallback);

        if (!uiView.uiView.getIsLoaded())
        {
            if (!uiView.uiView.getIsLoading())
            {
                Logger.PrintColor("blue", viewName + "startLoad（） 开启加载");
                uiView.uiView.StartLoad();
            }
        }
        uiView.uiView.setOpening(true);
        MainThread.Instance.StartCoroutine(AsynOpen(uiView.uiView, msg, openCallback));
    }

    public void Show<T>(System.Object msg = null, Action openCallback = null, bool isPush = true)
    {
        if (mViewAttributeDict.TryGetValue(typeof(T), out var attr))
        {
            Show(attr.viewName, msg, openCallback, isPush);
        }
        else
        {
            Logger.PrintError($"类型{typeof(T)}没有注册");
        }
    }


    /// <summary>
    /// 预加载界面
    /// </summary>
    public void Preload(UIViewEnum viewEnum, Action preloadCallback = null)
    {
        UIView uiView;
        m_UIViewDict.TryGetValue(viewEnum, out uiView);

        if (uiView == null)
        {
            Debug.LogError(viewEnum.ToString() + " 窗口没有注册");
            return;
        }
        if (uiView.uiView.getIsOpen())
        {
            preloadCallback();
            return;
        }

        if (!uiView.uiView.getIsLoaded())
        {
            if (!uiView.uiView.getIsLoading())
            {
                uiView.uiView.StartLoad();
            }
        }
        uiView.uiView.setOpening(false);
        MainThread.Instance.StartCoroutine(AsynOpen(uiView.uiView, null, preloadCallback));
    }

    /// <summary>
    /// 关闭界面
    /// </summary>
    public void Hide(UIViewEnum viewEnum)
    {
        if (!m_UIViewDict.TryGetValue(viewEnum, out var uiView))
        {
            Debug.LogError(viewEnum.ToString() + " 窗口没有注册");
            return;
        }

        //关闭界面时，如果是栈界面且是栈顶界面，就尝试先打开栈顶的下一个界面
        if (uiView.uiView.getIsStackView())
        {
            OpenViewInfo stackTop = UIViewStack.GetStackTop();
            if (stackTop != null && (UIViewEnum)stackTop.viewEnum == viewEnum)
            {
                UIViewStack.Pop();
                stackTop = UIViewStack.GetStackTop();
                if (stackTop != null)
                {
                    Show((UIViewEnum)stackTop.viewEnum, stackTop.msg, stackTop.openCallback);
                    return;
                }
            }
        }

        uiView.uiView.setOpening(false);
        if (uiView.uiView.getIsOpen())
        {
            uiView.uiView.setIsOpen(false);
            uiView.uiView.Hide();
        }
    }

    public bool GetIsShowing(UIViewEnum viewEnum)
    {
        m_UIViewDict.TryGetValue(viewEnum, out var uiView);
        if (uiView != null) return uiView.uiView.getIsOpen();
        Debug.LogError(viewEnum.ToString() + " 窗口没有注册");
        return false;
    }

    public void CloseAllView()
    {
        UIViewStack.ClearStack();
        UIViewStack.ClearCloseList();

        foreach (UIView uiView in m_UIViewDict.Values)
        {
            uiView.uiView.setOpening(false);
            if (uiView.uiView.getIsOpen())
                uiView.uiView.Hide();
        }
    }

    public void SaveStackAndCloseAllView()
    {
        UIViewStack.SaveStack();
        CloseAllView();
    }

    public void CloseAllViewAndRevertStack()
    {
        CloseAllView();
        UIViewStack.RevertStack();
        OpenViewInfo stackTop = UIViewStack.GetStackTop();
        Show((UIViewEnum)stackTop.viewEnum, stackTop.msg, stackTop.openCallback, false);
    }


    private IEnumerator AsynOpen(BaseView uiView, System.Object msg, Action callback)
    {
        while (!uiView.getIsLoaded())
        {
            // Logger.PrintColor("red", "等待界面加载完成：" + uiView.getViewEnum()+Time.time);
            yield return null;
        }

        if (uiView.getOpening())
        {
            //显示界面时，如果是栈界面
            if (uiView.getIsStackView())
            {
                //先判断加载完界面时该界面还是不是栈顶界面
                OpenViewInfo stackTop = UIViewStack.GetStackTop();
                if (stackTop != null && (UIViewEnum)stackTop.viewEnum != uiView.getViewEnum())
                {
                    uiView.setOpening(false);
                    Container go = uiView.getViewGO();
                    if (go != null)
                        go.visible = false;
                    yield break;
                }

                //关闭准备关闭的界面列表
                List<int> closeList = UIViewStack.GetCloseList();
                for (int i = 0, count = closeList.Count; i < count; ++i)
                {
                    UIViewEnum viewEnum = (UIViewEnum)closeList[i];
                    if (viewEnum != uiView.getViewEnum())
                    {
                        Hide(viewEnum);
                    }
                }
                UIViewStack.ClearCloseList();
            }
           Logger.PrintLog("显示界面：" + uiView.getViewEnum());
            uiView.Show(msg);
            if (callback != null)
            {
                callback.Invoke();
            }
            yield return null;
        }
        else
        {
            Logger.PrintDebug("@@@@@@@AsynOpen() already show()");
            //不需要打开窗口
            Container go = uiView.getViewGO();
            if (go != null)
                go.visible = false;
            if (callback != null)
                callback.Invoke();
        }
    }

    // [BlackList]
    /// <summary>
    /// 销毁一个包的所有界面
    /// </summary>
    /// <param name="packageName"></param>
    public void DestroyPackageView(PackageEnum packageName)
    {
        List<UIViewEnum> destroyViewEnumList = new List<UIViewEnum>();
        foreach (UIView uiView in m_UIViewDict.Values)
        {
            if (uiView.packageName == packageName)
            {
                destroyViewEnumList.Add(uiView.uiView.getViewEnum());
            }
        }
        for (int i = destroyViewEnumList.Count - 1; i >= 0; i--)
        {
            DestroyView(destroyViewEnumList[i]);
        }
        PreloadManager.Instance.ReleasePackage(packageName);
    }
    /// <summary>
    /// 销毁所有界面
    /// </summary>
    /// <param name="packageName"></param>
    public void DestroyAllView()
    {
        List<UIViewEnum> destroyViewEnumList = new List<UIViewEnum>();
        foreach (UIView uiView in m_UIViewDict.Values)
        {
            destroyViewEnumList.Add(uiView.uiView.getViewEnum());
        }
        for (int i = destroyViewEnumList.Count - 1; i >= 0; i--)
        {
            DestroyView(destroyViewEnumList[i]);
        }
        m_UIViewDict.Clear();
    }
    /// <summary>
    /// 销毁所有界面,除了哪个包---》清理按钮
    /// </summary>
    /// <param name="packageName"></param>
    public void DestroyAllViewExceptPackage(PackageEnum packageName)
    {
        List<UIViewEnum> destroyViewEnumList = new List<UIViewEnum>();
        foreach (UIView uiView in m_UIViewDict.Values)
        {
            if (uiView.packageName == packageName || uiView.packageName == PackageEnum.CommonScriptCode)
            {
                continue;
            }
            destroyViewEnumList.Add(uiView.uiView.getViewEnum());
        }
        for (int i = destroyViewEnumList.Count - 1; i >= 0; i--)
        {
            DestroyView(destroyViewEnumList[i]);
            m_UIViewDict.Remove(destroyViewEnumList[i]);
        }
    }

    public void DestroyView(UIViewEnum viewEnum)
    {
        UIView uiView;
        m_UIViewDict.TryGetValue(viewEnum, out uiView);
        if (uiView == null)
        {
            Debug.LogError(viewEnum.ToString() + " 窗口没有注册");
            return;
        }

        BaseView baseView = uiView.uiView;
        //if (baseView.getIsLoaded())
        //{
        baseView.Destroy();
        // }
    }


    public void DestroyUIRes(string packageName, string relativePath, UIViewEnum viewEnum)
    {
        string abRelativePath = relativePath;
        Logger.PrintLog(Utility.Platform.ConnectStrs("卸载UI：", abRelativePath));
        // AssetNodeManager.ReleaseNode(AssetType.FairyUI, packageName, abRelativePath);//老版本
        //没有作用，因为fairygui是UIPackage包体自己加载对象，有内部管理方式
        FGUIOperatHandletManager.Instance.ReleasetByRelativePath(packageName, abRelativePath);
        if (m_UIViewDict.ContainsKey(viewEnum))
        {
            m_UIViewDict.Remove(viewEnum);
        }
    }
    public void Dispose()
    {
        m_UIViewDict.Clear();
        mUIViewDict.Clear();
        mViewAttributeDict.Clear();
        FGUILayoutManager.Instance.Dispose();
        Logger.PrintColor("red", "@@@@@@@@@@@@@m_UILayerPanelDict.Clear()");
    }
}
