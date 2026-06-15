

using msg.rank;
using System.Collections.Generic;
using System.Linq;


[ControlAttribute]
public class RankControl : BaseControl<RankControl>
{
    public RankModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new RankModel();
    }
    protected override void onLoginSuccess()
    {
        ReqInitInfo();
    }

    // 事件监听处理
    protected override void onEventListener()
    {
        on<RankInfoResp>((uint)Cmd.RankInfoResp, OnRankInfoResp);// 排行榜信息回调
        on<RankBaseListResp>((uint)Cmd.RankBaseListResp, OnRankBaseListResp);// 排行榜基本信息列表回调
        on<RankListResp>((uint)Cmd.RankListResp, OnRankListResp);// 单个排行榜回调
        on<RankLikeResp>((uint)Cmd.RankLikeResp, OnRankLikeResp);// 排行榜点赞回调
        on<RankProgressRewardResp>((uint)Cmd.RankProgressRewardResp, OnRankProgressRewardResp);// 进度奖励回调
        on<RankProgressInfoResp>((uint)Cmd.RankProgressInfoResp, OnRankProgressInfoResp);// 进度信息回调
        on<RankProgressDetailResp>((uint)Cmd.RankProgressDetailResp, OnRankProgressDetailResp);// 排行榜进度前五信息回调
    }


    public void ReqInitInfo()
    {
        ReqRankInfo();
        ReqRankBaseList();
    }

    /// <summary>RankProgressDetailResp
    /// 请求排行榜信息
    /// </summary>
    public void ReqRankInfo()
    {
        var msg = new RankInfoReq();
        SendNetMsg((uint)Cmd.RankInfoReq, msg);
    }

    /// <summary>
    /// 排行榜基本信息列表请求
    /// </summary>
    public void ReqRankBaseList()
    {
        var msg = new RankBaseListReq();
        SendNetMsg((uint)Cmd.RankBaseListReq, msg);
    }

    /// <summary>
    /// 单个排行榜请求
    /// </summary>
    /// <param name="rankId"></param>
    public void ReqRankList(int rankId)
    {
        var msg = new RankListReq() { Id = rankId };
       SendNetMsg((uint)Cmd.RankListReq, msg);
    }

    /// <summary>
    /// 排行榜点赞请求
    /// </summary>
    /// <param name="rankId"></param>
    /// <param name="roleKey"></param>
    public void ReqRankLike(int rankId, long Key)
    {
        //var roleId = RoleData.Instance.getRoleInfo().roleId;
        //if (roleId == Key)
        //{
        //    UIHelper.Alert("不能给自己点赞");
        //    return;
        //}
        var msg = new RankLikeReq() { Id = rankId, Key = Key };
        SendNetMsg((uint)Cmd.RankLikeReq, msg);
    }

    /// <summary>
    /// 进度奖励请求
    /// </summary>
    /// <param name="rankId"></param>
    /// <param name="progressIds"></param>
    public void ReqRankProgressReward(int rankId, int[] progressIds)
    {
        var msg = new RankProgressRewardReq() { Id = rankId, progressIds = progressIds };
        SendNetMsg((uint)Cmd.RankProgressRewardReq, msg);
    }


    private Dictionary<string, long> reqTimer;
    private const int REQ_CD = 60;

    private bool GetIsReqNew(string flag,int cd = REQ_CD)
    {
        reqTimer ??= new Dictionary<string, long>();
        var now = TimeManager.GetServerUnixTime();
        if (reqTimer.TryGetValue(flag, out var last))
        {
            if (now - last < cd) return false;
        }
        reqTimer[flag] = now;
        return true;
    }

    /// <summary>
    /// 进度信息请求
    /// </summary>
    /// <param name="rankId"></param>
    /// <param name="roleKey"></param>
    public void ReqRankProgress(int rankId)
    {
        var msg = new RankProgressInfoReq() { Id = rankId };
        var info = Model.GetRankProgressInfo(rankId);
        var flag = Utility.Text.Format("ReqRankProgress_{0}", rankId);
        if (info != null && !GetIsReqNew(flag))
        {
            EventManager.Instance.Dispatch(EEventType.RankProgressEvent, info);
            return;
        }
        SendNetMsg((uint)Cmd.RankProgressInfoReq, msg);
    }

    /// <summary>
    /// 排行榜进度前五信息请求
    /// </summary>
    /// <param name="rankId"></param>
    /// <param name="roleKey"></param>
    public void ReqRankProgressTopFive(int progressId)
    {
        var msg = new RankProgressDetailReq() { progressId = progressId };
        SendNetMsg((uint)Cmd.RankProgressDetailReq, msg);
    }

    //-------------------------------------------------

    /// <summary>
    /// 进度奖励回调
    /// </summary>
    /// <param name="resp"></param>
    private void OnRankProgressRewardResp(RankProgressRewardResp resp)
    {
        Model.SetRankProgressReward(resp);
        //更新基本信息
        ReqRankBaseList();
    }

    private void OnRankProgressInfoResp(RankProgressInfoResp resp)
    {
        Model.SetRankProgressInfo(resp);
    }


    private void OnRankProgressDetailResp(RankProgressDetailResp resp)
    {
        EventManager.Instance.Dispatch(EEventType.RankTopFiveEvent, resp);
    }

    private void OnRankLikeResp(RankLikeResp resp)
    {
        //“xxx（点赞玩家的id）在主线排行（排行榜名称）点赞了你
        //var rankCfg = Model.GetRankCfg(resp.Id);
        //飘字系统处理
        //UIHelper.Alert(Utility.Text.Format("玩家{0}在{1}点赞了你", resp.Key, rankCfg.Name));
        // 排行榜点赞回调 是否要更新 最新数据??
        //EventManager.Instance.Dispatch(EEventType.RankLikeEvent, resp);

        //刷新排行榜
        ReqRankList(resp.Id);
    }

    private void OnRankListResp(RankListResp resp)
    {
        Model.SetRankList(resp);
    }

    private void OnRankInfoResp(RankInfoResp resp)
    {
        Model.SetRankInfo(resp);
    }

    /// <summary>
    /// 排行榜基本信息列表回调
    /// </summary>
    /// <param name="resp"></param>
    private void OnRankBaseListResp(RankBaseListResp resp)
    {
        Model.SetRankBaseList(resp);
        EventManager.Instance.Dispatch(EEventType.RankEvent_RedPoint);
        //var _rankVos = ConfigMgr.Instance.GetConfig<RankVo>().Values.ToList();
        //var rankState = false;
        //foreach (var vo in _rankVos)
        //{
        //    var rankId = vo.Id;
        //    var isShow = Model.GetIsCanDrawByRankId(rankId);
        //    isShow = true;
        //    //rankState = (rankState = false && isShow)? true;
        //    RedPointManager.Instance.SetState((int)ERedPointType.Rank + 1, isShow);
        //}
    }

    //-------------------------------------------------


    // 清理数据调用
    protected override void onClear()
    {
    }
}
