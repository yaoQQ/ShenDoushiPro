using FairyGUI;
using OnHookUI;
using System.Collections.Generic;
using UnityEngine;

[FGUIView(UIViewEnum.OnHookView_Main, typeof(OnHookView_Main))]
public class OnHookView_Main : BaseView
{
    public override string PackageName => G_OnHookView_Main.PACKAGE_NAME;
    public override string ComponentName => G_OnHookView_Main.COMPONENT_NAME;

    G_OnHookView_Main view;
    TableView<ItemRender> vipRewardList;
    TableView<OnHookRenderer_DungeonReward> dungeronTableView;
    TableView<ItemRender> rewardList;

    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {
        { EEventType.OnHookGetInfoResp, OnHookGetInfoResp },
        { EEventType.OnHookSpeedUpResp, OnHookSpeedUpResp },
        { EEventType.OnHookGainRewardResp, OnHookGainRewardResp },
    };

    public OnHookView_Main()
    {
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.OnHookView_Main, false);
    }

    protected override void OnFinishLoad(GComponent gameObject)
    {
        this.contentPane = gameObject.asCom;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        view = contentPane as G_OnHookView_Main;
        closeButton = view.CloseBtn;

        vipRewardList = new(view.BuyReward);
        dungeronTableView = new(view.CanReceiveList);
        rewardList = new(view.RewardList);

        view.Tips.visible = false;

        modal = true;
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == view.ShowDetailBtn)
        {
            UIViewManager.Instance.Show(UIViewEnum.OnHookView_Detail);
        }
        else if (clickedButton == view.FaskBattleBtn)
        {
            int times = ConfigMgr.GetGameConst("idle_accelerate_free_times")[0];
            if (OnHookControl.Instance.Model.freeCount < times)
            {
                OnHookControl.Instance.OnHookSpeedUpReq();
            }
            else
            {
                // TODO:
            }
        }
        else if (clickedButton == view.GetRewardBtn)
        {
            if (OnHookControl.Instance.Model.CanReceiveReward())
            {
                OnHookControl.Instance.OnHookGainRewardReq();
                HideView();
            }
            else
            {
                // TODO:Tips
            }
        }
    }

    protected override void OnShown()
    {
        base.OnShown();

        RefreshLeft();
        RefreshRight();

        GlobalTimeManager.Instance.timerController.AddTimer(G_OnHookView_Main.PACKAGE_NAME, 1000, int.MaxValue, RefreshRewardTimer);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (OnHookControl.Instance.Model.CanReqReward())
        {
            OnHookControl.Instance.OnHookGetInfoReq();
        }
    }

    protected override void OnHide()
    {
        base.OnHide();

        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(G_OnHookView_Main.URL);
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(G_OnHookView_Main.PACKAGE_NAME);
    }

    protected override void OnDestroy() { }

    #region 事件

    void OnHookGetInfoResp(EventSysArgsBase argsBase) => RefreshRight();

    void OnHookGainRewardResp(EventSysArgsBase argsBase) => RefreshRight();

    void OnHookSpeedUpResp(EventSysArgsBase argsBase) => RefreshRight();

    #endregion

    void RefreshLeft()
    {
        if (view == null) return;

        bool isBoughtVip = false;
        view.BuyBtn.visible = !isBoughtVip;
        view.ActiveGroup.visible = isBoughtVip;
        //vipRewardList.setDatas(); // TODO:

        if (isBoughtVip)
        {
            view.RemainTimes.text = "等待对接vip功能";     // TODO:
        }
    }

    void RefreshRight()
    {
        if (view == null) return;

        var dungeonId = OnHookControl.Instance.Model.dungeonId;
        if (!ON_HOOK_REWARD_DIC.DIC.TryGetValue(dungeonId, out var DATA))
            return;

        // 刷新进度条
        RefreshDungeonSlider();

        // 本关奖励
        List<List<int>> reward = new();
        for (int i = 0; i < 4; i++)
            if (i < DATA.Rewards.Count)
                reward.Add(DATA.Rewards[i]);
        dungeronTableView.setDatas(reward);

        // 当前奖励
        RefreshCurrentReward();

        // 免费次数
        RefreshFreeTimeTxt();

        // 刷新时间
        RefreshOnHookBtn();
    }

    void RefreshDungeonSlider()
    {
        if (view == null) return;
        var dungeonId = OnHookControl.Instance.Model.dungeonId;
        var index = ON_HOOK_REWARD_DIC.FindIndex(x => x.DungeonId == dungeonId);
        if (index == -1)
        {
            Logger.PrintError($"[挂机]没有副本信息：{dungeonId}");
            return;
        }

        // 先往后拿3关，看看能不能拿到
        List<int> ids = new List<int>();
        int rightIndex = Mathf.Clamp(index + 3, 0, ON_HOOK_REWARD_DIC.Arr.Length - 1);
        int leftIndex = Mathf.Clamp(3 - (rightIndex - index), 0, 4);
        for (int i = leftIndex; i < rightIndex; i++)
        {
            //Logger.PrintLog($"挂机:关卡索引id:{i}");
            if (i < ON_HOOK_REWARD_DIC.Arr.Length)
                ids.Add(ON_HOOK_REWARD_DIC.Arr[i].DungeonId);
        }
        Logger.PrintLog($"[挂机]当前关卡:{dungeonId}, index:{index}, 数量范围:{leftIndex}, right:{rightIndex}");

        int dungernIndex = index - leftIndex;
        view.s1_enabled.visible = index >= dungernIndex;
        view.s1_disabled.visible = !view.s1_enabled.visible;
        view.s1_text.text = ON_HOOK_REWARD_DIC.Arr[dungernIndex].DungeonId.ToString();
        float fillAmount = 0.2f;

        dungernIndex++;
        view.s2_enabled.visible = index >= dungernIndex;
        view.s2_disabled.visible = !view.s2_enabled.visible;
        view.s2_text.text = ON_HOOK_REWARD_DIC.Arr[dungernIndex].DungeonId.ToString();
        if (view.s2_enabled.visible)
            fillAmount = 0.4f;

        dungernIndex++;
        view.s3_enabled.visible = index >= dungernIndex;
        view.s3_disabled.visible = !view.s3_enabled.visible;
        view.s3_text.text = ON_HOOK_REWARD_DIC.Arr[dungernIndex].DungeonId.ToString();
        if (view.s3_enabled.visible)
            fillAmount = 0.6f;

        dungernIndex++;
        view.s4_enabled.visible = index >= dungernIndex;
        view.s4_disabled.visible = !view.s4_enabled.visible;
        view.s4_text.text = ON_HOOK_REWARD_DIC.Arr[dungernIndex].DungeonId.ToString();
        if (view.s4_enabled.visible)
            fillAmount = 1f;

        view.n13.fillAmount = fillAmount;
    }

    void RefreshCurrentReward()
    {
        if (view == null) return;

        var rewsources = OnHookControl.Instance.Model.GetResources();
        rewardList.setDatas(rewsources);

        view.NotRewards.visible = rewsources.Count == 0;
    }

    void RefreshFreeTimeTxt()
    {
        if (view == null) return;

        int times = ConfigMgr.GetGameConst("idle_accelerate_free_times")[0];
        view.FreeTimeTxt.text = $"{Mathf.Max(0, times - OnHookControl.Instance.Model.freeCount)}次";
    }

    // 计时器
    void RefreshOnHookBtn()
    {
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(G_OnHookView_Main.URL);
        if (view == null) return;

        view.IncomeTimeTxt.text = OnHookControl.Instance.Model.GetTimeString();
        if (!OnHookControl.Instance.Model.TimeIsFull())
        {
            GlobalTimeManager.Instance.timerController.AddTimer(G_OnHookView_Main.URL, 1000, int.MaxValue, RefreshOnHookBtn_TimeStr);
        }
    }

    void RefreshOnHookBtn_TimeStr(int obj = 0)
    {
        if (view == null) return;

        view.IncomeTimeTxt.text = OnHookControl.Instance.Model.GetTimeString();
        if (OnHookControl.Instance.Model.TimeIsFull())
        {
            RefreshOnHookBtn();
        }
    }

    // 计时器
    void RefreshRewardTimer(int obj)
    {
        if (view == null) return;

        var seconds = TimeHelper.GetlocalTimeStamp() - OnHookControl.Instance.Model.startTime.LongTimeStampToUint();
    }
}