using FairyGUI;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base 包预加载
/// </summary>
public class GameMainPackagePreload : AbstractPreloadOrder
{

    /// <summary>
    /// base 包预加载
    /// </summary>
    /// <param name="currPackage">base资源包 定义的package=BasePackage</param>
    public GameMainPackagePreload(AbstractPackage currPackage)
    {
        this.preloadStyle = PreloadStyle.FullLoadingBar;
        this.uiPreloadList = currPackage.getPackAllUIMidList();//初始设置所有界面 不显示
        this.scenePreload = new GameMainScene();//设置加载的场景

    }
    public override void onPreloadEnd()
    {
        base.onPreloadEnd();

        LoadingBarController.SetLoadContent("加载【base】包全部完成 " + loadCount + "/" + getPreLoadCount());
        if (scenePreload != null)
        {
            scenePreload.onEnter();
        }
        UIViewManager.Instance.Show(UIViewEnum.GameMainView);
        UIViewManager.Instance.Show(UIViewEnum.TaskTipsView);
        EventManager.Instance.Dispatch(EEventType.MainUI_Login_Enter_Init);
    }

    public override void onPreloadStepEnd(string loadStr)
    {

        loadCount++;
        int total = getPreLoadCount();
        float percent = (float)loadCount / total;
        //  NoticeManager.Instance.Dispatch(EliminateNoticeType.LoadStep)
        //  Logger.PrintColor("yellow", "onPreloadStepEnd() " + loadCount + "/" + total);
        //  NoticeManager.Instance.Dispatch(EliminateNoticeType.LoadStep)
        LoadingBarController.SetLoadContent(loadStr + " (" + loadCount + "/" + total + ")");
        LoadingBarController.SetProgress(percent);
    }
}
