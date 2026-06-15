using msg.task;
using System.Text;

[Control]
public class TaskControl : BaseControl<TaskControl>
{
    public TaskModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new();
    }

    protected override void onEventListener()
    {
        on<TaskListResp>((uint)Cmd.TaskListResp, TaskListResp);
        on<TaskUpdateResp>((uint)Cmd.TaskUpdateResp, TaskUpdateResp);
    }

    // 请求任务列表
    public void TaskListReq(TaskModudle type)
    {
        Logger.PrintLog($"[任务]TaskListReq:{type}");
        var req = new TaskListReq()
        {
            Type = type
        };
        SendNetMsg((uint)Cmd.TaskListReq, req);
    }

    void TaskListResp(TaskListResp resp)
    {
        Logger.PrintLog("[任务]任务列表请求回调");
        StringBuilder sb = new StringBuilder($"[任务]任务列表:{resp.Type},{resp.Tasks.Count}, 刷新时间:{resp.refreshTime.GetDateTime()}\n");
        foreach (var i in resp.Tasks)
        {
            sb.AppendLine(i.GetString());
        }
        Logger.PrintLog(sb.ToString());
        Model.SetTasks(resp.Type, resp.Tasks, resp.refreshTime);
        EventManager.Instance.Dispatch(EEventType.TaskListResp, resp.Type);
    }

    void TaskUpdateResp(TaskUpdateResp resp)
    {
        Logger.PrintLog($"[任务]任务更新回调");
        StringBuilder sb = new StringBuilder($"[任务]任务列表:{resp.Type},{resp.Tasks.Count}\n");
        foreach (var i in resp.Tasks)
        {
            sb.AppendLine(i.GetString());
        }
        Logger.PrintLog(sb.ToString());
        Model.UpdateTask(resp.Type, resp.Tasks);
        EventManager.Instance.Dispatch(EEventType.TaskUpdateResp, resp.Type);
    }

    // 领取奖励
    public void TaskRewardReq_Tasks(TaskModudle type)
    {
        int[] groupIds = Model.GetCompleteGroupIds(type);
        if (groupIds != null && groupIds.Length > 0)
        {
            Logger.PrintLog($"[任务]TaskRewardReq:{type},完成任务数:{groupIds.Length}");
            var req = new TaskRewardReq()
            {
                Type = type,
                groupIds = groupIds,
            };
            SendNetMsg((uint)Cmd.TaskRewardReq, req);
        }
    }

    public void TaskRewardReq_Treasure(TaskModudle type, int groupId)
    {
        Logger.PrintLog($"[任务]TaskRewardReq:{type}");
        var req = new TaskRewardReq()
        {
            Type = type,
            groupIds = new int[] { groupId },
        };
        SendNetMsg((uint)Cmd.TaskRewardReq, req);
    }
}