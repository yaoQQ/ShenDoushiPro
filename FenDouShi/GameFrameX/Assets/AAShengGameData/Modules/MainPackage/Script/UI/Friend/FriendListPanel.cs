//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using System.Collections.Generic;
using FairyGUI;
using Friend;

public class FriendListPanel : BaseRender
{
    public new G_FriendListPanel mRoot
    {
        get { return (G_FriendListPanel)base.mRoot; }
    }

    public override string mPackageName => G_FriendListPanel.PACKAGE_NAME;
    public override string mComponentName => G_FriendListPanel.COMPONENT_NAME;

    //界面控制器
    private Controller mPanelController;
    private int mControllerIndex;  // 控制器索引

    //好友列表
    private TableView<FriendListRender> listView;
    //当前好友列表
    private List<msg.friend.Friend> mFriendList;

    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void onCreate()
    {
        mPanelController = mRoot.GetController("opt");
        listView = new TableView<FriendListRender>(mRoot.friendList);
        // listView.setClickCallBack(onSelectItem);
    }

    /// <summary>
    /// 
    /// 注册事件监听
    /// </summary>
    protected override void onEventLister()
    {
        //列表更新
        On<int>(EEventType.EventFriendListUpdate, OnListUpdate);
        //好友功能基础信息更新
        On(EEventType.UpdateFriendDotInfo, OnFriendDotInfo);
    }

    //按钮点击
    protected override void OnButtonClick(GButton clickedButton)
    {
        base.OnButtonClick(clickedButton);
        if (clickedButton == mRoot.leftBtn)
        {
            // 左按钮点击切换控制器删除和普通好友状态
            if (mPanelController.selectedIndex == 1)
            {
                mPanelController.selectedIndex = 0;
                mControllerIndex = 0;
                LoadFriendList();
            }
            else
            {
                mPanelController.selectedIndex = 1;
                mControllerIndex = 1;
                LoadFriendList();
            }
        }
        else if (clickedButton == mRoot.rightBtn)
        {
            // 右按钮点击
            if (mPanelController.selectedIndex == 1)
            {
                //现在是一键删除模式，需要做一些处理
                if (mControllerIndex != 2)
                {
                    mControllerIndex = 2;
                    CheckCanSelectList();
                    LoadFriendList();
                }
                else
                {
                    //确认删除
                    var deleteIds = FriendControl.Instance.Model.DeleteSelect;
                    if (deleteIds.Count > 0)
                    {
                        MessageBoxVo msgVo = new MessageBoxVo();
                        msgVo.title = "提示";
                        msgVo.msg = "是否删除选中好友？";
                        msgVo.isCheckNoShowTodayKey = "friend_deleteAll_no_show_today";
                        msgVo.OkBtnfunc = () =>
                        {
                            FriendControl.Instance.ReqDeleteReq(deleteIds.ToArray());
                        };
                        CommonViewUtils.ShowMessageBox(msgVo);
                    }
                    else
                    {
                        CommonViewUtils.ShowTopTips("至少选择一位要被删除的好友");
                    }
                }
            }
            else
            {
                //一键赠领点击
                Logger.PrintDebug("一键领取");
                OneKeySendAndReceive();
            }
        }
    }

    /// <summary>
    /// 刷新数据
    /// </summary>
    protected override void dataChanged()
    {
        // 数据变化时刷新
        //重置选择器
        mPanelController.selectedIndex = 0;
        mControllerIndex = 0;
        listView.ScrollTop();
        var type = (ELeftTab)mData;
        if (type == ELeftTab.Friend)
        {
            LoadFriendList();
        }
        else if (type == ELeftTab.World)
        {
            LoadCrossFriendList();
        }

        LoadFriendInfo();
    }

    /// <summary>
    /// 加载好友列表
    /// </summary>
    private void LoadFriendList()
    {
        mFriendList = FriendControl.Instance.Model.GetFriendList();
        if (mFriendList == null)
        {
            FriendControl.Instance.ReqFriendListReq(0);
            return;
        }
        List<FriendListRenderData> listdatas = new List<FriendListRenderData>();
        for (int i = 0; i < mFriendList.Count; i++)
        {
            var d = new FriendListRenderData();
            d.controllerIndex = mControllerIndex; //申请列表
            d.friendData = mFriendList[i];
            listdatas.Add(d);
        }
        listView.setDatas(listdatas);
        //列表未空，默认切换控制器到0状态
        if (listdatas.Count <= 0)
        {
            mPanelController.selectedIndex = 0;
            mControllerIndex = 0;
        }
        //空列表提示
        mRoot.empty.visible = listdatas.Count == 0;
    }

