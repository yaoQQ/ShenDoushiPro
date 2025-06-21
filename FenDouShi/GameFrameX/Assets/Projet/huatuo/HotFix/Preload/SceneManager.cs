using UnityEngine;
using System.Collections;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets;

public class SceneManager : Singleton<SceneManager>
{
   public LuaScene curLuaScene=null;

    /// <summary>
    /// 切换场景
    /// </summary>
    /// <param name="scene">场景类</param>
    /// <param name="onChangeEnd">加载完回调</param>
    public void ChangeScene(LuaScene scene, Action onChangeEnd = null)
    {
        curLuaScene = scene;
        AsyncOperationHandle<SceneInstance> sceneHandle = Addressables.LoadSceneAsync(curLuaScene.getSceneName(), UnityEngine.SceneManagement.LoadSceneMode.Single);
        LoadingBarController.SetLoadContent("加载场景"+ scene.getSceneName());
        LoadingBarController.setLoadHandle(sceneHandle);
        sceneHandle.Completed += (op) =>
        {
            //加载完成
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                if (onChangeEnd != null)
                {
                    onChangeEnd();
                    LoadingBarController.SetLoadPercent("100%");
                }
              //  curLuaScene.onEnter();
            }
            else if(op.Status == AsyncOperationStatus.Failed)
            {
                Logger.PrintError("加载场景失败 name="+ scene.getSceneName());
            }
        };
      
    }

    public void ShowLittlePrinceScene()
    {

    }


    /// <summary>
    /// 重置当前场景
    /// </summary>
    public void Reset()
    {

    }
    public void Clear()
    {
        if (curLuaScene != null)
        {
            curLuaScene.onLeave();
        }
        curLuaScene = null;
        AssetNodeManager.ReleaseSceneNode();
    }
}
