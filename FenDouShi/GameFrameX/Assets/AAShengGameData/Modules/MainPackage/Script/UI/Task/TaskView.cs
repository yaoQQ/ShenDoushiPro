using FairyGUI;
using msg.task;
using System.Collections.Generic;
using TaskUI;
using UnityEngine;

[FGUIView(UIViewEnum.TaskView, typeof(TaskView))]
public partial class TaskView : BaseView
{
    public override string PackageName => G_TaskView.PACKAGE_NAME;
    public override string ComponentName => G_TaskView.COMPONENT_NAME;
    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {
        { EEventType.TaskListResp, TaskListResp },
        { EEventType.EventTaskUpdate, EventTaskUpdate },
    };

    protected override TopRenderData mTopRenderData => new TopRenderData
    {
        titleName = "任务",
        helpId = 10000,
        currencyId = 10001,
    };

    // TODO:object
    protected override LeftTabData[] mleftTabDatas => _leftTabDatas;
    LeftTabData[] _leftTabDatas = new LeftTabData[]
    {
        new (){ tabName = "每日任务", icon = UIHelper.GetFguiUrl("icon","1tag_icon_meirirenwu"), select = true , data = TaskModudle.Daily, redType = (int) ERedPointType.Task_Daily},
        new() { tabName = "每周任务", icon = UIHelper.GetFguiUrl("icon", "1tag_icon_meizhourenwu"), select = false, data = TaskModudle.Weekly, redType = (int)ERedPointType.Task_Weekly },
        // new(){ tabName = "契约战令", icon = UIHelper.GetFguiUrl(G_RedPoint.PACKAGE_NAME,"1tag_icon_qiyuezhanling"), select = false },
    };

    G_TaskView view;
    public static TaskModudle taskType;
    TableView<TaskItemRenderer> tasksTV;

    #region 生命周期

    public TaskView()
    {
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.TaskView, false);
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent;
        contentPane.MakeFullScreen();
        contentPane.AddRelation(GRoot.inst, RelationType.Size);

        view = gComponent as G_TaskView;

        tasksTV = new TableView<TaskItemRenderer>(view.TaskList);
        rewardTV = new TableView<ItemRender>(view.TreasureItemList);

        view.TreasureList.itemRenderer = TreasureList_ItemRenderer;
        view.TreasureList.numItems = 0;

        view.CloseTreasureMask.onClick.Set(HideTreasureView);
    }

    //protected override void DoShowAnimation() => OnShown();

    protected override void DoHideAnimation() => this.HideWindowImmediately();

    protected override void OnShown()
    {
        base.OnShown();

        mleftTabCompoment.setSelectIndex(0);
    }

    protected override void selectLeftTab(LeftTabData data)
    {
        SelectView((TaskModudle)data.data);
    }

    protected override void OnHide()
    {
        base.OnHide();
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(G_TaskView.COMPONENT_NAME);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        view = null;
    }

    #endregion

    #region 网络事件

    void TaskListResp(EventSysArgsBase argsBase)
    {
        if (argsBase is not EventSysArgs<TaskModudle> args) return;
        if (view == null) return;
        if (args.args1 != taskType) return;

        UpdateView();
    }

    void EventTaskUpdate(EventSysArgsBase argsBase)
    {
        if (view == null) return;

        tasksTV.setDatas(TaskControl.Instance.Model.GetTasks(taskType));
        UpdateProgress();
    }

    #endregion

    #region UI事件

    void TreasureList_ItemRenderer(int index, GObject item)
    {
        if (item is not G_TreasureBox boxItem) return;

        var tasks = TaskControl.Instance.Model.GetActiveTasks(taskType);
        if (index >= tasks.Count)
        {
            Logger.PrintError($"[任务]index超出索引?");
            return;
        }

        var activeTaskData = tasks[index];
        boxItem.Lock.visible = activeTaskData.IsNotComplete();
        boxItem.Complete.visible = activeTaskData.IsComplete();
        boxItem.Open.visible = activeTaskData.IsReceive();
        boxItem.data = activeTaskData;
        int value = 0;
        if (activeTaskData.IsDaily())
        {
            value = activeTaskData.DailyDATA().Value;
        }
        else if (activeTaskData.IsWeekly())
        {
            value = activeTaskData.WeeklyDATA().Value;
        }
        boxItem.n25.text = value.ToString();

        boxItem.onClick.Set(OnClick_Treasure);
    }

    void OnClick_Treasure(EventContext context)
    {
        var btn = context.sender as GButton;
        if (btn == null || btn.data == null || btn.data is not Task activeTaskData) return;

        if (activeTaskData.IsComplete())
        {
            TaskControl.Instance.TaskRewardReq_Treasure(taskType, activeTaskData.Group);
        }
        else
        {
            ShowTreasureView(btn, activeTaskData);
        }
    }

    #endregion

    void SelectView(TaskModudle taskType)
    {
        view.RefreshTime.text = string.Empty;

        HideTreasureView();

        TaskView.taskType = taskType;
        switch (taskType)
        {
            case TaskModudle.Daily:
                if (!TaskControl.Instance.Model.showDaily)
                {
                    //view.TaskList.numItems = 0;
                    TaskControl.Instance.TaskListReq(taskType);
                    return;
                }
                break;
            case TaskModudle.Weekly:
                if (!TaskControl.Instance.Model.showWeekly)
                {
                    //view.TaskList.numItems = 0;
                    TaskControl.Instance.TaskListReq(taskType);
                    return;
                }
                break;
        }

        UpdateView();
    }

    void UpdateView()
    {
        UpdateProgress();

        var tasks = TaskControl.Instance.Model.GetTasks(taskType);
        tasksTV.setDatas(TaskControl.Instance.Model.GetTasks(taskType));
        tasksTV.ScrollTop();

        // 倒计时
        RefreshRemainTime();

        uint remainTime = TaskControl.Instance.Model.GetRefreshTime(taskType);
        uint nowTs = TimeHelper.GetlocalTimeStamp();
        // Logger.PrintLog($"[任务]刷新时间:{TimeManager.GetDateTimeByUnixTime(remainTime)} 现在时间:{TimeManager.GetDateTimeByUnixTime(nowTs)}");
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(G_TaskView.COMPONENT_NAME);
        GlobalTimeManager.Instance.timerController.AddTimer(G_TaskView.COMPONENT_NAME, TaskConst.refreshTimeCoolDown, (int)Mathf.Max(0, remainTime - nowTs), RefreshRemainTime);
    }

    void RefreshRemainTime(int obj = 0)
    {
        if (view == null) return;
        uint nowTs = TimeHelper.GetlocalTimeStamp();
        uint refreshTime = TaskControl.Instance.Model.GetRefreshTime(taskType);
        view.RemainTime.text = TimeHelper.GetRemainTimeString_Task(refreshTime);
    }

    void UpdateProgress()
    {
        if (view == null) return;

        var pointNum = GetActiveItemCount();
        view.CurrentPoint.text = pointNum.ToString();

        List<Task> activeTasks = TaskControl.Instance.Model.GetActiveTasks(taskType);
        view.TreasureList.numItems = activeTasks.Count;
        view.SliderImg.fillAmount = Mathf.Clamp01(pointNum / 100f);
    }

    long GetActiveItemCount()
    {
        List<Task> activeTasks = TaskControl.Instance.Model.GetActiveTasks(taskType);
        long maxValue = 0;
        foreach (var i in activeTasks)
        {
            if (i.Value > maxValue)
            {
                maxValue = i.Value;
            }
        }
        return maxValue;
    }
}