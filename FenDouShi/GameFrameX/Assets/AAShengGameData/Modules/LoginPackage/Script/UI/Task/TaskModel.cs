using msg.task;
using System;
using System.Collections.Generic;

public class TaskModel : BaseModel
{
    public bool showDaily = false;
    public bool showWeekly = false;
    public List<Task> dailyActiveTasks = new();
    public List<Task> dailyTasks = new();
    public uint dailyRefreshTime;
    public List<Task> weeklyActiveTasks = new();
    public List<Task> weeklyTask = new();
    public uint weeklyRefreshTime;

    public void SetTasks(TaskModudle taskModudle, List<Task> tasks, long refreshTime)
    {
        switch (taskModudle)
        {
            case TaskModudle.Daily:
                showDaily = true;
                dailyRefreshTime = refreshTime.LongTimeStampToUint();

                dailyActiveTasks.Clear();
                dailyTasks.Clear();
                foreach (var i in tasks)
                {
                    if (!TASK_DAILY_DIC.DIC.TryGetValue(i.Id, out var DATA))
                    {
                        Logger.PrintError($"[任务]后端下发每日任务id:{i.Id}, 未填表");
                        continue;
                    }

                    if (DATA.IsActive == 1)
                    {
                        dailyActiveTasks.Add(i);
                    }
                    else
                    {
                        dailyTasks.Add(i);
                    }
                }
                dailyActiveTasks.Sort(SortActiveTasks);
                dailyTasks.Sort(SortTasks);
                break;
            case TaskModudle.Weekly:
                showWeekly = true;
                weeklyRefreshTime = refreshTime.LongTimeStampToUint();

                weeklyActiveTasks.Clear();
                weeklyTask.Clear();
                foreach (var i in tasks)
                {
                    if (!TASK_WEEKLY_DIC.DIC.TryGetValue(i.Id, out var DATA))
                    {
                        Logger.PrintError($"[任务]后端下发每周任务id:{i.Id}, 未填表");
                        continue;
                    }

                    if (DATA.IsActive == 1)
                    {
                        weeklyActiveTasks.Add(i);
                    }
                    else
                    {
                        weeklyTask.Add(i);
                    }
                }
                weeklyActiveTasks.Sort(SortActiveTasks);
                weeklyTask.Sort(SortTasks);
                break;
            default:
                Logger.PrintError($"[任务]未处理的任务类型:{taskModudle}");
                break;
        }

        CheckRedPoint(taskModudle);
    }

    public List<Task> GetActiveTasks(TaskModudle taskModudle)
    {
        List<Task> tasks = null;
        switch (taskModudle)
        {
            case TaskModudle.Daily:
                tasks = dailyActiveTasks;
                break;
            case TaskModudle.Weekly:
                tasks = weeklyActiveTasks;
                break;
            default:
                Logger.PrintError($"[任务]未处理的任务类型:{taskModudle}");
                break;
        }
        return tasks;
    }

    public List<Task> GetTasks(TaskModudle taskModudle)
    {
        List<Task> tasks = null;
        switch (taskModudle)
        {
            case TaskModudle.Daily:
                tasks = dailyTasks;
                break;
            case TaskModudle.Weekly:
                tasks = weeklyTask;
                break;
            default:
                Logger.PrintError($"[任务]未处理的任务类型:{taskModudle}");
                break;
        }
        return tasks;
    }

    public void UpdateTask(TaskModudle taskModudle, List<Task> updateTasks)
    {
        List<Task> tasks = GetTasks(taskModudle);
        List<Task> activeTasks = GetActiveTasks(taskModudle);
        List<Task> tempTasks = null;

        bool taskUpdate = false;
        foreach (var i in updateTasks)
        {
            // 区分活跃任务
            if (i.IsActive())
            {
                tempTasks = activeTasks;
            }
            else
            {
                tempTasks = tasks;
            }

            var taskData = tempTasks.Find(x => x.Group == i.Group && x.Id == i.Id);
            if (taskData == null)
            {
                tempTasks.Add(i);
                taskUpdate = true;

                if (i.IsComplete())
                {
                    EventManager.Instance.Dispatch(EEventType.EventTaskFinish, i);
                }
            }
            else
            {
                if (taskData.Value != i.Value)
                {
                    taskData.Value = i.Value;
                    taskUpdate = true;
                }

                if (taskData.Status != i.Status)
                {
                    taskData.Status = i.Status;
                    taskUpdate = true;
                    if (taskData.IsComplete())
                    {
                        EventManager.Instance.Dispatch(EEventType.EventTaskFinish, taskData);
                    }
                }
            }
        }

        tasks.Sort(SortTasks);
        activeTasks.Sort(SortActiveTasks);
        CheckRedPoint(taskModudle);
        EventManager.Instance.Dispatch(EEventType.EventTaskUpdate);
    }

    int SortTasks(Task x, Task y)
    {
        if (x == null) return 1;
        if (y == null) return -1;

        bool xHasReward = x.IsComplete();
        bool yHasReward = y.IsComplete();
        bool xIsGot = x.IsReceive();
        bool yIsGot = y.IsReceive();

        if (xIsGot && yIsGot)
        {
            return x.Id.CompareTo(y.Id);
        }
        else if (xIsGot)
        {
            return 1;
        }
        else if (yIsGot)
        {
            return -1;
        }
        else
        {
            if (xHasReward)
            {
                return -1;
            }
            else if (yHasReward)
            {
                return 1;
            }
        }

        return x.Id.CompareTo(y.Id);
    }

    int SortActiveTasks(Task x, Task y)
    {
        return x.Id.CompareTo(y.Id);
    }

    // 时间
    public uint GetRefreshTime(TaskModudle taskModudle)
    {
        switch (taskModudle)
        {
            case TaskModudle.Daily:
                return dailyRefreshTime;
            case TaskModudle.Weekly:
                return weeklyRefreshTime;
        }
        return 0;
    }

    public void CheckRedPoint(TaskModudle taskModudle)
    {
        bool isDone = false;
        var activeTasks = GetActiveTasks(taskModudle);
        foreach (var i in activeTasks)
        {
            if (i.IsComplete())
            {
                isDone = true;
                break;
            }
        }
        if (!isDone)
        {
            var tasks = GetTasks(taskModudle);
            foreach (var task in tasks)
            {
                if (task.IsComplete())
                {
                    isDone = true;
                    break;
                }
            }
        }

        ERedPointType redPointType = ERedPointType.None;
        switch (taskModudle)
        {
            case TaskModudle.Daily:
                redPointType = ERedPointType.Task_Daily;
                break;
            case TaskModudle.Weekly:
                redPointType = ERedPointType.Task_Weekly;
                break;
            // case TaskModudle.Achieve:
                // break;
        }
        RedPointManager.Instance.SetState(redPointType, isDone);
    }

    // 获取GroupIds
    public int[] GetCompleteGroupIds(TaskModudle taskModudle)
    {
        List<Task> tasks = GetTasks(taskModudle);
        if (tasks != null && tasks.Count > 0)
        {
            List<int> ints = ClassPoolManger.Instance.Get<List<int>>();
            foreach (var task in tasks)
            {
                if (task.IsComplete())
                {
                    ints.Add(task.Id);
                }
            }

            int[] arr = null;
            if (ints.Count > 0)
            {
                arr = ints.ToArray();
            }
            ints.Clear();
            ints.RecycleToPool();
            return arr;
        }
        return null;
    }
}

public enum ETaskStatus : byte
{
    NotComplete = 0,
    Complete = 1,
    Receive = 2
}