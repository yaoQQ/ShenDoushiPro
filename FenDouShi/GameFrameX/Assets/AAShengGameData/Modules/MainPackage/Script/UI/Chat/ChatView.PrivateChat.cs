using Chat;
using FairyGUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;

// 私聊
public partial class ChatView
{
    GTreeNode rootNode;
    GTreeNode recentNode;
    G_PrivateChatList_TabBtn recentTab;
    GTreeNode friendNode;
    G_PrivateChatList_TabBtn friendTab;

    msg.friend.Friend showingFriend;

    List<msg.friend.Friend> friends = new();
    bool reqFriendList0;

    void OnLoadFinish_PrivateChat()
    {
        view.PlayerList.treeNodeWillExpand += OnTreeNodeWillExpand;
        view.PlayerList.onClickItem.Set(OnClikc_PlayerListItem);

        rootNode = view.PlayerList.rootNode;

        recentNode = new GTreeNode(true, G_PrivateChatList_TabBtn.URL);
        rootNode.AddChild(recentNode);
        recentTab = recentNode.cell as G_PrivateChatList_TabBtn;
        recentTab.Name_NotSelect.text = ChatString.recent;
        recentTab.Name_Select.text = ChatString.recent;

        friendNode = new GTreeNode(true, G_PrivateChatList_TabBtn.URL);
        rootNode.AddChild(friendNode);
        friendTab = friendNode.cell as G_PrivateChatList_TabBtn;
        friendTab.Name_NotSelect.text = ChatString.friend;
        friendTab.Name_Select.text = ChatString.friend;
    }

    void OnTreeNodeWillExpand(GTreeNode node, bool expand)
    {
        //Logger.PrintLog($"OnTreeNodeWillExpand:{node.cell},{expand}");

        if (expand)
        {
            var tabBtn = node.cell as G_PrivateChatList_TabBtn;
            if (tabBtn != null)
            {
                RefreshTab();
                if (tabBtn == recentTab)
                {
                    friendNode.expanded = false;
                    ShowRecentList();
                }
                else if (tabBtn == friendTab)
                {
                    recentNode.expanded = false;
                    if (reqFriendList0)
                    {
                        ShowFriendList();
                    }
                }
            }
        }
        else
        {
            node.RemoveChildren();
        }
    }

    void OnClikc_PlayerListItem(EventContext context)
    {
        //Logger.PrintLog($"OnClikc_PlayerListItem:{context.data}");

        var playerIcon = context.data as G_PrivateChatList_PlayerIcon;
        if (playerIcon != null)
        {
            var uniqueData = playerIcon.data as ChatUniqueData;
            if (uniqueData == null)
            {
                var friendData = playerIcon.data as msg.friend.Friend;
                if (friendData != null)
                {
                    foreach (var i in channelData.uniqueDatas.Values)
                    {
                        if (friendData.roleId == i.uniqueId)
                        {
                            uniqueData = i;
                            break;
                        }
                    }
                }
            }

            SelectUniqueData(uniqueData);
        }
        else
        {
            SelectUniqueData(null);
        }
    }

    #region 事件

    // 请求好友列表更新
    void EventFriendListUpdate(EventSysArgsBase argsBase)
    {
        if (!visible) return;
        if (view == null) return;
        if (argsBase == null || argsBase is not EventSysArgs<int> args || args.args1 != 4) return;

        reqFriendList0 = true;

        friends.Clear();
        var friendList = FriendControl.Instance.Model.GetAllFriendList();
        if (friendList != null)
        {
            friends.AddRange(friendList);
        }
        ShowFriendList();
        RefreshTab();
    }

