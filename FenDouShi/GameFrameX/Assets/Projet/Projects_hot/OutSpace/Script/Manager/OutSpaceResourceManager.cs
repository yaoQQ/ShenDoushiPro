
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class OutSpaceResourceManager : Singleton<OutSpaceResourceManager>
{
    private Dictionary<string, GameObject> PrefabDic=new Dictionary<string, GameObject>();//特效对象池
    private Dictionary<string, Sprite> ImgDic = new Dictionary<string, Sprite>();//特效对象
    private Dictionary<string, GunInfo> gunInfoDic = new Dictionary<string, GunInfo>();//枪的的信息
    private Dictionary<string, AttriInfo> attriInfoDic = new Dictionary<string, AttriInfo>();//玩家属性

    private Dictionary<string, BoidMotionType> motionDic = new Dictionary<string, BoidMotionType>();//动画配置
    /// <summary>
    /// 获取玩家的添加的信息属性
    /// </summary>
    /// <param name="attriTypeStr">属性信息prefabe</param>
    /// <param name="callBack"> 加载成功回调</param>
    public void GetAttriInfo(string attriTypeStr, Action<AttriInfo> callBack)
    {
        //string lable = ProjectControler.OutSpacePro;
        AttriInfo attriInfo = null;
        if (attriInfoDic.ContainsKey(attriTypeStr))
        {
            attriInfo = attriInfoDic[attriTypeStr];
            callBack(attriInfo);
            // Logger.PrintDebug("@@@attriInfoDic.count" + attriInfoDic.Count);
        }
        else
        {
            AttriInfo newAttriInfo= ScriptableObject.CreateInstance<AttriInfo>();
            Logger.PrintColor("yellow", "强转 【isAttriInfo】newAttriInfo=" + newAttriInfo);
            AsyncOperationHandle<AttriInfo> boundleOper = Addressables.LoadAssetAsync<AttriInfo>(attriTypeStr);
            boundleOper.Completed += op =>
            {

                AssetLoadStatus mAssetLoadStatus = (op.Result != null) ? AssetLoadStatus.Success : AssetLoadStatus.Failed;
                if (mAssetLoadStatus == AssetLoadStatus.Success)
                {
                     AttriInfo attriList = op.Result as AttriInfo;
                    bool isAttriInfo = op.Result is AttriInfo;
                    bool isAttriScriptableObject = op.Result is ScriptableObject;
                    Logger.PrintColor("yellow", "强转  op.Result=" + op.Result);
                    Logger.PrintColor("yellow", "强转  op.Result.GetType()=" + op.Result.GetType());
                    Logger.PrintColor("yellow", "强转 【isAttriInfo】=" + isAttriInfo);
                    Logger.PrintColor("yellow", "强转 isAttriScriptableObject=" + isAttriScriptableObject);
                    Logger.PrintColor("yellow", "强转 【attriList】=" + attriList);
                    attriInfo = attriList;
                    attriInfoDic[attriTypeStr] = attriInfo;

                    Logger.PrintColor("green", "加载AttriInfo[ " + attriInfo + "]成功");
                    if (callBack != null)
                    {
                        callBack(attriInfo);
                    }
                }
                else if (mAssetLoadStatus == AssetLoadStatus.Failed)
                {
                    Logger.PrintColor("red", "加载失败  " + attriInfo);
                    return;
                }

            };
        }
    }
    /// <summary>
    /// 获取枪等级的信息属性
    /// </summary>
    /// <param name="gunTypeStr">枪的等级信息prefabe</param>
    /// <param name="callBack"> 加载成功回调</param>
    public void GetGunInfo(string gunTypeStr, Action<GunInfo> callBack)
    {
        GunInfo gunInfo = null;
        if (gunInfoDic.ContainsKey(gunTypeStr))
        {
            gunInfo = gunInfoDic[gunTypeStr];
            callBack(gunInfo);
            Logger.PrintDebug("@@@优化gunInfoDic.count" + gunInfoDic.Count);
        }
        else
        {
            AsyncOperationHandle<System.Object> boundleOper = Addressables.LoadAssetAsync<System.Object>(gunTypeStr);
            boundleOper.Completed += op =>
            {

                AssetLoadStatus mAssetLoadStatus = (op.Result != null) ? AssetLoadStatus.Success : AssetLoadStatus.Failed;
                if (mAssetLoadStatus == AssetLoadStatus.Success)
                {

                    GunInfo gunList = op.Result as GunInfo;
                    bool isgunInfo = op.Result is GunInfo;
                    bool isGunScriptableObject = op.Result is ScriptableObject;
                    bool isIlistGunScriptableObject = op.Result is System.Object;
                    Logger.PrintColor("green", "强转  op.Result=" + op.Result);
                    Logger.PrintColor("green", "强转 【isgunInfo】=" + isgunInfo);
                    Logger.PrintColor("green", "强转 isGunScriptableObject=" + isGunScriptableObject);
                    Logger.PrintColor("green", "强转 isIlistGunScriptableObject=" + isIlistGunScriptableObject);
                    Logger.PrintColor("green", "强转 【asgunList】=" + gunList);
                    gunInfo = gunList;
                    gunInfoDic[gunTypeStr] = gunInfo;

                    Logger.PrintColor("green", "加载GunInfo[ " + gunTypeStr + "]成功");
                    if (callBack != null)
                    {
                        callBack(gunInfo);
                    }
                }
                else if (mAssetLoadStatus == AssetLoadStatus.Failed)
                {
                    Logger.PrintColor("red", "加载失败  " + gunTypeStr);
                    return;
                }

            };
        }
    }
    ///// <summary>
    ///// 获取玩家的添加的信息属性
    ///// </summary>
    ///// <param name="attriTypeStr">属性信息prefabe</param>
    ///// <param name="callBack"> 加载成功回调</param>
    //public void GetAttriInfo(string attriTypeStr, Action<AttriInfo> callBack)
    //{
    //    string lable = ProjectControler.OutSpacePro;
    //    AttriInfo attriInfo = null;
    //    if (attriInfoDic.ContainsKey(attriTypeStr))
    //    {
    //        attriInfo = attriInfoDic[attriTypeStr];
    //        callBack(attriInfo);
    //       // Logger.PrintDebug("@@@attriInfoDic.count" + attriInfoDic.Count);
    //    }
    //    else
    //    {
    //        AsyncOperationHandle<IList<UnityEngine.Object>> boundleOper = Addressables.LoadAssetsAsync<UnityEngine.Object>(new List<string> { attriTypeStr, lable }, null, Addressables.MergeMode.Intersection);
    //        boundleOper.Completed += op =>
    //        {

    //            AssetLoadStatus mAssetLoadStatus = (op.Result != null) ? AssetLoadStatus.Success : AssetLoadStatus.Failed;
    //            if (mAssetLoadStatus == AssetLoadStatus.Success)
    //            {
    //                List<AttriInfo> attriList = op.Result as List<AttriInfo>;
    //                Logger.PrintColor("green", "强转  op.Result=" + op.Result);
    //                Logger.PrintColor("green", "强转  op.Result.GetType()=" + op.Result.GetType());
    //                Logger.PrintColor("green", "强转 attriList=" + attriList);
    //                attriInfo = attriList[0];
    //                attriInfoDic[attriTypeStr] = attriInfo;

    //                Logger.PrintColor("green", "加载AttriInfo[ " + attriInfo + "]成功");
    //                if (callBack != null)
    //                {
    //                    callBack(attriInfo);
    //                }
    //            }
    //            else if (mAssetLoadStatus == AssetLoadStatus.Failed)
    //            {
    //                Logger.PrintColor("red", "加载失败  " + attriInfo);
    //                return;
    //            }

    //        };
    //    }
    //}
    ///// <summary>
    ///// 获取枪等级的信息属性
    ///// </summary>
    ///// <param name="gunTypeStr">枪的等级信息prefabe</param>
    ///// <param name="callBack"> 加载成功回调</param>
    //public void GetGunInfo(string gunTypeStr, Action<GunInfo> callBack)
    //{
    //    string lable = ProjectControler.OutSpacePro;
    //    GunInfo gunInfo = null;
    //    if (gunInfoDic.ContainsKey(gunTypeStr))
    //    {
    //        gunInfo = gunInfoDic[gunTypeStr];
    //        callBack(gunInfo);
    //        Logger.PrintDebug("@@@优化gunInfoDic.count" + gunInfoDic.Count);
    //    }
    //    else
    //    {
    //        AsyncOperationHandle<IList<UnityEngine.Object>> boundleOper = Addressables.LoadAssetsAsync<UnityEngine.Object>(new List<string> { gunTypeStr, lable }, null, Addressables.MergeMode.Intersection);
    //        boundleOper.Completed += op =>
    //        {

    //            AssetLoadStatus mAssetLoadStatus = (op.Result != null) ? AssetLoadStatus.Success : AssetLoadStatus.Failed;
    //            if (mAssetLoadStatus == AssetLoadStatus.Success)
    //            {
    //               bool isGunInfo = op.Result is List<GunInfo>;
    //                List<GunInfo> gunList = op.Result as List<GunInfo>;
    //                Logger.PrintColor("green", "强转  op.Result=" + op.Result);
    //                Logger.PrintColor("green", "强转  isGunInfo=" + isGunInfo);
    //                Logger.PrintColor("green", "强转 gunList=" + gunList);
    //                gunInfo = gunList[0];
    //                gunInfoDic[gunTypeStr] = gunInfo;

    //                Logger.PrintColor("green", "加载GunInfo[ " + gunTypeStr + "]成功");
    //                if (callBack != null)
    //                {
    //                    callBack(gunInfo);
    //                }
    //            }
    //            else if (mAssetLoadStatus == AssetLoadStatus.Failed)
    //            {
    //                Logger.PrintColor("red", "加载失败  " + gunTypeStr);
    //                return;
    //            }

    //        };
    //    }
    //}
    public void RemoveGunInfo(string gunTypeStr)
    {
        if (gunInfoDic.ContainsKey(gunTypeStr))
        {
            gunInfoDic.Remove(gunTypeStr);
        }
    }
    public Sprite GetSpriteByName(string spriteName, Action<Sprite> callBack)
    {
        string lable = ProjectControler.OutSpacePro;
        Sprite getObj = null;
        if (ImgDic.ContainsKey(spriteName))
        {
            Sprite ObjBuddle = ImgDic[spriteName];
            getObj = GameObject.Instantiate<Sprite>(ObjBuddle);
            callBack(getObj);
        }
        else
        {
            AsyncOperationHandle<IList<Sprite>> boundleOper = Addressables.LoadAssetsAsync<Sprite>(new List<string> { spriteName, lable }, null, Addressables.MergeMode.Intersection);
            boundleOper.Completed += op =>
            {

                AssetLoadStatus mAssetLoadStatus = (op.Result != null) ? AssetLoadStatus.Success : AssetLoadStatus.Failed;
                if(mAssetLoadStatus== AssetLoadStatus.Success)
                {
                    List<Sprite> imgList = op.Result as List<Sprite>;
                    getObj = imgList[0];
                    ImgDic[spriteName] = getObj;
                    Sprite cloneObj = GameObject.Instantiate<Sprite>(getObj);
                    Logger.PrintColor("green", "加载图片[ " + spriteName + "]成功");
                    if (callBack != null)
                    {
                        callBack(cloneObj);
                    }
                }
                else if (mAssetLoadStatus == AssetLoadStatus.Failed)
                {
                    Logger.PrintColor("red", "加载失败  " + spriteName);
                    return;
                }
               
            };
        }
        return getObj;
    }
    /// <summary>
    /// 获得Assert资源
    /// </summary>
    /// <param name="addressStr">地址</param>
    /// <returns></returns>
    public GameObject getPrefabDirect(string addressStr)
    {
       
        string lable = ProjectControler.OutSpacePro;
        GameObject getObj = null;
        if (PrefabDic.ContainsKey(addressStr))
        {
            getObj = PrefabDic[addressStr];
        }
        else
        {
            AsyncOperationHandle<IList<GameObject>> boundleOper = Addressables.LoadAssetsAsync<GameObject>(new List<string> { addressStr, lable }, null, Addressables.MergeMode.Intersection);
            IList<GameObject> objList = boundleOper.WaitForCompletion();
         
            if (objList != null && objList.Count > 0)
            {
                getObj = objList[0];
                Logger.PrintColor("blue", "@@@加载Prefabe 成功 addressStr="+ getObj);
                PrefabDic[addressStr] = getObj;
            }
            else
            {
                Logger.PrintError("加载错误 packName" + ProjectControler.OutSpacePro + " addressStr=" + addressStr + " objList=" + objList);
            }
        }
        return getObj;
    }
    public void getPrefabByPath(string modelName, System.Action<GameObject> onLoadend)
    {
        GameObject obj = null;
        if (PrefabDic.ContainsKey(modelName))
        {
            obj = PrefabDic[modelName];
        }
        else
        {
          //  obj = Resources.Load(path) as GameObject;
            ModelManager.Instance.CreateModel(ProjectControler.OutSpacePro, modelName, (obj) =>
            {
                if (obj == null)
                {
                    Logger.PrintError("加载错误 packName" + ProjectControler.OutSpacePro + " modelName=" + modelName + " obj=" + obj);
                    return;
                }
                    Logger.PrintColor("blue", "ModelManager.Instance:CreateModel() 加载返回 obj=" + obj);
                    PrefabDic[modelName] = obj;
            });

          
        }
        onLoadend?.Invoke(obj);
    }
    /// <summary>
    /// 获取群组的资源信息属性
    /// </summary>
    /// <param name="motionStr">群组的资源名prefabe</param>
    /// <param name="callBack"> 加载成功回调</param>
    public void GetBoidsMotionAssert(string motionStrPath, Action<BoidMotionType> callBack)
    {
        if (motionDic.ContainsKey(motionStrPath))
        {
            callBack(motionDic[motionStrPath]);
            return;
        }
        string lable = ProjectControler.OutSpacePro;
        BoidMotionType boidMotion = null;

        AsyncOperationHandle<IList<BoidMotionType>> boundleOper = Addressables.LoadAssetsAsync<BoidMotionType>(new List<string> { motionStrPath, lable }, null, Addressables.MergeMode.Intersection);
        boundleOper.WaitForCompletion();
        boundleOper.Completed += op =>
        {

            AssetLoadStatus mAssetLoadStatus = (op.Result != null) ? AssetLoadStatus.Success : AssetLoadStatus.Failed;
            if (mAssetLoadStatus == AssetLoadStatus.Success)
            {
                List<BoidMotionType> imgList = op.Result as List<BoidMotionType>;
                boidMotion = imgList[0];


                Logger.PrintColor("green", "加载motionStrPath[ " + motionStrPath + "]成功");
                if (callBack != null)
                {
                    callBack(boidMotion);
                }
            }
            else if (mAssetLoadStatus == AssetLoadStatus.Failed)
            {
                Logger.PrintColor("red", "加载失败  " + motionStrPath);
                return;
            }

        };
    }

    public void clearAll()
    {

        PrefabDic.Clear();
        ImgDic.Clear();
        gunInfoDic.Clear();
        attriInfoDic.Clear();
        motionDic.Clear();
        ResourceManagerPool.Instance.Clear();
        GunManager.Instance.Clear();
        MonsterManager.Instance.Clear();
        OutSpacePlayerInfoManager.Instance.Clear();
        ModelManager.Instance.DestroyPackageModel(ProjectControler.OutSpacePro);
    }



}

