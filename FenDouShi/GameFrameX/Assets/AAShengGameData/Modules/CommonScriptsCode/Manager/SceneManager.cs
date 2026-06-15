using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SceneManager : Singleton<SceneManager>
{
   public IScene curScene=null;

    /// <summary>
    /// 切换场景
    /// </summary>
    /// <param name="scene">场景类</param>
    /// <param name="onChangeEnd">加载完回调</param>
    public void ChangeScene(IScene scene, Action onChangeEnd = null)
    {
        MainThread.Instance.StartCoroutine(WaitForMinimumTime(scene, onChangeEnd));
    }
    private void AsynPreload(IScene scene, Action onChangeEnd = null)
    {
        var sceneHandle = Addressables.LoadSceneAsync(scene.getSceneName(), UnityEngine.SceneManagement.LoadSceneMode.Single);
        LoadingBarController.SetLoadContent("加载场景" + scene.getSceneName());

        sceneHandle.Completed += (op) =>
        {
            //加载完成
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                if (curScene != null)
                {
                    curScene.onLeave();
                }
                curScene = scene;
                curScene.onEnter();
                if (onChangeEnd != null)
                {
                    onChangeEnd();
                }
            }
            else if (op.Status == AsyncOperationStatus.Failed)
            {
                Logger.PrintError("加载场景失败 name=" + scene.getSceneName());
            }
            Logger.PrintDebug("加载场景完成 isDone=" + op.IsDone + " status=" + op.Status);
        };
    }

    /// <summary>
    /// 最小加载时间控制,避免高速加载导致UI闪烁：
    /// </summary>
    /// <param name="minTime">最小加载时间控制</param>
    /// <returns></returns>
    private IEnumerator WaitForMinimumTime(IScene scene, Action onChangeEnd = null)
    {
        LoadingBarController.Show();
        float elapsedTime = 0;
        LoadingBarController.SetLoadContent("加载场景" + scene.getSceneName());
        while (elapsedTime < 0.2f)
        {
            elapsedTime += Time.deltaTime;
            LoadingBarController.SetProgress(elapsedTime);
            yield return null;
        }
        AsynPreload(scene, onChangeEnd);
    }


    /// <summary>
    /// 重置当前场景
    /// </summary>
    public void Reset()
    {

    }
    public void Clear()
    {
        curScene = null;
        AssetNodeManager.ReleaseSceneNode();
    }
}
