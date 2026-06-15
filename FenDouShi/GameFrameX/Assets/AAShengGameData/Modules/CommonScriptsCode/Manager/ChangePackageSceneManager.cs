using UnityEngine;
using System.Collections;

public class ChangePackageSceneManager : Singleton<ChangePackageSceneManager>
{
    private PackageEnum _currPackage = PackageEnum.None;
    public void SetCurrPackage(PackageEnum gameID)
    {
        _currPackage = gameID;
    }
    public void ToFightScene()
    {
        //清除上一个包依赖
        ResReleaseManager.ReleasePackage(_currPackage);
        //加载战斗包
        PreloadManager.Instance.PreLoadPackage(PackageEnum.FightPackage);
    }
    public void ReturnToMainScene()
    {
        //清除上一个包依赖
        ResReleaseManager.ReleasePackage(_currPackage);
        //加载主场景包
        PreloadManager.Instance.PreLoadPackage(PackageEnum.GameMainPackage);

    }

    public void ReturnToLoginScene()
    {
        //断开连接
        UnityWebSocketManager.Instance.Dispose();
        //清除所有包依赖
        ResReleaseManager.ReleaseAllPackage();
        //加载登入场景包
        PreloadManager.Instance.PreLoadPackage(PackageEnum.LoginPackage);
    }

}
