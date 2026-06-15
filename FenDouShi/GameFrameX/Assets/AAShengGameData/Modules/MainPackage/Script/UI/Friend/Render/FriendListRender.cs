//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using System.Linq;
using common;
using FairyGUI;
using FairyGUI.Utils;
using Friend;
using UnityEngine;

public class FriendListRenderData
{
    public int controllerIndex = 0; //列表控制器，等于类型
    public msg.friend.Friend friendData = null; // 好友数据
}

public class FriendListRender : BaseRender
{
    public new G_FriendListRender mRoot
    {
        get { return (G_FriendListRender)base.mRoot; }
    }

    public override string mPackageName => G_FriendListRender.PACKAGE_NAME;
    public override string mComponentName => G_FriendListRender.COMPONENT_NAME;

    //界面控制器
    private Controller mController;
    //好友数据
    private msg.friend.Friend mFriendData;

    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void onCreate()
    {
        mController = mRoot.GetController("option");
    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected override void onEventLister()
    {
        //点击申请好友更新
        On(EEventType.EventRecommendApplyedUpdate, OnRecommendApplyedUpdate);
        //好友友情点更新
        On<long[]>(EEventType.EventFriendDotUpdate, OnEventFriendDotUpdate);
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mRoot.addBtn)
        {
            //添加好友按钮
            FriendControl.Instance.ReqApplyReq(new long[] { mFriendData.roleId });
        }
        else if (clickedButton == mRoot.agreeBtn)
        {
            //同意按钮
            FriendControl.Instance.ReqHandleApplyReq(new long[] { mFriendData.roleId }, true);
        }
        else if (clickedButton == mRoot.refuseBtn)
        {
            //拒绝按钮
            FriendControl.Instance.ReqHandleApplyReq(new long[] { mFriendData.roleId }, false);
        }
        else if (clickedButton == mRoot.sendPointBtn)
        {
            //发送友情点
            var friendDotSendMax = ConfigMgr.GetGameConst("friend_dot_send_max")[0]; //好友收到点数上限
            var info = FriendControl.Instance.Model.GetFriendData();
            if (info != null)
            {
                if (info.sendDotCount >= friendDotSendMax)
                {
                    CommonViewUtils.ShowTopTips("已达发送上限");
                }
                else
                {
                    FriendControl.Instance.ReqSendDotReq(new long[] { mFriendData.roleId });
                }
            }
        }
        else if (clickedButton == mRoot.getPointBtn)
        {
            //收取友情点
            var friendDotReceiveMax = ConfigMgr.GetGameConst("friend_dot_receive_max")[0]; //好友收到点数上限
            var info = FriendControl.Instance.Model.GetFriendData();
            if (info != null)
            {
                if (info.receiveDotCount >= friendDotReceiveMax)
                {
                    CommonViewUtils.ShowTopTips("已达领取上限");
                }
                else
                {
                    FriendControl.Instance.ReqReceiveDotReq(new long[] { mFriendData.roleId });
                }
            }
        }
        else if (clickedButton == mRoot.deleteBtn)
        {
            //删除按钮
            if (mController.selectedIndex == 1)
            {
                //删除好友
                MessageBoxVo msgVo = new MessageBoxVo();
                msgVo.title = "提示";
                msgVo.msg = "是否从好友列表删除该好友？";
                msgVo.isCheckNoShowTodayKey = "friend_delete_no_show_today";
                msgVo.OkBtnfunc = () =>
                {
                    //删除好友请求
                    FriendControl.Instance.ReqDeleteReq(new long[] { mFriendData.roleId });
                };
                CommonViewUtils.ShowMessageBox(msgVo);
            }
            else if (mController.selectedIndex == 4)
            {
                //删除黑名单
                MessageBoxVo msgVo = new MessageBoxVo();
                msgVo.title = "提示";
                msgVo.msg = "是否要解除拉黑？";
                msgVo.isCheckNoShowTodayKey = "friend_black_delete_no_show_today";
                msgVo.OkBtnfunc = () =>
                {
                    //取消拉黑请求
                    FriendControl.Instance.ReqBlackReq(new long[] { mFriendData.roleId }, false);
                };
                CommonViewUtils.ShowMessageBox(msgVo);
            }
        }
        else if (clickedButton == mRoot.chatBtn)
        {
            //聊天按钮
            if (mController.selectedIndex == 0)
            {
                // 好友列表
                if (mFriendData != null)
                {
                    //打开聊天界面
                    UIViewManager.Instance.Show(UIViewEnum.ChatView, mFriendData);
                }
            }
        }
        else if (clickedButton == mRoot.selectDelte)
        {
            //多选删除按钮
            if (mRoot.selectDelte.selected)
            {
                //选中
                FriendControl.Instance.Model.DeleteSelect.Add(mFriendData.roleId);
            }
            else
            {
                //取消选中
                FriendControl.Instance.Model.DeleteSelect.Remove(mFriendData.roleId);
            }
        }
    }



