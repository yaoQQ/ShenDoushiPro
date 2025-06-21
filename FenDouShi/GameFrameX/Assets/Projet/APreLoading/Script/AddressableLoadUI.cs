using System.Collections;
using UnityEngine;
using FairyGUI;
using System.Net;
using UnityEngine.Networking;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Demonstrated how to load UI package from assetbundle. The bundle can be build from the Window Menu->Build FairyGUI example bundles.
/// </summary>
class AddressableLoadUI : MonoBehaviour
{
    GComponent _mainView;

    void Start()
    {
        Application.targetFrameRate = 60;

        Stage.inst.onKeyDown.Add(OnKeyDown);

        //AsyncOperationHandle HuotTuoHandle = Addressables.LoadAssetAsync<TextAsset>("HotFix");
        //Logger.PrintDebug("load HotFix begain");
        //HuotTuoHandle.Completed += (op) => {
        //    //  Debug.Log("3333 loadHuotTuoClass.Status=" + op.Status);
        //    if (op.Status == AsyncOperationStatus.Succeeded)
        //    {
        //        //   Debug.Log("==========222load HotFix complete========");
        //        TextAsset textAsset = (TextAsset)op.Result;
        //        //  Debug.Log("3333textAsset = " + textAsset);
        //        gameAss = System.Reflection.Assembly.Load(textAsset.bytes);
        //        Type test = gameAss.GetType("PrintHello");
        //        //  Debug.Log("3333textAsset  PrintHello= " + test);
        //        Logger.PrintDebug("load HotFix sucess");
        //        LoadHotPrefab();
        //    }
        //};
        Addressables.LoadAssetAsync<TextAsset>("BundleUsage_fui").Completed += OnLoadFairyUICompleted;
    }
    private void OnLoadFairyUICompleted(AsyncOperationHandle<TextAsset> op)
    {
        if (op.Status == AsyncOperationStatus.Succeeded)
        {
               Debug.Log("==========OnLoadFairyUICompleted complete========");
            TextAsset textAsset = (TextAsset)op.Result;
            //  Debug.Log("3333textAsset = " + textAsset);
            //gameAss = System.Reflection.Assembly.Load(textAsset.bytes);
  
        }

    }
    IEnumerator LoadUIPackage()
    {
        string url = Application.streamingAssetsPath.Replace("\\", "/") + "/fairygui-examples/bundleusage.ab";
        if (Application.isEditor)
        {
            UriBuilder uri = new UriBuilder(url);
            uri.Scheme = "file";
            url = uri.ToString();
        }

#if UNITY_2017_2_OR_NEWER

#if UNITY_2018_1_OR_NEWER
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
#else
        UnityWebRequest www = UnityWebRequest.GetAssetBundle(url);
#endif
        yield return www.SendWebRequest();

#if UNITY_2020_2_OR_NEWER
        if (www.result == UnityWebRequest.Result.Success)
#else
        if (!www.isNetworkError && !www.isHttpError)
#endif
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
#else
        WWW www = new WWW(url);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            AssetBundle bundle = www.assetBundle;
#endif
            if (bundle == null)
            {
                Debug.LogWarning("Run Window->Build FairyGUI example Bundles first.");
                yield return 0;
            }
            UIPackage.AddPackage(bundle);

            _mainView = UIPackage.CreateObject("BundleUsage", "Main").asCom;
            _mainView.fairyBatching = true;
            _mainView.MakeFullScreen();
            _mainView.AddRelation(GRoot.inst, RelationType.Size);

            GRoot.inst.AddChild(_mainView);
            _mainView.GetTransition("t0").Play();
        }
        else
        {
            Debug.LogWarning("Run Window->Build FairyGUI example Bundles first.");
        }
    }

    void OnKeyDown(EventContext context)
    {
        if (context.inputEvent.keyCode == KeyCode.Escape)
        {
            Application.Quit();
        }
    }
}
