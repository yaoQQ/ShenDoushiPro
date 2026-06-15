// 引入泛型集合命名空间，用于使用 Dictionary 等集合类型
using System.Collections.Generic;
// 引入调试相关命名空间，用于使用 Conditional 属性
using System.Diagnostics;
// 引入文本处理命名空间，用于使用 StringBuilder 类
using System.Text;

// 定义事件监听器委托，接收一个 EventSysArgsBase 类型的参数
public delegate void OnEventLister(EventSysArgsBase argsBase);

/// <summary>
/// 模块和界面的事件类
/// </summary>
// 定义事件管理类，继承自单例类 Singleton<EventManager>
public class EventManager : Singleton<EventManager>
{
    // 定义一个字典，用于存储事件类型和对应的事件成员，键为事件类型的整数表示，值为 EventMember 实例
    Dictionary<int, EventMember> handlerDic = new();

    // 触发事件
    // 注意,事件参数用完会立即回收到对象池,不会保留
    // 无参
    // 触发指定枚举类型事件的方法，将枚举类型转换为整数后调用另一个 Dispatch 方法
    public void Dispatch(EEventType eventType)
    {
        Dispatch((int)eventType);
    }

    // 触发指定整数类型事件的方法，创建无参事件参数，初始化后触发事件，最后回收参数
    public void Dispatch(int eventType)
    {
        // 从对象池获取一个无参的事件参数实例
        EventSysArgs args = ClassPoolManger.Instance.Get<EventSysArgs>();
        // 初始化事件参数
        args.Init(eventType);
        // 调用另一个 Dispatch 方法触发事件
        Dispatch(args);
        // 将事件参数回收到对象池
        args.RecycleToPool();
    }

    // 1个参数
    // 触发带一个参数的指定枚举类型事件的方法，将枚举类型转换为整数后调用另一个 Dispatch 方法
    public void Dispatch<T1>(EEventType eventType, T1 args1)
    {
        Dispatch((int)eventType, args1);
    }

    // 触发带一个参数的指定整数类型事件的方法，创建带一个参数的事件参数，初始化后触发事件，最后回收参数
    public void Dispatch<T1>(int eventType, T1 args1)
    {
        // 从对象池获取一个带一个参数的事件参数实例
        var args = ClassPoolManger.Instance.Get<EventSysArgs<T1>>();
        // 初始化事件参数
        args.Init(eventType, args1);
        // 调用另一个 Dispatch 方法触发事件
        Dispatch(args);
        // 将事件参数回收到对象池
        args.RecycleToPool();
    }

    // 2个参数
    // 触发带两个参数的指定枚举类型事件的方法，将枚举类型转换为整数后调用另一个 Dispatch 方法
    public void Dispatch<T1, T2>(EEventType eventType, T1 args1, T2 args2)
    {
        Dispatch((int)eventType, args1, args2);
    }

    // 触发带两个参数的指定整数类型事件的方法，创建带两个参数的事件参数，初始化后触发事件，最后回收参数
    public void Dispatch<T1, T2>(int eventType, T1 args1, T2 args2)
    {
        // 从对象池获取一个带两个参数的事件参数实例
        var args = ClassPoolManger.Instance.Get<EventSysArgs<T1, T2>>();
        // 初始化事件参数
        args.Init(eventType, args1, args2);
        // 调用另一个 Dispatch 方法触发事件
        Dispatch(args);
        // 将事件参数回收到对象池
        args.RecycleToPool();
    }

    // 3个参数
    // 触发带三个参数的指定枚举类型事件的方法，将枚举类型转换为整数后调用另一个 Dispatch 方法
    public void Dispatch<T1, T2, T3>(EEventType eventType, T1 args1, T2 args2, T3 args3)
    {
        Dispatch((int)eventType, args1, args2, args3);
    }

    // 触发带三个参数的指定整数类型事件的方法，创建带三个参数的事件参数，初始化后触发事件，最后回收参数
    public void Dispatch<T1, T2, T3>(int eventType, T1 args1, T2 args2, T3 args3)
    {
        // 从对象池获取一个带三个参数的事件参数实例
        var args = ClassPoolManger.Instance.Get<EventSysArgs<T1, T2, T3>>();
        // 初始化事件参数
        args.Init(eventType, args1, args2, args3);
        // 调用另一个 Dispatch 方法触发事件
        Dispatch(args);
        // 将事件参数回收到对象池
        args.RecycleToPool();
    }

    // 4个参数
    // 触发带四个参数的指定枚举类型事件的方法，将枚举类型转换为整数后调用另一个 Dispatch 方法
    public void Dispatch<T1, T2, T3, T4>(EEventType eventType, T1 args1, T2 args2, T3 args3, T4 args4)
    {
        Dispatch((int)eventType, args1, args2, args3, args4);
    }

    // 触发带四个参数的指定整数类型事件的方法，创建带四个参数的事件参数，初始化后触发事件，最后回收参数
    public void Dispatch<T1, T2, T3, T4>(int eventType, T1 args1, T2 args2, T3 args3, T4 args4)
    {
        // 从对象池获取一个带四个参数的事件参数实例
        var args = ClassPoolManger.Instance.Get<EventSysArgs<T1, T2, T3, T4>>();
        // 初始化事件参数
        args.Init(eventType, args1, args2, args3, args4);
        // 调用另一个 Dispatch 方法触发事件
        Dispatch(args);
        // 将事件参数回收到对象池
        args.RecycleToPool();
    }

