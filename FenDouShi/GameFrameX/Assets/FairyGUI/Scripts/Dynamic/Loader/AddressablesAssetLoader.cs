using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static FairyGUI.Dynamic.DelegateUIAssetLoader;

namespace FairyGUI.Dynamic
{
    /// <summary>
    /// 自定义委托的IUIAssetLoader派生类 为不方便实现接口的情况提供功能支持
    /// </summary>
    public sealed class AddressablesAssetLoader : IUIAssetLoader
    {
        private readonly Dictionary<string, byte[]> m_PackageBytes = new Dictionary<string, byte[]>();

         
        public LoadUIPackageBytesAsyncHandler LoadUIPackageBytesAsyncHandlerImpl { get; set; }
        public LoadUIPackageBytesHandler LoadUIPackageBytesHandlerImpl { get; set; }
        public LoadTextureAsyncHandler LoadTextureAsyncHandlerImpl { get; set; }
        public UnloadTextureHandler UnloadTextureHandlerImpl { get; set; }
        public LoadAudioClipAsyncHandler LoadAudioClipAsyncHandlerImpl { get; set; }
        public UnloadAudioClipHandler UnloadAudioClipHandlerImpl { get; set; }


        //Luch界面需要很快显示
        public async void InitLuchView(string packageName, Action callBack)
        {
            var op=  await  Addressables.LoadAssetAsync<TextAsset>(GetPackageAssetKey(packageName));
            if (op != null)
            {
                if (!m_PackageBytes.ContainsKey(packageName))
                {
                    m_PackageBytes.Add(packageName, op.bytes);
                }
                if (callBack != null)
                {
                    callBack();
                }
                Debug.Log($"<color='yellow'>AddressablesAssetLoader PreLoadAsync(): packageName={packageName} op.bytes.length={op.bytes.Length}</color>");
            }
        }
        /// <summary>
        /// 预加载登入的初始化环节
        /// </summary>
        public async UniTask PreLoadAsync(UIPackageMapping mapping)
        {
            //   m_PackageBytes.Clear();

            // 检查是否有需要预加载的包名
            if (mapping?.PackageNames == null || mapping.PackageNames.Length == 0)
            {
                Debug.LogWarning("AddressablesAssetLoader PreLoadAsync(): No package names to preload.");
                return;
            }
            List<string> list = new List<string>() { "common", "login" };
            string packageName;
            for (int i = 0; i < list.Count; i++)
            {
                packageName= list[i];
                if (!mapping.PackageNames.Contains(packageName))
                {
                    Debug.LogError($"登入预加载FairyGUI的 {packageName}不存在!");
                    return;
                }
                try
                {
                    if (m_PackageBytes.ContainsKey(packageName))
                    {
                        continue;
                    }
                    // 使用项目的资源加载方式加载UIPackage二进制数据
                    var op = await Addressables.LoadAssetAsync<TextAsset>(GetPackageAssetKey(packageName));
                    if (op != null)
                    {
                        m_PackageBytes.Add(packageName, op.bytes);
                        Debug.Log($"<color='yellow'>AddressablesAssetLoader PreLoadAsync(): packageName={packageName} op.bytes.length={op.bytes.Length}</color>");

                    }
                    else
                    {
                        Debug.LogError($"<color='red'>AddressablesAssetLoader PreLoadAsync(): Failed to load package {packageName}, loaded TextAsset is null.</color>");
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"<color='red'>AddressablesAssetLoader PreLoadAsync(): Error loading package {packageName}. Exception: {ex.Message}</color>");
                }
            }
        }
        /// <summary>
        /// 预加载所有的UIPackage的初始化
        /// </summary>
        public async UniTask PreEnterGameLoadAsync(UIPackageMapping mapping)
        {
         //   m_PackageBytes.Clear();

            // 检查是否有需要预加载的包名
            if (mapping?.PackageNames == null || mapping.PackageNames.Length == 0)
            {
                Debug.LogWarning("AddressablesAssetLoader PreLoadAsync(): No package names to preload.");
                return;
            }
           
            foreach (var packageName in mapping.PackageNames)
            {
                try
                {
                    if (m_PackageBytes.ContainsKey(packageName)) 
                    {
                        continue;
                    }
                    // 使用项目的资源加载方式加载UIPackage二进制数据
                    var op = await Addressables.LoadAssetAsync<TextAsset>(GetPackageAssetKey(packageName));
                    if (op != null)
                    {
                        m_PackageBytes.Add(packageName, op.bytes);
                        if (UIConfig.isShowAllWinowDebug)
                        {
                            Debug.Log($"<color='yellow'>AddressablesAssetLoader PreLoadAsync(): packageName={packageName} op.bytes.length={op.bytes.Length}</color>");
                        }
                    
                    }
                    else
                    {
                        Debug.LogError($"<color='red'>AddressablesAssetLoader PreLoadAsync(): Failed to load package {packageName}, loaded TextAsset is null.</color>");
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"<color='red'>AddressablesAssetLoader PreLoadAsync(): Error loading package {packageName}. Exception: {ex.Message}</color>");
                }
            }
        }

        public void LoadUIPackageBytesAsync(string packageName, LoadUIPackageBytesCallback callback)
        {
            if (LoadUIPackageBytesAsyncHandlerImpl == null)
                throw new NotImplementedException();

            LoadUIPackageBytesAsyncHandlerImpl(packageName, callback);
        }
        public void LoadUIPackageBytes(string packageName, out byte[] bytes, out string assetNamePrefix)
        {
            //if (LoadUIPackageBytesHandlerImpl == null)
            //    throw new NotImplementedException();
            //LoadUIPackageBytesHandlerImpl(packageName, out bytes, out assetNamePrefix);
            m_PackageBytes.TryGetValue(packageName, out bytes);
            assetNamePrefix = packageName;
        }
        public void LoadTextureAsync(string packageName, string assetName, string extension, LoadTextureCallback callback)
        {
            if (LoadTextureAsyncHandlerImpl == null)
                throw new NotImplementedException();
            LoadTextureAsyncHandlerImpl(packageName, assetName, extension, callback);
        }
        public void UnloadTexture(string packageName, Texture texture)
        {
            if (UnloadTextureHandlerImpl == null)
                throw new NotImplementedException();
            UnloadTextureHandlerImpl(packageName, texture);
        }
        public void LoadAudioClipAsync(string packageName, string assetName, string extension, LoadAudioClipCallback callback)
        {
            if (LoadAudioClipAsyncHandlerImpl == null)
                throw new NotImplementedException();

            LoadAudioClipAsyncHandlerImpl(packageName, assetName, extension, callback);
        }
        public void UnloadAudioClip(string packageName, AudioClip audioClip)
        {
            if (UnloadAudioClipHandlerImpl == null)
                throw new NotImplementedException();

            UnloadAudioClipHandlerImpl(packageName, audioClip);
        }
    

        private string GetPackageAssetKey(string packageName)
        {
            return packageName + "_fui";
        }

        public void UnloadAllPackage()
        {
            m_PackageBytes.Clear();
        }
        public byte[] GetPackageBytes(string packageName)
        {
            m_PackageBytes.TryGetValue(packageName, out var bytes);
            if (bytes == null)
            {
                Debug.LogError($"<color='red'>AddressablesAssetLoader GetPackageBytes(): Failed to get package {packageName}, bytes is null.</color>");
                return null;
            }
            return bytes;
        }
    }

    
}