using UnityEngine;

/// <summary>
/// model基类,用于存放数据及派发数据变更事件到视图
/// </summary>
public class BaseModel
{
    protected BaseModel()
    {
        // 初始化
        onInit();
        onEventListener();
    }


    /// <summary>
    /// 初始化方法,在构造函数中调用
    /// </summary>
    protected virtual void onInit()
    {
    }

    /// <summary>
    /// 事件监听添加
    /// </summary>
    protected virtual void onEventListener()
    {
    }

    /// <summary>
    /// 不带参数的事件派发
    /// </summary>
    /// <param name="eventType"></param>
    public void Dispatch(EEventType eventType)
    {
        EventManager.Instance.Dispatch(eventType);
    }

    /// <summary>
    /// 不带参数的事件派发
    /// </summary>
    /// <param name="eventType"></param>
    public void Dispatch<T>(EEventType eventType, T arg1)
    {
        EventManager.Instance.Dispatch(eventType, arg1);
    }

    public void Dispatch<T,T2>(EEventType eventType, T arg1, T2 arg2)
    {
        EventManager.Instance.Dispatch(eventType, arg1);
    }

}
