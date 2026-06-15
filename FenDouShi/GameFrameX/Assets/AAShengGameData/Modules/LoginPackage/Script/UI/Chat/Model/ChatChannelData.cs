using msg.chat;
using System;
using System.Collections.Generic;
using System.Xml;

public class ChatChannelData
{
    public int channelId;
    public ChatVo DATA;
    public Dictionary<long, ChatUniqueData> uniqueDatas = new();    // 私聊,uniqueId。0表示频道,数字表示私聊
    public int NewMsgCount
    {
        get
        {
            int count = 0;
            foreach (var i in uniqueDatas)
            {
                count += i.Value.newMsgCount;
            }
            return count;
        }
    }

    public bool HasNewMessage => NewMsgCount > 0;

    public uint lastSendTimeStamp;
    public uint nextSendTimeStamp;

    public ChatChannelData(int channelId)
    {
        this.channelId = channelId;
        DATA = CHAT_DIC.DIC[channelId];
        // 默认私聊频道0
        uniqueDatas.Add(0, new ChatUniqueData(channelId, 0));
    }

    public bool HasMessage(long uniqueId)
    {
        if (uniqueDatas.TryGetValue(uniqueId, out var data))
        {
            return data.HasMessage();
        }
        return false;
    }

    public int GetMsgCount(long uniqueId)
    {
        if (uniqueDatas.TryGetValue(uniqueId, out var data))
        {
            return data.itemDatas.Count;
        }
        return 0;
    }

    public int GetNewMsgCount(long uniqueId)
    {
        if (uniqueDatas.TryGetValue(uniqueId, out var data))
        {
            return data.newMsgCount;
        }
        return 0;
    }

    public void ResetData()
    {
        // 清除除了0以外的频道
        var ints = ClassPoolManger.Instance.Get<List<long>>();
        foreach (var i in uniqueDatas)
        {
            if (i.Key != 0)
            {
                ints.Add(i.Key);
            }
            else
            {
                i.Value.ResetData();
            }
        }
        foreach (var i in ints)
        {
            uniqueDatas.Remove(i);
        }

        lastSendTimeStamp = nextSendTimeStamp = 0;
    }

    // 设置频道信息
    public void SetChannelMsg(ChatGetInfoResp resp)
    {
        if (resp.messageInfos == null) return;

        foreach (ChatMessageInfo i in resp.messageInfos)
        {
            var uniqueData = GetOrCreateUniqueData(resp.uniqueId);
            uniqueData.SetChannelMsg(i);
        }

        EventManager.Instance.Dispatch(EEventType.Chat_SetChannelMessage, channelId);
    }

    // 添加单条信息
    public void AddSingleMessage(long uniqueId, ChatMessageInfo messageInfo)
    {
        ChatUniqueData uniqueData = GetOrCreateUniqueData(uniqueId);
        if (uniqueId != 0)
        {
            uniqueData.roleInfo = messageInfo.senderInfo;
        }
        uniqueData.AddSingleMessage(messageInfo);
    }

    public ChatUniqueData GetOrCreateUniqueData(msg.common.RoleInfo roleInfo)
    {
        if (roleInfo == null) return null;

        if (!uniqueDatas.TryGetValue(roleInfo.roleId, out ChatUniqueData uniqueData))
        {
            uniqueData = new(channelId, roleInfo.roleId)
            {
                roleInfo = roleInfo
            };
            uniqueDatas.Add(roleInfo.roleId, uniqueData);
        }
        return uniqueData;
    }

    public ChatUniqueData GetOrCreateUniqueData(long uniqueId)
    {
        if (!uniqueDatas.TryGetValue(uniqueId, out ChatUniqueData uniqueData))
        {
            uniqueData = new(channelId, uniqueId);
            uniqueDatas.Add(uniqueId, uniqueData);
        }
        return uniqueData;
    }

    public ChatUniqueData GetUniqueData(long uniqueId)
    {
        if (uniqueDatas.TryGetValue(uniqueId, out var data))
        {
            return data;
        }
        return null;
    }

    // 已读
    public void SendRead(long uniqueId)
    {
        var uniqueData = GetUniqueData(uniqueId);
        if (uniqueData != null)
        {
            uniqueData.SetRead();
        }
    }

    public void SetReadResp(long uniqueId)
    {
        var uniqueData = GetUniqueData(uniqueId);
        if (uniqueData != null)
        {
            uniqueData.SetReadResp();
        }
    }
}