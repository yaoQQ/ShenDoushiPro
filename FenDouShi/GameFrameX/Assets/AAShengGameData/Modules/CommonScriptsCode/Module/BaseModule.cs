using System.Collections.Generic;


/// <summary>
/// Module 相关模块的消息传递、中转管理基类
/// </summary>
// 定义 Module 相关模块的消息传递、中转管理基类
public abstract class BaseModule
{
    // 构造函数，当前为空
    public BaseModule()
    {
      
    }

    // 抽象方法，用于获取模块名称
    public abstract ModuleEnum ModuleName();

    // 受保护的通知列表，用于存储通知类型
    protected List<int> notificationList;

    // 受保护的网络协议ID数据字典，键为协议ID，值为网络消息处理方法
    protected Dictionary<uint, WebMessageReceive> netTProtocolIDData = new Dictionary<uint, WebMessageReceive>();

    // 虚拟方法，用于获取已注册的通知列表
    public virtual List<int> GetRegisterNotificationList()
    {
        // 如果通知列表为空，则初始化一个新的列表
        notificationList ??= new ();
        // 返回通知列表
        return notificationList;
    }

    // 抽象方法，用于处理通知事件
    public abstract void OnNotificationLister(int noticeType, EventSysArgsBase notice);

    // 抽象方法，用于初始化网络消息注册
    public abstract void InitRegisterNet();

    // 注册网络消息处理方法
    public void RegisterNetMsg(uint protoID)
    {
        // 将协议ID和对应的网络消息处理方法添加到字典中
        netTProtocolIDData.Add(protoID, OnNetMsgLister);
        // 向 UnityWebSocketManager 实例注册网络消息处理方法
        UnityWebSocketManager.Instance.RegisterEventHandler(protoID, OnNetMsgLister);
        //socket方案
        //NetworkManager.Instance.RegisterNetMsg(protoID);
    }

    // 抽象方法，用于处理网络消息
    public abstract void OnNetMsgLister(uint protoID, byte[] buffer);

    // 静态方法，用于发送网络消息
    public static void SendNetMsg(uint protoID, object msg)
    {
       // Package package = new Package(protoID, (int)ReturnCode.Success, ProtobufTool.PSerializer(msg));
       // 通过 UnityWebSocketManager 实例异步发送网络消息
       UnityWebSocketManager.Instance.SendAsync(protoID, ProtobufTool.PSerializer(msg));
    }
    public virtual void OnLoginSuccess() { }    // 登录游戏
    public virtual void OnReconnect() { 
    
    
    }       // 断线重连
    public virtual void OnLogoutSuccess() { }   // 换号/退出登录/退出游戏
    public virtual void OnRefreshOnZero() { }   // 凌晨0点刷新
    // 移除指定协议ID的网络消息处理方法
    public bool RemoveNetMsg(uint protoID)
    {
        // 用于存储获取到的网络消息处理方法
        WebMessageReceive mr;
        // 尝试从字典中获取指定协议ID对应的网络消息处理方法
        if (netTProtocolIDData.TryGetValue(protoID, out mr))
        {
            // 从 UnityWebSocketManager 实例中移除该协议ID的网络消息处理方法
            UnityWebSocketManager.Instance.RemoveEventHandler(protoID, OnNetMsgLister);
            // 从字典中移除该协议ID对应的条目
            netTProtocolIDData.Remove(protoID);
            // 移除成功，返回 true
            return true;
        }
        // 未找到对应的协议ID，移除失败，返回 false
        return false;
    }

    // 重置模块，清理相关数据
    public virtual void resetModule()
    {
        // 如果通知列表不为空
        if(notificationList!=null)
        {
            // 清空通知列表
            notificationList.Clear();
            // 将通知列表置为 null
            notificationList = null;
        }
        // 遍历网络协议ID数据字典
        foreach (KeyValuePair<uint, WebMessageReceive> kvp in netTProtocolIDData )
        {
            // 从 UnityWebSocketManager 实例中移除该协议ID的网络消息处理方法
            UnityWebSocketManager.Instance.RemoveEventHandler(kvp.Key, kvp.Value);
        }
        // 通过 ModuleManager 实例清除当前模块的数据
        ModuleManager.Instance.Clear(ModuleName());
        // 清空网络协议ID数据字典
        netTProtocolIDData.Clear();
    }



}
