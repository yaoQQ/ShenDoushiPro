using msg.mail;
using System.Collections.Generic;

[Control]
public class MailControl : BaseControl<MailControl>
{
    public MailModel Model { get; set; }

    protected override void onInit()
    {
        Model = new();
    }

    protected override void onEventListener()
    {
        on<QueryMailInfoResp>((uint)Cmd.QueryMailInfoResp, QueryMailInfoResp);
        on<NewMailsNoticeResp>((uint)Cmd.NewMailsNoticeResp, NewMailsNoticeResp);
        on<TakeMailAttachmentResp>((uint)Cmd.TakeMailAttachmentResp, TakeMailAttachmentResp);
        on<TakeAllMailAttachmentResp>((uint)Cmd.TakeAllMailAttachmentResp, TakeAllMailAttachmentResp);
        on<DeleteMailResp>((uint)Cmd.DeleteMailResp, DeleteMailResp);
        on<ReadMailResp>((uint)Cmd.ReadMailResp, ReadMailResp);
    }

    void QueryMailInfoResp(QueryMailInfoResp resp)
    {
        if (resp != null)
        {
            Model.SetMails(resp.Mails);
        }
        else
        {
            Model.SetMails(null);
        }
        Model.RefreshRedPoint();
        EventManager.Instance.Dispatch(EEventType.Mail_QueryMailInfoResp);
    }

    // 生命周期
    protected override void onLoginSuccess()
    {
        Model.OnLoginSuccess();
    }

    // 网络事件:请求邮件列表
    public void QueryMailInfoReq()
    {
        Logger.PrintLog("[邮件]QueryMailInfoReq()");
        var req = new QueryMailInfoReq();
        SendNetMsg((uint)Cmd.QueryMailInfoReq, req);

        // TODO:需要防止短时间内多次点击重复发送请求
    }

    // 新邮件通知
    void NewMailsNoticeResp(NewMailsNoticeResp resp)
    {
        Logger.PrintLog($"[邮件]NewMailsNoticeResp");
        Model.UpdateMails(resp.Mails);
        Model.RefreshRedPoint();
        EventManager.Instance.Dispatch(EEventType.Mail_QueryMailInfoResp);
    }

    // 领取单个邮件奖励
    public void TakeMailAttachmentReq(int mailId)
    {
        Logger.PrintLog($"[邮件]TakeMailAttachmentReq():{mailId}");
        if (Model.CanReceiveMail(mailId))
        {
            var req = new TakeMailAttachmentReq()
            {
                mailId = mailId
            };
            SendNetMsg((uint)Cmd.TakeMailAttachmentReq, req);
        }
    }

    void TakeMailAttachmentResp(TakeMailAttachmentResp resp)
    {
        Logger.PrintLog($"[邮件]TakeMailAttachmentResp");
        Model.UpdateMail(resp.mailUpdate, true);
        Model.RefreshRedPoint();
        EventManager.Instance.Dispatch(EEventType.Mail_UpdateMail, resp.mailUpdate.mailId);
    }

    // 领取所有邮件附件
    public void TakeAllMailAttachmentReq()
    {
        Logger.PrintLog("[邮件]TakeAllMailAttachmentReq()");
        if (Model.HasCanReceiveMail())
        {
            var req = new TakeAllMailAttachmentReq();
            SendNetMsg((uint)Cmd.TakeAllMailAttachmentReq, req);
        }
    }

    void TakeAllMailAttachmentResp(TakeAllMailAttachmentResp resp)
    {
        Logger.PrintLog("TakeAllMailAttachmentResp");
        List<int> mailIds = ClassPoolManger.Instance.Get<List<int>>();
        foreach (var i in resp.mailUpdates)
        {
            Model.UpdateMail(i, false);
            mailIds.Add(i.mailId);
        }
        Model.SortMails();
        Model.RefreshRedPoint();
        EventManager.Instance.Dispatch(EEventType.Mail_UpdateMails, mailIds);
        Model.Log();
    }

    // 删除所有已读已领取奖励邮件
    public void DeleteAllReadMail()
    {
        var mailIds = Model.GetCanDeleteMailIds();
        if (mailIds != null && mailIds.Length > 0)
        {
            DeleteMailReq(mailIds);
        }
    }

    public void DeleteMailReq(params int[] mailIds)
    {
        if (mailIds == null || mailIds.Length == 0)
            return;

        var req = new DeleteMailReq()
        {
            mailIds = mailIds
        };
        Logger.PrintLog($"[邮件]SendTakeAllMailAttachmentReq:{mailIds.Length}");
        SendNetMsg((uint)Cmd.DeleteMailReq, req);
    }

    void DeleteMailResp(DeleteMailResp resp)
    {
        Logger.PrintLog($"[邮件]DeleteMailResp:{resp.mailIds.Length}");
        Model.DeleteMail(resp.mailIds);
        Model.RefreshRedPoint();
        EventManager.Instance.Dispatch(EEventType.Mail_DeleteMailResp, resp.mailIds);
        Model.Log();
    }

    // 已读邮件请求
    public void ReadMailReq(int mailId)
    {
        ReadMailReq req = new ReadMailReq()
        {
            mailId = mailId
        };
        SendNetMsg((uint)Cmd.ReadMailReq, req);
    }

    public void ReadMailResp(ReadMailResp resp)
    {
        Logger.PrintLog($"[邮件]ReadMailResp");
        Model.UpdateMail(resp.mailUpdate, true);
        Model.RefreshRedPoint();
        EventManager.Instance.Dispatch(EEventType.Mail_UpdateMail, resp.mailUpdate.mailId);
    }
}