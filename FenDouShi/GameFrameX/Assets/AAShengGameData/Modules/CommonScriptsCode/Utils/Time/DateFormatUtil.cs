
using System;
using System.Collections.Generic;
using UnityEngine;

public static class DateFormatUtil
{

    /// <summary>
    /// 获取两个时间的距离
    /// </summary>
    /// <param name="beforeTime"></param>
    /// <param name="nowTime"></param>
    /// <returns></returns>
    public static string GetBeforeTimeDesc(DateTime beforeTime, DateTime? nowTime)
    {
        nowTime = nowTime == null ? TimeManager.GetServerDateTime() : nowTime.Value;
        double tempTime = (nowTime.Value - beforeTime).TotalSeconds;
        string timeDesc;
        if (tempTime < 60)
        {
            timeDesc = "刚刚";
        }
        else if (tempTime < 3600)
        {
            timeDesc = Math.Floor(tempTime / 60) + "分钟前";
        }
        else if (tempTime < 3600 * 24)
        {
            timeDesc = Math.Floor(tempTime / 3600) + "小时前";
        }
        else if (tempTime < 3600 * 24 * 7)
        {
            timeDesc = Math.Floor(tempTime / 3600 / 24) + "天前";
        }
        else if (tempTime < 3600 * 24 * 30)
        {
            timeDesc = Math.Floor(tempTime / 3600 / 24 / 7) + "周前";
        }
        else
        {
            timeDesc = Math.Floor(tempTime / 3600 / 24 / 30) + "月前";
        }
        return timeDesc;
    }

    /// <summary>
    /// 根据服务器时间毫秒
    /// </summary>
    /// <param name="beforeTime"></param>
    /// <returns></returns>
    public static string GetBeforeTimeDesc(long beforeTime)
    {
        var beforeDate = GetDateByMilSec(beforeTime);
        return GetBeforeTimeDesc(beforeDate, null);
    }

    /// <summary>
    /// 获取未来时间描述
    /// </summary>
    public static string GetLastTimeDesc(DateTime lastTime, DateTime? nowTime)
    {
        nowTime = nowTime == null ? TimeManager.GetServerDateTime() : nowTime.Value;
        double tempTime = (lastTime - nowTime.Value).TotalSeconds;
        string timeDesc;
        if (tempTime < 0)
        {
            timeDesc = "已经";
        }
        else if (tempTime < 60)
        {
            timeDesc = "即将";
        }
        else if (tempTime < 3600)
        {
            timeDesc = Math.Floor(tempTime / 60) + "分钟后";
        }
        else if (tempTime < 3600 * 24)
        {
            timeDesc = Math.Floor(tempTime / 3600) + "小时后";
        }
        else
        {
            timeDesc = Math.Floor(tempTime / 3600 / 24) + "天后";
        }
        return timeDesc;
    }

    /// <summary>
    /// 根据服务器时间毫秒
    /// </summary>
    /// <param name="beforeTime"></param>
    /// <returns></returns>
    public static string GetLastTimeDesc(long beforeTime)
    {
       var beforeDate = GetDateByMilSec(beforeTime);
        return GetLastTimeDesc(beforeDate, null);
    }

    /// <summary>
    /// 检查两个时间戳是否同一天
    /// </summary>
    public static bool IsTheSameDay(long totalSeconds, long? sec2 = null)
    {
        DateTime curDate = GetDateByMilSec(totalSeconds * 1000);
        DateTime nowDate = sec2 == null ? TimeManager.GetServerDateTime() : GetDateByMilSec(sec2.Value * 1000);
        return curDate.Year == nowDate.Year &&
               curDate.Month == nowDate.Month &&
               curDate.Day == nowDate.Day;
    }

    // 输入秒数：输出 12:30
    public static string FormatsHM(long totalSeconds)
    {
        DateTime t = GetDateByMilSec(totalSeconds * 1000);
        string str = t.Hour.ToString("00") + ":" + t.Minute.ToString("00");
        return str;
    }

