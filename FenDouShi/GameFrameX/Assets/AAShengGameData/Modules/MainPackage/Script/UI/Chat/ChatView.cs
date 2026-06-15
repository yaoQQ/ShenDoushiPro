using Chat;
using FairyGUI;
using msg.friend;
using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public partial class ChatView : BaseView
{
    public override string PackageName => G_ChatView.PACKAGE_NAME;
    public override string ComponentName => G_ChatView.COMPONENT_NAME;
    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {
        { EEventType.Chat_SetChannelMessage, Chat_SetChannelMessage },
        { EEventType.Chat_AddNewMessage, Chat_AddNewMessage },
        { EEventType.ChatReadChannelResp, ChatReadChannelResp },
        { EEventType.ChatSendMessageResp, ChatSendMessageResp },

        { EEventType.EventFriendListUpdate, EventFriendListUpdate },
    };

    G_ChatView view;

    // 聊天信息
    ChatChannelData channelData;
    ChatUniqueData uniqueData;

    CoolDownComponent coolDownCom;

    #region 生命周期

    public ChatView()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.ChatView, false);
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent;
        contentPane.MakeFullScreen();
        contentPane.AddRelation(GRoot.inst, RelationType.Size);

        view = gComponent as G_ChatView;
        closeButton = view.CloseBtn;
        view.CloseBtn_.onClick.Set(HideView);

        view.ChatChannelList.SetVirtual();
        view.ChatChannelList.itemRenderer = ChatChannel_ItemRenderer;
        view.ChatChannelList.onClickItem.Set(ChatChannel_OnClickItem);
        view.ChatChannelList.numItems = CHAT_DIC.Arr.Length;

        view.PlayerMessageList.SetVirtual();
        view.PlayerMessageList.itemProvider = MessageItemProvider;
        view.PlayerMessageList.itemRenderer = MessageItemRenderer;
        view.PlayerMessageList.numItems = 0;

        int inputTextCharLimit = ConfigMgr.GetGameConst("chat_message_word_limit")[0];
        view.InputText.promptText = Utility.Text.Format(ChatString.promptText, inputTextCharLimit);
        view.InputText.maxLength = inputTextCharLimit;

        // 发送聊天信息倒计时
        coolDownCom = ClassPoolManger.Instance.Get<CoolDownComponent>();
        coolDownCom.Init(view.CoolDownMask, view.CoolDownSecond);
        coolDownCom.onStart = OnSendBtnCoolDownStart;
        coolDownCom.onComplete = OnSendBtnCoolDownFinish;

        // 私聊
        OnLoadFinish_PrivateChat();
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == view.SendBtn)
        {
            if (channelData != null)
            {
                if (view.InputText.text.Length == 0)
                {
                    // TODO:tips
                }
                else
                {
                    uint nowTs = TimeManager.GetlocalTimeStamp();
                    if (nowTs > channelData.nextSendTimeStamp)
                    {
                        // TODO:屏蔽字库
                        ChatControl.Instance.ChatSendMessageReq(uniqueData.channelId, uniqueData.uniqueId, view.InputText.text);

                        view.SendBtn.enabled = false;
                        channelData.lastSendTimeStamp = nowTs;
                        channelData.nextSendTimeStamp = nowTs + (uint)channelData.DATA.SpeakCD;
                        coolDownCom.StartCoolDown(nowTs, channelData.nextSendTimeStamp);
                    }
                    else
                    {
                        Logger.PrintLog($"[聊天]发言cd冷却中：{TimeManager.GetDateTimeByUnixTime(nowTs)} -> {TimeManager.GetDateTimeByUnixTime(channelData.nextSendTimeStamp)}");
                    }
                }
            }
        }
        else if (clickedButton == view.EmojiBtn)
        {
            UIViewManager.Instance.Show(UIViewEnum.ChatEmojiView);
        }
    }

    protected override void DoShowAnimation()
    {
        OnShown();
        position = new Vector3(-width, position.y, position.z);
        TweenMoveX(0, 0.3f);
    }

    protected override void DoHideAnimation()
    {
        TweenMoveX(-width, 0.3f).OnComplete(this.HideWindowImmediately);
    }

    protected override void OnShown()
    {
        base.OnShown();

        EChatChannelType chatChannelType = ChatControl.Instance.Model.GetFirstShowType();

        msg.friend.Friend friendData = null;
        if (showArgs != null)
        {
            friendData = (msg.friend.Friend)showArgs;
            showArgs = null;
        }

        if (friendData != null)
        {
            chatChannelType = EChatChannelType.PrivateChat;
        }
        else
        {
            chatChannelType = ChatControl.Instance.Model.GetFirstShowType();
        }

        int index = CHAT_DIC.FindIndex(x => x.Id == (int)chatChannelType);
        if (index < 0) index = 0;

        view.ChatChannelList.selectedIndex = index;

        ShowView(chatChannelType, friendData);
    }

    protected override void OnHide()
    {
        base.OnHide();

        // channelData = null;
        recentNode.expanded = false;
        friendNode.expanded = false;
        coolDownCom?.Stop();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (coolDownCom != null)
        {
            coolDownCom.Destroy();
            coolDownCom.RecycleToPool();
            coolDownCom = null;
        }
    }

    #endregion

    #region UI事件

    // 频道列表
    void ChatChannel_ItemRenderer(int index, GObject item)
    {
        var DATA = CHAT_DIC.Arr[index];
        var channelData = ChatControl.Instance.Model.channelDatas[DATA.Id];
        G_ChatChancelBtn btn = item as G_ChatChancelBtn;

        btn.Name_Select.text = DATA.Name;
        btn.Name_NotSelect.text = DATA.Name;
        btn.icon.url = UIHelper.GetFguiUrl(G_ChatChancelBtn.PACKAGE_NAME, DATA.Icon);
        btn.Icon_Select.url = UIHelper.GetFguiUrl(G_ChatChancelBtn.PACKAGE_NAME, DATA.Icon);

        // 红点
        bool showRedPoint = channelData.NewMsgCount > 0;
        btn.RedPointIcon.visible = showRedPoint;
        if (showRedPoint)
        {
            btn.RedPointIcon.count.text = channelData.NewMsgCount.ToString();
        }
    }

    void ChatChannel_OnClickItem(EventContext context)
    {
        int index = view.ChatChannelList.GetChildIndex((GObject)context.data);
        if (index < 0) return;

        var DATA = CHAT_DIC.Arr[index];
        var type = (EChatChannelType)DATA.Id;
        ShowView(type, null);
    }

    // 聊天记录
    string MessageItemProvider(int index)
    {
        if (uniqueData != null)
        {
            return uniqueData.itemDatas[index].URL;
        }
        return string.Empty;
    }

    void MessageItemRenderer(int index, GObject item)
    {
        item.width = view.PlayerMessageList.width;

        if (uniqueData != null)
        {
            uniqueData.itemDatas[index].ItemRenderer(item);
        }
    }

    // 发送按钮冷却
    void OnSendBtnCoolDownStart()
    {
        if (view != null)
        {
            view.SendBtn.enabled = false;
        }
    }

    void OnSendBtnCoolDownFinish()
    {
        if (view != null)
        {
            view.SendBtn.enabled = true;
        }
    }

    #endregion

    #region 网络事件

    void Chat_SetChannelMessage(EventSysArgsBase argsBase)
    {
        if (argsBase is not EventSysArgs<int> args) return;
        if (channelData == null) return;
        if (view == null) return;

        //Logger.PrintLog($"[聊天]view接收频道信息,Chat_SetChannelMessage:channelId:{channelData.channelId} args1:{args.args1}");
        if (channelData.channelId != args.args1)
        {
            // 切频道了,只设置红点
            view.ChatChannelList.RefreshVirtualList();
        }
        else
        {
            // 当前频道,设置已读和信息
            //Logger.PrintLog($"[聊天]uniqueData:{uniqueData.uniqueId},聊天数量:{uniqueData.itemDatas.Count}");
            long uniqueId = uniqueData != null ? uniqueData.uniqueId : 0;
            channelData.SendRead(uniqueId);
            RefreshMessageList(false);
        }
    }

    void Chat_AddNewMessage(EventSysArgsBase argsBase)
    {
        if (argsBase is not EventSysArgs<ChatMsgDataBase> args) return;
        if (channelData == null) return;
        if (view == null) return;

        Logger.PrintLog("[聊天]view接收广播信息");

        long uniqueId = uniqueData.uniqueId;
        if ((channelData.channelId != (int)EChatChannelType.PrivateChat && args.args1.channelId == channelData.channelId)
            || (channelData.channelId == (int)EChatChannelType.PrivateChat && args.args1.channelId == channelData.channelId && args.args1.uniqueId == uniqueId))
        {
            Logger.PrintLog("[聊天]同一频道");
            // 如果是当前选择的频道，则刷新聊天内容
            channelData.SendRead(uniqueId);

            RefreshEmpty();
            var gList = view.PlayerMessageList;
            gList.numItems = uniqueData != null ? uniqueData.msgCount : 0;

            List<ChatMsgDataBase> itemDatas = uniqueData?.itemDatas;

            // 有信息,判断是否滚动到底部
            bool scrollBottom = false;
            if (itemDatas?.Count > 0)
            {
                var itemIndex = gList.ChildIndexToItemIndex(gList.numChildren);
                if (itemIndex >= itemDatas.Count)
                {
                    // 如果在界面底部,则滚动
                    scrollBottom = true;
                }
                else if (args.args1 is ChatMsgData_Self)
                {
                    // 如果是自己发言,则滚动
                    scrollBottom = true;
                }
            }

            if (scrollBottom)
            {
                gList.scrollPane.ScrollBottom(true);
            }
        }
        else
        {
            Logger.PrintLog("[聊天]非同一频道");
            // 切频道了,只设置红点
            view.ChatChannelList.RefreshVirtualList();

            if (channelData.channelId == (int)EChatChannelType.PrivateChat)
            {
                RefreshRedPoint();
                // Logger.PrintLog($"{recentNode.numChildren}");
                if (recentNode.numChildren != channelData.uniqueDatas.Count - 1)
                {
                    ShowRecentList();
                }
            }
        }
    }

    void ChatReadChannelResp(EventSysArgsBase argsBase)
    {
        if (argsBase is not EventSysArgs<int, long> args) return;
        if (view == null) return;

        view.ChatChannelList.RefreshVirtualList();
        if (channelData.channelId == (int)EChatChannelType.PrivateChat)
        {
            RefreshRedPoint();
        }
    }

    void ChatSendMessageResp(EventSysArgsBase argsBase)
    {
        if (view == null) return;

        view.InputText.text = string.Empty;
    }

    #endregion

    // 切换界面
    void ShowView(EChatChannelType channelType, msg.friend.Friend friend)
    {
        view.ChatChannelList.RefreshVirtualList();
        Logger.PrintLog($"[聊天]显示界面:{channelType}");
        channelData = ChatControl.Instance.Model.GetChannelData((int)channelType);
        if (friend == null)
            uniqueData = channelData.GetUniqueData(0);
        else
            uniqueData = channelData.GetOrCreateUniqueData(friend.roleInfo);

        bool showEmptyGuild = false;
        bool showLevelLimit = false;
        bool showPrivateChat = false;

        Vector2 msgListSize = new Vector2(1137, 836);
        Vector2 msgInputSize = new Vector2(1208, 153);

        switch (channelType)
        {
            case EChatChannelType.System:
                msgListSize.y = 928;
                break;
            case EChatChannelType.MutilServer:
                break;
            case EChatChannelType.SelfServer:
                break;
            case EChatChannelType.Guild:
                break;
            case EChatChannelType.PrivateChat:
                showPrivateChat = true;
                msgListSize.x = 821;
                msgInputSize.x = 883;
                break;
            case EChatChannelType.Province:
                break;
            default:
                break;
        }

        view.InputGroup.visible = channelType != EChatChannelType.System;
        view.InputGroupBg.size = msgInputSize;

        view.LevelLimitTxt.visible = showLevelLimit;
        view.PrivateChat.visible = showPrivateChat;

        view.EmptyGuild.visible = showEmptyGuild;

        view.PlayerMessageList.size = msgListSize;
        if (showPrivateChat)
        {
            // 私聊要请求一次好友列表
            reqFriendList0 = false;
            FriendControl.Instance.ReqFriendListReq(4);

            RefreshMessageList_0();

            showingFriend = friend;
            ShowPrivateChat();
        }
        else
        {
            SelectUniqueData(uniqueData);
        }

        coolDownCom.StartCoolDown(channelData.lastSendTimeStamp, channelData.nextSendTimeStamp);
    }

    // 选择聊天对象
    void SelectUniqueData(ChatUniqueData data)
    {
        if (data == null)
            uniqueData = channelData.uniqueDatas[0];
        else
            uniqueData = data;

        if (uniqueData.channelId != (int)EChatChannelType.PrivateChat
            || uniqueData.channelId == (int)EChatChannelType.PrivateChat && uniqueData.uniqueId != 0)
        {
            if (uniqueData.isSendGetInfo)
            {
                RefreshMessageList(false);

                // 已读
                channelData.SendRead(uniqueData.uniqueId);
            }
            else
            {
                RefreshMessageList_0();

                // 请求聊天记录
                ChatControl.Instance.ChatGetInfoReq(channelData.channelId, uniqueData.uniqueId);
            }
        }
        else
        {
            RefreshMessageList_0();
        }
    }

    // UI
    void RefreshMessageList_0()
    {
        if (view == null) return;

        view.PlayerMessageList.numItems = 0;

        RefreshEmpty();
    }

    void RefreshMessageList(bool anim)
    {
        if (view == null) return;

        view.PlayerMessageList.numItems = uniqueData.msgCount;
        view.PlayerMessageList.scrollPane.ScrollBottom(anim);

        RefreshEmpty();
    }

    void RefreshEmpty()
    {
        if (view == null) return;

        view.Empty.visible = uniqueData != null && !uniqueData.HasMessage();
    }
}