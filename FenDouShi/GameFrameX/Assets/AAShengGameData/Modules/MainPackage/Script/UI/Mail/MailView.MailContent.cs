using FairyGUI;
using FGUIMail;
using msg.mail;
using System.Collections.Generic;

public partial class MailView
{
    Mail selectMail;

    void ShowMail(Mail mailData)
    {
        if (view == null || mailData == null)
        {
            ShowNullMail();
            return;
        }

        if (mailData.HasReward())
        {
            ShowMailHasReward(mailData);
        }
        else
        {
            ShowMailNotReward(mailData);
        }

        // 设置已读状态
        if (!mailData.IsRead())
        {
            MailControl.Instance.ReadMailReq(mailData.mailId);
        }
    }

    void ShowNullMail()
    {
        selectMail = null;

        view.MailContentGroup.visible = false;
        view.Reward.visible = false;
        view.NotSelectMail.visible = true;
    }

    void ShowMailHasReward(Mail mailData)
    {
        selectMail = mailData;

        view.MailContentGroup.visible = true;
        view.Reward.visible = true;
        view.NotSelectMail.visible = false;

        view.UnReceive.visible = selectMail.CanReceive();
        view.Receive.visible = !view.UnReceive.visible;

        SetMailBaseData(mailData);

        List<CommonItemData> list = new();
        foreach (var i in selectMail.Attachments)
        {
            list.Add(new(i.itemId, i.Num)
            {
                GetIsDraw = mailData.IsReceive()
            });
        }
        rewardList.setDatas(list);
    }

    void ShowMailNotReward(Mail mailData)
    {
        selectMail = mailData;

        view.MailContentGroup.visible = true;
        view.Reward.visible = false;
        view.NotSelectMail.visible = false;

        SetMailBaseData(mailData);
    }

    // 邮件基础信息
    void SetMailBaseData(Mail mailData)
    {
        view.MailTitle.text = mailData.Title;
        view.MailSender.text = mailData.Sender;
        view.MailDeleteTime.text = mailData.deleteTime.GetRemainTimeString();

        if (mailData.HasReward())
        {
            view.ScrollText.height = 429;
        }
        else
        {
            view.ScrollText.height = 567;
        }
        view.ScrollText.content.text = $"       {mailData.Content}";

        // view.ScrollText.GoTo.visible = false;
    }

    void RefreshSelectMail()
    {
        if (selectMail != null)
        {
            selectMail = MailControl.Instance.Model.GetMail(selectMail.mailId);
            RefreshReward();
        }
    }

    void RefreshSelectMail(int mailId)
    {
        if (selectMail != null && selectMail.mailId == mailId)
        {
            selectMail = MailControl.Instance.Model.GetMail(mailId);
            RefreshReward();
        }
    }

    void RefreshReward()
    {
        view.UnReceive.visible = selectMail.CanReceive();
        view.Receive.visible = !view.UnReceive.visible;
    }
}