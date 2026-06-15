

using msg.rank;
using System.Collections.Generic;
using System.Linq;

public partial class RankModel : BaseModel
{

    private Dictionary<int, RankBaseInfo> _baseListMap;

    /// <summary>
    /// 已领取的进度奖励id列表
    /// </summary>
    private HashSet<int> _drawProgressIds;

    private Dictionary<int, HashSet<long>> _likeMap;

    private RankListResp _rankListResp;

    /// <summary>
    /// 是否红点，用于主界面显示红点
    /// </summary>
    private bool _isShowRedPoint;

    public void SetRankInfo(RankInfoResp resp)
    {
        if (resp?.progressIds == null)
        {
            return;
        }

        _isShowRedPoint = resp.isRedDot;
        _drawProgressIds ??= new HashSet<int>();
        if (resp.progressIds.Length > 0)
        {
            for (var i = 0; i < resp.progressIds.Length; i++)
            {
                var id = resp.progressIds[i];
                _drawProgressIds.Add(id);
            }
        }
        _likeMap ??= new Dictionary<int, HashSet<long>>();
        _likeMap.Clear();
        if (resp?.likeLists != null)
        {
            for (var i = 0; i < resp.likeLists.Count; i++)
            {
                var likeInfo = resp.likeLists[i];
                if (_likeMap.TryGetValue(likeInfo.Key, out var list))
                {
                    list.Add(likeInfo.Value);
                }
                else
                {
                    _likeMap[likeInfo.Key] = new HashSet<long> { likeInfo.Value };
                }
            }
        }
    }

    public bool GetIsDrawProgressId(int progressId)
    {
        return _drawProgressIds != null && _drawProgressIds.Contains(progressId);
    }

    /// <summary>
    /// 是否点赞
    /// </summary>
    /// <returns></returns>
    public bool GetIsLike(int rankId, long roleId)
    {
        if (_likeMap == null) return false;
        return _likeMap.TryGetValue(rankId, out var longs) && longs.Contains(roleId);
    }

    /// <summary>
    /// 排行榜基本信息列表回调
    /// </summary>
    /// <param name="resp"></param>
    public void SetRankBaseList(RankBaseListResp resp)
    {
        _baseListMap = resp.baseLists.ToDictionary(s => s.Id);
        EventManager.Instance.Dispatch(EEventType.RankBaseInfoEvent);
    }

    /// <summary>
    /// 排行榜基本信息
    /// </summary>
    /// <param name="rankId"></param>
    /// <returns></returns>
    public RankBaseInfo GetRankBaseInfoById(int rankId)
    {
        _baseListMap.TryGetValue(rankId, out var info);
        return info;
    }

    /// <summary>
    /// 是否有进度奖励可以领取
    /// </summary>
    /// <param name="rankId"></param>
    /// <returns></returns>
    public bool GetIsCanDrawByRankId(int rankId)
    {
        var rankInfo = GetRankBaseInfoById(rankId);
        return rankInfo is { hasReward: true };
    }

    /// <summary>
    /// 更新红点
    /// </summary>
    /// <param name="rankId"></param>
    /// <param name="hasReward"></param>
    private void SetDrawStateByRankId(int rankId, bool hasReward)
    {
        var rankInfo = GetRankBaseInfoById(rankId);
        rankInfo.hasReward = hasReward;
    }

    /// <summary>
    /// 单个排行榜回调
    /// </summary>
    /// <param name="resp"></param>
    public void SetRankList(RankListResp resp)
    {
        _rankListResp = resp;
        EventManager.Instance.Dispatch(EEventType.RankListEvent, resp);
    }

    public RankListResp GetRankList()
    {
        return _rankListResp;
    }


    private Dictionary<int, Dictionary<int, ProgressInfo>> _rankProgressMap;


    public void SetRankProgressInfo(RankProgressInfoResp resp)
    {
        _rankProgressMap ??= new Dictionary<int, Dictionary<int, ProgressInfo>>();
        var rankId = resp.Id;
        foreach (var info in resp.progressLists)
        {
            if (_rankProgressMap.TryGetValue(rankId, out var map))
            {
                map[info.Id] = info;
            }
            else
            {
                _rankProgressMap[rankId] = new Dictionary<int, ProgressInfo> { [info.Id] = info };
            }
        }
        EventManager.Instance.Dispatch(EEventType.RankProgressEvent, resp);
        //EventManager.Instance.Dispatch(EEventType.RankEvent_RedPoint);
    }

    public Dictionary<int, ProgressInfo> GetRankProgressInfo(int rankId)
    {
        if (_rankProgressMap == null) return null;
        return !_rankProgressMap.TryGetValue(rankId, out var map) ? null : map;
    }

    public ProgressInfo GetRankProgressInfo(int rankId, int progressId)
    {
        if (_rankProgressMap == null) return null;
        if (!_rankProgressMap.TryGetValue(rankId, out var map)) return null;
        return map.TryGetValue(progressId, out var info) ? info : null;
    }

    public bool GetTabRedPointState(int groupId)
    {
        var cfg = GetRankGroupCfgById(groupId);
        if (cfg == null) return false;
        foreach (var rankId in cfg.Ranks)
        {
            var isShow = GetRankRedPointState(rankId);
            if (isShow)
            {
                return true;
            }
        }
        return false;
    }

    public bool GetRankRedPointState(int rankId)
    {
       return GetIsCanDrawByRankId(rankId);
        //var progressCfg = GetRankProgressListCfg(rankId);
        //if (progressCfg == null) return false;
        //foreach (var cfg in progressCfg)
        //{
        //    var isShow = GetRankProgressRedPointState(rankId, cfg.Id);
        //    if (isShow)
        //    {
        //        return true;
        //    }
        //}
        //return false;
    }

    public bool GetRankProgressRedPointState(int rankId, int progressId)
    {
        var info = GetRankProgressInfo(rankId, progressId);
        if (info == null) return false;
        var isDraw = RankControl.Instance.Model.GetIsDrawProgressId(progressId);
        return !isDraw;
    }

    /// <summary>
    /// 进度奖励回调
    /// </summary>
    /// <param name="resp"></param>
    public void SetRankProgressReward(RankProgressRewardResp resp)
    {
        _drawProgressIds ??= new HashSet<int>();
        if (resp.progressIds.Length > 0)
        {
            for (var i = 0; i < resp.progressIds.Length; i++)
            {
                var id = resp.progressIds[i];
                _drawProgressIds.Add(id);
                //SetDrawStateByRankId(resp.Id,)
            }
        }
        EventManager.Instance.Dispatch(EEventType.RankProgressEvent, resp);
    }
}