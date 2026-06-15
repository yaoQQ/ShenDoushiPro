
//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-09-02 15:53:00.769
//------------------------------------------------------------

using Arena;
using FairyGUI;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: ArenaView
///	定义窗口标识UIViewEnum.ArenaView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
//[FGUIViewAttribute(UIViewEnum.ArenaView, typeof(ArenaView))]

//竞技场界面（待定）
public class ArenaView : BaseView
{
    //G_ArenaView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
    public override string PackageName => G_ArenaEntranceView.PACKAGE_NAME;
    public override string ComponentName => G_ArenaEntranceView.COMPONENT_NAME;
    G_ArenaEntranceView view;

    //竞技场列表
    private TableView<ArenaEntranceTotalItem> arenaItemList;
    //注册界面事件
    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {
        { EEventType.EventArenaInfoUpdate, OnArenaInfoUpdate },
        { EEventType.EventArenaMatchUpdate, OnArenaMatchUpdate },
        { EEventType.EventArenaRefreshUpdate, OnArenaRefreshUpdate },
        { EEventType.EventArenaNewSeason, OnArenaNewSeason },
        { EEventType.EventArenaTimesReward, OnArenaTimesReward }
    };

    protected override TopRenderData mTopRenderData => new TopRenderData
    {
        titleName = "竞技",
        //icon = "ui://Common/Gm/icon_gm",
        helpId = 10000,
        currencyId = 1,
    };
    public ArenaView()
    {
        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.ArenaView, false);
        Logger.PrintColor("yellow", "ArenaView()");
    }
    /// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
        Logger.PrintColor("yellow", $"ArenaView onLoadUIEnd complte!! gameObject={gameObject}");
        this.contentPane = gameObject.asCom;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        view = contentPane as G_ArenaEntranceView;
        InitComponent();

    }

    private void InitComponent()
    {
        Logger.PrintGreen("@@@@@@@@@InitComponents()");
        arenaItemList = new TableView<ArenaEntranceTotalItem>(view.ArenaItemList);
        arenaItemList.setClickCallBack(OnArenaItemClick);
    }
    /// <summary>
    /// 模式项点击处理
    /// </summary>
    private void OnArenaItemClick(object data, int index)
    {
        Logger.PrintDebug($"OnArenaItemClick() 点击了关卡项: {index}");
        if (data is msg.rank.RankItem rankItem)
        {
            // 请求进入战斗
            ArenaControl.Instance.ReqArenaMatch();
        }
    }
    /// <summary>
    /// 刷新竞技场列表
    /// </summary>
    private void RefreshArenaList()
    {
        var matchList = ArenaControl.Instance.Model.GetMatchList();
        arenaItemList.setDatas(matchList);

        // 更新其他UI信息
        UpdateArenaInfo();
    }
    //统一按钮回调
    protected override void OnButtonClick(GButton clickedButton)
    {
        if (view.LeftNextBtn == clickedButton)
        {
            //点击了左翻页按钮
            OnLeftPageClick();
        }
        else if (view.RightNextBtn == clickedButton)
        {
            //点击了右翻页按钮
            OnRightPageClick();
        }
        //else if (view.btnRefresh == clickedButton)
        //{
        //    //点击了刷新按钮
        //    OnRefreshClick();
        //}
    }

    /// <summary>
    /// 左翻页点击
    /// </summary>
    private void OnLeftPageClick()
    {
        Logger.PrintDebug("点击了左翻页按钮");
        // 实现翻页逻辑
    }

    /// <summary>
    /// 右翻页点击
    /// </summary>
    private void OnRightPageClick()
    {
        Logger.PrintDebug("点击了右翻页按钮");
        // 实现翻页逻辑
    }

    /// <summary>
    /// 刷新按钮点击
    /// </summary>
    private void OnRefreshClick()
    {
        Logger.PrintDebug("点击了刷新按钮");
        ArenaControl.Instance.ReqArenaRefresh();
    }

    /// <summary>
    /// 竞技场信息更新
    /// </summary>
    private void OnArenaInfoUpdate(object data)
    {
        Logger.PrintDebug("竞技场信息更新");
        RefreshArenaList();
    }

    /// <summary>
    /// 竞技场匹配列表更新
    /// </summary>
    private void OnArenaMatchUpdate(object data)
    {
        Logger.PrintDebug("竞技场匹配列表更新");
        RefreshArenaList();
    }

    /// <summary>
    /// 竞技场刷新列表更新
    /// </summary>
    private void OnArenaRefreshUpdate(object data)
    {
        Logger.PrintDebug("竞技场刷新列表更新");
        RefreshArenaList();
    }

    /// <summary>
    /// 竞技场新赛季
    /// </summary>
    private void OnArenaNewSeason(object data)
    {
        Logger.PrintDebug("竞技场新赛季");
        ArenaControl.Instance.ReqArenaInfo();
    }

    /// <summary>
    /// 竞技场次数奖励
    /// </summary>
    private void OnArenaTimesReward(object data)
    {
        Logger.PrintDebug("竞技场次数奖励更新");
        RefreshArenaList();
    }



    /// <summary>
    /// 更新竞技场信息
    /// </summary>
    private void UpdateArenaInfo()
    {
        var arenaInfo = ArenaControl.Instance.Model.GetArenaInfo();
        if (arenaInfo != null)
        {
            // 更新积分、排名等信息
            //view.labScore.text = arenaInfo.Score.ToString();
            //view.labRank.text = arenaInfo.Rank.ToString();
            //view.labFreeTimes.text = $"剩余次数: {arenaInfo.freeTimes}";
        }
    }

    //打开界面,fairyGUI打开动画播放完触发
    protected override void OnShown()
    {
        base.OnShown();

        // 界面打开时请求最新数据
        ArenaControl.Instance.ReqArenaInfo();
    }

    //关闭界面,fairyGUI关闭动画播放完触发
    protected override void OnHide()
    {
        base.OnHide();
    }

    //框架和fairyGUI底层销毁界面时触发
    protected override void OnDestroy()
    {

    }
}