    // 输入总秒数,输出8天8小时8分钟8秒
    public static string FormatToDayAndSecond(long totalSeconds)
    {
        long day = totalSeconds / (3600 * 24);
        long hour = totalSeconds / 3600 - day * 24;
        long minutes = totalSeconds / 60 - day * 24 * 60 - hour * 60;
        long seconds = (totalSeconds - hour * 3600) % 60;

        string time = "";
        if (day >= 1) time += day + "天";
        if (hour >= 1) time += hour + "小时";
        if (minutes >= 1) time += minutes + "分钟";
        if (seconds >= 1) time += seconds + "秒";

        return time;
    }

    public static string FormatToDayAndSecondWithExt(long totalSeconds, string dayStr = ":", string hourStr = ":", string miStr = ":", string seStr = "")
    {
        long day = totalSeconds / (3600 * 24);
        long hour = totalSeconds / 3600 - day * 24;
        long minutes = totalSeconds / 60 - day * 24 * 60 - hour * 60;
        long seconds = (totalSeconds - hour * 3600) % 60;

        string time = "";
        if (day >= 1) time += day + dayStr;
        if (hour >= 1) time += hour + hourStr;
        if (minutes >= 1) time += minutes + miStr;
        if (seconds >= 1) time += seconds + seStr;

        return time;
    }

    // 输入总秒数,输出 天 小时 分钟 秒
    public static string FormatToDayAndSecond2(long totalSeconds, string dayStr = "", string hourStr = ":", string miStr = ":", string seStr = "")
    {
        totalSeconds = totalSeconds < 0 ? 0 : totalSeconds;
        long day = totalSeconds / (3600 * 24);
        long hour = totalSeconds / 3600 - day * 24;
        long minutes = totalSeconds / 60 - day * 24 * 60 - hour * 60;
        long seconds = (totalSeconds - hour * 3600) % 60;

        bool onlyDay = false;
        if (day > 0)
        {
            dayStr = "天";
            onlyDay = true;
        }

        string hourStrFormatted = hour < 10 ? "0" + hour : hour.ToString();
        string minutesStrFormatted = minutes < 10 ? "0" + minutes : minutes.ToString();
        string secondsStrFormatted = seconds < 10 ? "0" + seconds : seconds.ToString();

        if (onlyDay)
        {
            return day + dayStr;
        }

        return (day > 0 ? day + dayStr : "") + hourStrFormatted + hourStr +
               minutesStrFormatted + miStr + secondsStrFormatted + seStr;
    }

    // 输入总秒数,输出 天 小时 分钟 秒
    public static string FormatToDayAndSecond3(long totalSeconds, string dayStr = ":", string hourStr = ":", string miStr = ":", string seStr = "")
    {
        long day = totalSeconds / (3600 * 24);
        long hour = totalSeconds / 3600 - day * 24;
        long minutes = totalSeconds / 60 - day * 24 * 60 - hour * 60;
        long seconds = (totalSeconds - hour * 3600) % 60;

        if (hour > 0 || day > 0)
        {
            return day + dayStr + hour + hourStr;
        }
        else
        {
            return minutes + miStr + seconds + seStr;
        }
    }

    public static string FormatToShortDayAndSecond(long totalSeconds)
    {
        long day = totalSeconds / (3600 * 24);
        long hour = totalSeconds / 3600 - day * 24;
        long minute = totalSeconds / 60 - day * 24 * 60 - hour * 60;
        long second = (totalSeconds - hour * 3600) % 60;

        string time = "";
        int count = 0;

        if (day > 0)
        {
            time += day + "天";
            count++;
        }
        if (hour > 0 && count < 2)
        {
            time += hour + "小时";
            count++;
        }
        if (minute > 0 && count < 2)
        {
            time += minute + "分钟";
            count++;
        }
        if (second > 0 && count < 2)
        {
            time += second + "秒";
            count++;
        }

        return time;
    }

    /// <summary>
    /// 输入总秒数,输出8天8小时8分钟
    /// </summary>
    public static string FormatToDayAndMinutes(long totalSeconds, bool forceShowZero, bool oneMinute = false)
    {
        long day = totalSeconds / (3600 * 24);
        long hour = totalSeconds / 3600 - day * 24;
        long minutes = totalSeconds / 60 - day * 24 * 60 - hour * 60;
        long seconds = (totalSeconds - hour * 3600) % 60;

        string time = "";
        if (day >= 1 || forceShowZero) time += day + "天";
        if (hour >= 1 || forceShowZero) time += hour + "小时";
        if (minutes >= 1 || forceShowZero) time += minutes + "分钟";
        if (minutes == 0 && seconds > 0 && oneMinute) time += "1分钟";

        return time;
    }

