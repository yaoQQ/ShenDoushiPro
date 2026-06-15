using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class FGUIOperatHandletManager : Singleton<FGUIOperatHandletManager>
{
    //所有包的依赖Addressable资源
    //<packageName,<keyPath,handle>
    private SortedList<string, SortedList<string, AsyncOperationHandle>> assetsHandles;

    public void Init() {
        assetsHandles=new ();


    }

    public void ShowAllPckageToDebug()
    {
        Logger.PrintColor("yellow", $"=========总包体资源加载情况===============");
        foreach (var op in assetsHandles)
        {
            Logger.PrintColor("yellow", $"=========包体【{op.Key}】所加载资源列表=======");
            foreach (var h in op.Value)
            {
                Logger.PrintDebug($"FGUIOperatHandletManager 【h.Key={h.Key}】【 h.Value={h.Value.Result}】 ");
            }
        }
    }

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="packageName">包名</param>
    /// <param name="address">所在addressables key</param>
    /// <returns></returns>
    public async UniTask<T> LoadAssetAsync<T>(string packageName, string address) where T : UnityEngine.Object
    {
        string keyPath = Utility.Platform.ConnectStrs(packageName, ":", address);
        if (assetsHandles.TryGetValue(packageName, out var handles))
        {
            if (handles.TryGetValue(keyPath, out var opHandle))
            {
                Logger.PrintColor("red", $"LoadAssetAsync() 已经存在 keyPath={keyPath}");
                return (T)opHandle.Result;
            }
        }
        else
        {
            handles = new();
            assetsHandles.Add(packageName, handles);
        }

        var handle = Addressables.LoadAssetAsync<T>(address);
        handles.Add(keyPath, handle);
        return await handle.ToUniTask();
    }

    public async UniTask<Texture> LoadTexture(string packageName, string path)
    {
        return await LoadAssetAsync<Texture>(packageName, path);
    }

    public async UniTask<AudioClip> LoadAudioClip(string packageName, string path)
    {
        return await LoadAssetAsync<AudioClip>(packageName, path);
    }

    public async UniTask<TextAsset> LoadText(string packageName, string path)
    {
        return await LoadAssetAsync<TextAsset>(packageName, path);
    }

    /// <summary>
    /// 同步加载
    /// </summary>
    /// <param name="packageName">包名</param>
    /// <param name="address">key在Addressables上地址</param>
    /// <returns></returns>
    public T LoadAsset<T>(string packageName, string address)
    {
        string keyPath = Utility.Platform.ConnectStrs(packageName, ":", address);
        if (assetsHandles.ContainsKey(packageName))
        {
            if (assetsHandles[packageName].TryGetValue(keyPath, out var opHandle))
            {
                Logger.PrintColor("yellow", $"LoadAssetAsync() 已经存在 keyPath={keyPath}");
                return (T)opHandle.Result;
            }
        }
        else
        {
            assetsHandles.Add(packageName, new SortedList<string, AsyncOperationHandle>());
        }


        var handle = Addressables.LoadAssetAsync<T>(address);
        handle.WaitForCompletion();
        assetsHandles[packageName].Add(keyPath, handle);
        return (T)handle.Result;
    }

    public void ReleasetByRelativePath(string packageName, string address)
    {
        if (!assetsHandles.ContainsKey(packageName))
        {
            return;
        }
        string keyPath = Utility.Platform.ConnectStrs(packageName, ":", address);
        if (assetsHandles[packageName].TryGetValue(keyPath, out var handle))
        {
            Addressables.Release(handle);
            assetsHandles[packageName].Remove(keyPath);
            Logger.PrintDebug($"UnloadAsset() address={address}");
        }
    }
    public void ReleasePackage(string packageName)
    {
        if (assetsHandles.TryGetValue(packageName, out var assertPoHandles))
        {
            int valueCount = assertPoHandles.Values.Count;
            for (int i = valueCount - 1; i >= 0; i--)
            {
                var handle = assertPoHandles.Values[i];
                Addressables.Release(handle);
            }
            assetsHandles.Remove(packageName);
        }
    }
    public void ReleaseAsset(string packageName, string assetStr)
    {
        if (assetsHandles.TryGetValue(packageName, out var assertPoHandles))
        {
            if (assertPoHandles.TryGetValue(assetStr, out var handle))
            {
                Addressables.Release(handle);
                assetsHandles[packageName].Remove(assetStr);
            }
        }
    }

    public void ReleaseAllAssets()
    {
        foreach (var handle in assetsHandles.Values)
        {
            foreach (var op in handle.Values)
            {
                Addressables.Release(op);
            }
        }
        assetsHandles.Clear();
        Logger.PrintGreen("FGUIOperatHandletManager.ReleaseAllAssets() assetsHandles.Values=" + assetsHandles.Values);
        assetsHandles =null;
        Logger.PrintGreen("FGUIOperatHandletManager.ReleaseAllAssets() assetsHandles=" + assetsHandles);
    }
}

