using UnityEngine;

public class ResReleaseManager 
{
    public static void ReleasePackage(PackageEnum packageId)
    {
        if(packageId== PackageEnum.None)
        {
            return;
        }
        string packageName = packageId.ToString();
        UIViewManager.Instance.DestroyPackageView(packageId);;
        // EffectManager.Instance.DestroyPackageEffect(packageName);
        ModelManager.Instance.DestroyPackageModel(packageName);
        //Resources.UnloadUnusedAssets();
        AddressableGameProcessManager.Instance.DestroyPackage(packageName);
     
        PreloadManager.Instance.ReleasePackage(packageId);
        Logger.PrintColor("red",Utility.Platform.ConnectStrs("卸载【", packageName,"】包结束："));
    }
    public static void ReleaseAllPackage()
    {
        Logger.PrintColor("red","===============Start 卸载所有包==========");
        UIViewManager.Instance.DestroyAllView();
        // EffectManager.Instance.DestroyPackageEffect(packageName);
        ModelManager.Instance.DestroyAllPackageModel();
        AddressableGameProcessManager.Instance.DestroyAllPackage();
        PreloadManager.Instance.ReleaseAllPackage();
        Resources.UnloadUnusedAssets();
        Logger.PrintColor("red", "===============End 卸载所有包完成==========");
    }

    //除了Login包界面都清除
    public static void ReleaseAllPackageExceptViewPackage()
    {
        Logger.PrintColor("red", "===============Start 卸载所有包==========");
        UIViewManager.Instance.DestroyAllViewExceptPackage(PackageEnum.LoginPackage);
        // EffectManager.Instance.DestroyPackageEffect(packageName);
        ModelManager.Instance.DestroyAllPackageModel();
        AddressableGameProcessManager.Instance.DestroyAllPackage();
        Resources.UnloadUnusedAssets();
        Logger.PrintColor("red", "===============End 卸载所有包完成==========");
    }
}
