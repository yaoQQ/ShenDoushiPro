
using System.Collections.Generic;
using System.Linq;

public partial class AthenaTrialModel
{
    private Dictionary<int, TrialTypeVo> typeCfg;

    private Dictionary<int, Dictionary<int, TrialDegreeVo>> stageCfg;

    private Dictionary<int, Dictionary<int, TrialLevelVo>> levelCfg;

    private List<TrialTypeVo> typeList;

    private void InitCfg()
    {
        if (typeCfg != null) { return; }
        typeCfg = ConfigMgr.Instance.GetConfig<TrialTypeVo>();
        var tempCfg = ConfigMgr.Instance.GetConfig<TrialDegreeVo>().Values.ToList();
        typeList = typeCfg.Values.ToList();
        stageCfg = new Dictionary<int, Dictionary<int, TrialDegreeVo>>();
        levelCfg = new Dictionary<int, Dictionary<int, TrialLevelVo>>();
        for (int i = 0; i < tempCfg.Count; i++)
        {
            var vo = tempCfg[i];
            var typeId = vo.TypeId;
            if (!stageCfg.TryGetValue(typeId, out var innerDic))
            {
                stageCfg[typeId] = new Dictionary<int, TrialDegreeVo>();
            }
            stageCfg[typeId][vo.Id] = vo;
        }
        var tempCfg2 = ConfigMgr.Instance.GetConfig<TrialLevelVo>().Values.ToList();
        for (int i = 0; i < tempCfg2.Count; i++)
        {
            var vo = tempCfg2[i];
            var typeId = vo.TypeId;
            if (!levelCfg.TryGetValue(typeId, out var innerDic))
            {
                levelCfg[typeId] = new Dictionary<int, TrialLevelVo>();
            }
            levelCfg[typeId][vo.Level] = vo;
        }
    }

    public List<TrialTypeVo> GetTypeList()
    {
        InitCfg();
        return typeList;
    }

    public TrialTypeVo GetTypeCfgByTypeId(int type)
    {
        InitCfg();
        if (typeCfg != null && typeCfg.TryGetValue(type, out var vo))
        {
            return vo;
        }
        return null;
    }

    public TrialLevelVo GetLevelCfg(int type, int level)
    {
        InitCfg();
        if (levelCfg == null || !levelCfg.TryGetValue(type, out var result)) return null;
        return result.TryGetValue(level, out var result2) ? result2 : null;
    }

    public TrialLevelVo GetLevelCfgById(int level)
    {
        var cfg = ConfigMgr.Instance.GetConfigVoById<TrialLevelVo>(level);
        return cfg;
    }

    public Dictionary<int, TrialDegreeVo> GetStageCfgByType(int type)
    {
        InitCfg();
        if (stageCfg.TryGetValue(type, out var result))
        {
            return result;
        }
        return null;
    }

    public TrialDegreeVo GetStageCfgById(int id)
    {
        var cfg = ConfigMgr.Instance.GetConfigVoById<TrialDegreeVo>(id);
        return cfg;
    }

    /// <summary>
    /// 展示的扫荡最大次数
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetShowSweepMaxCnt(int type)
    {
        var cfg = GetTypeCfgByTypeId(type);
        return cfg.FreeSweepCnt;
    }


    /// <summary>
    /// 得到typeId
    /// </summary>
    /// <param name="stageId"></param>
    /// <returns></returns>
    public int GetTypeIdByStageId(int stageId)
    {
        var cfg = GetStageCfgById(stageId);
        return cfg?.TypeId ?? 0;
    }

    /// <summary>
    /// 得到阶段id
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetStageIdByType(int type)
    {
        var maxLevel = GetPassMaxLevel(type);
        var cfgs = GetStageCfgByType(type).Values.OrderBy(s => s.Id).ToList();

        for (var i = 0; i < cfgs.Count; i++)
        {
            var cfg = cfgs[i];
            if (maxLevel <= cfg.Level)
            {
                return cfg.Id;
            }
        }
        return 0;
    }

}