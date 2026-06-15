//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using System.Collections.Generic;
using common;
using FairyGUI;
using Friend;

public class FriendApplyPanel : BaseRender
{
    public new G_FriendApplyPanel mRoot
    {
        get { return (G_FriendApplyPanel)base.mRoot; }
    }

    public override string mPackageName => G_FriendApplyPanel.PACKAGE_NAME;
    public override string mComponentName => G_FriendApplyPanel.COMPONENT_NAME;

    /// <summary>
    /// 好友列表
    /// </summary>
    private TableView<FriendListRender> applyListView;

    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void onCreate()
    {
        applyListView = new TableView<FriendListRender>(mRoot.applyList);
    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected override void onEventLister()
    {
        //列表更新
        On<int>(EEventType.EventFriendListUpdate, OnListUpdate);
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mRoot.agreeBtn)
        {
            //全部同意
            var agreeList = new List<long>();
            var mapplyList = FriendControl.Instance.Model.GetApplyList();
            var friendMax = ConfigMgr.GetGameConst("friend_max")[0]; //本服好友上限
            var info = FriendControl.Instance.Model.GetFriendData();
            if (info != null)
            {
                friendMax = friendMax - info.friendCount; //好友上限
            }
            if (mapplyList != null)
            {
                if (friendMax <= 0)
                {
                    CommonViewUtils.ShowTopTips("好友上限已满，无法同意");
                    return;
                }
                for (int i = 0; i < mapplyList.Count; i++)
                {
                    if (friendMax > 0)
                    {
                        agreeList.Add(mapplyList[i].roleId);
                        friendMax--;
                    }
                }
            }

            if (agreeList.Count > 0)
            {
                FriendControl.Instance.ReqHandleApplyReq(agreeList.ToArray(), true);
            }
            else
            {
                CommonViewUtils.ShowTopTips("暂无申请列表");
            }
        }
        else if (clickedButton == mRoot.refuseBtn)
        {
            //全部拒绝
            var refuseList = new List<long>();
            var mapplyList = FriendControl.Instance.Model.GetApplyList();
            if (mapplyList != null)
            {
                for (int i = 0; i < mapplyList.Count; i++)
                {
                    refuseList.Add(mapplyList[i].roleId);
                }
            }

            if (refuseList.Count > 0)
            {
                FriendControl.Instance.ReqHandleApplyReq(refuseList.ToArray(), false);
            }
            else
            {
                CommonViewUtils.ShowTopTips("暂无申请列表");
            }
        }
    }

    protected override void dataChanged()
    {
        //加载好友列表
        LoadFriendList();
    }

    /// <summary>
    /// 加载好友列表
    /// </summary>
    private void LoadFriendList()
    {
        var mapplyList = FriendControl.Instance.Model.GetApplyList();
        if (mapplyList == null)
        {
            FriendControl.Instance.ReqFriendListReq(3);
            return;
        }
        List<FriendListRenderData> listdatas = new List<FriendListRenderData>();
        for (int i = 0; i < mapplyList.Count; i++)
        {
            var d = new FriendListRenderData();
            d.controllerIndex = 3; //申请列表
            d.friendData = mapplyList[i];
            listdatas.Add(d);
        }
        applyListView.setDatas(listdatas);
        mRoot.empty.visible = listdatas.Count == 0;
    }

    /// <summary>
    /// 列表更新
    /// </summary>
    /// <param name="eventType"></param>
    private void OnListUpdate(int eventType)
    {
        if (eventType == 3)
        {
            //申请列表更新
            LoadFriendList();
        }
    }

}