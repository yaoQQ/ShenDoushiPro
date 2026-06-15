//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-09-03 16:38:51.870
//------------------------------------------------------------

using Arena;
using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: ArenaView
///	定义窗口标识UIViewEnum.ArenaView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.ArenaEntranceView, typeof(ArenaEntranceView))]
public class ArenaEntranceView : BaseView
{
    //G_ArenaView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
    public override string PackageName => G_ArenaEntranceView.PACKAGE_NAME;
    public override string ComponentName => G_ArenaEntranceView.COMPONENT_NAME;
    G_ArenaEntranceView view;
    //竞技场列表
    private TableView<ArenaEntranceTotalItem> arenaEntranceItemList;

    private int _currIndex = 0;
    private List<ArenaEntranceData> areaEntranceList;
    //注册界面事件
    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {
         { EEventType.EventArenaInfoUpdate, OnArenaInfoUpdate },

    };


    public ArenaEntranceView()
    {
        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.ArenaEntranceView, false);
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
        arenaEntranceItemList = new TableView<ArenaEntranceTotalItem>(view.ArenaItemList);
        arenaEntranceItemList.setClickCallBack(OnArenaItemClick);

        view.ArenaItemList.scrollPane.onScroll.Add(OnArenaListScroll);
        areaEntranceList= ArenaControl.Instance.Model.GetConfigToEntranceData();
        if (areaEntranceList == null)
        {
            Logger.PrintError("竞技入口areaEntranceList is null");
        }
        pageIndex = 0;
    }

    /// <summary>
    /// Arena列表滚动事件处理
    /// </summary>
    private void OnArenaListScroll(EventContext context)
    {
        // 获取当前可见的第一个元素索引
        int firstVisibleIndex = view.ArenaItemList.GetFirstChildInView();
        Logger.PrintDebug($"Arena列表滚动到了索引: {firstVisibleIndex}");
        // 添加边界检查
        pageIndex = firstVisibleIndex;
        view.titleText.text = "_currIndex:" + pageIndex;
    }
    private int pageIndex
    {
        get
        {
            return _currIndex;
        }
        set
        {
            _currIndex= value;
            if (_currIndex < 0)
            {
                _currIndex = 0;
            }
            if (_currIndex >= areaEntranceList?.Count)
            {
                _currIndex = areaEntranceList.Count - 1;
            }
            if (_currIndex == 0)
            {
                view.LeftNextBtn.visible = false;
            }else if (_currIndex== areaEntranceList.Count-1)
            {
                view.RightNextBtn.visible = false;
            }
            else
            {
                view.LeftNextBtn.visible = true;
                view.RightNextBtn.visible = true;
            }
        }
    }

    /// <summary>
    /// item点击处理
    /// </summary>
    private void OnArenaItemClick(object data, int index)
    {
        Logger.PrintDebug($"OnArenaItemClick() 点击了竞技场项: {index}");
        if (data is ArenaBriefData arenaData)
        {
            if (arenaData.isOpen)
            {
                // 打开对应的竞技场界面
                OpenSpecificArena(arenaData.rankVo1.Id);
            }
            else
            {
                // 显示解锁条件
                ShowUnlockCondition(arenaData);
            }
        }
    }

    /// <summary>
    /// 打开特定竞技场
    /// </summary>
    /// <param name="arenaId">竞技场ID</param>
    private void OpenSpecificArena(int arenaId)
    {
        Logger.PrintDebug($"打开竞技场ID: {arenaId}");
        // 根据竞技场ID打开对应的界面
        switch (arenaId)
        {
            case 1:
                // 打开普通竞技场
                //ArenaControl.Instance.ReqArenaInfo();
                break;
            case 2:
                // 打开高级竞技场
                //ArenaControl.Instance.ReqArenaInfo();
                break;
            case 3:
                // 打开特殊竞技场
                //ArenaControl.Instance.ReqArenaInfo();
                break;
        }
    }

    /// <summary>
    /// 显示解锁条件
    /// </summary>
    /// <param name="arenaData">竞技场数据</param>
    private void ShowUnlockCondition(ArenaBriefData arenaData)
    {
        string condition = $"解锁条件: 等级{arenaData.rankVo1.MinValue}级且服务器开启第{arenaData.openDay}天";
        Logger.PrintDebug(condition);
        // 这里可以显示提示信息
    }
    //统一按钮回调
    protected override void OnButtonClick(GButton clickedButton)
    {
        if (view.LeftNextBtn == clickedButton)
        {
           
            //点击了左翻页按钮
            pageIndex--;
            Logger.PrintDebug("click LeftNextBtn _currIndex=" + pageIndex);
            view.ArenaItemList.ScrollToView(pageIndex, true,true);
        }
        else if (view.RightNextBtn == clickedButton)
        {
            //点击了右翻页按钮
            pageIndex++;
            Logger.PrintDebug($"click RightNextBtn _currIndex={pageIndex} rankList.Count={areaEntranceList.Count}");
            view.ArenaItemList.ScrollToView(pageIndex, true, true);
        }
        view.titleText.text= "_currIndex:" + pageIndex;
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
    /// 刷新竞技场列表
    /// </summary>
    private void RefreshArenaList()
    {
        if (areaEntranceList == null)
        {
            Logger.PrintError("竞技入口areaEntranceList is null");
            return;
        }
        Logger.PrintToJson("打印竞技入口areaEntranceList：",areaEntranceList);
        arenaEntranceItemList.setDatas(areaEntranceList);
        arenaEntranceItemList.setMaxNum(areaEntranceList.Count);
        // 更新其他UI信息
        UpdateArenaInfo();
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

        RefreshArenaList();
    }

    //关闭界面,fairyGUI关闭动画播放完触发
    protected override void OnHide()
    {
        base.OnHide();
    }

    //框架和fairyGUI底层销毁界面时触发
    protected override void OnDestroy()
    {
        // 移除滚动事件监听
        if (view != null && view.ArenaItemList != null && view.ArenaItemList.scrollPane != null)
        {
            view.ArenaItemList.scrollPane.onScroll.Remove(OnArenaListScroll);
        }
    }
}
