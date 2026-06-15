using Chat;
using FairyGUI;
using msg.chat;
using msg.common;
using System;

// 消息UI类型
public enum EChatMsgType : byte
{
    Time = 0,   // 时间
    System,     // 系统信息
    Other,      // 别人的信息
    Self,       // 我的信息
}

public abstract class ChatMsgDataBase
{
    public abstract EChatMsgType ChatMsgType { get; }
    public abstract string URL { get; }
    public abstract long SendTime { get; }

    public int channelId;
    public long uniqueId;

    public abstract void ItemRenderer(GObject item);
}

public class ChatMsgData_Time : ChatMsgDataBase
{
    public override EChatMsgType ChatMsgType => EChatMsgType.Time;
    public override string URL => G_ChatMessage_Time.URL;
    public override long SendTime => long.MaxValue;

    public long sendTime;
    public DateTime sendDateTime;
    public string sendTimeStr;

    public override void ItemRenderer(GObject item)
    {
        var timeCom = item as G_ChatMessage_Time;
        timeCom.time.text = sendTimeStr;
    }
}

public class ChatMsgData_System : ChatMsgDataBase
{
    public override EChatMsgType ChatMsgType => EChatMsgType.System;
    public override string URL => G_ChatMessage_Sys.URL;
    public override long SendTime => messageInfo.sendTime;

    public ChatMessageInfo messageInfo;
    public string text;

    public override void ItemRenderer(GObject item)
    {
        var sysCom = item as G_ChatMessage_Sys;
        sysCom.title.text = "            " + text;
    }
}

public class ChatMsgData_Other : ChatMsgDataBase
{
    public override EChatMsgType ChatMsgType => EChatMsgType.Other;
    public override string URL => G_ChatMessage_Other.URL;
    public override long SendTime => messageInfo.sendTime;

    public ChatMessageInfo messageInfo;

    public override void ItemRenderer(GObject item)
    {
        var otherCom = item as G_ChatMessage_Other;

        var roleInfo = messageInfo.senderInfo;
        otherCom.name.text = roleInfo.Name;
        otherCom.level.text = $"{roleInfo.Level}级";

        otherCom.content.text = messageInfo.Content;
    }
}

public class ChatMsgData_Self : ChatMsgDataBase
{
    public override EChatMsgType ChatMsgType => EChatMsgType.Self;
    public override string URL => G_ChatMessage_Self.URL;
    public override long SendTime => messageInfo.sendTime;

    public ChatMessageInfo messageInfo;

    public override void ItemRenderer(GObject item)
    {
        var selfCom = item as G_ChatMessage_Self;

        var roleInfo = messageInfo.senderInfo;
        selfCom.name.text = roleInfo.Name;
        selfCom.level.text = $"{roleInfo.Level}级";

        selfCom.content.text = messageInfo.Content;
    }
}