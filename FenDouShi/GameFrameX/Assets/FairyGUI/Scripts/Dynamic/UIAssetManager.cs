using System;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI.Dynamic
{
    public sealed partial class UIAssetManager : IUIAssetManager
    {
        public void Initialize(IUIAssetManagerConfiguration configuration)
        {
            if (m_Initialized)
                throw new Exception("UIAssetManager has been initialized!");

            m_Initialized = true;
            m_PackageHelper = configuration.PackageHelper;
            m_AssetLoader = configuration.AssetLoader;
            m_UnloadUnusedUIPackageImmediately = configuration.UnloadUnusedUIPackageImmediately;

            NTexture.CustomDestroyMethod += DestroyTexture;
            NAudioClip.CustomDestroyMethod = DestroyAudioClip;
            UIPackage.OnPackageAcquire += OnUIPackageAcquire;
            UIPackage.OnPackageRelease += OnUIPackageRelease;
            UIPackage.GetUIPackageByIdFunc = GetUIPackageByIdFunc;
            UIPackage.GetUIPackageByNameFunc = GetUIPackageByNameFunc;
            UIPackage.GetUIPackageAsyncByIdHandler = GetUIPackageAsyncById;
            UIPackage.GetUIPackageAsyncByNameHandler = GetUIPackageAsyncByName;
            UIPackage.RemoveAllPackagesHandler = UnloadAllUIPackages;
            UIPackage.RemoveUnusedPackagesHandler = UnloadUnusedUIPackages;
            UIPanel.GetPackageFunc = GetPackageFunc;
            AsyncCreationHelper.BeforeCreateObject += BeforeCreateObject;
            AsyncCreationHelper.AfterCreateObject += AfterCreateObject;

#if UNITY_EDITOR
            Debugger.CreateDebugger(this);
#endif
        }

        public void Dispose()
        {
            if (!m_Initialized)
                throw new Exception("UIAssetManager has not been initialized!");

            m_Initialized = false;
            
#if UNITY_EDITOR
            Debugger.DestroyDebugger();
#endif

            UnloadAllUIPackages();

            foreach (var texPair in m_LoadedTextures)
            {
                for (int i = 0; i < texPair.Value.Count; i++)
                {
                    m_AssetLoader.UnloadTexture(texPair.Key, texPair.Value[i]);
                }
            }
            m_LoadedTextures.Clear();

            foreach (var audioPair in m_LoadedAudioClips)
            {
                for (int i = 0; i < audioPair.Value.Count; i++)
                {
                    m_AssetLoader.UnloadAudioClip(audioPair.Key, audioPair.Value[i]);
                }
            }
            m_LoadedAudioClips.Clear();

            m_NTextureAssetRefInfos.Clear();
            m_NAudioClipAssetRefInfos.Clear();

            NTexture.CustomDestroyMethod -= DestroyTexture;
            NAudioClip.CustomDestroyMethod -= DestroyAudioClip;
            UIPackage.OnPackageAcquire -= OnUIPackageAcquire;
            UIPackage.OnPackageRelease -= OnUIPackageRelease;
            UIPackage.GetUIPackageByIdFunc -= GetUIPackageByIdFunc;
            UIPackage.GetUIPackageByNameFunc -= GetUIPackageByNameFunc;
            UIPackage.RemoveAllPackagesHandler -= UnloadAllUIPackages;
            UIPackage.RemoveUnusedPackagesHandler -= UnloadUnusedUIPackages;
            UIPanel.GetPackageFunc -= GetPackageFunc;
            AsyncCreationHelper.BeforeCreateObject -= BeforeCreateObject;
            AsyncCreationHelper.AfterCreateObject -= AfterCreateObject;
        }

        private UIPackage GetUIPackageByNameFunc(string name)
        {
           // Debug.Log("GetUIPackageByNameFunc() name=" + name);
            return FindOrCreateUIPackage(name);
        }

        private UIPackage GetUIPackageByIdFunc(string id)
        {
            if (m_PackageHelper == null)
                throw new Exception("请设置PackageHelper");

            var packageName = m_PackageHelper.GetPackageNameById(id);
            if (string.IsNullOrEmpty(packageName))
            {
                // 获取packageName失败
                Debug.Log($"<color='red'>UIPackageMapping 获取packageName失败:  id={id} 请确认UIPackageMapping是否包含包体名！</color>");
                return null;
            }
            
            return FindOrCreateUIPackage(packageName);
        }

        private void GetUIPackageAsyncByName(string arg1, UIPackage.GetUIPackageAsyncCallback arg2)
        {
            FindOrCreateUIPackageAsync(arg1, arg2);
        }

        private void GetUIPackageAsyncById(string id, UIPackage.GetUIPackageAsyncCallback callback)
        {
            if (m_PackageHelper == null)
                throw new Exception("请设置PackageHelper");
            
            var packageName = m_PackageHelper.GetPackageNameById(id);
            if (string.IsNullOrEmpty(packageName))
            {
                // 获取packageName失败
                Debug.Log($"获取packageName失败: {packageName}");
                callback(null);
                return;
            }
            
            FindOrCreateUIPackageAsync(packageName, callback);
        }

        private void UnloadAllUIPackages()
        {
            foreach (var packageRef in m_UIPackageRefs.Values)
                UIPackage.RemovePackage(packageRef.Name);

            m_UIPackageRefs.Clear();
        }

        private void UnloadUnusedUIPackages()
        {
            m_RemoveBuffer.Clear();

            foreach (var packageRef in m_UIPackageRefs.Values)
            {
                if (packageRef.RefCount > 0)
                    continue;

                m_RemoveBuffer.Add(packageRef.Name);
                UIPackage.RemovePackage(packageRef.Name);
            }

            foreach (var packageName in m_RemoveBuffer)
                m_UIPackageRefs.Remove(packageName);
        }

        private void GetPackageFunc(string packagePath, string packageName, UIPackage.GetUIPackageAsyncCallback onComplete)
        {
            FindOrCreateUIPackageAsync(packageName, onComplete);
        }

        private UIPackage FindOrCreateUIPackage(string packageName)
        {
            var packageRef = FindUIPackageRef(packageName);
            if (packageRef != null)
            {
              //  Debug.Log($"已经加载过包资源  UIPackage={packageRef.UIPackage.name} 直接返回复用" );
                return packageRef.UIPackage;
            }
            if (m_AssetLoader == null)
                throw new Exception("请设置AssetLoader");

            m_AssetLoader.LoadUIPackageBytes(packageName, out var bytes, out var assetNamePrefix);
            if (bytes == null)
            {
                Debug.LogError($"加载UIPackage失败: {packageName}");
                return null;
            }
            
            return AddUIPackage(packageName, bytes, assetNamePrefix);
        }

        private void FindOrCreateUIPackageAsync(string packageName, UIPackage.GetUIPackageAsyncCallback onComplete)
        {
            var packageRef = FindUIPackageRef(packageName);
            if (packageRef != null)
            {
                onComplete?.Invoke(packageRef.UIPackage);
                return;
            }

            if (m_AssetLoader == null)
                throw new Exception("请设置AssetLoader");

            m_AssetLoader.LoadUIPackageBytesAsync(packageName, (bytes, assetNamePrefix) =>
            {
                if (bytes == null)
                {
                    Debug.LogError($"加载UIPackage失败: {packageName}");
                    onComplete?.Invoke(null);
                    return;
                }
                var uiPackage = AddUIPackage(packageName, bytes, assetNamePrefix);
                Debug.Log("加载包完成："+uiPackage.name);
                onComplete?.Invoke(uiPackage);
            });
        }


        private bool m_Initialized;
        private IUIPackageHelper m_PackageHelper;
        private IUIAssetLoader m_AssetLoader;
        private bool m_UnloadUnusedUIPackageImmediately;

        private readonly Dictionary<string, List<Texture>> m_LoadedTextures = new Dictionary<string, List<Texture>>();
        private readonly Dictionary<string, List<AudioClip>> m_LoadedAudioClips = new Dictionary<string, List<AudioClip>>();

        private readonly HashSet<string> m_RemoveBuffer = new HashSet<string>();
    }
}