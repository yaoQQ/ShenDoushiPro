

using msg.func;
using System.Collections.Generic;
using System.Linq;

public partial class SystemOpenModel : BaseModel
{
    public HashSet<int> openList { get; private set; }

    public Dictionary<int, FuncManualInfo> taskInfoMap { get; private set; }

    private List<int> cacheFuncOpenList;

    /// <summary>
    /// 初始化系统开启数据
    /// </summary>
    /// <param name="openfunctionIds"></param>
    public void InitData(int[] openfunctionIds)
    {
        if (openfunctionIds is { Length: > 0 })
            openList = openfunctionIds.ToHashSet();
    }

    /// <summary>
    /// 新系统开启
    /// </summary>
    /// <param name="functionIds"></param>
    public void OnNewSystemOpen(int[] functionIds)
    {
        openList ??= new HashSet<int>();
        cacheFuncOpenList ??= new List<int>();
        foreach (var item in functionIds)
        {
            if (openList.Add(item))
            {
                cacheFuncOpenList.Add(item);
            }
        }
        cacheFuncOpenList.Sort(FuncOpenSort);
        TryShowSystemNewOpenView();
        EventManager.Instance.Dispatch(EEventType.SystemOpenEvent);
    }

    private int FuncOpenSort(int a, int b)
    {
        var cfg_a = GetSystemCfgById(a);
        var cfg_b = GetSystemCfgById(b);
        if (cfg_a == null || cfg_b == null)
            return 0;
        var condition_a = cfg_a.OpenCondition;
        var condition_b = cfg_b.OpenCondition;
        if (condition_a != condition_b)
        {
            return condition_a.CompareTo(condition_b);
        }
        var sort_a = cfg_a.Sort;
        var sort_b = cfg_b.Sort;
        if (sort_a != sort_b)
        {
            return sort_b.CompareTo(sort_a);
        }
        return cfg_a.Id.CompareTo(cfg_b.Id);
    }

    public bool GetIsSystemOpen(int id)
    {
        return openList != null && openList.Contains(id);
    }

    //0未完成1已完成2已领取
    public int GetSystemOpenRewardState(int id)
    {
        if (taskInfoMap != null && taskInfoMap.TryGetValue(id, out var taskInfo))
        {
            return taskInfo.Status;
        }
        return 0;
        //return getRewardList != null && getRewardList.Contains(id);
    }

    public bool GetIsGetSystemOpenReward(int id)
    {
        var state = GetSystemOpenRewardState(id);
        return state == 2;
    }

    public int GetTaskValue(int funcId, int taskId)
    {
        if (taskInfoMap == null || !taskInfoMap.TryGetValue(funcId, out var taskInfo)) return 0;
        return taskInfo.Tasks != null ? taskInfo.Tasks[taskId] : 0;
    }

    public void InitTaskInfo(List<FuncManualInfo> taskListInfoes)
    {
        taskInfoMap = taskListInfoes.ToDictionary(p => p.functionId);
        EventManager.Instance.Dispatch(EEventType.SystemOpen_InfoUpdate);
    }

    public void UpdateTaskInfo(List<FuncManualInfo> taskListInfoes)
    {
        taskInfoMap ??= new Dictionary<int, FuncManualInfo>();
        foreach (var info in taskListInfoes)
        {
            taskInfoMap[info.functionId] = info;
        }
        EventManager.Instance.Dispatch(EEventType.SystemOpen_InfoUpdate);
    }
    public bool GetIsHaveReward(int funcId)
    {
        var state = GetSystemOpenRewardState(funcId);
        return state == 1;
    }


    public bool ShieldPopView {  get; set; } = false;

    public void TryShowSystemNewOpenView()
    {
        //返回到主界面→弹出全屏功能开启提示 先屏蔽掉了
        var isMainUI = false;
        if (!isMainUI)
        {
            return;
        }
#if UNITY_EDITOR
        if (ShieldPopView)
        {
            UIViewManager.Instance.Hide(UIViewEnum.SystemNewOpenView);
            return;
        }
#endif
        var isShowing = UIViewManager.Instance.GetIsShowing(UIViewEnum.SystemNewOpenView);
        if (cacheFuncOpenList is not { Count: > 0 })
        {
            if (isShowing)
                UIViewManager.Instance.Hide(UIViewEnum.SystemNewOpenView);
            return;
        }
        var funcId = cacheFuncOpenList[0];
        cacheFuncOpenList.Remove(funcId);
        if (isShowing)
            EventManager.Instance.Dispatch(EEventType.SystemOpen_NewSystemOpen_Show, funcId);
        else
            UIViewManager.Instance.Show(UIViewEnum.SystemNewOpenView, funcId);
    }

    /// <summary>
    /// 设置开放功能列表
    /// </summary>
    /// <param name="openfunctionIds"></param>
    public void setOpenfunctionIds(int[] openfunctionIds)
    {
        InitData(openfunctionIds);
    }
}



