

using System.Collections.Generic;
using System.Linq;
using common;
using msg.friend;

public class FriendModel : BaseModel
{
    //友情信息
    private FriendDataResp mDotInfo;
    private List<msg.friend.Friend> mRecommendList; //推荐好友列表
    private long[] mRecommendAppliedList; //推荐好友申请列表
    private SearchResp mSerachData; //搜索数据
    private List<msg.friend.Friend> mApplyList; //申请列表
    private List<msg.friend.Friend> mBlackList; //黑名单列表
    private List<msg.friend.Friend> mFriendList; //好友列表
    private List<msg.friend.Friend> mCrossList; //跨服好友列表
    private List<msg.friend.Friend> mAllFriendList; //所有好友列表
    public List<long> DeleteSelect = new List<long>(); //删除选择列表

    // 初始化调用
    protected override void onInit()
    {

    }

    // 监听事件
    protected override void onEventListener()
    {
    }

    //设置好友列表
    public void SetFriendList(FriendListResp data)
    {
        if (data.Type == 0)
        {
            //好友列表
            mFriendList = data.Friends;
        }
        else if (data.Type == 1)
        {
            //跨服好友
            mCrossList = data.Friends;
        }
        else if (data.Type == 2)
        {
            //黑名单
            mBlackList = data.Friends;
        }
        else if (data.Type == 3)
        {
            //申请列表
            mApplyList = data.Friends;
        }
        else if (data.Type == 4)
        {
            mAllFriendList = data.Friends;
        }
        Dispatch(EEventType.EventFriendListUpdate, data.Type);
    }

    /// <summary>
    /// 获取所有好友列表
    /// </summary>
    /// <returns></returns>
    public List<msg.friend.Friend> GetAllFriendList()
    {
        return mAllFriendList;
    }

    /// <summary>
    /// 清空所有好友列表
    /// </summary>
    public void ClearAllFriendList()
    {
        mAllFriendList = null;
    }

    /// <summary>
    /// 获取好友列表
    /// </summary>
    /// <returns></returns>
    public List<msg.friend.Friend> GetFriendList()
    {
        return mFriendList;
    }

    /// <summary>
    /// 获取跨服好友列表
    /// </summary>
    /// <returns></returns>
    public List<msg.friend.Friend> GetCrossList()
    {
        return mCrossList;
    }

    /// <summary>
    /// 获取黑名单列表
    /// </summary>
    /// <returns></returns>
    public List<msg.friend.Friend> GetBlackList()
    {
        return mBlackList;
    }

    /// <summary>
    /// 获取申请列表
    /// </summary>
    /// <returns></returns>
    public List<msg.friend.Friend> GetApplyList()
    {
        return mApplyList;
    }

    //获取好友信息
    public msg.friend.Friend GetFriend(long id)
    {
        if (mFriendList != null)
        {
            return mFriendList.Find(f => f.roleId == id);
        }

        if (mCrossList != null)
        {
            return mCrossList.Find(f => f.roleId == id);
        }
        return null;
    }

    /// <summary>
    /// 设置友情信息
    /// </summary>
    /// <param name="info"></param>
    public void SetFriendData(FriendDataResp info)
    {
        mDotInfo = info;
        Dispatch(EEventType.UpdateFriendDotInfo);
    }

    /// <summary>
    /// 获取友情信息
    /// </summary>
    /// <returns></returns>
    public FriendDataResp GetFriendData()
    {
        return mDotInfo;
    }

    /// <summary>
    /// 更换获取友情点数
    /// </summary>
    public void UpdateReceiveDotCount(int count)
    {
        if (mDotInfo != null)
        {
            mDotInfo.receiveDotCount = count;
            Dispatch(EEventType.UpdateFriendDotInfo);
        }
    }

    /// <summary>
    /// 更新发送友情点数
    /// </summary>
    /// <param name="count"></param>
    public void UpdateSendDotCount(int count)
    {
        if (mDotInfo != null)
        {
            mDotInfo.sendDotCount = count;
            Dispatch(EEventType.UpdateFriendDotInfo);
        }
    }

    /// <summary>
    /// 更新点赞列表
    /// </summary>
    /// <param name="ids"></param>
    public void UpdateDot(long[] ids, bool isSend)
    {
        //更新点赞列表
        if (mFriendList != null)
        {
            UpdateDotList(mFriendList, ids, isSend);
        }

        if (mCrossList != null)
        {
            UpdateDotList(mCrossList, ids, isSend);
        }
        //通知更新
        Dispatch(EEventType.EventFriendDotUpdate, ids);
    }

    private void UpdateDotList(List<msg.friend.Friend> list, long[] ids, bool isSend)
    {
        list.ForEach(f =>
        {
            if (ids.Contains(f.roleId))
            {
                if (isSend)
                {
                    //赠送成功
                    f.sentFriendDot = true;
                }
                else
                {
                    //收到赠送
                    f.hasFriendDot = false;
                }

            }
        });
    }


    /// <summary>
    /// 更新好友点赞状态
    /// </summary>
    /// <param name="id"></param>
    public void UpdateFriendDot(long id)
    {
        if (mFriendList != null)
        {
            var friend = mFriendList.Find(f => f.roleId == id);
            friend.hasFriendDot = true;
        }

        if (mCrossList != null)
        {
            var friend = mCrossList.Find(f => f.roleId == id);
            friend.hasFriendDot = true;
        }

        Dispatch(EEventType.EventFriendDotUpdate, new long[] { id });
    }

