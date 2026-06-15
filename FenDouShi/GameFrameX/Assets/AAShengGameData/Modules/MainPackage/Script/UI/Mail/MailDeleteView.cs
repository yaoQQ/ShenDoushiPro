using FairyGUI;
using FGUIMail;

public class MailDeleteView : BaseView
{
    public override string PackageName => G_MailDeleteComfirm.PACKAGE_NAME;
    public override string ComponentName => G_MailDeleteComfirm.COMPONENT_NAME;

    G_MailDeleteComfirm view;

    public MailDeleteView()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.MailDeleteView, false);
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent;

        view = contentPane as G_MailDeleteComfirm;
        view.MakeFullScreen();
        closeButton = view.CloseBtn;
        view.CancelBtn.onClick.Set(HideView);
        view.ComfirmBtn.onClick.Set(OnClicked_Comfirm);
    }

    void OnClicked_Comfirm()
    {
        Logger.PrintLog("[ÓÊ¼þ]É¾³ýÒÑ¶ÁÓÊ¼þ");
        if (MailControl.Instance.Model.HasCanDeleteMail())
        {
            MailControl.Instance.DeleteAllReadMail();
        }
        HideView();
    }
}