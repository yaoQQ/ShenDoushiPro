using msg.chat;
using System;
using System.Collections.Generic;

public class ChatUniqueData
{
    public int channelId;
    public long uniqueId;
    public msg.common.RoleInfo roleInfo;

    public List<ChatMsgDataBase> itemDatas = new();
    public int msgCount => itemDatas.Count;
    public int newMsgCount;

    public bool isSendGetInfo;

    public ChatUniqueData(int channelId, long uniqueId)
    {
        this.channelId = channelId;
        this.uniqueId = uniqueId;

        // 系统频道不用请求聊天信息
        isSendGetInfo = channelId == (int)EChatChannelType.System;
    }

    public void SetChannelMsg(ChatMessageInfo messageInfo)
    {
        AddMessage(messageInfo);
    }

    public void AddSingleMessage(ChatMessageInfo messageInfo)
    {
        var newMsg = AddMessage(messageInfo);
        if (newMsg != null)
        {
            newMsgCount++;
            EventManager.Instance.Dispatch(EEventType.Chat_AddNewMessage, newMsg);
        }
    }

    ChatMsgDataBase CreateTimeData(long sendTime)
    {
        var nowDateTime = TimeManager.GetlocalDateTime();

        ChatMsgData_Time time = ClassPoolManger.Instance.Get<ChatMsgData_Time>();
        time.sendTime = sendTime;
        time.sendDateTime = time.sendTime.GetDateTime();
        if (time.sendDateTime.Year != nowDateTime.Year)
        {
            time.sendTimeStr = time.sendTime.GetTimeString("yyyy MM-dd HH:mm");
        }
        else if (time.sendDateTime.DayOfYear != nowDateTime.DayOfYear)
        {
            time.sendTimeStr = time.sendTime.GetTimeString("MM-dd HH:mm");
        }
        else
        {
            time.sendTimeStr = time.sendTime.GetTimeString("HH:mm");
        }

        return time;
    }

    ChatMsgDataBase AddMessage(ChatMessageInfo messageInfo)
    {
        uint newTimeStamp = messageInfo.sendTime.LongTimeStampToUint();
        DateTime sendDateTime = messageInfo.sendTime.GetDateTime();

        // 是否显示时间
        if (itemDatas.Count == 0)
        {
            var timeData = CreateTimeData(messageInfo.sendTime);
            itemDatas.Add(timeData);
        }
        else
        {
            bool addTimeData = false;
            var lastData = itemDatas[itemDatas.Count - 1];
            uint lastSendTimeStamp = lastData.SendTime.LongTimeStampToUint();
            addTimeData = MathF.Max(0, newTimeStamp - lastSendTimeStamp) > ChatConst.showTimeIntervalSeconds;
            if (addTimeData)
            {
                var timeData = CreateTimeData(messageInfo.sendTime);
                itemDatas.Add(timeData);
            }
        }

        // 添加消息
        ChatMsgDataBase newMsg = null;
        if ((EChatChannelType)channelId == EChatChannelType.System)
        {
            var systemMsg = ClassPoolManger.Instance.Get<ChatMsgData_System>();
            systemMsg.messageInfo = messageInfo;
            if (messageInfo.Content.Contains(ChatString.system))
            {
                systemMsg.text = messageInfo.Content.Replace(ChatString.system, string.Empty);
            }
            else
            {
                systemMsg.text = messageInfo.Content;
            }
            newMsg = systemMsg;
        }
        else
        {
            switch (messageInfo.msgType)
            {
                case MsgType.Normal:
                    if (messageInfo.senderInfo != null)
                    {
                        if (messageInfo.senderInfo.roleId == LoginControl.Instance.Model.RoleId)
                        {
                            var selfMsg = ClassPoolManger.Instance.Get<ChatMsgData_Self>();
                            selfMsg.messageInfo = messageInfo;
                            newMsg = selfMsg;
                        }
                        else
                        {
                            var otherMsg = ClassPoolManger.Instance.Get<ChatMsgData_Other>();
                            otherMsg.messageInfo = messageInfo;
                            newMsg = otherMsg;
                        }
                    }
                    break;
                case MsgType.UnionRecruit:
                    break;
                case MsgType.UnionInvite:
                    break;
                case MsgType.SystemMessage:
                    var systemMsg = ClassPoolManger.Instance.Get<ChatMsgData_System>();
                    systemMsg.messageInfo = messageInfo;
                    newMsg = systemMsg;
                    break;
                default:
                    Logger.PrintError($"[聊天]未处理的消息类型:{messageInfo.msgType}");
                    break;
            }
        }

        if (newMsg != null)
        {
            newMsg.channelId = channelId;
            newMsg.uniqueId = uniqueId;

            itemDatas.Add(newMsg);
        }

        return newMsg;
    }

    public bool HasMessage() => itemDatas.Count > 0;

    public void SetRead()
    {
        if (newMsgCount > 0)
        {
            ChatControl.Instance.ChatReadChannelReq(channelId, uniqueId);
        }
    }

    public void SetReadResp()
    {
        newMsgCount = 0;
    }

    public void ResetData()
    {
        itemDatas.Clear();
        newMsgCount = 0;

        isSendGetInfo = channelId == (int)EChatChannelType.System;
    }
}