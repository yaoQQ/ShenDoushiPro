using FairyGUI;
using fight;

public class FightMainView : BaseView
{
    public override string PackageName => G_FightMain.PACKAGE_NAME;
    public override string ComponentName => G_FightMain.COMPONENT_NAME;

    public FightMainView()
    {
        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.FightMainView, false);
        Logger.PrintColor("yellow", "FightMainView()");
    }

    /// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="uiName"></param>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
        Logger.PrintColor("yellow", $" onLoadUIEnd complte!! gameObject={gameObject}");
        this.contentPane = gameObject.asCom;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
    }

    /// <summary>
    /// fairyGUI(Window)初始化完成 加入舞台后触发的函数
    /// </summary>
    protected override void OnShown()
    {
        base.OnShown();
        contentPane.GetChild("returnBtn").asButton.onClick.Add(() =>
        {
            CommonViewUtils.ShowTopTips("点击返回按钮");
            ChangePackageSceneManager.Instance.ReturnToMainScene();
        });

        CommonViewUtils.ShowTopTips("这只是一个临时测试战斗场景！");
    }
}