    // 根据事件参数触发事件的方法，调用模块管理器处理事件并执行事件监听器列表
    public void Dispatch(EventSysArgsBase args)
    {
        // 调用模块管理器执行通知处理
        ModuleManager.Instance.ExecuteNotificationHandle(args);
        // 执行事件监听器列表
        ExecuteHandlerList(args);
    }

    // 角色信息更新，协议回调事件处理
    public void DispatchProtocolEvent(uint cmd, object message)
    {
        switch (cmd)
        {
            case (uint)msg.role.Cmd.RoleInfoResp:
                // 角色信息返回，触发角色信息更新事件
                if (message is msg.role.RoleInfoResp roleInfoResp)
                {
                    Dispatch(EEventType.EventRoleInfoUpdate, roleInfoResp);
                }
                break;
            case (uint)msg.role.Cmd.RoleLevelUpResp:
                // 角色升级返回，触发角色信息更新事件
                if (message is msg.role.RoleLevelUpResp levelUpResp)
                {
                    Dispatch(EEventType.EventRoleInfoUpdate, levelUpResp);
                }
                break;
            case (uint)msg.role.Cmd.RolePowerCompareResp:
                // 角色战力对比返回，触发角色信息更新事件
                if (message is msg.role.RolePowerCompareResp powerCompareResp)
                {
                    Dispatch(EEventType.EventRoleInfoUpdate, powerCompareResp);
                }
                break;
            // 可以根据需要添加更多的协议回调事件处理
        }
    }

    // 注册
    // 注册指定枚举类型事件监听器的方法，将枚举类型转换为整数后调用另一个注册方法
    public void AddEventLister(EEventType eventType, OnEventLister onHandler)
    {
        AddEventLister((int)eventType, onHandler);
    }

    // 注册指定整数类型事件监听器的方法，若事件成员不存在则创建并添加到字典中，然后添加监听器
    public void AddEventLister(int eventType, OnEventLister onHandler)
    {
        // 尝试从字典中获取指定事件类型的事件成员
        if (!handlerDic.TryGetValue(eventType, out var eventMember))
        {
            // 若未获取到，则从对象池获取一个新的事件成员实例
            eventMember = ClassPoolManger.Instance.Get<EventMember>();
            // 设置事件成员的事件类型
            eventMember.eventType = eventType;
            // 将事件成员添加到字典中
            handlerDic.Add(eventType, eventMember);
        }
        // 向事件成员中添加事件监听器
        eventMember.Add(onHandler);
    }

    // 注销
    // 注销指定枚举类型事件监听器的方法，将枚举类型转换为整数后调用另一个注销方法
    public void RemoveEventLister(EEventType eventType, OnEventLister onHandler)
    {
        RemoveEventLister((int)eventType, onHandler);
    }

    // 注销指定整数类型事件监听器的方法，若事件成员存在则移除监听器，并检查是否可以从字典中移除该事件成员
    public void RemoveEventLister(int eventType, OnEventLister onHandler)
    {
        // 尝试从字典中获取指定事件类型的事件成员
        if (handlerDic.TryGetValue(eventType, out var eventMember))
        {
            // 从事件成员中移除指定的事件监听器
            eventMember.Remove(onHandler);
            // 检查是否可以从字典中移除该事件成员
            CheckIfCanRemoveFromDic(eventMember);
        }
    }

    // 执行指定事件参数对应的事件监听器列表的方法
    void ExecuteHandlerList(EventSysArgsBase args)
    {
        // 尝试从字典中获取指定事件类型的事件成员
        if (handlerDic.TryGetValue(args.eventType, out var eventMember))
        {
            // 调用事件成员的 Send 方法触发事件监听器
            eventMember.Send(args);
            // 检查是否可以从字典中移除该事件成员
            CheckIfCanRemoveFromDic(eventMember);
        }
    }

    // 检查指定事件成员是否可以从字典中移除的方法，若监听器数量为 0 则移除并回收该事件成员
    void CheckIfCanRemoveFromDic(EventMember eventMember)
    {
        // 检查事件成员的监听器数量是否为 0
        if (eventMember.ListenerCount == 0)
        {
            // 尝试从字典中移除该事件成员
            if (handlerDic.Remove(eventMember.eventType, out var member))
            {
                // 将移除的事件成员回收到对象池
                member.RecycleToPool();
            }
        }

    }

    // 仅在 Unity 编辑器环境下生效的条件方法，用于打印事件系统的日志
    [Conditional("UNITY_EDITOR")]
    public void Log()
    {
        // 创建一个 StringBuilder 实例，用于拼接日志信息
        StringBuilder sb = new StringBuilder("[事件]事件系统Log\n");
        // 遍历字典中的所有事件成员
        foreach (var i in handlerDic.Values)
        {
            // 将事件成员的信息添加到日志中
            sb.AppendLine(i.ToString());
        }
        // 调用日志打印方法输出日志信息
        Logger.PrintLog(sb.ToString());
    }
}
