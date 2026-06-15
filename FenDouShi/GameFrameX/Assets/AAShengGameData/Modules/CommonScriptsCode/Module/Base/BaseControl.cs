


using FairyGUI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public interface INetEvent
{
    uint ProtoID { get; set; }
    void PDeserialize(byte[] buffer);
}

public class NetEvent<T> : INetEvent
{
    public uint ProtoID { get; set; }

    public void PDeserialize(byte[] buffer)
    {
        var obj = ProtobufTool.PDeserialize<T>(buffer);
        if (obj == null)
        {
            Logger.PrintError($"协议解析失败：PDeserialize failed ====> ProtoID:{ProtoID}");
            return;
        }
        // 使用 Newtonsoft.Json 将对象序列化为 JSON 字符串，打印对象所有内容
        if (UIConfig.isShowNetDebug && ProtoID != 101006)
        {
            string objJson = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            Logger.PrintDebug($"收取协议：protoID-->{ProtoID}  msg={objJson}");
        }
        Callback?.Invoke(obj);
    }

    public NetEventDataCallback<T> Callback { get; set; }
}

public delegate void NetEventDataCallback<T>(T resp);


public class BaseControl<T> where T : BaseControl<T>
{
    protected BaseControl() { }

    private static readonly Lazy<T> _lazyInstance =
        new Lazy<T>(() =>
        {
            var constructor = typeof(T).GetConstructor(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                null, Type.EmptyTypes, null);

            if (constructor == null)
                throw new InvalidOperationException("无法找到私有构造函数");

            return (T)constructor.Invoke(null);
        });


    public static T Instance => _lazyInstance.Value;

    private Dictionary<uint, INetEvent> mNetEventList = new Dictionary<uint, INetEvent>();


    private void init()
    {
        onInit();
        onEventListener();
        EventManager.Instance.AddEventLister(EEventType.Assembly_Invoke, OnAssemblyInvoke);
        EventManager.Instance.AddEventLister(EEventType.Assembly_Invoke_CallBack, OnAssemblyCallBack);
    }


    private void clear()
    {

        foreach (KeyValuePair<uint, INetEvent> kvp in mNetEventList)
        {
            // 从 UnityWebSocketManager 实例中移除该协议ID的网络消息处理方法
            UnityWebSocketManager.Instance.RemoveEventHandler(kvp.Value.ProtoID, OnNetMsgLister);
        }
        mNetEventList.Clear();
        EventManager.Instance.RemoveEventLister(EEventType.Assembly_Invoke, OnAssemblyInvoke);
        EventManager.Instance.RemoveEventLister(EEventType.Assembly_Invoke_CallBack, OnAssemblyCallBack);

        onClear();
        Debug.Log("==============>>>>>>> BaseControl clear");
    }

    //注册协议监听事件
    protected void on<T1>(uint protoID, NetEventDataCallback<T1> func) where T1 : ProtoBuf.IExtensible
    {
        UnityWebSocketManager.Instance.RegisterEventHandler(protoID, OnNetMsgLister);
        //创建一个协议事件对象
        var netEvent = new NetEvent<T1>();
        netEvent.ProtoID = protoID; //协议ID
        netEvent.Callback = func; //回调
        mNetEventList.Add(protoID, netEvent);
    }

    /// <summary>
    /// 移除指定协议ID的网络消息处理方法
    /// </summary>
    protected bool off(uint protoID)
    {
        // 尝试从字典中获取指定协议ID对应的网络消息处理方法
        if (mNetEventList.ContainsKey(protoID))
        {
            // 从 UnityWebSocketManager 实例中移除该协议ID的网络消息处理方法
            UnityWebSocketManager.Instance.RemoveEventHandler(protoID, OnNetMsgLister);
            // 从字典中移除该协议ID对应的条目
            mNetEventList.Remove(protoID);
            // 移除成功，返回 true
            return true;
        }
        // 未找到对应的协议ID，移除失败，返回 false
        return false;
    }

    private void OnNetMsgLister(uint protoID, byte[] buffer)
    {
        //判断协议监听事件对象里面是否包含该协议ID
        if (mNetEventList.TryGetValue(protoID, out var netEvent))
        {
            //解析协议数据
            netEvent.PDeserialize(buffer);
        }
    }


    // 发送网络消息
    protected void SendNetMsg(uint protoID, object msg)
    {
        // 通过 UnityWebSocketManager 实例异步发送网络消息
        if (UIConfig.isShowNetDebug && protoID != 101005)
        {
            string objJson = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
            Logger.PrintColor("yellow", $"发送协议：protoID-->{protoID}  msg={objJson}");
        }
        UnityWebSocketManager.Instance.SendAsync(protoID, ProtobufTool.PSerializer(msg));
    }

    /// <summary>
    /// 初始化时调用的方法,子类可以重写此方法进行初始化操作
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
    /// 退出登录时调用的方法,子类可以重写此方法进行清理操作
    /// </summary>
    protected virtual void onClear()
    {
    }

    /// <summary>
    /// 登录成功时调用的方法,子类可以重写此方法进行登录后的操作
    /// </summary>
    protected virtual void onLoginSuccess()
    {
    }

    /// <summary>
    /// 凌晨0点刷新时调用的方法,子类可以重写此方法进行刷新操作
    /// </summary>
    protected virtual void OnRefreshOnZero()
    {
    }



    public void InvokeAssemblyMethod(string controlName, int tag)
    {
        EventManager.Instance.Dispatch(EEventType.Assembly_Invoke, controlName, tag, GetType().Name);
    }

    protected void InvokeAssemblyCallback()
    {
        EventManager.Instance.Dispatch(EEventType.Assembly_Invoke_CallBack, GetType().Name, 0, "callback");
    }

    /// <summary>
    /// 鐢ㄤ簬璺ㄧ▼搴忛泦璋冪敤鐨勪簨浠剁洃鍚?鏂规??
    /// </summary>
    private void OnAssemblyInvoke(EventSysArgsBase eventArgs)
    {
        if (eventArgs != null && eventArgs is EventSysArgs<string, int, string> args && args.args1 == GetType().Name)
        {
            OnAssembly(args.args2, args.args3);
        }
    }


    /// <summary>
    /// 璺ㄧ▼搴忛泦琚?璋冪敤鏃惰Е鍙戠殑鏂规硶锛屽瓙绫诲彲浠ラ噸鍐欐?ゆ柟娉曚互澶勭悊鐗瑰畾鐨勮皟鐢ㄩ�昏緫锛屽畾涔変笉鍚岀殑tag鍊间互鍖哄垎涓嶅悓鐨勮皟鐢ㄥ満鏅?
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="callControlName"></param>
    protected virtual void OnAssembly(int tag, string callControlName)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventArgs"></param>
    protected virtual void OnAssemblyCallBack(EventSysArgsBase eventArgs)
    {

    }
}
