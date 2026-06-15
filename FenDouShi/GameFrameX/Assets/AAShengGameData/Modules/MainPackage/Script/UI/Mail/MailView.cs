using FairyGUI;
using FGUIMail;
using msg.mail;
using System.Collections.Generic;
using System.Linq;

public partial class MailView : BaseView
{
    public override string PackageName => G_MailView.PACKAGE_NAME;
    public override string ComponentName => G_MailView.COMPONENT_NAME;
    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {
        { EEventType.Mail_QueryMailInfoResp, Mail_QueryMailInfoResp },
        { EEventType.Mail_DeleteMailResp, Mail_DeleteMailResp },
        { EEventType.Mail_UpdateMail, Mail_UpdateMail },
        { EEventType.Mail_UpdateMails, Mail_UpdateMails },
    };

    G_MailView view;
    Controller mailTabController;
    List<int> mailIds;

    TableView<ItemRender> rewardList;

    #region 生命周期

    public MailView()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.MailView, false);
    }

    protected override void OnFinishLoad(GComponent gameObject)
    {
        base.OnFinishLoad(gameObject);
        contentPane = gameObject;
        contentPane.MakeFullScreen();
        contentPane.AddRelation(GRoot.inst, RelationType.Size);

        view = contentPane as G_MailView;
        closeButton = view.CloseBtn;

        mailTabController = view.GetController(MailString.MailTab);
        if (mailTabController == null)
            Logger.PrintError($"[邮件]无法获取控制器:{MailString.MailTab}");
        else
            mailTabController.onChanged.Set(OnChangeController_MailTab);

        view.MailItemList.itemRenderer = RenderMailItem;
        view.MailItemList.SetVirtual();
        view.MailItemList.onClickItem.Set(OnSelect_Mail);

        // 邮件内容
        rewardList = new(view.RewardList);
    }

    protected override void RegisterRedPoint()
    {
        RegisterRedPoint(ERedPointType.Mail_Tab1, view.tab1);
        RegisterRedPoint(ERedPointType.Mail_Tab2, view.tab2);
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == view.DeleteAllBtn)
        {
            if (MailControl.Instance.Model.HasCanDeleteMail())
            {
                Logger.PrintLog("[邮件]删除所有已读和已领取邮件");
                UIViewManager.Instance.Show(UIViewEnum.MailDeleteView);
            }
        }
        else if (clickedButton == view.ReceiveAllBtn)
        {
            Logger.PrintLog("[邮件]领取所有附件");
            var canTake = MailControl.Instance.Model.HasCanReceiveMail();
            if (canTake)
            {
                MailControl.Instance.TakeAllMailAttachmentReq();
            }
        }
        else if (clickedButton == view.ReceiveBtn)
        {
            Logger.PrintLog("[邮件]领取附件");
            MailControl.Instance.TakeMailAttachmentReq(selectMail.mailId);
        }
    }

    protected override void DoShowAnimation() => OnShown();

    protected override void DoHideAnimation() => this.HideWindowImmediately();

    protected override void OnShown()
    {
        base.OnShown();

        CancelSelectMailItem();
        view.MailItemList.numItems = 0;
        view.MailItemList.scrollPane.ScrollTop();
        RecycleMailIds();

        mailTabController.selectedIndex = 0;

        // 登录后，第一次打开界面请求一次邮件列表
        if (!MailControl.Instance.Model.queryMailInfoReq)
        {
            MailControl.Instance.Model.queryMailInfoReq = true;
            MailControl.Instance.QueryMailInfoReq();
        }
        else
        {
            RefreshMailList();
        }
    }

    protected override void OnHide()
    {
        base.OnHide();
        view.MailItemList.numItems = 0;
        RecycleMailIds();
    }

    protected override void OnDestroy()
    {
        if (mailTabController != null)
        {
            mailTabController.onChanged.Clear();
            mailTabController = null;
        }
    }

    public override void Dispose()
    {
        base.Dispose();

        view = null;
    }
    #endregion

    #region UI事件
    private void OnChangeController_MailTab()
    {
        CancelSelectMailItem();
        view.MailItemList.scrollPane.ScrollTop();
        RefreshMailList();
    }

    void OnSelect_Mail()
    {
        //Logger.PrintLog($"[邮件]Select index:{view.MailItemList.selectedIndex}");
        int mailId = mailIds[view.MailItemList.selectedIndex];
        Mail mailData = MailControl.Instance.Model.GetMail(mailId);
        if (mailData != null)
        {
            ShowMail(mailData);
        }
        else
        {
            ShowNullMail();
        }
    }
    #endregion

    #region 网络事件
    void Mail_QueryMailInfoResp(EventSysArgsBase argsBase)
    {
        if (view == null)
            return;

        RefreshMailList();
        view.MailItemList.scrollPane.ScrollTop();
    }

    void Mail_DeleteMailResp(EventSysArgsBase argsBase)
    {
        if (view == null)
            return;

        if (argsBase != null && argsBase is EventSysArgs<int[]> args && args.args1 != null && args.args1.Length > 0)
        {
            CancelSelectMailItem();
            RefreshMailList();
        }
    }

    void Mail_UpdateMail(EventSysArgsBase argsBase)
    {
        if (view == null)
            return;

        if (argsBase is EventSysArgs<int> args)
        {
            view.MailItemList.RefreshVirtualList();
            RefreshSelectMail(args.args1);
        }
    }

    void Mail_UpdateMails(EventSysArgsBase argsBase)
    {
        if (view == null)
            return;

        if (argsBase is EventSysArgs<List<int>> args)
        {
            view.MailItemList.RefreshVirtualList();
            RefreshSelectMail();

            args.args1.Clear();
            args.args1.RecycleToPool();
        }
    }
    #endregion

    // 显示邮件物体
    void RenderMailItem(int index, GObject item)
    {
        if (item is not G_MailItem mailItem)
            return;

        var mailId = mailIds[index];
        var mailData = MailControl.Instance.Model.GetMail(mailId);
        if (mailData == null)
            return;
        //Logger.PrintLog($"渲染邮件物体,索引:{index}:{mailItem.id}");

        bool isRead = mailData.IsRead();    // 已读
        bool hasReward = mailData.HasReward();    // 有奖励
        bool isReceive = mailData.IsReceive();  // 已领取奖励

        // 已读/未读
        mailItem.New.visible = !isRead;

        mailItem.Light.visible = !isRead || hasReward && !isReceive;
        mailItem.Drak.visible = !mailItem.Light.visible;
        if (mailItem.Light.visible)
        {
            mailItem.ML_ReadIcon.visible = isRead;
            mailItem.ML_UnReadIcon.visible = !isRead;
        }

        // 标题
        mailItem.MailTitle_Read.text = mailData.Title;
        mailItem.MailTitle_Unread.text = mailData.Title;
        mailItem.Select_MailTitle.text = mailData.Title;

        // 发送日期
        var sendTimeTxt = mailData.sendTime.GetSendTime();
        mailItem.SendTime_NotSelect_Read.text = sendTimeTxt;
        mailItem.SendTime_NotSelect_Unread.text = sendTimeTxt;
        mailItem.Select_SendTime.text = sendTimeTxt;

        mailItem.SendTime_NotSelect_Read.visible = mailItem.MailTitle_Read.visible = mailItem.BG_Read.visible = isRead && (!hasReward || hasReward && isReceive);
        mailItem.SendTime_NotSelect_Unread.visible = mailItem.MailTitle_Unread.visible = mailItem.BG_Unread.visible = !mailItem.BG_Read.visible;

        // 奖励
        mailItem.HasRewardIcon.visible = hasReward;
        mailItem.Select_HasRewardIcon.visible = hasReward;
        if (hasReward)
        {
            mailItem.RewardDark.visible = isRead && isReceive;
            mailItem.RewardLight.visible = !mailItem.RewardDark.visible;
            mailItem.RL_Receive.visible = hasReward && isReceive;
            mailItem.RL_UnReceive.visible = !mailItem.RL_Receive.visible;

            mailItem.Receive1.visible = hasReward && isReceive;
            mailItem.UnReceive1.visible = !mailItem.Receive1.visible;
        }
    }

    // 刷新邮件列表
    void RefreshMailList()
    {
        RecycleMailIds();
        if (view == null)
        {
            Logger.PrintError("[邮件]view不存在");
            return;
        }

        mailIds = MailControl.Instance.Model.GetMailIds(mailTabController.selectedIndex + 1);

        Logger.PrintLog($"[邮件]控制器类型：{mailTabController.selectedIndex + 1}, 邮件数量:{mailIds.Count}");
        int mailCount = mailIds.Count;
        view.MailItemList.numItems = mailCount;
        view.MailItemList.visible = mailCount > 0;
        view.NotMails.visible = !view.MailItemList.visible;
    }

    // 回收临时对象
    void RecycleMailIds()
    {
        if (mailIds != null)
        {
            mailIds.Clear();
            mailIds.RecycleToPool();
            mailIds = null;
        }
    }

    void CancelSelectMailItem()
    {
        view.MailItemList.ClearSelection();
        ShowNullMail();
    }
}