using FairyGUI;
using msg.common;
using OnHookUI;
using roleLevel;
using System;
using System.Collections.Generic;
using UnityEngine;

// 领取挂机奖励
[FGUIView(UIViewEnum.OnHookView_ReceiveReward, typeof(OnHookView_ReceiveReward))]
public class OnHookView_ReceiveReward : BaseView
{
    public override string PackageName => G_OnHookView_ReceiveReward.PACKAGE_NAME;
    public override string ComponentName => G_OnHookView_ReceiveReward.COMPONENT_NAME;
    G_OnHookView_ReceiveReward view;

    bool doAnim = false;
    TableView<ItemRender> rewardList;

    // 挂机奖励
    long onHookTime;                        // 挂机累计时长
    int beforeLevel;                        // 领取前的等级
    int beforeExp;                          // 领取前的经验
    int afterLevel;                         // 领取后的等级
    int afterExp;                           // 领取后的经验
    List<CommonItemData> rewardInfos;       // 领取收益

    int totalExp;
    float maxTime = 0.8f;
    bool levelUp;

    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {

    };

    public OnHookView_ReceiveReward()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.OnHookView_ReceiveReward, false);
    }

    protected override void OnFinishLoad(GComponent gameObject)
    {
        this.contentPane = gameObject.asCom;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        view = contentPane as G_OnHookView_ReceiveReward;
        closeButton = view.closeMask;
        //view.closeMask.onClick.Set(HideView);

        rewardList = new(view.RewardList);

        modal = true;
    }

    protected override void DoShowAnimation() => OnShown();

    protected override void DoHideAnimation() => HideWindowImmediately();

    protected override void OnShown()
    {
        base.OnShown();

        doAnim = false;

        onHookTime = OnHookControl.Instance.Model.onHookTime;
        beforeLevel = OnHookControl.Instance.Model.beforeLevel;
        beforeExp = OnHookControl.Instance.Model.beforeExp;
        afterLevel = OnHookControl.Instance.Model.afterLevel;
        afterExp = OnHookControl.Instance.Model.afterExp;
        rewardInfos = OnHookControl.Instance.Model.GetReward();
        OnHookControl.Instance.Model.rewardInfos = null;

        levelUp = afterLevel > beforeLevel;
        if (rewardInfos == null) return;

        int seconds = (int)onHookTime.LongTimeStampToUint();
        var timeStamp = new TimeSpan(seconds / 60 / 60, seconds / 60, seconds % 60);
        view.time.text = timeStamp.ToString(@"hh\:mm\:ss");

        RefreshExp();
        if (beforeExp != afterExp || beforeLevel != afterLevel)
        {
            doAnim = true;
        }

        // 计算总经验
        totalExp = 0;
        if (beforeLevel == afterLevel)
        {
            if (ROLE_LEVEL_DIC.DIC.TryGetValue(beforeLevel + 1, out var nextLevel))
            {
                totalExp = Mathf.Max(0, afterExp - beforeExp);
            }
        }
        else
        {
            for (int i = beforeLevel; i < afterLevel; i++)
            {
                if (ROLE_LEVEL_DIC.DIC.TryGetValue(i + 1, out var nextLevel))
                {
                    if (i == 0)
                    {
                        // 当前等级
                        totalExp += nextLevel.Exp - beforeExp;
                    }
                    else
                    {
                        // 下一级
                        if (ROLE_LEVEL_DIC.DIC.TryGetValue(i + 1, out nextLevel))
                            totalExp += nextLevel.Exp;
                    }
                }
            }
        }
        Logger.PrintLog($"[挂机]总经验:{totalExp}");

        // 奖励
        if (rewardInfos != null)
            rewardList.setDatas(rewardInfos);
    }

    protected override void OnUpdate()
    {
        if (view == null) return;

        if (doAnim)
        {
            if (beforeLevel < afterLevel)
            {
                int levelUpNeedExp = 0;
                if (ROLE_LEVEL_DIC.DIC.TryGetValue(beforeLevel + 1, out var nextLevel))
                {
                    levelUpNeedExp = nextLevel.Exp;
                }
                if (beforeExp < levelUpNeedExp)
                {
                    beforeExp = Mathf.Clamp(beforeExp + (int)(totalExp / maxTime * Time.deltaTime), 0, levelUpNeedExp);
                    RefreshExp();
                }
                else
                {
                    beforeExp = 0;
                    beforeLevel++;
                    RefreshExp();
                }
            }
            else
            {
                if (beforeExp < afterExp)
                {
                    beforeExp = Mathf.Clamp(beforeExp + (int)(totalExp / maxTime * Time.deltaTime), 0, afterExp);
                    RefreshExp();
                }
                else
                {
                    doAnim = false;
                    Logger.PrintLog($"[挂机]升级结束");
                }
            }
        }
    }

    protected override void OnHide()
    {
        base.OnHide();

        if (levelUp)
        {
            Logger.PrintLog("[挂机]打开升级界面");

            levelUp = false;
            UIViewManager.Instance.Show(UIViewEnum.RoleLevelView, afterLevel);
        }
    }

    protected override void OnDestroy()
    {

    }

    // UI
    void RefreshExp()
    {
        var LEVEL_DATA = ROLE_LEVEL_DIC.DIC[beforeLevel];
        int levelUpNeedExp = 0;
        if (ROLE_LEVEL_DIC.DIC.TryGetValue(beforeLevel + 1, out var NEXT))
        {
            levelUpNeedExp = NEXT.Exp;
        }
        view.level.text = LEVEL_DATA.Level.ToString();
        view.n56.max = levelUpNeedExp;
        view.n56.value = levelUpNeedExp == 0 ? 0 : beforeExp;
    }
}