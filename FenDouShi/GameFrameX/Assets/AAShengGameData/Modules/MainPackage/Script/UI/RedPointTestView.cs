using FairyGUI;
using RedPointTest;
using System.Collections.Generic;

public class RedPointTest_View : BaseView
{
    public override string PackageName => G_RedPointTestView.PACKAGE_NAME;
    public override string ComponentName => G_RedPointTestView.COMPONENT_NAME;

    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {
        { EEventType.Test_AddItem, Bag_AddItem },
    };

    G_RedPointTestView view;

    public RedPointTest_View()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.RedPointTestView, true);
    }

    protected override void OnFinishLoad(GComponent gameObject)
    {
        base.OnFinishLoad(gameObject);

        contentPane = gameObject.asCom;
        contentPane.MakeFullScreen();

        // 默认状态
        List<int> ints = new List<int>() { 1, 2, 3, 4, 5 };
        //RedPointManager.Instance.SetState(ERedPointType.BagEquipment, () => ints.Contains(1));
        //RedPointManager.Instance.SetState(ERedPointType.BagFragment, () => ints.Contains(1));

        RedPointManager.Instance.CreateNode(1000, 100);
        RedPointManager.Instance.CreateNode(1000, 200);
        RedPointManager.Instance.CreateNode(1000, 300);
        RedPointManager.Instance.CreateNode(1000, 400);
        RedPointManager.Instance.CreateNode(1000, 500);
        RedPointManager.Instance.SetState(ERedPointType.BagFragment, true);

        view = contentPane.asCom as G_RedPointTestView;
        view.n114.onClick.Add(Hide);

        // 红点测试
        view.n101.text.text = "Log";
        view.n101.onClick.Add(() =>
        {
            RedPointManager.Instance.Log();
            ClassPoolManger.Instance.Log();
            EventManager.Instance.Log();
        });

        view.n108.text.text = "添加红点组件";
        view.n108.onClick.Add(() => { RegisterRedPoint(ERedPointType.Bag, view.n101, ERedPointAlignment.RightTop); });

        view.n111.text.text = "删除红点组件";
        view.n111.onClick.Add(() => { RemoveRedPoint(view.n101); });

        view.n115.text.text = "显示红点";
        view.n115.onClick.Add(() => { RedPointManager.Instance.SetState(ERedPointType.BagFragment, true); });
        view.n116.text.text = "隐藏红点";
        view.n116.onClick.Add(() => { RedPointManager.Instance.SetState(ERedPointType.BagFragment, false); });

        // 事件测试
        view.n117.text.text = "触发事件1";
        view.n117.onClick.Add(() =>
        {
            EventManager.Instance.Dispatch(EEventType.Test_AddItem, "111", 222, 33.33f);
        });
        view.n118.text.text = "触发事件2";
        view.n118.onClick.Add(() =>
        {
            EventManager.Instance.Dispatch(EEventType.Test_AddItem, "11", 13213, 12312);
        });
    }

    protected override void RegisterRedPoint()
    {
        RegisterRedPoint(ERedPointType.Bag, view.n101, ERedPointAlignment.RightTop, -20, 20);
        AttachRedPoint(ERedPointType.Bag, view.n101.n4);

        RegisterRedPoint(ERedPointType.BagFragment, view.n102, ERedPointAlignment.RightTop, -20, 20);
        RegisterRedPoint(ERedPointType.BagFragment, view.n103, ERedPointAlignment.LeftTop);
        RegisterRedPoint(100, view.n104, ERedPointAlignment.LeftBottom);
    }

    protected override void OnShown()
    {
        base.OnShown();
        Logger.PrintLog($"OnShow:{ComponentName}");
    }

    protected override void OnHide()
    {
        base.OnHide();
        Logger.PrintLog($"OnHide:{ComponentName}");
    }

    protected override void OnDestroy()
    {
        Logger.PrintLog($"OnDestroy:{ComponentName}");
    }

    private void Bag_AddItem(EventSysArgsBase argsBase)
    {
        var args = argsBase as EventSysArgs<string, int, float>;
        Logger.PrintLog(args.args1);
        Logger.PrintLog(args.args2.ToString());
        Logger.PrintLog(args.args3.ToString());
    }
}