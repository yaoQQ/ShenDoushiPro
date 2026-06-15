using msg.mail;
using System;

// 邮件相关工具类
public static class MailExtension
{
    public static bool IsUnRead(this Mail mail)
    {
        return mail != null && mail.Status == 0;
    }

    public static bool IsRead(this Mail mail)
    {
        return mail != null && mail.Status >= 1;
    }

    public static bool IsReceive(this Mail mail)
    {
        return mail != null && mail.Status == 2;
    }

    public static bool HasReward(this Mail mail)
    {
        if (mail != null)
        {
            var attachments = mail.Attachments;
            return attachments != null && attachments.Count > 0;
        }
        return false;
    }

    public static bool CanReceive(this Mail mail)
    {
        if (mail != null)
        {
            var attachments = mail.Attachments;
            if (attachments != null && attachments.Count > 0)
            {
                foreach (var attachment in attachments)
                {
                    if (!attachment.Got)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static bool AlreadyReceive(this Mail mail)
    {
        if (mail != null)
        {
            var attachments = mail.Attachments;
            if (attachments != null && attachments.Count > 0)
            {
                foreach (var attachment in attachments)
                {
                    if (!attachment.Got)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        return false;
    }
}