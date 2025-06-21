using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// base 包预加载
/// </summary>
public class LoadBasePreload : AbstractPreloadOrder
{

    /// <summary>
    /// base 包预加载
    /// </summary>
    /// <param name="currPackage">base资源包 定义的package=BasePackage</param>
    public LoadBasePreload(AbstractPackage currPackage)
    {
        this.preloadStyle = PreloadStyle.FullLoadingBar;
        this.uiPreloadList = currPackage.getPackAllUIMidList();//初始设置所有界面 不显示
        this.scenePreload = new StartGameScene();//设置加载的场景
       
    }
    public override void onPreloadEnd()
    {
        base.onPreloadEnd();

        LoadingBarController.SetLoadContent("加载【base】包全部完成 " + loadCount + "/" + getPreLoadCount());

        scenePreload.onEnter();
    }
    
    public override void onPreloadStepEnd(string loadStr)
    {

        loadCount++;
        int total = getPreLoadCount();
        float percent = (float)loadCount / total;
        //  NoticeManager.Instance.Dispatch(EliminateNoticeType.LoadStep)
      //  Logger.PrintColor("yellow", "onPreloadStepEnd() " + loadCount + "/" + total);
        //  NoticeManager.Instance.Dispatch(EliminateNoticeType.LoadStep)
        LoadingBarController.SetLoadContent(loadStr +" ("+ loadCount + "/"+ total+")");
        LoadingBarController.setLoadValue(percent);
    }
}
