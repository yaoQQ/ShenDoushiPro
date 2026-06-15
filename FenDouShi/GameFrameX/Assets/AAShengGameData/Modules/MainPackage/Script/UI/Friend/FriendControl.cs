

using System;
using System.Diagnostics;
using System.Linq;
using common;
using msg.friend;


[ControlAttribute]
public class FriendControl : BaseControl<FriendControl>
{
    public FriendModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new FriendModel();
    }


    // 事件监听处理
    protected override void onEventListener()
    {
        //列表返回
        on<FriendListResp>((uint)Cmd.FriendListResp, OnFriendListResp);
        //新好友返回
        on<NewFriendResp>((uint)Cmd.NewFriendResp, OnNewFriendResp);
        //申请添加好友返回
        on<ApplyResp>((uint)Cmd.ApplyResp, OnApplyResp);
        //处理好友申请返回
        on<NewApplyResp>((uint)Cmd.NewApplyResp, OnNewApplyResp);
        //处理好友申请返回
        on<HandleApplyResp>((uint)Cmd.HandleApplyResp, OnHandleApplyResp);
        //删除好友返回
        on<DeleteResp>((uint)Cmd.DeleteResp, OnDeleteResp);
        //黑名单添加或删除请求返回
        on<BlackResp>((uint)Cmd.BlackResp, OnBlackResp);
        //推荐好友列表返回
        on<RecommendListResp>((uint)Cmd.RecommendListResp, OnRecommendListResp);
        //搜索好友返回
        on<SearchResp>((uint)Cmd.SearchResp, OnSearchResp);
        ////点赞返回
        on<SendDotResp>((uint)Cmd.SendDotResp, OnSendDotResp);
        //收取点赞返回
        on<ReceiveDotResp>((uint)Cmd.ReceiveDotResp, OnReceiveDotResp);
        //获取友情点信息返回
        on<FriendDataResp>((uint)Cmd.FriendDataResp, OnFriendDataResp);
        //友情点变化
        on<NewDotResp>((uint)Cmd.NewDotResp, OnNewDotResp);

    }

    protected override void onLoginSuccess()
    {
        Logger.PrintDebug("这里登录成功");
        ////请求友情点信息
        ReqFriendDataReq();
    }

    /// <summary>
    /// 请求好友列表
    /// </summary>
    /// <param name="type"></param>
    public void ReqFriendListReq(int type)
    {
        var msg = new FriendListReq();
        msg.Type = type;
        SendNetMsg((uint)Cmd.FriendListReq, msg);
    }

    /// <summary>
    /// 申请添加好友
    /// </summary>
    /// <param name="ids"></param>
    public void ReqApplyReq(long[] ids)
    {
        var msg = new ApplyReq();
        msg.ridLists = ids;
        SendNetMsg((uint)Cmd.ApplyReq, msg);
    }

    /// <summary>
    /// 处理好友申请
    /// </summary>
    /// <param name="rid"></param>
    /// <param name="agree"></param>
    public void ReqHandleApplyReq(long[] rids, bool agree)
    {
        var msg = new HandleApplyReq();
        msg.ridLists = rids;
        msg.Agree = agree;
        SendNetMsg((uint)Cmd.HandleApplyReq, msg);
    }

    /// <summary>
    /// 删除好友
    /// </summary>
    /// <param name="ids"></param>
    public void ReqDeleteReq(long[] ids)
    {
        var msg = new DeleteReq();
        msg.ridLists = ids;
        SendNetMsg((uint)Cmd.DeleteReq, msg);
    }

    /// <summary>
    /// 黑名单添加或删除请求
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="isAdd"></param>
    public void ReqBlackReq(long[] ids, bool isAdd)
    {
        var msg = new BlackReq();
        msg.ridLists = ids;
        msg.isAdd = isAdd;
        SendNetMsg((uint)Cmd.BlackReq, msg);
    }

    /// <summary>
    /// 推荐好友请求
    /// </summary>
    public void ReqRecommendListReq()
    {
        var msg = new RecommendListReq();
        SendNetMsg((uint)Cmd.RecommendListReq, msg);
    }

    /// <summary>
    /// 搜索好友请求
    /// </summary>
    /// <param name="name"></param>
    public void ReqSearchReq(string name)
    {
        var msg = new SearchReq();
        msg.Name = name;
        SendNetMsg((uint)Cmd.SearchReq, msg);
    }

    /// <summary>
    /// 发送点赞请求
    /// </summary>
    /// <param name="ids"></param>
    public void ReqSendDotReq(long[] ids)
    {
        var msg = new SendDotReq();
        msg.ridLists = ids;
        SendNetMsg((uint)Cmd.SendDotReq, msg);
    }

    /// <summary>
    /// 接收点赞请求
    /// </summary>
    /// <param name="ids"></param>
    public void ReqReceiveDotReq(long[] ids)
    {
        var msg = new ReceiveDotReq();
        msg.ridLists = ids;
        SendNetMsg((uint)Cmd.ReceiveDotReq, msg);
    }

    /// <summary>
    /// 请求友情点信息
    /// </summary>
    public void ReqFriendDataReq()
    {
        var msg = new FriendDataReq();
        SendNetMsg((uint)Cmd.FriendDataReq, msg);
    }

    /// <summary>
    /// 处理好友列表返回
    /// </summary>
    /// <param name="msg"></param>
    private void OnFriendListResp(FriendListResp msg)
    {
        Logger.PrintDebug("OnFriendListResp");
        Model.SetFriendList(msg);
    }

    /// <summary>
    /// 处理添加好友返回
    /// </summary>
    /// <param name="msg"></param>
    private void OnNewFriendResp(NewFriendResp msg)
    {
        Logger.PrintDebug("OnNewFriendResp");
        Model.AddFriendList(msg.newFriends);
    }

    /// <summary>
    /// 处理申请添加好友返回
    /// </summary>
    private void OnApplyResp(ApplyResp msg)
    {
        Logger.PrintDebug("OnApplyResp");
        if (msg.ridLists.Length > 0) {
            Model.AddRecommend(msg.ridLists);
        }
    }

    //// <summary>
    /// 处理处理好友申请返回
    /// </summary>
    private void OnNewApplyResp(NewApplyResp msg)
    {
        Logger.PrintDebug("OnNewApplyResp");
        Model.AddApplyList(msg.Apply);
    }

    /// <summary>
    /// 处理处理好友申请返回
    /// </summary>
    /// <param name="msg"></param>
    private void OnHandleApplyResp(HandleApplyResp msg)
    {
        Logger.PrintDebug("OnHandleApplyResp");
        Model.RemoveApplyList(msg.ridLists);
    }

    /// <summary>
    /// 处理删除好友返回
    /// </summary>
    /// <param name="msg"></param>
    private void OnDeleteResp(DeleteResp msg)
    {
        Logger.PrintDebug("OnDeleteResp");
        Model.RemoveFriendList(msg.ridLists);
    }

    /// <summary>
    /// 处理黑名单添加或删除请求返回
    /// </summary>
    /// <param name="msg"></param>
    private void OnBlackResp(BlackResp msg)
    {
        Logger.PrintDebug("OnBlackResp");
        Model.UpdateBlackList(msg.blackLists, msg.isAdd);
    }

    /// <summary>
    /// 处理推荐好友列表返回
    /// </summary>
    /// <param name="msg"></param>
    private void OnRecommendListResp(RecommendListResp msg)
    {
        Logger.PrintDebug("OnRecommendListResp");
        Model.SetRecommendList(msg.recommendLists, msg.appliedLists);
    }

    /// <summary>
    /// 处理搜索好友返回
    /// </summary>
    /// <param name="msg"></param>
    private void OnSearchResp(SearchResp msg)
    {
        Logger.PrintDebug("OnSearchResp");
        Model.SetSerachData(msg);
    }

    /// <summary>
    /// 处理点赞返回
    /// </summary>
    /// <param name="msg"></param>
    private void OnSendDotResp(SendDotResp msg)
    {
        Logger.PrintDebug("OnSendDotResp");
        Model.UpdateSendDotCount(msg.sendDotCount);
        Model.UpdateDot(msg.ridLists, true);
        CommonViewUtils.ShowTopTips("赠送成功");
    }

    /// <summary>
    /// 接受点赞返回
    /// </summary>
    /// <param name="msg"></param>
    private void OnReceiveDotResp(ReceiveDotResp msg)
    {
        Logger.PrintDebug("OnReceiveDotResp");
        Model.UpdateReceiveDotCount(msg.receiveDotCount);
        Model.UpdateDot(msg.ridLists, false);
    }

    /// <summary>
    /// 友情点信息
    /// </summary>
    /// <param name="msg"></param>
    private void OnFriendDataResp(FriendDataResp msg)
    {
        Logger.PrintDebug("OnDotInfoResp");
        Model.SetFriendData(msg);
    }

    /// <summary>
    /// 处理好友点赞
    /// </summary>
    /// <param name="msg"></param>
    private void OnNewDotResp(NewDotResp msg)
    {
        Logger.PrintDebug("OnNewDotResp");
        Model.UpdateFriendDot(msg.friendId);
    }
}
