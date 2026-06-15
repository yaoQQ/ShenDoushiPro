using UnityEngine;
using System.Collections.Generic;
public class LoginScene : BaseScene
{
    //场景名
    private List<string> loadModelList=new List<string>();
    private int loadNum = 0;
    public override string getSceneName()
    {
       // Logger.PrintColor("red", "@@@@getSceneName=LoginScene");
        return "LoginScene";
    }

    /// <summary>
    /// 加载完场景进入
    /// </summary>
    public override void onEnter()
    {
        LoadOneStep();
    }
    private void LoadOneStep()
    {
        loadNum++;
        float percent = 1;
        LoadingBarController.SetProgress(percent);
        if (loadNum >= loadModelList.Count)
        {
            loadSceneComplete();
        }
        else
        {
            int total= loadNum> loadModelList.Count? loadNum: loadModelList.Count;
            LoadingBarController.SetLoadContent($"加载场景 {getSceneName()} {loadNum}/{total}");
        }
    }
    private void loadSceneComplete()
    {
        endInit();
     //   LoginModule.LoginOutSocket();
    }
    public override void onReset()
    {
        Logger.PrintDebug(" StartGameScene onReset()");
    }
    public override void onLeave()
    {
        base.onLeave();
        Logger.PrintDebug(" StartGameScene onLeave()");
        //ResReleaseManager.ReleasePackage(PackageEnum.BasePackage);
        UIViewManager.Instance.DestroyView(UIViewEnum.LoginOnInitView);
        UIViewManager.Instance.DestroyView(UIViewEnum.LoginSelectServerView);
    }
}
