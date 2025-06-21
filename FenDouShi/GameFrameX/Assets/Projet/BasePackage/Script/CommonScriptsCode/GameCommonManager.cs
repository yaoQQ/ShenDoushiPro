using UnityEngine;
using System.Collections;

public class GameCommonManager : Singleton<GameCommonManager>
{

    public void ReturnMainScene(GameEnum gameID)
    {

        ResReleaseManager.ReleasePackage(gameID);
        PreloadManager.Instance.BackToScene(GameEnum.BasePackage);
    }

}
