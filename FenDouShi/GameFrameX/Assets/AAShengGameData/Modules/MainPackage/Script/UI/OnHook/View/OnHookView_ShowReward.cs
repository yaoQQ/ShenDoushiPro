using FairyGUI;
using msg.onHook;
using OnHookUI;
using System.Collections.Generic;

// 离线奖励
[FGUIView(UIViewEnum.OnHookView_ShowReward, typeof(OnHookView_ShowReward))]
public class OnHookView_ShowReward : BaseView
{
    public override string PackageName => G_OnHookView_ShowReward.PACKAGE_NAME;
    public override string ComponentName => G_OnHookView_ShowReward.COMPONENT_NAME;
    G_OnHookView_ShowReward view;

    TableView<ItemRender> rewardTableView;

    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {
        { EEventType.OnHookGetInfoResp, OnHookGetInfoResp },
        { EEventType.OnHookGainRewardResp, OnHookGainRewardResp }
    };

    public OnHookView_ShowReward()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.OnHookView_ShowReward, false);
    }

    protected override void OnFinishLoad(GComponent gameObject)
    {
        this.contentPane = gameObject.asCom;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        view = contentPane as G_OnHookView_ShowReward;
        closeButton = view.CloseBtn;

        rewardTableView = new(view.RewardList);

        modal = true;
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == view.Comfirm)
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
        RefreshReward();
        RefreshOnHookBtn();
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
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(G_OnHookView_ShowReward.URL);
    }

    protected override void OnDestroy()
    {

    }

    #region 事件

    void OnHookGetInfoResp(EventSysArgsBase argsBase)
    {
        if (view == null) return;

        RefreshReward();
        RefreshOnHookBtn();
    }

    void OnHookGainRewardResp(EventSysArgsBase argsBase) { }

    #endregion

    void RefreshReward()
    {
        if (view == null) return;

        var rewsources = OnHookControl.Instance.Model.GetResources();
        rewardTableView.setDatas(rewsources);
    }

    // 计时器
    void RefreshOnHookBtn()
    {
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(G_OnHookView_ShowReward.URL);
        if (view == null) return;

        view.Time.text = OnHookControl.Instance.Model.GetTimeString();
        if (!OnHookControl.Instance.Model.TimeIsFull())
        {
            GlobalTimeManager.Instance.timerController.AddTimer(G_OnHookView_ShowReward.URL, 1000, int.MaxValue, RefreshOnHookBtn_TimeStr);
        }
    }

    void RefreshOnHookBtn_TimeStr(int obj = 0)
    {
        if (view == null) return;

        view.Time.text = OnHookControl.Instance.Model.GetTimeString();
        if (OnHookControl.Instance.Model.TimeIsFull())
        {
            RefreshOnHookBtn();
        }
    }
}