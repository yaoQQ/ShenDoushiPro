using System.Collections.Generic;
//using ProtoBufSpace;
//using ProtoBuf;

/// <summary>
/// Module 相关模块的消息传递、中转管理基类
/// </summary>
public abstract class BaseModule
{

    public BaseModule()
    {
      
    }

    public abstract ModuleEnum ModuleName();

    protected List<string> notificationList;

    protected Dictionary<uint, MessageReceive> netTProtocolIDData = new Dictionary<uint, MessageReceive>();

    public abstract List<string> GetRegisterNotificationList();


    public abstract void OnNotificationLister(string noticeType, BaseNotice notice);

    public abstract void InitRegisterNet();


    //public void RegisterNetMsg(TProtocol protoID)
    //{
    //    uint protoIDValue = (uint)protoID;
    //    netTProtocolIDData.Add(protoIDValue, OnNetMsgLister);
    //    NetworkEventManager.Instance.RegisterEventHandler(protoIDValue, OnNetMsgLister);
    //    //  JavaNetWorkManager.Instance.RegisterEventHandler(protoIDValue, OnJsonMsgLister);
    //}

    public void RegisterNetMsg(uint protoID)
    {
        netTProtocolIDData.Add(protoID, OnNetMsgLister);
        NetworkEventManager.Instance.RegisterEventHandler(protoID, OnNetMsgLister);
        //  JavaNetWorkManager.Instance.RegisterEventHandler(protoIDValue, OnJsonMsgLister);
    }
    public void RegisterNetMsg(protoEnum protoID)
    {
 
    }
    public abstract void OnNetMsgLister(uint protoID, byte[] buffer);
    public abstract void OnJsonMsgLister(uint protoID, string jsonData);

    public void SendNetMsg(string serverName, uint protoID)
    {
        //Package package = new Package(protoID, (int)ReturnCode.Success, ProtobufTool.PSerializer(msg));
        //NetworkManager.Instance.SendMessage(serverName, package);
    }
    /// <summary>
    /// java json to unity
    /// </summary>
    /// <param name="protoID"></param>
    /// <param name="jsonData"></param>
    public void SendToJavaMsg(string protoID, string  jsonData) {
      //  JsonPackage package = new JsonPackage(protoID, (int)ReturnCode.Success, jsonData);
       // NetworkManager.Instance.SendMsgToJavaMessage(package);
    }

    public bool RemoveNetMsg(uint protoID)
    {
        MessageReceive mr;
        if (netTProtocolIDData.TryGetValue(protoID, out mr))
        {
            NetworkEventManager.Instance.RemoveEventHandler(protoID, mr);
            netTProtocolIDData.Remove(protoID);
            return true;
        }
        return false;
    }

    public void resetModule()
    {
        if(notificationList!=null)
        {
            notificationList.Clear();
            notificationList = null;
        }
        foreach (KeyValuePair<uint, MessageReceive> kvp in netTProtocolIDData )
        {
            NetworkEventManager.Instance.RemoveEventHandler(kvp.Key, kvp.Value);
        }
        netTProtocolIDData.Clear();
    }
}
