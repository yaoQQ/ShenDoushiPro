using FairyGUI;
using msg.task;
using System;
using System.Collections.Generic;
using TaskUI;

[FGUIView(UIViewEnum.TaskTipsView, typeof(TaskTipsView))]
public class TaskTipsView : BaseView
{
    public override string PackageName => G_TaskTipsView.PACKAGE_NAME;
    public override string ComponentName => G_TaskTipsView.COMPONENT_NAME;

    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {
        { EEventType.EventTaskFinish, EventTaskFinish },
    };

    public G_TaskTipsView view;

    // 每周任务的优先级大于每日任务
    List<Task> waitDailyDatas = new();
    List<Task> waitWeeklyDatas = new();
    public Queue<TaskTipsData> showingObjs = new();

    public TaskTipsView()
    {
        GComponentPool<G_TaskTips>.Instance.Init(G_TaskTips.PACKAGE_NAME, G_TaskTips.COMPONENT_NAME);
        setViewAttribute(UIViewLayerType.Alert_box, UIViewEnum.TaskTipsView, false);
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        contentPane = gComponent;
        contentPane.touchable = false;
        view = contentPane as G_TaskTipsView;
    }

    protected override void DoShowAnimation() => OnShown();

    protected override void DoHideAnimation() => this.HideWindowImmediately();

    void EventTaskFinish(EventSysArgsBase argsBase)
    {
        if (view == null) return;
        if (argsBase is not EventSysArgs<Task> args) return;
        var taskData = args.args1;
        if (!taskData.IsActive())
        {
            if (taskData.IsDaily())
            {
                waitDailyDatas.Add(taskData);
                waitDailyDatas.Sort(SortByGroupId);
            }
            else if (taskData.IsWeekly())
            {
                waitWeeklyDatas.Add(taskData);
                waitWeeklyDatas.Sort(SortByGroupId);
            }
            CheckShowNext();
        }
    }

    int SortByGroupId(Task x, Task y)
    {
        return x.Group.CompareTo(y.Group);
    }

    public void CheckShowNext()
    {
        if (showingObjs.Count >= 3) return;
        if (waitDailyDatas.Count == 0 && waitWeeklyDatas.Count == 0) return;

        // 其余item往上移动
        foreach (var i in showingObjs)
        {
            i.MoveUp();
        }

        // 新的item
        Task data = null;
        while (data == null && (waitDailyDatas.Count != 0 || waitWeeklyDatas.Count != 0))
        {
            if (waitDailyDatas.Count > 0)
            {
                data = waitDailyDatas[0];
                waitDailyDatas.RemoveAt(0);
            }
            else if (waitWeeklyDatas.Count > 0)
            {
                data = waitWeeklyDatas[0];
                waitWeeklyDatas.RemoveAt(0);
            }
            if (data != null)
            {
                TaskTipsData tipsData = ClassPoolManger.Instance.Get<TaskTipsData>();
                showingObjs.Enqueue(tipsData);
                tipsData.Create(this, data);
                break;
            }
        }
    }

    protected override void OnDestroy()
    {
        view = null;
    }
}