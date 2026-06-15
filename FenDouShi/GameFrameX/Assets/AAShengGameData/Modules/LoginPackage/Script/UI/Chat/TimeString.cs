using System.Collections.Generic;

public static class TimeString
{
    public const string expired = "已过期";
    public const string day = "天";
    public const string hour = "小时";
    public const string min = "分钟";
    public const string second = "秒";

    public static Dictionary<int, string> numToStr = new();

    public static string GetString(this int seconds)
    {
        if (!numToStr.TryGetValue(seconds, out var str))
        {
            str = seconds.ToString();
            numToStr.Add(seconds, str);
        }
        return str;
    }
}