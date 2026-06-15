using System;

public static class TimeHelper
{
    public static string GetSendTime(this long unixTime)
    {
        return GetTimeString(unixTime, "yyyy.MM.dd");
    }

    public static DateTime GetDateTime(this long unixTime)
    {
        uint timeStamp = LongTimeStampToUint(unixTime);
        return TimeManager.GetDateTimeByUnixTime(timeStamp);
    }

    public static string GetTimeString(this long unixTime, string format)
    {
        uint timeStamp = LongTimeStampToUint(unixTime);
        return TimeManager.GetDateTimeByUnixTime(timeStamp).ToString(format);
    }

    public static string GetRemainTimeString(this long unixTime)
    {
        var timeStamp = LongTimeStampToUint(unixTime);
        var deleteDateTime = TimeManager.GetDateTimeByUnixTime(timeStamp);
        var serverDateTime = TimeManager.GetlocalDateTime();

        TimeSpan ts = deleteDateTime.Subtract(serverDateTime);
        if (ts.TotalDays > 1)
        {
            return $"{ts.Days}{TimeString.day}{ts.Hours}{TimeString.hour}";
        }
        else if (ts.TotalHours > 1)
        {
            return $"{ts.Hours}{TimeString.hour}{ts.Minutes}{TimeString.min}";
        }
        else if (ts.TotalSeconds >= 0)
        {
            return $"{ts.Minutes}{TimeString.min}";
        }
        else
        {
            return TimeString.expired;
        }
    }

    public static uint LongTimeStampToUint(this long unixTime)
    {
        return (uint)(unixTime / 1000);
    }

    // 任务界面倒计时
    public static string GetRemainTimeString_Task(this uint remailTime)
    {
        var deleteDateTime = TimeManager.GetDateTimeByUnixTime(remailTime);
        var serverDateTime = DateTime.UtcNow.AddHours(8);

        TimeSpan ts = deleteDateTime.Subtract(serverDateTime);
        if (ts.TotalDays > 1)
        {
            return $"{ts.Days}{TimeString.day}{ts.Hours}{TimeString.hour}{ts.Minutes}{TimeString.min}";
        }
        else if (ts.TotalSeconds >= 0)
        {
            return $"{ts.Hours}{TimeString.hour}{ts.Minutes}{TimeString.min}{ts.Seconds}{TimeString.second}";
        }
        else
        {
            return string.Empty;
        }
    }

    // TODO:服务器时间戳
    public static uint GetlocalTimeStamp()
    {
        var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToUInt32(ts.TotalSeconds);
    }
}