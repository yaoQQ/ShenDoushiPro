//using FairyGUI.Dynamic;
//using UnityEngine.AddressableAssets;

//public class MyUIAssetManagerConfiguration : IUIAssetManagerConfiguration
//{
//    public IUIAssetManager m_UIAssetManager = new UIAssetManager();

//    public IUIPackageHelper PackageHelper { get; private set; }

//    public IUIAssetLoader AssetLoader { get; private set; } = new AddressableLoader();

//    public bool UnloadUnusedUIPackageImmediately => true;

//    public MyUIAssetManagerConfiguration()
//    {
//        var handler = Addressables.LoadAssetAsync<UIPackageMapping>("Assets/BundleAssets/UI/UIPackageMapping.asset");
//        PackageHelper = handler.WaitForCompletion();

//        m_UIAssetManager.Initialize(this);
//    }

//    public void OnDestroy()
//    {
//        m_UIAssetManager.Dispose();
//    }
//}