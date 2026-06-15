
using UnityEngine;

/// <summary>
/// 引擎拓展类
/// </summary>
public static class EngineExtensions
{

    /// <summary>
    /// 设置自身位置的X轴
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="value"></param>
    public static void SetLocalPositionX(this Transform transform, float value)
    {
        var localPosition = transform.localPosition;
        localPosition = new Vector3(value, localPosition.y, localPosition.z);
        transform.localPosition = localPosition;
    }

    /// <summary>
    /// 设置自身位置的Y轴
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="value"></param>
    public static void SetLocalPositionY(this Transform transform, float value)
    {
        var localPosition = transform.localPosition;
        localPosition = new Vector3(localPosition.x, value, localPosition.z);
        transform.localPosition = localPosition;
    }

    /// <summary>
    /// 设置自身位置的Z轴
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="value"></param>
    public static void SetLocalPositionZ(this Transform transform, float value)
    {
        var localPosition = transform.localPosition;
        localPosition = new Vector3(localPosition.x, localPosition.y, value);
        transform.localPosition = localPosition;
    }
}

