//------------------------------------------------------------
//------------------------------------------------------------
// 经验模块
//------------------------------------------------------------

using Experience;
using FairyGUI;
using msg.experience;
using System.Collections.Generic;

/// <summary>
/// 经验类型枚举
/// </summary>
public enum ExperienceEnum
{
    FightDaily = 1,     // 日常战斗
    AthenaTrial = 2,        // 雅典娜试炼
    SupremeSanctuary = 3,        // 至尊圣域
    EndlessTrial = 4,        // 无尽试炼
}

/// <summary>
/// 经验视图
/// Script Name: ExperienceView
/// 对应UIViewEnum.ExperienceView
/// </summary>
[FGUIViewAttribute(UIViewEnum.ExperienceView, typeof(ExperienceView))]
public class ExperienceView : BaseView
{
    //G_ExperienceView 是fairyGUI自动生成的UI组件类
    public override string PackageName => G_ExperienceView.PACKAGE_NAME;
    public override string ComponentName => G_ExperienceView.COMPONENT_NAME;
    G_ExperienceView view;
    // 列表视图
    private TableView<ExperienceListItem> listView;
    private Dictionary<int, ExperienceVo> _experienceDic;
    // 事件列表
    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {
        {EEventType.EventExperienceInfoUpdate, OnExperienceInfoUpdate}
    };

    protected override TopRenderData mTopRenderData => new TopRenderData
    {
        titleName = "历练",
        //icon = "ui://Common/Gm/icon_gm",
        helpId = 10000,
        currencyId = 1,
    };
    public ExperienceView()
    {
        // 设置视图属性，包括层级、类型和是否模态
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.ExperienceView, false);
        Logger.PrintColor("yellow", "ExperienceView()");
    }
    /// <summary>
    /// 完成UI加载后的回调
    /// </summary>
    /// <param name="gameObject">fairyGUI组件对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
        Logger.PrintColor("yellow", $"ExperienceView onLoadUIEnd complte!! gameObject={gameObject}");
        this.contentPane = gameObject.asCom;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        view = contentPane as G_ExperienceView;
        modal = true;
        InitComponents();
    }

    /// <summary>
    /// 初始化组件
    /// </summary>
    private void InitComponents()
    {
        listView = new TableView<ExperienceListItem>(view.experienceItemList);
        listView.setClickCallBack(OnClickExpItem);

        if (_experienceDic == null)
        {
            _experienceDic = ConfigMgr.Instance.GetConfig<ExperienceVo>();
        }
    }
    private void OnClickExpItem(object data, int index)
    {
        Logger.PrintDebug("点击经验项 index = " + index + "  data=" + data);
        ExperienceVo experienceVo = data as ExperienceVo;
        if (experienceVo == null)
        {
            Logger.PrintError("ExperienceVo is null, id = " + (experienceVo != null ? experienceVo.Id.ToString() : "null"));
            return;
        }
        switch (experienceVo.Id)
        {
            case (int)ExperienceEnum.FightDaily:
                // 打开日常战斗视图
               // CommonViewUtils.ShowTopTips("前往日常战斗");
                HideView();
                UIViewManager.Instance.Show(UIViewEnum.FightDailyView);
                break;
            case (int)ExperienceEnum.AthenaTrial:
                CommonViewUtils.ShowTopTips("前往雅典娜试炼功能暂未开放");
                break;
            case (int)ExperienceEnum.SupremeSanctuary:
                // 打开竞技场主视图
                // ViewManager.Instance.ShowView(UIViewEnum.ArenaMainView);
                CommonViewUtils.ShowTopTips("前往至尊圣域功能暂未开放");
                break;
            case (int)ExperienceEnum.EndlessTrial:
                // 打开竞技场主视图
                // ViewManager.Instance.ShowView(UIViewEnum.ArenaMainView);
                CommonViewUtils.ShowTopTips("前往无尽试炼功能暂未开放");
                break;
        }

    }
    // 按钮点击事件
    protected override void OnButtonClick(GButton clickedButton)
    {
        // 处理不同按钮点击
        if (view.closeBtn == clickedButton)
        {
            this.HideView();
        }
        else if (view.tipsBtn == clickedButton)
        {
            // 显示提示信息
            CommonViewUtils.ShowTopTips("这里是经验模块的相关提示信息");
        }
    }


    /// <summary>
    /// 经验信息更新事件回调
    /// </summary>
    private void OnExperienceInfoUpdate(EventSysArgsBase argsBase)
    {
        Logger.PrintGreen("OnExperienceInfoUpdate() argsBase = " + argsBase);
        if (argsBase != null && argsBase is EventSysArgs<List<ExperienceInfo>> args)
        {
            Logger.PrintGreen("OnExperienceInfoUpdate() _experienceInfoList = " + args.args1);

            if (args.args1 != null)
            {
                UpdateView(args.args1);
            }
        }

    }
    protected override void DoShowAnimation() => OnShown();

    protected override void DoHideAnimation() => this.HideWindowImmediately();
    /// <summary>
    /// 更新视图
    /// </summary>
    private void UpdateView(List<ExperienceInfo> dataList)
    {
        if (_experienceDic == null)
        {
            _experienceDic = ConfigMgr.Instance.GetConfig<ExperienceVo>();
        }
        List<ExperienceVo> dataListVo = new List<ExperienceVo>();
        for(int i = 0; i < dataList.Count; i++)
        {
            _experienceDic.TryGetValue(dataList[i].Id, out var experienceVo);
            if (experienceVo != null) {
                dataListVo.Add(experienceVo);
             }
        }
        listView.setDatas(dataListVo);
        listView.setMaxNum(dataList.Count);
    }

    // 当fairyGUI组件显示时调用
    protected override void OnShown()
    {
        base.OnShown();

        // 重新请求经验信息
        ExperienceViewControl.Instance.ReqExperienceGetInfoReq();
    }

    // 当fairyGUI组件隐藏时调用
    protected override void OnHide()
    {
        base.OnHide();
    }

    // 当fairyGUI组件销毁时调用
    protected override void OnDestroy()
    {

    }
}