    public static string FormatToOffLineTime(long totalSeconds)
    {
        long day = totalSeconds / (3600 * 24);
        if (day > 0) return day + "天";

        long hour = totalSeconds / 3600 - day * 24;
        if (hour > 0) return hour + "小时";

        long minutes = totalSeconds / 60 - day * 24 * 60 - hour * 60;
        if (minutes > 1) return minutes + "分钟";

        return "1分钟";
    }

    // 输入总秒数,输出8分8秒
    public static string FormatToMinutesAndSecond(long totalSeconds, bool forceShowZero)
    {
        long minutes = totalSeconds / 60;
        long seconds = totalSeconds % 60;

        string time = "";
        if (minutes > 0 || forceShowZero) time += minutes + "分";
        time += seconds + "秒";

        return time;
    }

    // 输入总秒数,输出8天8小时
    public static string FormatToDayAndHour(long totalSeconds)
    {
        long day = totalSeconds / (3600 * 24);
        long hour = totalSeconds / 3600 - day * 24;

        string time = "";
        if (day >= 1) time += day + "天";
        if (hour >= 1) time += hour + "小时";

        return time;
    }

    // 输入总秒数,输出8天8小时，超过1天不显示小时
    public static string FormatToDayAndHour2(long totalSeconds)
    {
        long day = totalSeconds / (3600 * 24);
        long hour = totalSeconds / 3600 - day * 24;

        string time = "";
        if (day >= 1)
        {
            time += day + "天";
        }
        else
        {
            time += hour + "小时";
        }

        return time;
    }

    // 输入总秒数，输出8小时08分08秒
    public static string FormatTickToCNTimes(long totalSeconds, bool show00 = true)
    {
        long hour = totalSeconds / 3600;
        long minutes = (totalSeconds - hour * 3600) / 60;
        long seconds = (totalSeconds - hour * 3600) % 60;

        string strTime = "";
        if (hour > 0) strTime += hour + "小时";

        string strFen = show00 ? "分" : "分钟";
        if (minutes == 0)
        {
            if (show00) strTime += "00" + strFen;
        }
        else if (minutes < 10)
        {
            strTime += "0" + minutes + strFen;
        }
        else
        {
            strTime += minutes + strFen;
        }

        if (seconds == 0)
        {
            if (show00) strTime += "00秒";
        }
        else if (seconds < 10)
        {
            strTime += "0" + seconds + "秒";
        }
        else
        {
            strTime += seconds + "秒";
        }

        return strTime;
    }

    public static string FormatTickMinimTime(long totalSeconds)
    {
        long hour = totalSeconds / 3600;
        long minutes = (totalSeconds - hour * 3600) / 60;
        long seconds = (totalSeconds - minutes * 60) % 60;

        string strTime = "";
        if (hour == 0)
            strTime = "00:";
        else if (hour > 0)
            strTime += (hour < 10 ? "0" + hour : hour.ToString()) + ":";

        if (minutes == 0)
            strTime += "00:";
        else if (minutes < 10)
            strTime += "0" + minutes + ":";
        else
            strTime += minutes + ":";

        if (seconds == 0)
            strTime += "00";
        else if (seconds < 10)
            strTime += "0" + seconds;
        else
            strTime += seconds;

        return strTime;
    }

    // 输入总秒数，输出00:01:44
    public static string FormatTickTime(long totalSeconds, bool hideHour = false, bool onlyDay = false)
    {
        totalSeconds = (long)Math.Floor((double)totalSeconds);
        long hour = totalSeconds / 3600;
        long minutes = (totalSeconds - hour * 3600) / 60;
        long seconds = (totalSeconds - hour * 3600) % 60;
        long day = totalSeconds / (3600 * 24);

        string strTime = "";

        if (day > 0 && onlyDay)
        {
            return strTime + day + "天";
        }

        if (hour > 0)
            strTime += (hour < 10 ? "0" + hour : hour.ToString()) + ":";
        else if (!hideHour)
            strTime += "00:";

        if (minutes == 0)
            strTime += "00:";
        else if (minutes < 10)
            strTime += "0" + minutes + ":";
        else
            strTime += minutes + ":";

        if (seconds == 0)
            strTime += "00";
        else if (seconds < 10)
            strTime += "0" + seconds;
        else
            strTime += seconds;

        return strTime;
    }

