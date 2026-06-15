

using System.Collections.Generic;
using System.Linq;

public partial class RankModel
{
    private List<RankGroupVo> _rankGroupVos;

    private Dictionary<int, RankVo> _rankVos;

    private Dictionary<int, RankProgressVo> _rankProgressVos;

    private Dictionary<int, List<RankProgressVo>> _rankId2ProgressMap;


    private void InitCfg()
    {
        if (_rankGroupVos != null) return;
        var cfg = ConfigMgr.Instance.GetConfig<RankGroupVo>();
        _rankProgressVos = ConfigMgr.Instance.GetConfig<RankProgressVo>();
        _rankVos = ConfigMgr.Instance.GetConfig<RankVo>();
        _rankGroupVos = cfg.Values.ToList();
        _rankId2ProgressMap = _rankProgressVos.Values
            .GroupBy(rank => rank.RankId)
            .ToDictionary(group => group.Key, group => group.ToList());
    }


    public List<RankGroupVo> GetRankGroupCfg()
    {
        InitCfg();
        return _rankGroupVos;
    }

    public RankGroupVo GetRankGroupCfgById(int id)
    {
        InitCfg();
        var cfg = _rankGroupVos.Find(s => s.Id == id);
        return cfg;
    }





    public RankVo GetRankCfg(int rankId)
    {
        InitCfg();
        _rankVos.TryGetValue(rankId, out var rankVo);
        return rankVo;
    }

    public RankProgressVo GetRankProgressCfg(int progressId)
    {
        InitCfg();
        _rankProgressVos.TryGetValue(progressId, out var rankVo);
        return rankVo;
    }

    public List<RankProgressVo> GetRankProgressListCfg(int rankId)
    {
        InitCfg();
        _rankId2ProgressMap.TryGetValue(rankId, out var cfg);
        return cfg;
    }

    public RankGroupVo GetRankGroupCfgByRankId(int rankId)
    {
        var groupCfg = GetRankGroupCfg();
        var find = groupCfg.Find(s => s.Ranks.Contains(rankId));
        return find;
    }

    public string GetImageRankUrl(int rank)
    {
        return UIHelper.GetCommonUrl(Utility.Text.Format("common_icon_paiming0{0}", rank));
    }
}