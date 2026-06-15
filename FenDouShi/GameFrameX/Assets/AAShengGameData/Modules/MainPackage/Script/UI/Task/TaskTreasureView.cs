using FairyGUI;
using msg.task;
using System.Collections.Generic;
using TaskUI;
using UnityEngine;

public partial class TaskView
{
    TableView<ItemRender> rewardTV;

    void ShowTreasureView(GButton treasureBtn, Task activeTaskData)
    {
        G_TreasureBox treasure = (G_TreasureBox)treasureBtn;
        if (treasureBtn == null || treasureBtn.parent == null || treasure == null)
        {
            HideTreasureView();
            return;
        }

        if (view == null) return;
        view.CloseTreasureMask.visible = true;
        view.TreasureView.visible = true;
        view.TreasureView.position = treasure.position + treasure.parent.position + new Vector3(37, treasure.Open.size.y - 10);

        List<List<int>> reward = null;
        if (activeTaskData.IsDaily())
        {
            var dailyData = activeTaskData.DailyDATA();
            reward = dailyData.Reward;
        }
        else if (activeTaskData.IsWeekly())
        {
            var weeklyData = activeTaskData.WeeklyDATA();
            reward = weeklyData.Reward;
        }

        if (reward == null)
        {
            HideTreasureView();
            return;
        }

        // TODO:类对象池
        var rewardItems = new List<CommonItemData>();
        foreach (var i in reward)
        {
            if (i.Count != 2)
            {
                Logger.PrintError($"[任务]任务宝箱奖励配表出错:{activeTaskData.Id},数组长度:{i.Count}");
            }
            else if (i[0] != TaskConst.dailyItemId && i[0] != TaskConst.weeklyItemId)
            {
                var emptyGood = new CommonItemData(i[0], i[1])
                {
                    GetIsDraw = activeTaskData.IsReceive()
                };
                rewardItems.Add(emptyGood);
            }
        }
        rewardTV.setDatas(rewardItems);
    }

    void HideTreasureView()
    {
        view.CloseTreasureMask.visible = false;
        view.TreasureView.visible = false;
    }
}