    // 刷新红点
    void RefreshRedPoint()
    {
        RefreshTab();
        if (recentNode.expanded)
        {
            for (int i = 0; i < recentNode.numChildren; i++)
            {
                var child = recentNode.GetChildAt(i);
                var playerIcon = child.cell as G_PrivateChatList_PlayerIcon;
                var data = playerIcon.data as ChatUniqueData;
                playerIcon.SetInfo(data.roleInfo);
            }
        }
        else if (friendNode.expanded)
        {
            for (int i = 0; i < friendNode.numChildren; i++)
            {
                var child = friendNode.GetChildAt(i);
                var playerIcon = child.cell as G_PrivateChatList_PlayerIcon;
                var data = playerIcon.data as msg.friend.Friend;
                playerIcon.SetInfo(data.roleInfo);
            }
        }
    }

    #endregion

    void ShowPrivateChat()
    {
        RefreshTab();
        recentNode.expanded = true;
    }

    void ShowRecentList()
    {
        if (!recentNode.expanded) return;

        // 将私聊玩家放到最前
        List<ChatUniqueData> sortList = channelData.uniqueDatas.Values.ToList();
        var recentPlayerCount = ConfigMgr.GetGameConst("chat_player_limit")[0];
        if (sortList.Count > recentPlayerCount)
        {
            // 优先移除无未读消息的好友
            foreach (var i in friends)
            {
                var _uniqueData = sortList.Find(x => x.uniqueId == i.roleId);
                if (_uniqueData != null && _uniqueData.itemDatas.Count == 0)
                {
                    sortList.Remove(_uniqueData);

                    if (sortList.Count <= recentPlayerCount) break;
                }
            }
        }

        if (sortList.Count > recentPlayerCount)
        {
            // 其次移除无未读消息的陌生人
            for (int i = sortList.Count - 1; i >= 0; i--)
            {
                ChatUniqueData uniqueData = sortList[i];
                bool isFriend = friends.Exists(x => x.roleId == uniqueData.uniqueId);
                if (!isFriend && uniqueData.itemDatas.Count == 0)
                {
                    sortList.RemoveAt(i);

                    if (sortList.Count <= recentPlayerCount) break;
                }
            }
        }

        if (sortList.Count > recentPlayerCount)
        {
            // 再移除有未读消息的好友
            foreach (var i in friends)
            {
                var _uniqueData = sortList.Find(x => x.uniqueId == i.roleId);
                if (_uniqueData != null)
                {
                    sortList.Remove(_uniqueData);

                    if (sortList.Count <= recentPlayerCount) break;
                }
            }
        }

        if (sortList.Count > recentPlayerCount)
        {
            // 最后再移除有未读消息的陌生人
            for (int i = sortList.Count - 1; i >= 0; i--)
            {
                ChatUniqueData uniqueData = sortList[i];
                bool isFriend = friends.Exists(x => x.roleId == uniqueData.uniqueId);
                if (!isFriend)
                {
                    sortList.RemoveAt(i);

                    if (sortList.Count <= recentPlayerCount) break;
                }
            }
        }

        if (sortList.Count > recentPlayerCount)
        {
            sortList.RemoveRange(recentPlayerCount, sortList.Count - recentPlayerCount);
        }

        ChatUniqueData friendUniqueData = null;
        G_PrivateChatList_PlayerIcon recentTreeNode = null;
        if (showingFriend != null)
        {
            var index = sortList.FindIndex(x => x.uniqueId == showingFriend.roleId);
            if (index >= 0)
            {
                friendUniqueData = sortList[index];
                sortList.RemoveAt(index);
            }
        }
        // 按照发言时间排序
        //foreach(var i in sortList)
        //{
        //    Logger.PrintLog($"{i.uniqueId}, {i.itemDatas}");
        //}
        sortList.Sort(RecentPlayerSort);
        //foreach (var i in sortList)
        //{
        //    Logger.PrintLog($"---{i.uniqueId}, {i.itemDatas}");
        //}
        if (friendUniqueData != null)
        {
            sortList.Insert(0, friendUniqueData);
        }

        // 删除不存在列表里的人
        recentNode.RemoveChildren();

        foreach (ChatUniqueData i in sortList)
        {
            if (i.roleInfo == null) continue;

            GTreeNode treeNode = new GTreeNode(false, G_PrivateChatList_PlayerIcon.URL);
            recentNode.AddChild(treeNode);
            var playerIcon = treeNode.cell as G_PrivateChatList_PlayerIcon;
            playerIcon.data = i;
            playerIcon.SetInfo(i.roleInfo);

            if (friendUniqueData == i)
            {
                recentTreeNode = playerIcon;
            }
        }

        if (showingFriend != null)
        {
            showingFriend = null;
            // 选中
            var index = view.PlayerList.GetChildIndex(recentTreeNode);
            //Logger.PrintLog($"index:{index}");
            if (index >= 0)
            {
                view.PlayerList.selectedIndex = index;
                SelectUniqueData(friendUniqueData);
            }
        }
    }

