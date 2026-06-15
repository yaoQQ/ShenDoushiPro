
using UnityEngine;

/// <summary>
/// 本地数据存储
/// </summary>

public class PlayerPrefsTools
{
    /// <summary>
    /// 是否有某值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool HasValue(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    /// <summary>
    /// 删除某值
    /// </summary>
    /// <param name="key"></param>
    public static void DeleteValue(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    /// <summary>
    /// 获取小数
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static float GetFloat(string key, float defaultValue = 0)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    /// <summary>
    /// 设置小数
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    /// <summary>
    /// 获取整数
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static int GetInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    /// <summary>
    /// 设置整数
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    /// <summary>
    /// 获取字符串
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string GetString(string key, string defaultValue = null)
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    /// <summary>
    /// 设置字符串
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    /// <summary>
    /// 存储服务器时间戳
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static void SetServerTimeStr(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return;
        }
        var curTime = TimeManager.GetServerUnixTime();
        SetString(key, curTime.ToString());
    }

    /// <summary>
    /// 是否同一天
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool GetIsSameDay(string key)
    {
        if (string.IsNullOrEmpty(key) || !HasValue(key))
        {
            return false;
        }
        var timeStr = GetString(key);
        if (long.TryParse(timeStr, out var result))
        {
            var curTime = TimeManager.GetServerUnixTime();
            return DateFormatUtil.IsTheSameDay(curTime, result);
        }
        return false;
    }

    static PlayerPrefsTools()
    {
        GlobalLiftCycle.Instance.onApplicationPause += Save;
    }

    public static void Save(bool isPause = false)
    {
        PlayerPrefs.Save();
    }
}