using UnityEngine;
#if TEST_SCENE
using UnityEngine.Rendering.Universal;
#endif

/// <summary>
/// base 包预加载
/// </summary>
public class OutSpacePreload : AbstractPreloadOrder
{

    /// <summary>
    /// base 包预加载
    /// </summary>
    /// <param name="currPackage">base资源包 定义的package=BasePackage</param>
    public OutSpacePreload(AbstractPackage currPackage)
    {
        this.preloadStyle = PreloadStyle.FullLoadingBar;
        this.uiPreloadList = currPackage.getPackAllUIMidList();//初始设置所有界面 不显示
#if TEST_SCENE
        this.scenePreload = null;//测试不加载当前场景
       
#else
        this.scenePreload = new OutSpaceScene();//设置加载的场景
#endif

    }
    public override void onPreloadEnd()
    {
        Debug.Log("===================== OutSpacePreload begain onPreloadEnd()");
        base.onPreloadEnd();
        Debug.Log("=====================OutSpacePreload end onPreloadEnd()");
        LoadingBarController.SetLoadContent("加载【OutSpacePreload】包全部完成 " + loadCount + "/" + getPreLoadCount());

        if (scenePreload != null)
            scenePreload.onEnter();

#if TEST_SCENE
        //Camera uiCamera = UIManager.Instance.UICamera;
        //OutSpaceCameraManager.Instance.refreshCamera();
        //OutSpaceCameraManager.Instance.MainCamera.GetUniversalAdditionalCameraData().cameraStack.Add(uiCamera);

        // UIViewManager.Instance.Open(UIViewEnum.PostProcessingView);
        LoadingBarController.Hide();
#endif

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
        LoadingBarController.setLoadValue(percent);


    }


}
