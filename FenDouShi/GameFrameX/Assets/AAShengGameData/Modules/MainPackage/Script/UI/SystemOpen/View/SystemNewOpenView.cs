using FairyGUI;
using System.Collections.Generic;
using SystemOpen;

[FGUIViewAttribute(UIViewEnum.SystemNewOpenView, typeof(SystemNewOpenView))]
public class SystemNewOpenView : BaseView
{
    public override string PackageName => G_SystemNewOpenView.PACKAGE_NAME;

    public override string ComponentName => G_SystemNewOpenView.COMPONENT_NAME;

    private G_SystemNewOpenView mainView;

    private bool IsCanClose;

    private string SystemNewOpenView_autoCloseKey = "SystemNewOpenView_autoCloseKey";

    private string SystemNewOpenView_CanCloseKey = "SystemNewOpenView_CanCloseKey";


    protected override Dictionary<EEventType, OnEventLister> EventList => new Dictionary<EEventType, OnEventLister>() {

        { EEventType.SystemOpen_NewSystemOpen_Show,OnRefreshView}
    };

    private void OnRefreshView(EventSysArgsBase argsBase)
    {
        switch (argsBase)
        {
            case null:
                return;
            case EventSysArgs<int> args:
                {
                    RefreshView(args.args1);
                    break;
                }
        }
    }


    public SystemNewOpenView()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.SystemNewOpenView, false);
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mainView = contentPane as G_SystemNewOpenView;
        contentPane.MakeFullScreen();
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        modal = true;
    }


    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mainView.viewMask)
        {
            OnCloseClick();
        }
    }

    private void OnCloseClick()
    {
        if (!IsCanClose) return;
        SystemOpenControl.Instance.Model.TryShowSystemNewOpenView();
    }

    protected override void OnShown()
    {
        base.OnShown();
        if (showArgs is not int funcId)
        {
            IsCanClose = true;
            return;
        }
        RefreshView(funcId);
    }

    protected override void OnHide()
    {
        base.OnHide();
        Clear();
    }

    private void Clear()
    {
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(SystemNewOpenView_CanCloseKey);
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(SystemNewOpenView_autoCloseKey);
    }

    private void RefreshView(int funcId)
    {
        IsCanClose = false;
        GlobalTimeManager.Instance.timerController.AddTimer(SystemNewOpenView_CanCloseKey, 500, 1, (time) =>
        {
            IsCanClose = true;
        });
        GlobalTimeManager.Instance.timerController.AddTimer(SystemNewOpenView_autoCloseKey, 1500, 1, (time) =>
        {
            IsCanClose = true;
            OnCloseClick();
        });
        var cfg = SystemOpenControl.Instance.Model.GetSystemCfgById(funcId);
        if (cfg == null) return;
        mainView.image_icon.url = UIHelper.GetFguiUrl(PackageName, cfg.Icon);
        mainView.Text_name.text = cfg.Name;
    }
}
