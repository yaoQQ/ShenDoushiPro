using FairyGUI;
using msg.task;
using System.Collections.Generic;
using TaskUI;
using UnityEngine;

public class TaskItemRenderer : BaseRender
{
    public new G_TaskItem mRoot
    {
        get { return (G_TaskItem)base.mRoot; }
    }

    public override string mPackageName => G_TaskItem.PACKAGE_NAME;
    public override string mComponentName => G_TaskItem.COMPONENT_NAME;

    TableView<ItemRender> rewardTV;

    protected override void onCreate()
    {
        rewardTV = new TableView<ItemRender>(mRoot.RewardList);
    }

    protected override void dataChanged()
    {
        var taskData = (Task)mData;

        string name = string.Empty;
        string content = string.Empty;
        int targetValue = 0;
        int jump = 0;
        List<List<int>> reward = null;
        if (taskData.IsDaily())
        {
            var DATA = taskData.DailyDATA();
            name = DATA.Name;
            content = DATA.Desc;
            targetValue = DATA.Value;
            jump = DATA.Jump;
            reward = DATA.Reward;
        }
        else if (taskData.IsWeekly())
        {
            var DATA = taskData.WeeklyDATA();
            name = DATA.Name;
            content = DATA.Desc;
            targetValue = DATA.Value;
            jump = DATA.Jump;
            reward = DATA.Reward;
        }
        else
        {
            return;
        }

        mRoot.TaskName.text = name;
        mRoot.TaskDescription.text = $"{content} ({Mathf.Clamp(taskData.Value, 0, targetValue)}/{targetValue})";

        var rewardItems = new List<CommonItemData>();
        if (reward.Count > 0 && reward[0].Count > 0)
        {
            mRoot.TaskPointCount.text = reward[0][1].ToString();

            foreach (var i in reward)
            {
                if (i.Count != 2)
                {
                    Logger.PrintError($"[任务]任务宝箱奖励配表出错:{taskData.Id},数组长度:{i.Count}");
                }
                else if (i[0] != TaskConst.dailyItemId && i[0] != TaskConst.weeklyItemId)
                {
                    rewardItems.Add(new CommonItemData(i[0], i[1])
                    {
                        GetIsDraw = taskData.IsReceive()
                    });
                }
            }
        }

        mRoot.GoTo.visible = taskData.IsNotComplete() && jump > 0;
        if (mRoot.GoTo.visible)
        {
            mRoot.GoTo.data = taskData;
            mRoot.GoTo.onClick.Set(OnClick_TaskItem_GoTo);
        }
        else
        {
            mRoot.GoTo.data = null;
        }

        mRoot.Receive.visible = taskData.IsComplete();
        if (mRoot.Receive.visible)
        {
            mRoot.Receive.data = taskData;
            mRoot.Receive.onClick.Set(OnClick_TaskItem_Receive);
        }
        else
        {
            mRoot.Receive.data = null;
        }

        mRoot.AlreadyReceive.visible = taskData.IsReceive();

        rewardTV.setDatas(rewardItems);
    }

    void OnClick_TaskItem_GoTo(EventContext context)
    {
        var btn = context.sender as GButton;
        if (btn == null || btn.data == null || btn.data is not Task taskData) return;

        if (taskData.IsNotComplete())
        {
            // TODO:跳转
        }
    }

    void OnClick_TaskItem_Receive(EventContext context)
    {
        var btn = context.sender as GButton;
        if (btn == null || btn.data == null || btn.data is not Task taskData) return;

        if (taskData.IsComplete())
        {
            TaskControl.Instance.TaskRewardReq_Tasks(TaskView.taskType);
        }
    }
}