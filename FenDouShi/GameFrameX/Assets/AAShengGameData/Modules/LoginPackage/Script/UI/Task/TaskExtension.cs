using msg.task;

public static class TaskExtension
{
    public static bool IsNotComplete(this Task task)
    {
        return task != null && task.Status == 0;
    }

    public static bool IsComplete(this Task task)
    {
        return task != null && task.Status == 1;
    }

    public static bool IsReceive(this Task task)
    {
        return task != null && task.Status == 2;
    }

    public static TaskDailyVo DailyDATA(this Task task)
    {
        if (task != null)
        {
            if (TASK_DAILY_DIC.DIC.TryGetValue(task.Id, out var DATA))
            {
                return DATA;
            }
        }
        return null;
    }

    public static TaskWeeklyVo WeeklyDATA(this Task task)
    {
        if (task != null)
        {
            if (TASK_WEEKLY_DIC.DIC.TryGetValue(task.Id, out var DATA))
            {
                return DATA;
            }
        }
        return null;
    }

    public static bool IsDaily(this Task task)
    {
        return task != null && TASK_DAILY_DIC.DIC.ContainsKey(task.Id);
    }

    public static bool IsWeekly(this Task task)
    {
        return task != null && TASK_WEEKLY_DIC.DIC.ContainsKey(task.Id);
    }

    public static bool IsActive(this Task task)
    {
        if (task == null) return false;
        if (TASK_DAILY_DIC.DIC.TryGetValue(task.Id, out var DAILY) && DAILY.IsActive == 1) return true;
        if (TASK_WEEKLY_DIC.DIC.TryGetValue(task.Id, out var WEEKLY) && WEEKLY.IsActive == 1) return true;
        return false;
    }

    public static string GetTipsTitle(this Task task)
    {
        string titleHead = string.Empty;
        string titleName = string.Empty;
        if (task.IsDaily())
        {
            titleHead = TaskString.dailyHead;
            var DATA = task.DailyDATA();
            titleName = DATA.Name;
        }
        else if (task.IsWeekly())
        {
            titleHead = TaskString.weeklyHead;
            var DATA = task.WeeklyDATA();
            titleName = DATA.Name;
        }

        return $"{titleHead}{titleName}";
    }

    public static string GetTipsContent(this Task task)
    {
        string titleContent = string.Empty;
        if (task.IsDaily())
        {
            var DATA = task.DailyDATA();
            titleContent = DATA.Desc;
        }
        else if (task.IsWeekly())
        {
            var DATA = task.WeeklyDATA();
            titleContent = DATA.Desc;
        }

        return $"{titleContent}";
    }

    public static string GetString(this Task task)
    {
        return $"Task:group:{task.Group}, id:{task.Id}, value:{task.Value}, state:{task.Status}";
    }
}