    // 输入总秒数，输出00:01:44
    public static string FormatMinutesTime(long totalSeconds)
    {
        long hour = totalSeconds / 3600;
        long minutes = (totalSeconds - hour * 3600) / 60;
        long seconds = totalSeconds % 60;

        string strTime = "";

        if (hour > 0)
        {
            strTime += (hour < 10 ? "0" + hour : hour.ToString()) + ":";
        }

        if (minutes == 0)
            strTime += "00";
        else if (minutes < 10)
            strTime += "0" + minutes;
        else
            strTime += minutes;

        if (hour <= 0)
        {
            if (seconds == 0)
                strTime += ":00";
            else if (seconds < 10)
                strTime += ":0" + seconds;
            else
                strTime += ":" + seconds;
        }

        return strTime;
    }

    /// <summary>
    /// 输入总秒数，输出X天00:01:44
    /// </summary>
    public static string FormatTickTimeHaveDay(long totalSeconds, bool hideHour = false)
    {
        long day = totalSeconds / (3600 * 24);
        long hour = totalSeconds / 3600 - day * 24;
        long minutes = totalSeconds / 60 - day * 24 * 60 - hour * 60;
        long seconds = totalSeconds - hour * 3600 - day * 24 * 3600 - minutes * 60;

        string strTime = "";

        if (day > 0)
        {
            strTime += day + "天";
        }

        if (hour > 0)
            strTime += (hour < 10 ? "0" + hour : hour.ToString()) + ":";
        else if (!hideHour)
            strTime += "00:";

        if (minutes == 0)
            strTime += "00:";
        else if (minutes < 10)
            strTime += "0" + minutes + ":";
        else
            strTime += minutes + ":";

        if (seconds == 0)
            strTime += "00";
        else if (seconds < 10)
            strTime += "0" + seconds;
        else
            strTime += seconds;

        return strTime;
    }

    // 输出：2015年3月10日
    public static string FormatSecToDate(long totalSeconds)
    {
        DateTime date = GetDateByMilSec(totalSeconds * 1000);
        return date.Year + "年" + date.Month + "月" + date.Day + "日";
    }

    // 输出：2015.3.10
    public static string FormatSecToDatePoint(long totalSeconds, string yStr = ".", string mStr = ".", string dStr = "")
    {
        DateTime date = GetDateByMilSec(totalSeconds * 1000);
        return date.Year + yStr + date.Month + mStr + date.Day + dStr;
    }

    // 输出：3月10日
    public static string FormatSecToDateMD(long totalSeconds, string monthStr = "月", string dayStr = "日")
    {
        DateTime date = GetDateByMilSec(totalSeconds * 1000);
        return date.Month + monthStr + date.Day + dayStr;
    }

    // 输出：3月10日8:00
    public static string FormatSecToDateMDHM(long totalSeconds, string timeStr = "", string monthStr = "月", string dayStr = "日")
    {
        return FormatSecToDateMD(totalSeconds, monthStr, dayStr) + timeStr + FormatsHM(totalSeconds);
    }

    public static long GetCurZeroTime()
    {
        DateTime date = TimeManager.GetServerDateTime();
        date = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        return (long)(date.Ticks / TimeSpan.TicksPerSecond);
    }

    public static string SecToDateCn(long totalSeconds)
    {
        DateTime date = GetDateByMilSec(totalSeconds * 1000);
        return date.Year + "年" +
               date.Month.ToString("00") + "月" +
               date.Day.ToString("00") + "日" +
               date.Hour.ToString("00") + ":" +
               date.Minute.ToString("00");
    }

    // 输入1970 年 1 月 1 日以来的秒数，输出 "2012-10-10 08:08"
    public static string Format(long totalSeconds)
    {
        DateTime t = GetDateByMilSec(totalSeconds * 1000);
        return t.Year + "-" + t.Month.ToString("00") + "-" + t.Day.ToString("00") + " " +
               t.Hour.ToString("00") + ":" + t.Minute.ToString("00");
    }