    /// <summary>
    /// 加载跨服好友列表
    /// </summary>
    private void LoadCrossFriendList()
    {
        mFriendList = FriendControl.Instance.Model.GetCrossList();
        if (mFriendList == null)
        {
            FriendControl.Instance.ReqFriendListReq(1);
            return;
        }
        List<FriendListRenderData> listdatas = new List<FriendListRenderData>();
        for (int i = 0; i < mFriendList.Count; i++)
        {
            var d = new FriendListRenderData();
            d.controllerIndex = mControllerIndex;
            d.friendData = mFriendList[i];
            listdatas.Add(d);
        }
        listView.setDatas(listdatas);
        mRoot.empty.visible = listdatas.Count == 0;
    }

    /// <summary>
    /// 列表更新
    /// </summary>
    /// <param name="eventType"></param>
    private void OnListUpdate(int eventType)
    {
        if (eventType == 0)
        {
            //好友列表更新
            LoadFriendList();
        }
        else if (eventType == 1)
        {
            //跨服好友列表
            LoadCrossFriendList();
        }
    }


    /// <summary>
    /// 加载好友信息
    /// </summary>
    private void LoadFriendInfo()
    {
        var info = FriendControl.Instance.Model.GetFriendData();
        if (info != null)
        {
            var friendMax = ConfigMgr.GetGameConst("friend_max")[0]; //本服好友上限
            var friendCossMax = ConfigMgr.GetGameConst("friend_cross_max")[0]; //跨服好友上限
            var friendDotReceiveMax = ConfigMgr.GetGameConst("friend_dot_receive_max")[0]; //好友收到点数上限
            //设置好友数量
            var type = (ELeftTab)mData;
            if (type == ELeftTab.Friend)
            {
                mRoot.friendCount.text = "本服好友：" + info.friendCount + "/" + friendMax;
            }
            else if (type == ELeftTab.World)
            {
                mRoot.friendCount.text = "跨服好友：" + info.crossFriendCount + "/" + friendCossMax;
            }

            //设置收到点数
            var have = 0;
            mRoot.friendPoint.SetVar("have", have + "").SetVar("today", info.receiveDotCount + "/" + friendDotReceiveMax).FlushVars();
        }
    }

    /// <summary>
    /// 好友信息更新
    /// </summary>
    private void OnFriendDotInfo()
    {
        LoadFriendInfo();
    }

    /// <summary>
    /// 一键赠送和领取
    /// </summary>
    private void OneKeySendAndReceive()
    {
        if (mFriendList != null)
        {
            var getIds = new List<long>();
            var sendIds = new List<long>();
            var friendDotReceiveMax = ConfigMgr.GetGameConst("friend_dot_receive_max")[0]; //好友收到点数上限
            var friendDotSendMax = ConfigMgr.GetGameConst("friend_dot_send_max")[0]; //好友发出点数上限
            var friendDatainfo = FriendControl.Instance.Model.GetFriendData();
            var canGetCount = friendDotReceiveMax - friendDatainfo.receiveDotCount;
            var canSendCount = friendDotSendMax - friendDatainfo.sendDotCount;
            //根据最大赠送和获取点数限制筛选好友
            for (int i = 0; i < mFriendList.Count; i++)
            {
                var f = mFriendList[i];
                if (!f.sentFriendDot && canSendCount > 0)
                {
                    sendIds.Add(f.roleId);
                    canSendCount--;
                }

                if (f.hasFriendDot && canGetCount > 0)
                {
                    getIds.Add(f.roleId);
                    canGetCount--;
                }
            }
            //发出赠送和领取请求
            if (sendIds.Count > 0)
            {
                FriendControl.Instance.ReqSendDotReq(sendIds.ToArray());
            }

            if (getIds.Count > 0)
            {
                FriendControl.Instance.ReqReceiveDotReq(getIds.ToArray());
            }
            //没有可以赠送和收取时提示
            if (sendIds.Count == 0 && getIds.Count == 0)
            {
                CommonViewUtils.ShowTopTips("暂无可收送的友情点");
            }
        }
        else
        {
            CommonViewUtils.ShowTopTips("暂无可收送的友情点");
        }
    }

    /// <summary>
    /// 检查可以删除选中的好友列表
    /// </summary>
    private void CheckCanSelectList()
    {
        FriendControl.Instance.Model.DeleteSelect.Clear();
        //查找7天上线的好友自动勾选离线大于7天好友
    }
}