using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class ResourceManager
{

    public static async Task<SceneInstance> LoadScene(string sceneName, LoadSceneMode mode, Action<float> update, bool isActiveOnLoaded = true, int priority = 100)
    {
        var handle = Addressables.LoadSceneAsync(sceneName, mode, isActiveOnLoaded, priority);

        //var _update = GlobalMonoBehavior.Instance.AddUpdate(e: () =>
        //{
        //    update?.Invoke(handle.PercentComplete);
        //});

        var res = await handle.Task;

      //  GlobalMonoBehavior.Instance.RemoveUpdate(_update);

        return res;
    }

    public static async Task<SceneInstance> UnloadScene(SceneInstance sceneInstance, bool autoReleaseHandler = true)
    {
        var res = await Addressables.UnloadSceneAsync(sceneInstance, autoReleaseHandler).Task;
        return res;
    }

    public static void UnloadSceneByName()
    {
        SceneManager.Instance.Clear();
    }

    public static async Task<GameObject> LoadPrefab(string address)
    {
        var res = await Addressables.LoadAssetAsync<GameObject>(address).Task;

        return res;
    }


    public static async Task<TextAsset> LoadTextAsset(string address)
    {
        var res = await Addressables.LoadAssetAsync<TextAsset>(address).Task;

        return res;
    }
}