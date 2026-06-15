using msg.chat;
using System.Collections.Generic;

public class ChatModel : BaseModel
{
    Dictionary<int, ChatChannelData> _channelDatas;
    public Dictionary<int, ChatChannelData> channelDatas
    {
        get
        {
            if (_channelDatas == null)
            {
                _channelDatas = new();
                foreach (var i in CHAT_DIC.Arr)
                {
                    channelDatas.Add(i.Id, new(i.Id));
                }
            }
            return _channelDatas;
        }
        set
        {
            _channelDatas = value;
        }
    }

    public void ClearData()
    {
        foreach (var i in channelDatas)
        {
            i.Value.ResetData();
        }
    }

    public ChatChannelData GetChannelData(EChatChannelType channelType)
    {
        return GetChannelData((int)channelType);
    }

    public ChatChannelData GetChannelData(int channelType)
    {
        return channelDatas[channelType];
    }

    // 优先打开有新消息的界面
    public EChatChannelType GetFirstShowType()
    {
        EChatChannelType chatChannelType = EChatChannelType.SelfServer;
        if (channelDatas[(int)EChatChannelType.PrivateChat].HasNewMessage)
        {
            chatChannelType = EChatChannelType.PrivateChat;
        }
        else if (channelDatas[(int)EChatChannelType.Guild].HasNewMessage)
        {
            chatChannelType = EChatChannelType.Guild;
        }
        else if (channelDatas[(int)EChatChannelType.Province].HasNewMessage)
        {
            chatChannelType = EChatChannelType.Province;
        }
        else if (channelDatas[(int)EChatChannelType.MutilServer].HasNewMessage)
        {
            chatChannelType = EChatChannelType.MutilServer;
        }
        else if (channelDatas[(int)EChatChannelType.SelfServer].HasNewMessage)
        {
            chatChannelType = EChatChannelType.SelfServer;
        }
        else
        {
            foreach (var i in channelDatas)
            {
                if (i.Value.HasNewMessage)
                {
                    chatChannelType = (EChatChannelType)i.Key;
                    break;
                }
            }
        }
        return chatChannelType;
    }

    // 添加新的聊天消息
    public void AddChatMessage(ChatBroadcastResp resp)
    {
        AddChatMessage(resp.channelId, resp.uniqueId, resp.messageInfo);
    }

    public void AddChatMessage(int channelId, long uniqueId, ChatMessageInfo messageInfo)
    {
        if (channelDatas.TryGetValue(channelId, out var channelData))
        {
            channelData.AddSingleMessage(uniqueId, messageInfo);
        }
        else
        {
            Logger.PrintError($"[聊天]无法处理聊天信息:{messageInfo.GetString()}");
        }
    }
}