using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class AddressableFairyguiAssetNode : AssetNode
{
    /// <summary>资源节点的AssetBundle的加载状态</summary>
    public AssetBundleLoadStatus mAssetBundleLoadStatus = AssetBundleLoadStatus.None;
    /// <summary>资源节点的Asset的加载状态</summary>
    public AssetLoadStatus mAssetLoadStatus = AssetLoadStatus.None;
    private AsyncOperationHandle boundleOper = new AsyncOperationHandle();//加载addressable的引用
    public override bool IsAssetLoadSuccess
    {
        get { return mAssetLoadStatus == AssetLoadStatus.Success; }
    }
    public AsyncOperationHandle operationHandle
    {
        get
        {
            return boundleOper;
        }
    }

    public AddressableFairyguiAssetNode(AssetType assetType, string packageName, string relativePath) : base(assetType, packageName, relativePath)
    {
        Logger.PrintColor("blue", "AddressableFairyguiAssetNode assetType=" + assetType + " packageName =" + packageName + " relativePath=" + relativePath);
    }
    public void LoadFromFileAsync()
    {

        if (mAssetLoadStatus == AssetLoadStatus.Loading)
        {
            return;
        }
        string lable = packageName;
        string addressStr = relativePath;
        string[] oriStr = addressStr.Split('/');
        if (oriStr.Length > 1)
        {
            addressStr = oriStr.GetValue(oriStr.Length - 1) as string;
        }

        // Logger.PrintColor("black", "准备加载 AddressablePrefabeNode LoadFromFileAsync addressStr=" + addressStr + " lable=" + lable);
        mAssetLoadStatus = AssetLoadStatus.Loading;
        switch (assetType)
        {
            case AssetType.FairyUI:
                boundleOper = Addressables.LoadAssetsAsync<Object>(new List<string> { addressStr, lable }, null, Addressables.MergeMode.Intersection);
                updateCompleteState();
                break;
            case AssetType.Audio:
                boundleOper = Addressables.LoadAssetsAsync<AudioClip>(new List<string> { addressStr, lable }, null, Addressables.MergeMode.Intersection);
                updateCompleteState();
                break;
            case AssetType.Text:
                break;
            default:
                break;
        }
    }

    private void updateCompleteState()
    {
        boundleOper.Completed += op =>
        {

            mAssetLoadStatus = (op.Result != null) ? AssetLoadStatus.Success : AssetLoadStatus.Failed;
            object obj = getResult(boundleOper);
            if (mAssetLoadStatus == AssetLoadStatus.Failed)
            {
                Logger.PrintColor("red", "加载失败  " + this.relativePath);
                return;
            }
            Logger.PrintColor("green", "加载完成 AddressablePrefabeNode " + " result= " + obj.ToString() + " assetType=" + assetType + "   mAssetLoadStatus=" + mAssetLoadStatus);
        };
    }


    public override object GetAsset()
    {
        if (boundleOper.IsDone && boundleOper.Result != null)
        {
            //Logger.PrintColor("red","AddressablePrefabeNode GetAsset()="+ boundleOper.Result);
            return getResult(boundleOper);
        }
        Logger.PrintError("AddressablePrefabeNode 获取资源失败:" + this.relativePath);
        return null;
    }
    private object getResult(AsyncOperationHandle handle)
    {
        //FairyUI 返回AsyncOperationHandle
        //其他返回资源
        if (assetType == AssetType.FairyUI)
        {
            return handle;
        }
        else if (handle.Result is IList)
        {//不是场景加载
            switch (this.assetType)
            {
                case AssetType.Audio:
                    List<AudioClip> audioList = handle.Result as List<AudioClip>;
                    if (audioList.Count > 0)
                    {
                        //Logger.PrintColor("red", "getResult() op.Result[0]=" + objList[0]);
                        return audioList[0];
                    }
                    return null;
                default:
                    List<GameObject> objList = handle.Result as List<GameObject>;
                    if (objList.Count > 0)
                    {
                        //Logger.PrintColor("red", "getResult() op.Result[0]=" + objList[0]);
                        return objList[0];
                    }
                    return null;
            }

        }
        return null;
    }
    public override void Release()
    {
        Addressables.Release(boundleOper);
        mAssetLoadStatus = AssetLoadStatus.None;
    }


    ///// <summary>
    ///// 没加载过资源调用  可以设置成加载依赖
    ///// </summary>
    ///// <param name="assetBundle">加载完成的资源</param>
    //public virtual IEnumerator<object> OnAssetBundleLoadedAsync()
    //{

    //    if (!boundleOper.IsValid() || boundleOper.Status == AsyncOperationStatus.Failed)
    //    {
    //        Logger.PrintLog("AddressablePrefabeNode OnAssetBundleLoadedAsync() 重新加载资源");
    //        LoadFromFileAsync();
    //        yield break;
    //    }
    //    yield return GetAsset();
    //}

}