    int RecentPlayerSort(ChatUniqueData x, ChatUniqueData y)
    {
        if (x == null || y == null)
        {
            if (x == null) return -1;
            if (y == null) return 1;
        }
        if (x.newMsgCount != 0 || y.newMsgCount != 0)
        {
            return y.newMsgCount.CompareTo(x.newMsgCount);
        }
        if (x.itemDatas.Count == 0 || y.itemDatas.Count == 0)
        {
            return y.itemDatas.Count.CompareTo(x.itemDatas.Count);
        }
        return y.itemDatas[y.itemDatas.Count - 1].SendTime.CompareTo(x.itemDatas[x.itemDatas.Count - 1].SendTime);
    }

    void ShowFriendList()
    {
        if (!friendNode.expanded) return;

        foreach (msg.friend.Friend i in friends)
        {
            var treeNode = new GTreeNode(false, G_PrivateChatList_PlayerIcon.URL);
            friendNode.AddChild(treeNode);
            var playerIcon = treeNode.cell as G_PrivateChatList_PlayerIcon;
            playerIcon.data = i;
            playerIcon.SetInfo(i.roleInfo);
        }
    }

    void RefreshTab()
    {
        if (recentTab == null || friendTab == null) return;

        // 最近
        int recentTabMaxCount = 0;
        int recentTabOnlineCount = 0;
        int recentRedPointCount = 0;
        foreach (var i in channelData.uniqueDatas.Values)
        {
            if (i.uniqueId <= 0) continue;
            if (i.roleInfo == null)
            {
                Logger.PrintError($"[聊天]聊天对象没有角色信息:{i.uniqueId}");
                continue;
            }

            recentTabMaxCount++;
            if (i.roleInfo.offlineTime <= 0)
            {
                recentTabOnlineCount++;
            }
            recentRedPointCount += i.newMsgCount;
        }
        string recentStr = $"({recentTabOnlineCount}/{recentTabMaxCount})";
        recentTab.Count_Select.text = recentStr;
        recentTab.Count_NotSelect.text = recentStr;

        recentTab.RedPoint.visible = recentRedPointCount > 0;
        if (recentTab.RedPoint.visible)
        {
            recentTab.RedPoint.count.text = recentRedPointCount.ToString();
        }

        // 好友红点
        int friendCount = friends.Count;
        int friendOnlineCount = 0;
        int friendRedPointCount = 0;
        foreach (var i in friends)
        {
            if (i.roleInfo.offlineTime <= 0)
            {
                friendOnlineCount++;
            }

            friendRedPointCount += channelData.GetNewMsgCount(i.roleId);
        }
        string countStr = $"({friendOnlineCount}/{friendCount})";
        friendTab.Count_Select.text = countStr;
        friendTab.Count_NotSelect.text = countStr;

        friendTab.RedPoint.visible = friendRedPointCount > 0;
        if (friendTab.RedPoint.visible)
        {
            friendTab.RedPoint.count.text = friendRedPointCount.ToString();
        }
    }
}