using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace FairyGUI.Dynamic
{
    /// <summary>
    /// 自定义委托的IUIAssetLoader派生类 为不方便实现接口的情况提供功能支持
    /// </summary>
    public sealed class DelegateUIAssetLoader : IUIAssetLoader
    {
        public delegate void LoadUIPackageBytesAsyncHandler(string packageName, LoadUIPackageBytesCallback callback);

        public delegate void LoadUIPackageBytesHandler(string packageName, out byte[] bytes, out string assetNamePrefix);
        public delegate UniTask<(byte[] bytes, string assetNamePrefix)> LoadUIPackageBytesHandler2(string packageName);

        public delegate void LoadTextureAsyncHandler(string packageName, string assetName, string extension, LoadTextureCallback callback);

        public delegate void UnloadTextureHandler(string packageName, Texture texture);

        public delegate void LoadAudioClipAsyncHandler(string packageName, string assetName, string extension, LoadAudioClipCallback callback);

        public delegate void UnloadAudioClipHandler(string packageName, AudioClip audioClip);

        public LoadUIPackageBytesAsyncHandler LoadUIPackageBytesAsyncHandlerImpl { get; set; }
        public LoadUIPackageBytesHandler LoadUIPackageBytesHandlerImpl { get; set; }
        public LoadUIPackageBytesHandler2 LoadUIPackageBytesHandlerImpl2 { get; set; }
        public LoadTextureAsyncHandler LoadTextureAsyncHandlerImpl { get; set; }
        public UnloadTextureHandler UnloadTextureHandlerImpl { get; set; }
        public LoadAudioClipAsyncHandler LoadAudioClipAsyncHandlerImpl { get; set; }
        public UnloadAudioClipHandler UnloadAudioClipHandlerImpl { get; set; }

        public void LoadUIPackageBytesAsync(string packageName, LoadUIPackageBytesCallback callback)
        {
            if (LoadUIPackageBytesAsyncHandlerImpl == null)
                throw new NotImplementedException();

            LoadUIPackageBytesAsyncHandlerImpl(packageName, callback);
        }

        public void LoadUIPackageBytes(string packageName, out byte[] bytes, out string assetNamePrefix)
        {
            if (LoadUIPackageBytesHandlerImpl == null)
                throw new NotImplementedException();
            LoadUIPackageBytesHandlerImpl(packageName, out bytes, out assetNamePrefix);
        }
        public async UniTask<(byte[] bytes, string assetNamePrefix)> LoadUIPackageBytes2(string packageName)
        {
            if (LoadUIPackageBytesHandlerImpl2 == null)
                throw new NotImplementedException();
           return await LoadUIPackageBytesHandlerImpl2(packageName);

        }

        public void LoadTextureAsync(string packageName, string assetName, string extension, LoadTextureCallback callback)
        {
            if (LoadTextureAsyncHandlerImpl == null)
                throw new NotImplementedException();
            LoadTextureAsyncHandlerImpl(packageName,assetName, extension, callback);
        }

        public void UnloadTexture(string packageName, Texture texture)
        {
            if (UnloadTextureHandlerImpl == null)
                throw new NotImplementedException();
            UnloadTextureHandlerImpl(packageName,texture);
        }

        public void LoadAudioClipAsync(string packageName, string assetName, string extension, LoadAudioClipCallback callback)
        {
            if (LoadAudioClipAsyncHandlerImpl == null)
                throw new NotImplementedException();

            LoadAudioClipAsyncHandlerImpl(packageName,assetName, extension, callback);
        }

        public void UnloadAudioClip(string packageName, AudioClip audioClip)
        {
            if (UnloadAudioClipHandlerImpl == null)
                throw new NotImplementedException();

            UnloadAudioClipHandlerImpl(packageName,audioClip);
        }

        public void UnloadAllPackage()
        {

        }
    }
}