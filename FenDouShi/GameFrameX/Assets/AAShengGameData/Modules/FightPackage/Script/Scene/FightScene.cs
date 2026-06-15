using UnityEngine;
using System.Collections.Generic;
public class FightScene : BaseScene
{
    //场景名
    private List<string> loadModelList;
    private int loadNum = 0;
    public override string getSceneName()
    {

        Logger.PrintColor("red", "@@@@getSceneName=FightScene");
        return "FightScene";

    }

    /// <summary>
    /// 加载完场景进入
    /// </summary>
    public override void onEnter()
    {
        loadModelList = new List<string>() { };
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
        LoadingBarController.SetProgress(percent);
        int total = loadNum > loadModelList.Count ? loadNum : loadModelList.Count;
        LoadingBarController.SetLoadContent($"加载场景 {loadNum}/{total}");
        if (loadNum >= loadModelList.Count)
        {
            endInit();
        }

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
