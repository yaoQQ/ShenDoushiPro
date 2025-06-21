using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using UnityEngine.InputSystem;
#if AR_SYSTEM
using UnityEngine.XR;
#endif

public class StartGameScene : BaseScene
{
    //场景名
    //private string package = ProjectControler.basePackage;
    private List<string> loadModelList;
    private int loadNum = 0;
    public override string getSceneName()
    {

#if AR_SYSTEM
        Logger.PrintColor("red", "@@@@getSceneName=AR_StartGame");
        return "AR_StartGame";
#else
        Logger.PrintColor("red", "@@@@getSceneName=StartGame");
        return "StartGame";
#endif

    }

    /// <summary>
    /// 加载完场景进入
    /// </summary>
    public override void onEnter()
    {
        //loadModelList = new List<string>() { "player" };
        loadModelList = new List<string>() { };
        // Logger.PrintColor("blue"," @@@@@@@@StartGameScene onEnter()");
        //可以加载完其他模型调用
        Camera uiCamera = UIManager.Instance.UICamera;
        CameraManager.Instance.refreshCamera();
        CameraManager.Instance.MainCamera.GetUniversalAdditionalCameraData().cameraStack.Add(uiCamera);

#if AR_SYSTEM
      //  LoaderUtility.Deinitialize();
#endif

        //  LoadModel();
        UIViewManager.Instance.Open(UIViewEnum.Platform_Top_Cost_View);
        LoadOneStep();

    }

    private void loadSceneObjectsCompelet()
    {
       LoadingBarController.showEnterBtn();
    }

    private void LoadModel()
    {
        LoadingBarController.SetLoadContent("加载场景内对象完成 " + loadNum + "/" + loadModelList.Count);
       
        Logger.PrintColor("red", "LoadModel()");
        //test@@@
       // LittlePriceModelManager.Instance.createModel(GameEnum.LittlePrincePackage.ToString(), "player", loadPlayerComplete);
       // LittlePriceModelManager.Instance.createModel(GameEnum.BasePackage.ToString(), "SelectPanel", loadSelectComplete);
   
    }

    //初始化人物位置和状态
    private void loadSelectComplete(GameObject obj)
    {
        Logger.PrintColor("red", "SelectPanel loadComplete  obj=" + obj.name);
        GameObject selectPanel = GameObject.Instantiate(obj);
        selectPanel.transform.position = Vector3.zero;
        selectPanel.transform.localEulerAngles = Vector3.zero;
        LoadOneStep();

    
    }
    private void loadPlayerComplete(GameObject obj)
    {
        Logger.PrintColor("red", "Player loadComplete  obj=" + obj.name);
        GameObject player = GameObject.Instantiate(obj);
        player.transform.position = Vector3.zero;
        player.transform.localEulerAngles = Vector3.zero;
        //ThirdPersonController.Instance.gameObject.GetComponent<PlayerInput>().enabled = false;
        //Logger.PrintColor("red", "2222Player loadComplete  obj=" + obj.name);
        //Transform playerParent = ThirdPersonController.Instance.transform;
       
#if AR_SYSTEM
      
        //playerParent.position = new Vector3(0, -0.56f, 9.13f);
        //playerParent.eulerAngles = new Vector3(0, 0, 0);
        //player.transform.localScale = Vector3.one * 0.1f;
#else
        //playerParent.position = new Vector3(26.54f, 8.57f, 97);
        //playerParent.eulerAngles = new Vector3(0, 180f, 0);
#endif
        //Transform playerChild = playerParent.Find("PrincePlayer");
        //playerChild.localEulerAngles = new Vector3(0, 163f, 0);
        //player.transform.Find("PlayerFollowCamera").gameObject.SetActive(false);
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
        Logger.PrintColor("red", "loadNum=" + loadNum + "  loadModelList.Count=" + loadModelList.Count); ;
        Logger.PrintColor("red", "percent=" + percent);
        Debug.Log("@@@@@@@@@@@@@@@@@@@@ loadNum=" + loadNum + "  loadModelList.Count=" + loadModelList.Count);
        if (loadNum >= loadModelList.Count)
        {
            loadSceneComplete();
        }
        else
        {
            LoadingBarController.SetLoadContent("加载场景 loadNum" + "/" + loadModelList.Count);
        }
    }
    private void loadSceneComplete()
    {
       
        UIViewManager.Instance.Open(UIViewEnum.LoginView2);
        LoadingBarController.showEnterBtn();
        LoadingBarController.SetLoadContent("加载场景完成 " + loadNum + "/" + loadModelList.Count);
        endInit();
#if AR_SYSTEM
        //  LoaderUtility.Initialize();
#endif
    }
    public override void onReset()
    {
        Logger.PrintDebug(" StartGameScene onReset()");
    }
    public override void onLeave()
    {
        base.onLeave();
        Logger.PrintDebug(" StartGameScene onLeave()");
    }
}
