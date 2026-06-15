using msg.chat;
using System.Text;
using System.Xml;

[Control]
public class ChatControl : BaseControl<ChatControl>
{
    public ChatModel Model { get; private set; }

    bool isLogin;

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new();
    }

    protected override void onEventListener()
    {
        on<ChatLoginResp>((uint)msg.chat.Cmd.ChatLoginResp, ChatLoginResp);
        on<ChatGetInfoResp>((uint)msg.chat.Cmd.ChatGetInfoResp, ChatGetInfoResp);
        on<ChatReadChannelResp>((uint)msg.chat.Cmd.ChatReadChannelResp, ChatReadChannelResp);
        on<ChatSendMessageResp>((uint)msg.chat.Cmd.ChatSendMessageResp, ChatSendMessageResp);
        on<ChatCloseChannelResp>((uint)msg.chat.Cmd.ChatCloseChannelResp, ChatCloseChannelResp);
        on<ChatBroadcastResp>((uint)msg.chat.Cmd.ChatBroadcastResp, ChatBroadcastResp);
        on<ChatGetChatRoleResp>((uint)msg.chat.Cmd.ChatGetChatRoleResp, ChatGetChatRoleResp);
    }

    protected override void onLoginSuccess()
    {
        Log("onLoginSuccess");
        isLogin = false;
    }

    // 登录聊天回调
    void ChatLoginResp(ChatLoginResp resp)
    {
        Log("重复调用登录回调");

        if (isLogin)
        {
            return;
        }
        isLogin = true;

        Model.ClearData();

        StringBuilder sb = new StringBuilder("登录回调\n");
        foreach (ChatChannelInfo i in resp.Infos)
        {
            var channelData = Model.GetChannelData(i.channelId);
            ChatUniqueData uniqueData = null;
            if (i.channelId == (int)EChatChannelType.PrivateChat && i.roleInfo != null)
            {
                uniqueData = channelData.GetOrCreateUniqueData(i.roleInfo);
            }
            else
            {
                uniqueData = channelData.GetOrCreateUniqueData(0);
            }
            uniqueData.newMsgCount = i.Num;
            sb.AppendLine($"频道id:{i.channelId},uniqueId:{uniqueData.uniqueId}, 未读信息数:{i.Num}, 发送人:{i.roleInfo.GetString()}");
        }
        Log(sb.ToString());
    }

    // 聊天频道获取聊天记录请求
    public void ChatGetInfoReq(int channelId, long uniqueId)
    {
        var channelData = Model.GetChannelData(channelId);
        var uniqueData = channelData.GetUniqueData(uniqueId);
        if (uniqueData == null || uniqueData.isSendGetInfo) return;

        Log($"获取聊天记录请求:channelId:{channelId}, uniqueId:{uniqueId}");
        ChatGetInfoReq req = new ChatGetInfoReq()
        {
            channelId = channelId,
            uniqueId = uniqueId
        };
        SendNetMsg((uint)Cmd.ChatGetInfoReq, req);
    }

    void ChatGetInfoResp(ChatGetInfoResp resp)
    {
        var channelData = Model.GetChannelData(resp.channelId);
        var uniqueData = channelData.GetUniqueData(resp.uniqueId);
        if (uniqueData == null || uniqueData.isSendGetInfo) return;

        //Log($"获取聊天记录回调:channelId:{resp.channelId}, uniqueId:{resp.uniqueId}");
#if UNITY_EDITOR
        StringBuilder sb = new StringBuilder($"获取聊天记录回调:channelId:{resp.channelId}, uniqueId:{resp.uniqueId}\n");
        if (resp.messageInfos != null)
        {
            foreach (ChatMessageInfo i in resp.messageInfos)
            {
                sb.AppendLine($"{i.GetString()}\n");
            }
        }
        Log(sb.ToString());
#endif

        uniqueData.isSendGetInfo = true;
        channelData.SetChannelMsg(resp);
    }

    // 聊天频道已读请求
    public void ChatReadChannelReq(int channelId, long uniqueId)
    {
        Log($"聊天频道已读请求:channelId:{channelId}, uniqueId:{uniqueId}");
        ChatReadChannelReq req = new ChatReadChannelReq()
        {
            channelId = channelId,
            uniqueId = uniqueId
        };
        SendNetMsg((uint)Cmd.ChatReadChannelReq, req);
    }

    void ChatReadChannelResp(ChatReadChannelResp resp)
    {
        Log($"聊天频道已读回调:channelId:{resp.channelId}, uniqueId:{resp.uniqueId}");
        var channelData = Model.GetChannelData(resp.channelId);
        channelData.SetReadResp(resp.uniqueId);

        EventManager.Instance.Dispatch(EEventType.ChatReadChannelResp, resp.channelId, resp.uniqueId);
    }

    // 聊天信息发送请求
    public void ChatSendMessageReq(int channelId, long uniqueId, string content)
    {
        Log($"聊天信息发送请求:channelId:{channelId}, uniqueId:{uniqueId},content:{content}");
        ChatSendMessageReq req = new ChatSendMessageReq()
        {
            channelId = channelId,
            uniqueId = uniqueId,
            Content = content
        };
        SendNetMsg((uint)Cmd.ChatSendMessageReq, req);
    }

    void ChatSendMessageResp(ChatSendMessageResp resp)
    {
        Log($"聊天信息发送回调:channelId:{resp.channelId}, uniqueId:{resp.uniqueId},content:{resp.Content}");
        EventManager.Instance.Dispatch(EEventType.ChatSendMessageResp);
    }

    // 聊天频道关闭请求
    public void ChatCloseChannelReq(int channelId, long uniqueId)
    {
        Log($"聊天频道关闭请求:channelId:{channelId}, uniqueId:{uniqueId}");
        ChatCloseChannelReq req = new ChatCloseChannelReq()
        {
            channelId = channelId,
            uniqueId = uniqueId,
        };
        SendNetMsg((uint)Cmd.ChatCloseChannelReq, req);
    }

    void ChatCloseChannelResp(ChatCloseChannelResp resp)
    {
        Log($"聊天频道关闭回调:channelId:{resp.channelId}, uniqueId:{resp.uniqueId}");
    }

    // 聊天广播回调
    void ChatBroadcastResp(ChatBroadcastResp resp)
    {
        Log($"聊天广播回调:channelId:{resp.channelId}, uniqueId:{resp.uniqueId}\n聊天记录:{resp.messageInfo.GetString()}");
        Model.AddChatMessage(resp);
    }

    // 获取聊天对象角色信息请求
    public void ChatGetChatRoleReq(long roleId)
    {
        Log($"获取聊天对象信息请求:roleId:{roleId}");
        ChatGetChatRoleReq req = new ChatGetChatRoleReq()
        {
            roleId = roleId,
        };
        SendNetMsg((uint)Cmd.ChatGetChatRoleReq, req);
    }

    void ChatGetChatRoleResp(ChatGetChatRoleResp resp)
    {
        Log($"获取聊天对象信息回调:{resp.Info.GetString()}");
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public void Log(string info)
    {
        Logger.PrintLog($"[聊天]{info}");
    }

    public void OnFriendListResp()
    {
    }
}