using Chat;
using msg.chat;

public static class ChatExtension
{
    public static string GetString(this msg.common.RoleInfo roleInfo)
    {
        if (roleInfo != null)
            return $"玩家信息:{roleInfo.roleId},{roleInfo.Name},server:{roleInfo.serverId},{roleInfo.serverName},level:{roleInfo.Level},power:{roleInfo.fightPower},unionName:{roleInfo.unionName},official:{roleInfo.Official},offlineTime:{roleInfo.offlineTime}";
        return string.Empty;
    }

    public static string GetString(this ChatMessageInfo i)
    {
        return $"发送者:{i.senderInfo.GetString()}\n发送时间:{i.sendTime.GetSendTime()}, 聊天内容:{i.Content}, 消息类型:{i.msgType}";
    }

    public static bool ChannelIsOpen(int channelId)
    {
        return true;

        // TODO:
        if (channelId == 0) return true;
        if (!CHAT_DIC.DIC.TryGetValue(channelId, out var data)) return true;
        return SystemOpenControl.Instance.Model.GetIsSystemOpen(data.FunctionId);
    }

    public static void SetInfo(this G_PrivateChatList_PlayerIcon playerIcon, msg.common.RoleInfo roleInfo)
    {
        if (playerIcon == null || roleInfo == null) return;

        playerIcon.Name_Select.text = roleInfo.Name;
        playerIcon.Name_NotSelect.text = roleInfo.Name;
        playerIcon.level.text = $"{roleInfo.Level}级";
        playerIcon.State_Online.visible = roleInfo.offlineTime <= 0;
        playerIcon.State_Offline.visible = roleInfo.offlineTime > 0;
        if (playerIcon.State_Offline.visible)
        {
            var nowDateTime = TimeManager.GetlocalDateTime();
            var deleteTime = nowDateTime - roleInfo.offlineTime.GetDateTime();

            playerIcon.State_Offline.text = "离线";// TODO:
        }

        var channelData = ChatControl.Instance.Model.GetChannelData(EChatChannelType.PrivateChat);
        playerIcon.Player_RedPoint.visible = channelData.GetNewMsgCount(roleInfo.roleId) > 0;
        if (playerIcon.Player_RedPoint.visible)
        {
            playerIcon.Player_RedPoint.count.text = channelData.GetNewMsgCount(roleInfo.roleId).ToString();
        }
    }
}