    // 输入1970 年 1 月 1 日以来的秒数，输出 "20121010"
    public static string FormatDayInt(long totalSeconds)
    {
        DateTime t = GetDateByMilSec(totalSeconds * 1000);
        return t.Year + t.Month.ToString("00") + t.Day.ToString("00");
    }

    // 输入1970 年 1 月 1 日以来的秒数，输出 "2012-10-10 08:08:08"
    public static string FormatWithSecond(long totalSeconds)
    {
        DateTime t = GetDateByMilSec(totalSeconds * 1000);
        return Format(totalSeconds) + ":" + t.Second.ToString("00");
    }

    /// <summary>-1过去，0今天，1未来</summary>
    public static int IsToday(long totalSeconds)
    {
        DateTime curDate = GetDateByMilSec(totalSeconds * 1000);
        DateTime nowDate = TimeManager.GetServerDateTime();

        if (curDate.Year < nowDate.Year) return -1;
        else if (curDate.Year == nowDate.Year)
        {
            if (curDate.Month < nowDate.Month) return -1;
            else if (curDate.Month == nowDate.Month)
            {
                if (curDate.Day < nowDate.Day) return -1;
                else if (curDate.Day == nowDate.Day) return 0;
            }
        }
        return 1;
    }


    /// <summary>获取周几 0：周日 6：周六</summary>
    public static int GetWeekDaySec(long totalSeconds)
    {
        DateTime curDate = GetDateByMilSec(totalSeconds * 1000);
        return (int)curDate.DayOfWeek;
    }

    /// <summary>获取周几 0：周日 6：周六</summary>
    public static int GetWeekDayMilSec(long totalMilSeconds)
    {
        DateTime curDate = GetDateByMilSec(totalMilSeconds);
        return (int)curDate.DayOfWeek;
    }

