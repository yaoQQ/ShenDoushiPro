
using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class OutSpaceScene : BaseScene
{
    //场景名
    private List<string> loadModelList;
    private int loadNum = 0;

    public override string getSceneName()
    {
#if AR_SYSTEM
        //  return "AROutSpaceScene";
        //  return "AR_OutSpaceLevel_TestMotion";
        return SelectPlanet.TestScene;
#else
        return "AR_OutSpaceLevel_TestMotion";

#endif

    }

    /// <summary>
    /// 加载完场景进入
    /// </summary>
    public override void onEnter()
    {
        //long startTime = GetTimeStamp();
        Logger.PrintColor("red", "@@@@@@@OutSpacePreload onEnter()");
        //SceneRoot = GameObject.Find("SceneRoot");
        //if (SceneRoot == null)
        //{
        //    Logger.PrintError("LittlePrinceScene 的SceneRoot==null");
        //    return;
        //}
        loadModelList = new List<string>();
        ////Logger.PrintColor("blue", " @@@@@@@@StartGameScene onEnter()");
        ////可以加载完其他模型调用
        //Camera uiCamera = UIManager.Instance.UICamera;
        //OutSpaceCameraManager.Instance.refreshCamera();
        //OutSpaceCameraManager.Instance.MainCamera.GetUniversalAdditionalCameraData().cameraStack.Add(uiCamera);


        ////  LoadModel();
        //UIViewManager.Instance.Open(UIViewEnum.PostProcessingView);
        LoadOneStep();
    }

    private void loadSceneObjectsCompelet()
    {

     
    }


    private void LoadModel()
    {
        LoadingBarController.SetLoadContent("加载场景完成 " + this.loadNum + "/" + loadModelList.Count);
    }
    private void loadPlayerComplete(GameObject obj)
    {

        Logger.PrintColor("yellow", "Player loadComplete  obj=" + obj.name);
        GameObject player = GameObject.Instantiate(obj);
        player.transform.position = Vector3.zero;
        player.transform.Find("PlayerArmature").transform.position = new Vector3(0f, 7.9f, 0);
        LoadOneStep();
    }
  
    private void LoadOneStep()
    {
        loadNum++;
        float percent = 1;
        if (loadModelList.Count > 0)
        {
            percent = loadNum / loadModelList.Count;
        }
        LoadingBarController.setLoadValue(percent);
        Logger.PrintColor("red", "@@@@@@@loadNum=" + loadNum + " loadModelList.Count=" + loadModelList.Count);
        if (loadNum >= loadModelList.Count)
        {
            endInit();
            LoadingBarController.SetLoadContent("加载场景完成 " + loadNum + "/" + loadModelList.Count);

            loadSceneObjectsCompelet();  //bug无法获得回调中的 GameObject.FindObjectOfType（T） 的T类型
                                         // LittlePrinceGameManager.Instance.StartGame();
                                         //  LoadingBarController.SetLoadEnterFun(startGame);
            LoadingBarController.Hide();
        }
        else
        {
            LoadingBarController.SetLoadContent("加载场景 loadNum" + "/" + loadModelList.Count);
        }
    }

    public override void onReset()
    {
        Logger.PrintDebug(" OutSpaceScene onReset()");
    }
    public override void onLeave()
    {
        base.onLeave();
        Logger.PrintDebug(" OutSpaceScene onLeave()");
    }
}