    /// <summary>
    /// 刷新数据
    /// </summary>
    protected override void dataChanged()
    {
        // 数据变化时刷新
        var data = mData as FriendListRenderData;
        if (data != null)
        {
            mController.selectedIndex = data.controllerIndex;
            mFriendData = data.friendData;
            if (data.friendData != null)
            {
                LoadPlayerInfo(data.friendData);
            }

            //判断状态
            if (mController.selectedIndex == 5)
            {
                //当前状态为添加好友
                var appliedIds = FriendControl.Instance.Model.GetRecommendAppliedList();
                if (appliedIds != null && appliedIds.Contains(mFriendData.roleId))
                {
                    //已经申请过控制器状态改变
                    mController.selectedIndex = 6;
                }
            }
            else if (mController.selectedIndex == 0)
            {
                LoadDotInfo(); //刷新点赞状态
            }
            else if (mController.selectedIndex == 2)
            {
                //多选删除好友
                var state = FriendControl.Instance.Model.DeleteSelect.Contains(mFriendData.roleId);
                mRoot.selectDelte.selected = state;
            }
        }
    }

    /// <summary>
    /// 更新点赞状态
    /// </summary>
    private void LoadDotInfo()
    {
        //好友列表判断收取友情点
        var info = FriendControl.Instance.Model.GetFriendData();
        if (info != null)
        {
            if (mFriendData.hasFriendDot)
            {
                //已收取友情点
                mRoot.getPointBtn.grayed = false;
                mRoot.getPointBtn.touchable = true;
            }
            else
            {
                mRoot.getPointBtn.grayed = true;
                mRoot.getPointBtn.touchable = false;
            }

            if (mFriendData.sentFriendDot)
            {
                //已经发送友情点
                mRoot.sendPointBtn.grayed = true;
                mRoot.sendPointBtn.touchable = false;
            }
            else
            {
                mRoot.sendPointBtn.grayed = false;
                mRoot.sendPointBtn.touchable = true;
            }
        }
    }

    /// <summary>
    /// 加载好友信息
    /// </summary>
    /// <param name="friendData"></param>
    private void LoadPlayerInfo(msg.friend.Friend friendData)
    {
        //名字
        mRoot.playName.text = friendData.roleInfo.Name;
        //等级
        mRoot.levelLabel.text = friendData.roleInfo.Level + "";
        //战斗力
        mRoot.powerLabel.text = friendData.roleInfo.fightPower + "";
        //联盟
        var unionName = (friendData.roleInfo.unionName == null || friendData.roleInfo.unionName == "") ? "暂无" : friendData.roleInfo.unionName;
        mRoot.unionName.SetVar("unionName", unionName).FlushVars();
        //在线时长
        if (friendData.roleInfo.offlineTime <= 0)
        {
            mRoot.onlineTime.text = "在线";
            mRoot.onlineTime.color = ToolSet.ConvertFromHtmlColor("#489875");
        }
        else
        {
            mRoot.onlineTime.text = "上次在线：" + DateFormatUtil.GetBeforeTimeDesc(friendData.roleInfo.offlineTime);
            mRoot.onlineTime.color = ToolSet.ConvertFromHtmlColor("#94A1AD");
        }
    }

    /// <summary>
    /// 好友申请返回
    /// </summary>
    private void OnRecommendApplyedUpdate()
    {
        if (mData != null && mData is FriendListRenderData)
        {
            var data = mData as FriendListRenderData;
            if (data.controllerIndex == 5 && mFriendData != null)
            {
                var appliedIds = FriendControl.Instance.Model.GetRecommendAppliedList();
                if (appliedIds != null && appliedIds.Contains(mFriendData.roleId))
                {
                    //已经申请过控制器状态改变
                    mController.selectedIndex = 6;
                }
            }
        }
    }

    //友情点更新
    private void OnEventFriendDotUpdate(long[] ids)
    {
        if (mData != null && mFriendData != null && mController.selectedIndex == 0)
        {
            if (ids.Contains(mFriendData.roleId))
            {
                mFriendData = FriendControl.Instance.Model.GetFriend(mFriendData.roleId);
                LoadDotInfo(); //更新点赞状态
            }
        }
    }

}