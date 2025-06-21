using UnityEngine;

public class ResReleaseManager 
{
    public static void ReleasePackage(GameEnum GameId)
    {
        string packageName = GameId.ToString();
        Logger.PrintLog(CommonUtils.ConnectStrs("卸载包：", packageName));
      //  UIViewManager.Instance.DestroyPackageView(packageName);
      // EffectManager.Instance.DestroyPackageEffect(packageName);
        ModelManager.Instance.DestroyPackageModel(packageName);
        Resources.UnloadUnusedAssets();
        AddressableGameProcessManager.Instance.DestroyPackage(packageName);
        Logger.PrintLog(CommonUtils.ConnectStrs("卸载包结束：", packageName));
        PreloadManager.Instance.ReleasePackage(packageName);
    }


}
