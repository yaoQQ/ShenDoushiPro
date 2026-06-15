using System;
using Unity.Mathematics;
using UnityEngine;
public class TimeManager
{
    private static DateTime m_defaultTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
    private static int m_timeZone = 8;
    /// <summary>心跳返回的服务器时间(秒)</summary>
    private static long m_heartbeatServerTime = 0;
    /// <summary>心跳返回时的客户端时间(秒)</summary>
    private static float m_heartbeatClientTime = 0f;

    private const int SecondsDay = 86400;//一天的秒数

    /// <summary>
    /// 设置心跳返回的服务器时间
    /// </summary>
    /// <param name="serverTime"></param>
    public static void SetHeartbeatServerTime(uint heartbeatServerTime)
    {
        m_heartbeatServerTime = heartbeatServerTime;
        m_heartbeatClientTime = Time.realtimeSinceStartup;
     //   Logger.PrintColor("yellow", "OnHeartbeatResp() 心跳指令回调m_heartbeatServerTime="+ m_heartbeatServerTime);
    }

    /// <summary>
    /// 获取服务器时间
    /// </summary>
    public static DateTime GetServerDateTime()
    {
        return m_defaultTime.AddSeconds((double)m_heartbeatServerTime + Time.realtimeSinceStartup - m_heartbeatClientTime + m_timeZone * 3600);
    }

    /// <summary>
    /// 获取服务器时间
    /// </summary>
    public static uint GetServerUnixTime()
    {
        return (uint)m_heartbeatServerTime + (uint)Time.realtimeSinceStartup - (uint)m_heartbeatClientTime;
    }

    /// <summary>
    /// 根据UnixTime获取DateTime
    /// </summary>
    public static DateTime GetDateTimeByUnixTime(uint unixTime)
    {
        return m_defaultTime.AddSeconds(unixTime + m_timeZone * 3600);
    }

    /// <summary>
    /// 根据DateTime获取UnixTime
    /// </summary>
    public static uint GetUnixTimeByDateTime(DateTime dateTime)
    {
        return (uint)(dateTime - m_defaultTime.AddSeconds(m_timeZone * 3600)).TotalSeconds;
    }
    /// <summary>
    /// 根据DateTime获取UnixTime
    /// </summary>
    public static string GetlocalTime()
    {
        var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();

    }
    public static uint GetlocalTimeStamp()
    {
        var ts = GetlocalDateTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToUInt32(ts.TotalSeconds);
    }

    public static DateTime GetlocalDateTime()
    {
        return DateTime.UtcNow.AddHours(8);
    }

    /// <summary>
    /// 开服天数
    /// </summary>
    /// <returns></returns>
    public static int GetOpenServerDay()
    {
        var serverOpenTime = LoginControl.Instance.Model?.LoginResp?.serverOpenTime ?? 0;
        if (serverOpenTime <= 0)
        {
            return 0;
        }
        var curTime = GetServerUnixTime();
        if (curTime <= 1000)
        {
            //有坑
            return 0;
        }
        var day = (int)math.ceil((curTime - (serverOpenTime / 1000)) / SecondsDay);
        return day < 0 ? 1 : day + 1;
    }


    /// <summary>
    /// 获取中国格式今天周几
    /// </summary>
    public static int GetChinaWeekDay(long now = 0)
    {
        if (now == 0) { now = GetServerUnixTime(); }
        var weekDay = (int)ToDateTime(now).DayOfWeek;
        weekDay = weekDay == 0 ? 7 : weekDay;
        return weekDay;
    }

    /// <summary>
    /// 将Long转为DateTime
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    private static DateTime ToDateTime( long self)
    {
        return TimeZone.CurrentTimeZone.ToLocalTime(m_defaultTime.AddSeconds(self));
    }
}