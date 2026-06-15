

using System.Collections.Generic;
using System.Linq;

public partial class SystemOpenModel
{
    private Dictionary<int, FuncVo> systemOpenCfg;

    private List<FuncVo> systemOpenCfgs;

    private Dictionary<int, FuncConditionVo> conditionMap;

    private Dictionary<int, FuncTaskVo> taskMap;

    private void InitCfg()
    {
        if (systemOpenCfg != null) return;
        systemOpenCfg = ConfigMgr.Instance.GetConfig<FuncVo>();
        conditionMap = ConfigMgr.Instance.GetConfig<FuncConditionVo>();
        taskMap = ConfigMgr.Instance.GetConfig<FuncTaskVo>();
        systemOpenCfgs = systemOpenCfg.Values.ToList();
    }

    public string GetSystemOpenTipDes(int funcId)
    {
        var cfg = GetSystemCfgById(funcId);
        var openCondition = cfg.OpenCondition;
        if (openCondition <= 0)
        {
            return string.Empty;
        }
        var openType = cfg.OpenConditionType;
        var conditionCfg = GetConditionCfgById(cfg.OpenCondition);
        if (conditionCfg == null)
        {
            return string.Empty;
        }
        var descStr = "";
        var typeStr = openType == 1 ? "且" : "或";
        if (conditionCfg.OpenDay > 0)
        {
            descStr = Utility.Text.Format("{0}{1}{2}", descStr, Utility.Text.Format("开服第{0}天", conditionCfg.OpenDay), typeStr);
        }
        if (conditionCfg.Level > 0)
        {
            descStr = Utility.Text.Format("{0}{1}{2}", descStr, Utility.Text.Format("{0}级", conditionCfg.Level), typeStr);
        }
        if (conditionCfg.DungeonId > 0)
        {
            descStr = Utility.Text.Format("{0}{1}{2}", descStr, Utility.Text.Format("通过关卡{0}", conditionCfg.DungeonId), typeStr);
        }
        if (conditionCfg.VipLevel > 0)
        {
            descStr = Utility.Text.Format("{0}{1}{2}", descStr, Utility.Text.Format("vip{0}级", conditionCfg.VipLevel), typeStr);
        }
        descStr = descStr.Substring(0, descStr.Length - 1);
        return Utility.Text.Format("{0}开启", descStr);
    }

    public FuncVo GetSystemCfgById(int systemId)
    {
        InitCfg();
        if (systemOpenCfg.TryGetValue(systemId, out var itemExcel))
        {
            return itemExcel;
        }
        else
        {
            Logger.PrintLog($"GetSystemCfgById itemId={systemId} not found!");
            return null;
        }
    }

    public List<FuncVo> GetSystemCfgs()
    {
        InitCfg();
        return systemOpenCfgs;
    }

    public FuncTaskVo GetTaskCfgById(int taskId)
    {
        InitCfg();
        return taskMap.GetValueOrDefault(taskId);
    }

    public List<FuncTaskVo> GetTaskCfgs(int funcId)
    {
        InitCfg();
        var result = new List<FuncTaskVo>();
        var cfg = GetSystemCfgById(funcId);
        if (cfg?.NoticeTask == null || cfg.NoticeTask.Count == 0) return result;
        foreach (var c in cfg.NoticeTask)
        {
            var taskCfg = GetTaskCfgById(c);
            result.Add(taskCfg);
        }
        return result;
    }

    public FuncConditionVo GetConditionCfgById(int id)
    {
        InitCfg();
        return conditionMap.GetValueOrDefault(id);
    }
}



