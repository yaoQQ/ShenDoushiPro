//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-08-20 09:52:04.282
//------------------------------------------------------------

using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using common;
using System.Linq;
using DataTableFrame;
using System;
using roleLevel;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: RoleLevelView
///	定义窗口标识UIViewEnum.RoleLevelView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.RoleLevelView, typeof(RoleLevelView))]
public class RoleLevelView : BaseView
{
    //G_RoleLevelView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
    public override string PackageName => G_RoleLevelView.PACKAGE_NAME;
    public override string ComponentName => G_RoleLevelView.COMPONENT_NAME;
    G_RoleLevelView view;

    //奖励列表
    private TableView<ItemRender> mRewardList;
    //功能列表
    private TableView<LevelUpFunctionOpenListRender> mFuncList;
    //当前等级
    private int mlevel = 0;

    protected override bool IsFullScreen => true;

    //注册界面事件
    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {


    };
    public RoleLevelView()
    {
        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.RoleLevelView, false);
        Logger.PrintColor("yellow", "RoleLevelView()");
    }
    /// <summary>
    /// 界面加载完成,触发函数
    /// 
    /// </summary>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
        Logger.PrintColor("yellow", $"RoleLevelView onLoadUIEnd complte!! gameObject={gameObject}");
        this.contentPane = gameObject.asCom;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        view = contentPane as G_RoleLevelView;

        //背景点击关闭
        view.n4.onClick.Add(bgClick);
        //列表
        mRewardList = new TableView<ItemRender>(view.rewardList);
        mFuncList = new TableView<LevelUpFunctionOpenListRender>(view.funcList);
    }

    //统一按钮回调
    protected override void OnButtonClick(GButton clickedButton)
    {

    }

    private void bgClick()
    {
        Hide();
    }

    //打开界面,fairyGUI打开动画播放完触发
    protected override void OnShown()
    {
        base.OnShown();

        OnHookControl.Instance.isHookLevelUp = false;

        //等级
        mlevel = (int)showArgs;
        view.level.text = "[color=#fffee2,#fdecb7]" + mlevel + "[/color]";
        //加载奖励
        loadReward();
        //加载功能开启
        loadFuncOpen();
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

    //奖励加载
    private void loadReward()
    {
        var itemDatas = new List<CommonItemData>();
        var cfg = ConfigMgr.Instance.GetConfigVoById<RoleLevelVo>(mlevel);
        cfg.Reward.ForEach(reward =>
        {
            var itemData = new CommonItemData(reward[0], reward[1]);
            itemDatas.Add(itemData);
        });

        mRewardList.setDatas(itemDatas);
    }

    /// <summary>
    /// 加载功能开启
    /// </summary>
    private void loadFuncOpen()
    {
        var funcs = new List<FuncVo> { };

        var funcCfg = ConfigMgr.Instance.GetConfig<FuncVo>();
        var cfgList = funcCfg.Values.ToList();
        //功能排序
        cfgList.Sort((cfg_a, cfg_b) =>
        {
            var sort_a = cfg_a.Sort;
            var sort_b = cfg_b.Sort;
            if (sort_a != sort_b)
            {
                return sort_b.CompareTo(sort_a);
            }
            return cfg_a.Id.CompareTo(cfg_b.Id);
        });
        cfgList.ForEach(cfg =>
        {
            if (cfg.IsNotice == 1)
            {
                var isOpen = SystemOpenControl.Instance.Model.GetIsSystemOpen(cfg.Id);
                if (!isOpen && funcs.Count < 3)
                {
                    funcs.Add(cfg);
                }
            }
        });
        if (funcs.Count == 0)
        {
            view.func.visible = false;
        }
        else
        {
            view.func.visible = true;
            mFuncList.setDatas(funcs);
        }

    }
}