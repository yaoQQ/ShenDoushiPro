//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using Arena;
using FairyGUI;
using msg.fightdaily;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEntranceTotalItem : BaseRender
{

    public new G_ArenaEntranceTotalItem mRoot
    {
        get
        {
            return (G_ArenaEntranceTotalItem)base.mRoot;

        }
    }


    public override string mPackageName => G_ArenaEntranceTotalItem.PACKAGE_NAME;
    public override string mComponentName => G_ArenaEntranceTotalItem.COMPONENT_NAME;

    /// <summary>
    /// //节点创建时
    /// </summary>
    protected override void OnCreate()
    {
        // 初始化子组件事件
        InitSubComponents();
    }

    /// <summary>
    /// 初始化子组件
    /// </summary>
    private void InitSubComponents()
    {
        // 初始化左侧区域
        if (mRoot.AreaItemHenLeft != null)
        {
            // 设置左侧区域的点击事件
            mRoot.AreaItemHenLeft.onClick.Add(OnLeftAreaClick);
        }

        // 初始化右侧区域
        if (mRoot.AreaItemHenRight != null)
        {
            // 设置右侧区域的点击事件
            mRoot.AreaItemHenRight.onClick.Add(OnRightAreaClick);
        }

        // 初始化垂直区域（如果有）
        if (mRoot.ArenaVerBlockRight != null)
        {
            mRoot.ArenaVerBlockRight.onClick.Add(OnVerAreaClick);
        }
    }



    protected override void OnButtonClick(GButton clickedButton)
    {

    }

    /// <summary>
    /// 左侧区域点击
    /// </summary>
    private void OnLeftAreaClick()
    {
        Debug.Log("点击左侧竞技场区域");
        // 这里可以触发进入对应的竞技场
        //OpenArenaView(1); // 假设1是左侧竞技场的类型
    }

    /// <summary>
    /// 右侧区域点击
    /// </summary>
    private void OnRightAreaClick()
    {
        Debug.Log("点击右侧竞技场区域");
        // 这里可以触发进入对应的竞技场
      //  OpenArenaView(2); // 假设2是右侧竞技场的类型
    }

    /// <summary>
    /// 垂直区域点击
    /// </summary>
    private void OnVerAreaClick()
    {
        Debug.Log("点击垂直竞技场区域");
        // 这里可以触发进入对应的竞技场
      //  OpenArenaView(3); // 假设3是垂直竞技场的类型
    }

    /// <summary>
    /// 打开竞技场界面
    /// </summary>
    /// <param name="arenaType">竞技场类型</param>
    private void OpenArenaView(int arenaType)
    {
        // 请求竞技场信息
        ArenaControl.Instance.ReqArenaInfo();

        // 打开对应的竞技场界面
        // 这里需要根据实际项目框架来实现界面跳转
        Debug.Log($"打开竞技场类型: {arenaType}");
    }


    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected override void InitEventLister()
    {
    }
    /// <summary>
    /// 刷新数据
    /// </summary>
    protected override void DataChanged()
    {
        if (mData is ArenaEntranceData entranceData)
        {
            RefreshEntranceData(entranceData);
        }
    }

    /// <summary>
    /// 刷新入口数据
    /// </summary>
    /// <param name="entranceData">入口数据</param>
    private void RefreshEntranceData(ArenaEntranceData entranceData)
    {
        if (entranceData == null) return;
        mRoot.ArenaVerBlockLeft.visible = false;
        mRoot.AreaItemHenRight.visible = false;
        mRoot.ArenaVerBlockLeft.visible = false;
        mRoot.ArenaVerBlockRight.visible = false;
        if (entranceData.leftHorArena != null)
        {
            RefreshBlockArea(ArenaBlockTypeEnum.leftHor, entranceData.leftHorArena, mRoot.AreaItemHenLeft);
        }
        if (entranceData.rightHorArena != null)
        {
            RefreshBlockArea(ArenaBlockTypeEnum.righHort, entranceData.rightHorArena, mRoot.AreaItemHenRight);
        }
        if (entranceData.leftVerticalArena != null)
        {
            RefreshBlockArea(ArenaBlockTypeEnum.leftVer, entranceData.leftVerticalArena, mRoot.ArenaVerBlockLeft);
        }
        if (entranceData.rightVerticalArena != null)
        {
            RefreshBlockArea(ArenaBlockTypeEnum.rightVer, entranceData.rightVerticalArena, mRoot.ArenaVerBlockRight);
        }
      }

    /// <summary>
    /// 刷新左侧区域
    /// </summary>
    /// <param name="arenaData">竞技场数据</param>
    private void RefreshBlockArea(ArenaBlockTypeEnum blockType,ArenaBriefData arenaBriefData,GComponent targetItem)
    {
        targetItem.visible = true;
        switch (blockType)
        {
            case ArenaBlockTypeEnum.leftHor: // 刷新左侧区域
                Logger.PrintDebug("刷新左侧区域 targetItem="+ targetItem + "  arenaBriefData="+ arenaBriefData);
                RefreshLeftHorItem(targetItem,arenaBriefData);

                break;
            case ArenaBlockTypeEnum.righHort:  // 刷新右侧区域
                Logger.PrintDebug("刷新右侧区域 targetItem=" + targetItem + "  arenaBriefData=" + arenaBriefData);

                RefreshRightHorItem(targetItem,arenaBriefData);
                break;
            case ArenaBlockTypeEnum.leftVer:   // 刷新左侧区域
                RefreshLeftVerItem(targetItem,arenaBriefData);
                break;
            case ArenaBlockTypeEnum.rightVer: // 刷新右侧区域
                RefreshRightVerItem(targetItem, arenaBriefData);
                break;
        }

    }
    /// <summary>
    /// 刷新右侧区域
    /// </summary>
    /// <param name="arenaData">竞技场数据</param>
    private void RefreshLeftHorItem(GComponent item, ArenaBriefData arenaData)
    {
        if (arenaData == null) return;
        Logger.PrintDebug("RefreshLeftHorItem() item=" + item);
        G_AreaHenLeftBlock block = item as G_AreaHenLeftBlock;
        Logger.PrintDebug("RefreshLeftHorItem() block=" + block);
        // 这里根据实际UI结构设置数据
        // 例如设置标题、时间、奖励等信息
        SetArenaChildItem(block.Areaitem1, arenaData.rankVo1);
        SetArenaChildItem(block.Areaitem2, arenaData.rankVo2);

    }
    

    private void SetArenaChildItem(GComponent item,RankVo rankVo)
    {
        GTextField infoText = item.GetChild("infoText") as GTextField;
        GList goodList = item.GetChild("goodList") as GList;
        G_FightLog FightLog = item.GetChild("FightLog") as G_FightLog;
        GLoader titleBgLoad = item.GetChild("titleBgLoad") as GLoader;
        GLoader BgLoad = item.GetChild("BgLoad") as GLoader;
        GGroup maskGroup = item.GetChild("maskGroup") as GGroup;
        GTextField lockInfo = item.GetChild("lockInfo") as GTextField;


        infoText.text = "距预赛结束：2天 10:20:50";
        TableView<ItemRender> mItemRender = new TableView<ItemRender>(goodList);
        var rewards = new List<CommonItemData>();
        //for (int i = 0; i < mLent; i++)
        //{
        //    var goodItem = fightDailyVo.FirstRewards[i];
        //    rewards.Add(new CommonItemData(goodItem[0], goodItem[1]));
        //}
        mItemRender.setDatas(rewards);

        titleBgLoad.url = UIHelper.GetFguiUrl(mPackageName, rankVo.RankTitle);
        BgLoad.url = UIHelper.GetFguiUrl(mPackageName, rankVo.RankBg);

        var dic = ConfigMgr.Instance.GetConfig<FuncVo>();

        //正在战斗(无功能)
        FightLog.visible = false;
        int limitNum = ArenaControl.Instance.Model.IsArenaEntranceItemLock(rankVo);
        bool isLock = limitNum > 0;
        maskGroup.visible = isLock;
        if (isLock)
        {
           lockInfo.text = ArenaControl.Instance.Model.GetArenaEntranceLockStr(rankVo);
        }

    }
    /// <summary>
    /// 刷新右侧区域
    /// </summary>
    /// <param name="arenaData">竞技场数据</param>
    private void RefreshRightHorItem(GComponent item, ArenaBriefData arenaData)
    {
        if (arenaData == null) return;
        Logger.PrintDebug("RefreshRightHorItem() item=" + item);
        G_AreaHenRightBlock block = item as G_AreaHenRightBlock;
        Logger.PrintDebug("RefreshRightHorItem() block=" + block);
        // 这里根据实际UI结构设置数据
        // 例如设置标题、时间、奖励等信息
        Logger.PrintDebug("block="+ block+ "  arenaData="+ arenaData);
        Logger.PrintDebug("block.Areaitem3=" + block.Areaitem3);
        Logger.PrintDebug("block.Areaitem4=" + block.Areaitem4);
        Logger.PrintDebug("arenaData.rankVo1=" + arenaData.rankVo1);
        Logger.PrintDebug("arenaData.rankVo2=" + arenaData.rankVo2);
        SetArenaChildItem(block.Areaitem3, arenaData.rankVo1);
        SetArenaChildItem(block.Areaitem4, arenaData.rankVo2);
    }

    /// <summary>
    /// 刷新左侧区域
    /// </summary>
    /// <param name="arenaData">竞技场数据</param>
    private void RefreshLeftVerItem(GComponent item, ArenaBriefData arenaData)
    {
        if (arenaData == null) return;
        G_ArenaVerBlockLeft block = item as G_ArenaVerBlockLeft;
        // 这里根据实际UI结构设置数据
        // 例如设置标题、时间、奖励等信息
        SetArenaChildItem(block.AreanVerItem, arenaData.rankVo1);
        SetArenaChildItem(block.AreanVerItem, arenaData.rankVo2);
    }
    /// <summary>
    /// 刷新右侧区域
    /// </summary>
    /// <param name="arenaData">竞技场数据</param>
    private void RefreshRightVerItem(GComponent item, ArenaBriefData arenaData)
    {
        if (arenaData == null) return;
        G_ArenaVerBlocRight block = item as G_ArenaVerBlocRight;
        // 这里根据实际UI结构设置数据
        // 例如设置标题、时间、奖励等信息
        SetArenaChildItem(block.AreanVerItem, arenaData.rankVo1);
        SetArenaChildItem(block.AreanVerItem2, arenaData.rankVo2);
    }
}