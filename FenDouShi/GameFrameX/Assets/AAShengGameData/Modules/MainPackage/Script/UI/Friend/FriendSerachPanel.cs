//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using common;
using FairyGUI;
using Friend;

public class FriendSerachPanel : BaseRender
{
    public new G_FriendSerachPanel mRoot
    {
        get { return (G_FriendSerachPanel)base.mRoot; }
    }

    public override string mPackageName => G_FriendSerachPanel.PACKAGE_NAME;
    public override string mComponentName => G_FriendSerachPanel.COMPONENT_NAME;

    /// <summary>
    /// 好友列表
    /// </summary>
    private TableView<FriendListRender> friendList;

    private List<msg.friend.Friend> mFriendList; //推荐好友列表
    private long[] mAppliedList; //推荐好友已申请列表

    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void onCreate()
    {
        friendList = new TableView<FriendListRender>(mRoot.serachList);
    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected override void onEventLister()
    {
        //推荐列表更新
        On(EEventType.EventRecommendListUpdate, OnEventListUpdate);
        On(EEventType.EventSerachDataUpdate, OnEventSerachDataUpdate);
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mRoot.changeBtn)
        {
            //换一批
            FriendControl.Instance.ReqRecommendListReq();
        }
        else if (clickedButton == mRoot.serachBtn)
        {
            //搜索按钮
            mRoot.inpuName.text = mRoot.inpuName.text.Trim();
            if (mRoot.inpuName.text.Length > 0)
            {
                FriendControl.Instance.ReqSearchReq(mRoot.inpuName.text);
            }
            else
            {
                CommonViewUtils.ShowTopTips("请输入玩家名称");
            }
        }
        else if (clickedButton == mRoot.addAllBtn)
        {
            //添加全部
            var list = mFriendList;
            var appliedIds = FriendControl.Instance.Model.GetRecommendAppliedList();
            var addIds = new List<long>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (appliedIds != null && appliedIds.Contains(item.roleId))
                    {
                        continue;
                    }
                    else
                    {
                        addIds.Add(item.roleId);
                    }
                }
            }
            if (addIds.Count > 0)
            {
                FriendControl.Instance.ReqApplyReq(addIds.ToArray());
            }
            else
            {
                CommonViewUtils.ShowTopTips("暂无可申请的好友");
            }
        }
    }


    protected override void dataChanged()
    {
        //加载好友列表
        LoadRecommendList();
        ///加载好友信息
        LoadFriendInfo();
        //清空搜索框
        mRoot.inpuName.text = "";
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
            mRoot.friendCountLabel.SetVar("friendCount", info.friendCount + "/" + friendMax).SetVar("crossCount", info.crossFriendCount + "/" + friendCossMax).FlushVars();
            //设置收到点数
            var have = 0;
            mRoot.friendPoint.SetVar("have", have+"").SetVar("today", info.receiveDotCount + "/"+ friendDotReceiveMax).FlushVars();
        }
    }


    /// <summary>
    /// 加载推荐好友列表
    /// </summary>
    private void LoadRecommendList()
    {
        var list = FriendControl.Instance.Model.GetRecommendList();
        if (list == null)
        {
            //暂无推荐好友列表，请求
            FriendControl.Instance.ReqRecommendListReq();
            return;
        }
        //设置数据
        mFriendList = list;
        mAppliedList = FriendControl.Instance.Model.GetRecommendAppliedList();
        LoadFriendList();
    }

    //推荐列表更新
    private void OnEventListUpdate()
    {
        LoadRecommendList();
    }

    //加载好友列表
    private void LoadFriendList()
    {
        //设置数据
        List<FriendListRenderData> listdatas = new List<FriendListRenderData>();
        for (int i = 0; i < mFriendList.Count; i++)
        {
            var d = new FriendListRenderData();
            d.controllerIndex = 5;
            d.friendData = mFriendList[i];
            listdatas.Add(d);
        }
        friendList.setDatas(listdatas);
        mRoot.empty.visible = listdatas.Count == 0;
    }

    /// <summary>
    /// 搜索结果更新
    /// </summary>
    /// <param name="argsBase"></param>
    private void OnEventSerachDataUpdate()
    {
        var data = FriendControl.Instance.Model.GetSerachData();
        if (data != null && data.searchLists != null && data.searchLists.Count > 0)
        {
            mFriendList = data.searchLists;
            mAppliedList = FriendControl.Instance.Model.GetRecommendAppliedList();
            LoadFriendList();
        }
        else
        {
            CommonViewUtils.ShowTopTips("暂无该玩家");
        }
    }

}