    /**
     * 设置推荐好友列表
     * @param list 
     */
    public void SetRecommendList(List<msg.friend.Friend> list, long[] appliedIds)
    {
        mRecommendList = list;
        mRecommendAppliedList = appliedIds ?? new long[0];
        Dispatch(EEventType.EventRecommendListUpdate);
    }

    /// <summary>
    /// 获取推荐好友列表
    /// </summary>
    /// <returns></returns>
    public List<msg.friend.Friend> GetRecommendList()
    {
        return mRecommendList;
    }

    /// <summary>
    /// 清除好友系统列表
    /// </summary>
    public void ClearList()
    {
        mRecommendList = null;
        mRecommendAppliedList = null;
        mApplyList = null;
        mBlackList = null;
        mFriendList = null;
        mCrossList = null;
    }

    /// <summary>
    /// 获取推荐好友申请列表
    /// </summary>
    /// <returns></returns>
    public long[] GetRecommendAppliedList()
    {
        return mRecommendAppliedList;
    }

    //申请添加好友提交
    public void AddRecommend(long[] ids)
    {
        if (mRecommendAppliedList != null)
        {
            mRecommendAppliedList = mRecommendAppliedList.Concat(ids).ToArray();
            Dispatch(EEventType.EventRecommendApplyedUpdate, ids);
        }
    }

    /// <summary>
    /// 设置搜索数据
    /// </summary>
    public void SetSerachData(SearchResp data)
    {
        mSerachData = data;
        if (data.appliedLists != null)
        {
            AddRecommend(data.appliedLists);
        }
        Dispatch(EEventType.EventSerachDataUpdate);
    }

    public SearchResp GetSerachData()
    {
        return mSerachData;
    }

    /// <summary>
    /// 删除申请列表
    /// </summary>
    /// <param name="ids"></param>
    public void RemoveApplyList(long[] ids)
    {
        if (mApplyList != null)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                var id = ids[i];
                //删除申请列表中的id
                mApplyList.RemoveAll(f => f.roleId == id);
            }
            Dispatch(EEventType.EventFriendListUpdate, 3);
        }
    }

    /// <summary>
    /// 申请列表增加
    /// </summary>
    /// <param name="friend"></param>
    public void AddApplyList(msg.friend.Friend friend)
    {
        if (mApplyList != null)
        {
            mApplyList.Add(friend);
            Dispatch(EEventType.EventFriendListUpdate, 3);
        }
    }

    /// <summary>
    /// 增加黑名单
    /// </summary>
    /// <param name="friend"></param>
    public void UpdateBlackList(List<msg.friend.Friend> friend, bool isAdd)
    {
        if (mBlackList != null)
        {
            if (isAdd)
            {
                mBlackList.AddRange(friend);
            }
            else
            {
                mBlackList.RemoveAll(f => friend.Exists(f2 => f2.roleId == f.roleId));
            }

            Dispatch(EEventType.EventFriendListUpdate, 2);
        }
    }

    /// <summary>
    /// 增加好友
    /// </summary>
    /// <param name="friends"></param>
    public void AddFriendList(List<msg.friend.Friend> friends)
    {
        var state1 = false;
        var state2 = false;
        friends.ForEach(f =>
        {
            if (f.isCross)
            {
                //跨服好友
                if (mCrossList != null)
                {
                    mCrossList.Add(f);
                    state2 = true;
                }
            }
            else
            {
                //好友列表
                if (mFriendList != null)
                {
                    mFriendList.Add(f);
                    state1 = true;
                }
            }
        });
        //判断新增的类型，进行事件通知
        if (state1)
        {
            if (mDotInfo != null)
            {
                mDotInfo.friendCount = mFriendList.Count;
                Dispatch(EEventType.UpdateFriendDotInfo);
            }
            Dispatch(EEventType.EventFriendListUpdate, 0);
        }
        if (state2)
        {
            if (mDotInfo != null)
            {
                mDotInfo.crossFriendCount = mCrossList.Count;
                Dispatch(EEventType.UpdateFriendDotInfo);
            }
            Dispatch(EEventType.EventFriendListUpdate, 1);
        }
    }

    /// <summary>
    /// 删除好友列表
    /// </summary>
    /// <param name="ids"></param>
    public void RemoveFriendList(long[] ids)
    {
        var state1 = false;
        var state2 = false;
        //删除列表
        for (int i = 0; i < ids.Length; i++)
        {
            //删除好友列表中的id
            if (mFriendList != null)
            {
                mFriendList.RemoveAll(f => f.roleId == ids[i]);
                state1 = true;
            }
            //删除跨服好友列表中的id
            if (mCrossList != null)
            {
                mCrossList.RemoveAll(f => f.roleId == ids[i]);
                state2 = true;
            }
        }
        //判断新增的类型，进行事件通知
        if (state1)
        {
            if (mDotInfo != null)
            {
                mDotInfo.friendCount = mFriendList.Count;
                Dispatch(EEventType.UpdateFriendDotInfo);
            }
            Dispatch(EEventType.EventFriendListUpdate, 0);
        }
        if (state2)
        {
            if (mDotInfo != null)
            {
                mDotInfo.crossFriendCount = mCrossList.Count;
                Dispatch(EEventType.UpdateFriendDotInfo);
            }
            Dispatch(EEventType.EventFriendListUpdate, 1);
        }
    }
}