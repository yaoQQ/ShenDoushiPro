/// <summary>
/// base 包预加载
/// </summary>
public class LoginPackagePreload : AbstractPreloadOrder
{

    /// <summary>
    /// base 包预加载
    /// </summary>
    /// <param name="currPackage">base资源包 定义的package=BasePackage</param>
    public LoginPackagePreload(AbstractPackage currPackage)
    {
        this.preloadStyle = PreloadStyle.FullLoadingBar;
        this.uiPreloadList = currPackage.getPackAllUIMidList();//初始设置所有界面 不显示
        this.scenePreload = new LoginScene();//设置加载的场景

    }
    public override void onPreloadEnd()
    {
        base.onPreloadEnd();

        LoadingBarController.SetLoadContent("加载【base】包全部完成 " + loadCount + "/" + getPreLoadCount());
        if (scenePreload != null)
        {
            scenePreload.onEnter();
        }
        UIViewManager.Instance.Show(UIViewEnum.LoginOnInitView, null, () => { 
            Logger.PrintGreen("UIViewEnum.LoginOnInitView【base】包加载完成，显示登录界面");
        },false);
    }

    public override void onPreloadStepEnd(string loadStr)
    {

        loadCount++;
        int total = getPreLoadCount();
        float percent = (float)loadCount / total;
        LoadingBarController.SetLoadContent(loadStr + " (" + loadCount + "/" + total + ")");
        LoadingBarController.SetProgress(percent);
    }
 
}
