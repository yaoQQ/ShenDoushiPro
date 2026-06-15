// 引入泛型集合命名空间，用于使用 Dictionary 等集合类型
using System.Collections.Generic;

// 事件注册类，用于管理事件的注册和注销操作
public class EventRegister
{
    // 初始化标志，用于判断是否已经初始化
    public bool init;
    // 存储事件类型和事件监听器的字典
    Dictionary<EEventType, OnEventLister> eventList;

    // 初始化方法，用于设置事件列表
    public void Init(Dictionary<EEventType, OnEventLister> eventList)
    {
        // 如果尚未初始化
        if (!init)
        {
            // 将初始化标志设置为已初始化
            init = true;
            // 将传入的事件列表赋值给当前类的事件列表
            this.eventList = eventList;
        }
    }

    // 注册事件监听器的方法
    public void RegisterListener()
    {
        // 检查事件列表不为空且包含元素
        if (eventList != null && eventList.Count > 0)
        {
            // 遍历事件列表中的每个事件
            foreach (var i in eventList)
            {
                // 调用事件管理器实例的添加事件监听器方法
                EventManager.Instance.AddEventLister(i.Key, i.Value);
            }
        }
    }

    // 注销事件监听器的方法
    public void DeregisterListener()
    {
        // 检查事件列表不为空且包含元素
        if (eventList != null && eventList.Count > 0)
        {
            // 遍历事件列表中的每个事件
            foreach (var i in eventList)
            {
                // 调用事件管理器实例的移除事件监听器方法
                EventManager.Instance.RemoveEventLister(i.Key, i.Value);
            }
        }
    }
}