    public static DateTime GetDateByMilSec(long totalMilSeconds)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(totalMilSeconds).ToLocalTime();
    }

    public static readonly string[] WeekWord = { "日", "一", "二", "三", "四", "五", "六" };

    public static string GetCurrWeekDayChinese(long totalSeconds, string preWord = "星期")
    {
        int weekIndex = GetWeekDaySec(totalSeconds);
        return preWord + WeekWord[weekIndex];
    }

    public static int GetDay(long totalSeconds)
    {
        DateTime curDate = GetDateByMilSec(totalSeconds * 1000);
        return curDate.Day;
    }

    public static int GetHour(long totalSeconds)
    {
        DateTime curDate = GetDateByMilSec(totalSeconds * 1000);
        return curDate.Hour;
    }

    public static int GetMin(long totalSeconds)
    {
        DateTime curDate = GetDateByMilSec(totalSeconds * 1000);
        return curDate.Minute;
    }

    public static int GetSeconds(long totalSeconds)
    {
        DateTime curDate = GetDateByMilSec(totalSeconds * 1000);
        return curDate.Second;
    }

    // 输入一个具体秒数，返回一个具体的小时
    public static long GetHoursFromSeconds(long totalSeconds)
    {
        return totalSeconds / 3600;
    }

    // 输入一个具体秒数，返回一个具体的分钟
    public static long GetMinutesFromSeconds(long totalSeconds)
    {
        return totalSeconds / 60;
    }

    public static string FormatTimeH(long totalTime)
    {
        long min = (totalTime % 3600) / 60;
        long sec = totalTime % 60;
        return (min == 0 ? "" : min + "分") + sec + "秒";
    }

    public static string FormatTimeM(long totalTime)
    {
        long hour = totalTime / 3600;
        long min = (totalTime % 3600) / 60;
        long sec = totalTime % 60;
        return (hour == 0 ? "" : hour + "小时") + (min == 0 ? "" : min + "分") + sec + "秒";
    }


    public static string FormatTimeD0(long totalTime)
    {
        long hour = totalTime / 3600;
        long min = (totalTime % 3600) / 60;
        long sec = totalTime % 60;
        long day = hour / 24;

        if (day > 0)
        {
            hour = hour % 24;
            return day + "天" + hour + "小时";
        }
        else
        {
            if (hour > 0)
            {
                return hour + "小时" + min + "分";
            }
            else
            {
                return (min == 0 ? "" : min + "分") + sec + "秒";
            }
        }
    }

    public static string FormatTimeD(long totalTime)
    {
        long hour = totalTime / 3600;
        long min = (totalTime % 3600) / 60;
        long sec = totalTime % 60;
        long day = hour / 24;

        if (day > 0)
        {
            hour = hour % 24;
            return day + "天" + hour + "小时" + min + "分" + sec + "秒";
        }
        else
        {
            if (hour > 0)
            {
                return hour + "小时" + min + "分" + sec + "秒";
            }
            else
            {
                return (min == 0 ? "" : min + "分") + sec + "秒";
            }
        }
    }

    // // 输入一个时间戳，返回从现在开始距离这个时间还有多久，输出//00:00:00
    // public static string GetLeftTimeFromNowOn(long targetTime)
    // {
    //     long totalTime = GetTimeFromNowOnSec(targetTime);
    //     if (totalTime < 0) return "0";

    //     long hour = totalTime / 3600;
    //     long min = (totalTime % 3600) / 60;
    //     long sec = totalTime % 60;

    //     return hour.ToString("00") + ":" + min.ToString("00") + ":" + sec.ToString("00");
    // }

    // /// <summary>输入一个时间戳，返回从现在开始距离多少天</summary>
    // public static long GetTimeFromNowDay(long targetTime)
    // {
    //     long totalTime = GetTimeFromNowOnSec(targetTime);
    //     if (totalTime < 0) return 0;
    //     return (long)Math.Ceiling(totalTime / 86400.0);
    // }

    // 输入1970 年 1 月 1 日以来的秒数，输出为："今日"或"明日" + "几点"
    public static string GetHourData(long totalSeconds)
    {
        DateTime curServerDate = GetDateByMilSec(totalSeconds * 1000);
        DateTime curletDate = DateTime.Now;

        string str = "";
        int hour = curServerDate.Hour;
        if (curServerDate.Day == curletDate.Day)
        {
            str = "今日" + hour + "点";
        }
        else
        {
            str = "明日" + hour + "点";
        }
        return str;
    }

    /// <summary>
    /// 获取当前时间距离今日24时还有多久，输出总秒数
    /// </summary>
    public static long GetDistanceTodayHowLong(long? serverMilSecs = null)
    {
        DateTime nowDate;
        if (serverMilSecs.HasValue)
        {
            nowDate = GetDateByMilSec(serverMilSecs.Value);
        }
        else
        {
            nowDate = DateTime.Now;
        }

        int cur_hour = nowDate.Hour;
        int cur_min = nowDate.Minute;
        int cur_s = nowDate.Second;

        long distanceTime = (24 - cur_hour) * 3600 + (60 - cur_min) * 60 + (60 - cur_s);
        return distanceTime;
    }

    // 输入1970 年 1 月 1 日以来的秒数，输出"2012-08-12"
    public static string FormatPassDate1(long totalSeconds)
    {
        DateTime t = GetDateByMilSec(totalSeconds * 1000);
        return t.Year + "-" + t.Month + "-" + t.Day;
    }

    // 输入1970 年 1 月 1 日以来的秒数，输出"2012.08.12"
    public static string FormatPassDate2(long totalSeconds)
    {
        DateTime t = GetDateByMilSec(totalSeconds * 1000);
        return t.Year + "." + t.Month + "." + t.Day;
    }

    // 输入1970 年 1 月 1 日以来的秒数，输出 2012-08-12 08时
    public static string FormatPassDate(long totalSeconds)
    {
        DateTime t = GetDateByMilSec(totalSeconds * 1000);
        return t.Year + "-" + t.Month + "-" + t.Day + " " + t.Hour + "时";
    }

    // 输入1970 年 1 月 1 日以来的秒数，输出 08-12 15:33
    public static string FormatPassDate3(long totalSeconds)
    {
        DateTime t = GetDateByMilSec(totalSeconds * 1000);
        return t.Month + "-" + t.Day + " " + t.Hour.ToString("00") + ":" + t.Minute.ToString("00");
    }

    // 输入1970 年 1 月 1 日以来的秒数，输出08:08:08小时分钟秒
    public static string FormatHours(long totalSeconds)
    {
        DateTime t = GetDateByMilSec(totalSeconds * 1000);
        return t.Hour.ToString("00") + ":" + t.Minute.ToString("00") + ":" + t.Second.ToString("00");
    }

    // 输入秒数，输出8秒
    public static int FormatS(long totalSeconds)
    {
        DateTime t = GetDateByMilSec(totalSeconds * 1000);
        return t.Second;
    }

    // 输入秒数，输出08:08分钟秒
    public static string FormatMS(long totalSeconds)
    {
        DateTime t = GetDateByMilSec(totalSeconds * 1000);
        return t.Minute.ToString("00") + ":" + t.Second.ToString("00");
    }

    // 输入秒数，输出08:08:08小时分钟秒
    public static string FormatHMS(long totalSeconds)
    {
        if (totalSeconds == 0) return "00:00:00";
        DateTime t = GetDateByMilSec(totalSeconds * 1000);
        return t.Hour.ToString("00") + ":" + t.Minute.ToString("00") + ":" + t.Second.ToString("00");
    }

    // 当前时间到下一个时间(时分)的距离，秒数
    public static long DeltaMidCustomTime(int hour, int min, bool needAcrossDay)
    {
        DateTime nowDate = TimeManager.GetServerDateTime();
        DateTime nextTimeDate = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, hour, min, 0);

        if (nowDate.Hour < hour || !needAcrossDay)
        {
            // 不跨天
        }
        else
        {
            // 跨天
            nextTimeDate = nextTimeDate.AddDays(1);
        }

        return (long)((nextTimeDate.Ticks - nowDate.Ticks) / TimeSpan.TicksPerSecond);
    }

    public static string FormatHMS2(long totalSeconds, string hourStr = ":", string minuteStr = ":", string secStr = "")
    {
        long hour = totalSeconds / (60 * 60);
        totalSeconds -= hour * 60 * 60;
        long min = totalSeconds / 60;
        totalSeconds -= min * 60;
        long sec = totalSeconds;

        return hour.ToString("00") + hourStr +
               min.ToString("00") + minuteStr +
               sec.ToString("00") + secStr;
    }

    public static string FormatHM(long totalSeconds, string hourStr = ":", string minuteStr = "", bool addZero = true)
    {
        long hour = totalSeconds / (60 * 60);
        totalSeconds -= hour * 60 * 60;
        long min = totalSeconds / 60;

        return (addZero ? hour.ToString("00") : hour.ToString()) + hourStr +
               (addZero ? min.ToString("00") : min.ToString()) + minuteStr;
    }

    // 判断当前是否在某个时间区间（24小时制）
    public static bool IsInTimeZone(int lowHours, int high)
    {
        DateTime nowDate = TimeManager.GetServerDateTime();
        int curHour = nowDate.Hour;
        return curHour >= lowHours && curHour <= high;
    }

    /// <summary>8时8分-》8分8秒</summary>
    public static string ToHMMS(long seconds, string hourStr = "时", string miStr = "分", string seStr = "秒")
    {
        long h = seconds / 3600;
        long m = (seconds - h * 3600) / 60;
        if (h > 0)
        {
            return h + hourStr + m + miStr;
        }
        else
        {
            long s = seconds - (h * 3600 + m * 60);
            return m + miStr + s + seStr;
        }
    }

    /// <summary>8天8时-》8分8秒</summary>
    public static string ToDH_MS(long totalSeconds)
    {
        long day = totalSeconds / (3600 * 24);
        long hour = totalSeconds / 3600 - day * 24;
        long minutes = totalSeconds / 60 - day * 24 * 60 - hour * 60;
        long seconds = (totalSeconds - hour * 3600) % 60;

        if (day > 0 || hour > 0)
        {
            return day + "天" + hour + "时";
        }
        else
        {
            return minutes + "分" + seconds + "秒";
        }
    }

    /// <summary>8天8时-》8时8分-》8分8秒</summary>
    public static string ToDH_HM_MS(long totalSeconds)
    {
        long day = totalSeconds / (3600 * 24);
        long hour = totalSeconds / 3600 - day * 24;
        long minutes = totalSeconds / 60 - day * 24 * 60 - hour * 60;
        long seconds = (totalSeconds - hour * 3600) % 60;

        if (day > 0)
        {
            return day + "天" + hour + "时";
        }
        else if (hour > 0)
        {
            return hour + "时" + minutes + "分";
        }
        else
        {
            return minutes + "分" + seconds + "秒";
        }
    }

    /// <summary>返回整取时间</summary>
    public static string GetCurrFloor(long totalSeconds)
    {
        long day = totalSeconds / (3600 * 24);
        if (day > 0) return day + "天";

        long hour = totalSeconds / 3600 - day * 24;
        if (hour > 0) return hour + "时";

        long minutes = totalSeconds / 60 - day * 24 * 60 - hour * 60;
        if (minutes > 0) return minutes + "分";

        long seconds = (totalSeconds - hour * 3600) % 60;
        return seconds + "秒";
    }

    /// <summary>
    /// 格式化输出时间
    /// </summary>
    public static string Time2Str(long time, string fmt = "yyyy-MM-dd hh:mm:ss")
    {
        DateTime date = GetDateByMilSec(time * 1000);
        return date.ToString(fmt);
    }

    /// <summary>
    /// 获取到零点的时间字符串
    /// </summary>
    public static string GetToZeroTimeStr()
    {
        long zeroT = GetCurZeroTime();
        long t = (long)(zeroT - LoginControl.Instance.Model.ServerTime / 1000);
        return FormatToDayAndSecond2(t);
    }



    public static string FormatLeftTime(long seconds, string color = "00ff06")
    {
        string rtn = "";
        if (seconds > 86400) // 如果大于天 返回xx天xx小时
        {
            var day = seconds / 86400;
            var hour = (seconds % 86400) / 3600;
            var min = ((seconds % 86400) % 3600) / 60;
            rtn = string.Format("{0}天{1:00}时{2:00}分", day, hour, min);
        }
        else if (seconds > 3600) // 如果大于1小时 返回xx小时xx分钟
        {
            var hour = seconds / 3600;
            var min = (seconds % 3600) / 60;
            rtn = string.Format("{0}小时{1:00}分", hour, min);
        }
        else if (seconds > 60) // 如果大于1分钟 返回xx分钟xx秒
        {
            var min = seconds / 60;
            var sec = seconds % 60;
            rtn = string.Format("{0:00}分{1:00}秒", min, sec);
        }
        else
        {
            var sec = seconds;
            rtn = string.Format("00分{0:00}秒", sec);
        }
        rtn = string.Format("<color=#{0}>{1}</color>", color, rtn);
        return rtn;
    }

    public static string FormatLeftTime2(long seconds, string color = "00ff06")
    {
        string rtn = "";
        if (seconds > 86400)
        {
            var day = seconds / 86400;
            rtn = Utility.Text.Format("{0}天", day);
        }
        else if (seconds > 3600)
        {
            var hour = seconds / 3600;
            rtn = Utility.Text.Format("{0}时", hour);
        }
        else if (seconds > 60)
        {
            var min = seconds / 60;
            rtn = Utility.Text.Format("{0}分", min);
        }
        else
        {
            var sec = seconds;
            rtn = Utility.Text.Format("{0}秒", sec);
        }
        rtn = Utility.Text.Format("<color=#{0}>{1}</color>", color, rtn);
        return rtn;
    }

    /// <summary>
    /// 时间戳转换为日期  秒
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static string GetDateTimeStr(long seconds, string pattern = "yyyy-MM-dd HH:mm:ss")
    {
        var dataTime = DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime.ToLocalTime();
        var time = dataTime.ToString(pattern);
        return time;
    }

    /// <summary>
    /// 时间戳转换为日期  毫秒
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static string GetDateTimeStr2(long seconds, string pattern = "yyyy-MM-dd HH:mm:ss")
    {
        return GetDateTimeStr(seconds / 1000);
    }

    /// <summary>
    /// 时间戳转换成描述 2025-01-24 23:59:59
    /// </summary>
    /// <param name="dateString"></param>
    /// <returns></returns>
    public static long GetSecondsByDateStr(string dateString)
    {
        if (DateTime.TryParse(dateString, out DateTime date))
        {
            var utcDate = date.ToUniversalTime();
            var timeSpan = utcDate - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)timeSpan.TotalSeconds;
        }
        else
        {
            Logger.PrintLog("Invalid date format.");
        }
        return 0;
    }
}