//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using System.Collections.Generic;
using common;
using Friend;

public class FriendBlackPanel : BaseRender
{
    public new G_FriendBlackPanel mRoot
    {
        get { return (G_FriendBlackPanel)base.mRoot; }
    }

    public override string mPackageName => G_FriendBlackPanel.PACKAGE_NAME;
    public override string mComponentName => G_FriendBlackPanel.COMPONENT_NAME;

    /// <summary>
    /// 好友列表
    /// </summary>
    private TableView<FriendListRender> blackListView;

    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void onCreate()
    {
        blackListView = new TableView<FriendListRender>(mRoot.blackList);
    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected override void onEventLister()
    {
        //列表更新
        On<int>(EEventType.EventFriendListUpdate, OnListUpdate);
    }

    /// <summary>
    /// 刷新数据
    /// </summary>
    protected override void dataChanged()
    {
        // 数据变化时刷新
        LoadBlackList();
    }

    /// <summary>
    /// 加载好友列表
    /// </summary>
    private void LoadBlackList()
    {
        var mblackList = FriendControl.Instance.Model.GetBlackList();
        if (mblackList == null)
        {
            FriendControl.Instance.ReqFriendListReq(2);
            return;
        }
        List<FriendListRenderData> listdatas = new List<FriendListRenderData>();
        for (int i = 0; i < mblackList.Count; i++)
        {
            var d = new FriendListRenderData();
            d.controllerIndex = 4; //黑名单
            d.friendData = mblackList[i];
            listdatas.Add(d);
        }
        blackListView.setDatas(listdatas);
        mRoot.empty.visible = listdatas.Count == 0;

        //黑名单数量
        var maxCount = ConfigMgr.GetGameConst("friend_black_max")[0];
        mRoot.blackCountLabel.SetVar("count", listdatas.Count+"/"+maxCount).FlushVars();
    }

    /// <summary>
    /// 列表更新
    /// </summary>
    /// <param name="eventType"></param>
    private void OnListUpdate(int eventType)
    {
        if (eventType == 2)
        {
            //黑名单列表更新
            LoadBlackList();
        }
    }

}