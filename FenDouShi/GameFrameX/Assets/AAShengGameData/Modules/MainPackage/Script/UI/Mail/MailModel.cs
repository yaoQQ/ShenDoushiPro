using msg.mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public class MailModel : BaseModel
{
    public bool queryMailInfoReq;   // 第一次打开邮件界面请求邮件列表，第二次及以后不请求
    public bool hasNewMail;
    public List<Mail> mails;

    public void OnLoginSuccess()
    {
        queryMailInfoReq = false;
        hasNewMail = false;
        mails = null;
    }

    public bool Contains(int mailId)
    {
        return mails != null && mails.Exists(x => x.mailId == mailId);
    }

    // 直接设置邮件列表
    public void SetMails(List<Mail> newMails)
    {
        mails = newMails;
        SortMails();
        Log();
    }

    // 更新邮件
    public void UpdateMails(List<Mail> newMails)
    {
        if (mails == null)
        {
            mails = newMails;
        }
        else
        {
            if (newMails != null && newMails.Count > 0)
            {
                for (int i = newMails.Count - 1; i >= 0; i--)
                {
                    Mail newMail = newMails[i];
                    UpdateMail(newMail);
                }
            }
        }
        SortMails();
        Log();
    }

    public void UpdateMail(Mail newMail)
    {
        int index = mails.FindIndex(x => x.mailId == newMail.mailId);
        if (index < 0)
        {
            mails.Add(newMail);
        }
        else
        {
            mails[index] = newMail;
        }
    }

    public void UpdateMail(MailUpdate mailUpdate, bool sort)
    {
        if (mails == null || mailUpdate == null)
            return;

        Mail mail = mails.Find(x => x.mailId == mailUpdate.mailId);
        if (mail == null)
        {
            Logger.PrintError($"[邮件]更新单个邮件附件失败，本地没有邮件数据：{mailUpdate.mailId}");
        }
        else
        {
            Logger.PrintLog($"[邮件]更新单个邮件附件，mailid:{mailUpdate.mailId},status:{mailUpdate.Status},Attach:{mailUpdate.Attachments.Count}");

            mail.Status = mailUpdate.Status;
            if (mailUpdate.Attachments != null)
            {
                foreach (var i in mailUpdate.Attachments)
                {
                    var item = mail.Attachments.Find(x => x.itemId == i.itemId);
                    if (item == null)
                    {
                        mail.Attachments.Add(i);
                    }
                    else
                    {
                        item.Num = i.Num;
                        item.Got = i.Got;
                        item.expireTime = i.expireTime;
                    }
                }
            }

            if (sort)
            {
                SortMails();
            }
        }
    }

    // 邮件排序
    public void SortMails()
    {
        if (mails != null && mails.Count > 1)
        {
            mails.Sort(SortMail);
        }
    }

    int SortMail(Mail x, Mail y)
    {
        if (x == null) return 1;
        if (y == null) return -1;

        bool xHasReward = x.HasReward();
        bool yHasReward = y.HasReward();
        bool xIsGot = xHasReward && x.Status == 2 || !xHasReward && x.Status == 1;
        bool yIsGot = yHasReward && y.Status == 2 || !yHasReward && y.Status == 1;

        if (xIsGot && yIsGot)
        {
            return y.sendTime.CompareTo(x.sendTime);
        }
        else if (xIsGot)
        {
            return 1;
        }
        else if (yIsGot)
        {
            return -1;
        }

        return y.sendTime.CompareTo(x.sendTime);
    }

    public bool HasCanReceiveMail()
    {
        if (mails != null)
        {
            foreach (var i in mails)
            {
                if (i.CanReceive())
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CanReceiveMail(int mailId)
    {
        if (mails != null)
        {
            Mail mail = mails.Find(x => x.mailId == mailId);
            return mail != null && mail.CanReceive();
        }
        return false;
    }

    public bool HasCanDeleteMail()
    {
        if (mails == null || mails.Count == 0) return false;

        foreach (var mail in mails)
        {
            if (mail.IsRead())
            {
                if (!mail.HasReward())
                {
                    return true;
                }
                else if (mail.IsReceive())
                {
                    return true;
                }
            }
        }

        return false;
    }

    public int[] GetCanDeleteMailIds()
    {
        if (mails == null || mails.Count == 0)
            return null;

        List<int> mailIdList = ClassPoolManger.Instance.Get<List<int>>();
        foreach (var mail in mails)
        {
            if (mail.IsRead())
            {
                if (!mail.HasReward())
                {
                    mailIdList.Add(mail.mailId);
                }
                else if (mail.IsReceive())
                {
                    mailIdList.Add(mail.mailId);
                }
            }
        }

        // TODO:GC
        int[] mailIds = mailIdList.ToArray();
        mailIdList.Clear();
        mailIdList.RecycleToPool();
        return mailIds;
    }

    public void DeleteMail(int[] deleteIds)
    {
        if (mails != null && deleteIds != null && deleteIds.Length > 0)
        {
            for (int i = 0; i < deleteIds.Length; i++)
            {
                int mailId = deleteIds[i];
                for (int j = mails.Count - 1; j >= 0; j--)
                {
                    if (mails[j].mailId == mailId)
                    {
                        mails.RemoveAt(j);
                    }
                }
            }
            Logger.PrintLog($"[邮件]删除了{deleteIds.Length}封邮件");
            SortMails();
        }
    }

    [Conditional("UNITY_EDITOR")]
    public void Log()
    {
        int mailCount = mails != null ? mails.Count : 0;
        StringBuilder sb = new StringBuilder($"[邮件]邮件数量:{mailCount}:\n");
        //if (mailCount > 0)
        //{
        //    foreach (var mail in mails)
        //    {
        //        sb.AppendLine($"mailId:{mail.mailId}, configId:{mail.configId}, title:{mail.Title}, content:{mail.Content}");
        //        sb.AppendLine($"附件:{mail.Attachments.Count}, status:{mail.Status}, sendTime:{mail.sendTime}, deleteTime:{mail.deleteTime}, sender:{mail.Sender}");
        //        sb.AppendLine("附件:");
        //        foreach (var i in mail.Attachments)
        //        {
        //            sb.AppendLine($"itemId:{i.itemId}, num:{i.Num}, got:{i.Got}, expireTime:{i.expireTime}");
        //        }
        //        sb.AppendLine("\n");
        //    }
        //}
        Logger.PrintLog(sb.ToString());
    }

    public Mail GetMail(int mailId)
    {
        if (mails != null)
        {
            return mails.Find(x => x.mailId == mailId);
        }
        return null;
    }

    // 获取不同类型的邮件,对应mail表type列
    public List<Mail> GetMails(int mailType)
    {
        if (mails != null && mails.Count > 0)
        {
            return mails.FindAll(x =>
            {
                if (MAIL_DIC.DIC.TryGetValue(x.configId, out var DATA))
                {
                    return DATA != null && DATA.Type == mailType;
                }
                return false;
            });
        }
        return null;
    }

    public List<int> GetMailIds()
    {
        List<int> mailIds = ClassPoolManger.Instance.Get<List<int>>();
        if (mails != null && mails.Count > 0)
        {
            foreach (var i in mails)
            {
                mailIds.Add(i.mailId);
            }
        }
        return mailIds;
    }

    public List<int> GetMailIds(int mailType)
    {
        List<int> mailIds = ClassPoolManger.Instance.Get<List<int>>();
        if (mails != null && mails.Count > 0)
        {
            foreach (var i in mails)
            {
                if (MAIL_DIC.DIC.TryGetValue(i.configId, out var DATA)
                    && DATA != null && DATA.Type == mailType)
                {
                    mailIds.Add(i.mailId);
                }
            }
        }
        return mailIds;
    }

    // 红点
    public void RefreshRedPoint()
    {
        bool tab1RedPoint = false;
        bool tab2RedPoint = false;

        if (mails != null)
        {
            foreach (var i in mails)
            {
                if (MAIL_DIC.DIC.TryGetValue(i.configId, out var DATA)
                    && DATA != null)
                {
                    if (!tab1RedPoint && DATA.Type == (int)EMailType.Normal)
                    {
                        if (i.IsUnRead() || i.CanReceive())
                        {
                            tab1RedPoint = true;
                        }
                    }
                    else if (!tab2RedPoint && DATA.Type == (int)EMailType.System)
                    {
                        if (i.IsUnRead() || i.CanReceive())
                        {
                            tab2RedPoint = true;
                        }
                    }
                }

                if (tab1RedPoint && tab2RedPoint)
                    break;
            }
        }

        RedPointManager.Instance.SetState(ERedPointType.Mail_Tab1, tab1RedPoint);
        RedPointManager.Instance.SetState(ERedPointType.Mail_Tab2, tab2RedPoint);
    }
}

public enum EMailType : byte
{
    Normal = 1,
    System = 2,
}