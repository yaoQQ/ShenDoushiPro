using System.Threading.Tasks;
using TaskUI;
using UnityEngine;

public class TaskTipsData
{
    GComponentPool<G_TaskTips> pool;
    TaskTipsView window;
    Vector3 startPosition;

    public int index;
    public G_TaskTips tipsCom;

    public TaskTipsData()
    {
        pool = GComponentPool<G_TaskTips>.Instance;
    }

    public void Create(TaskTipsView taskTipsView, msg.task.Task data)
    {
        window = taskTipsView;
        if (window == null) return;
        startPosition = window.view.CreatePosition.position;

        index = 0;
        pool.Get(InstantiateItem, data);
    }

    void InstantiateItem(G_TaskTips tips, object data)
    {
        if (window == null || window.view == null) return;
        if (data is not msg.task.Task taskData) return;

        window.view.AddChild(tips);
        tipsCom = tips;
        tipsCom.title.text = taskData.GetTipsTitle();
        tipsCom.content.text = taskData.GetTipsContent();

        tipsCom.alpha = 1;
        tipsCom.position = startPosition + new Vector3(TaskConst.tipsItemStartXOffset, 0);
        tipsCom.TweenMoveX(TaskConst.tipsItemMoveX, 0.2f);
        MoveUp();

        // 隐藏流程
        GlobalTimeManager.Instance.timerController.AddTimer(tipsCom, TaskConst.tipsItemSeconds, 1, x =>
        {
            Hide();
        });
    }

    public void MoveUp()
    {
        index++;
        if (tipsCom == null) return;

        //Logger.PrintLog($"[完成任务Tips]往上移动");
        tipsCom.TweenMoveY(startPosition.y - TaskConst.tipsItemMoveY * index, 0.2f);
    }

    public void Hide()
    {
        if (tipsCom == null) return;

        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(tipsCom);
        tipsCom.TweenFade(0, 0.2f).OnComplete(() =>
        {
            pool.Recycle(tipsCom);
            tipsCom = null;
            if (window != null)
            {
                window.showingObjs.Dequeue();
                window.CheckShowNext();
            }
        });
    }
}