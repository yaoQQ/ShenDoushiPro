using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class ModelManager : Singleton<ModelManager>
{
    private Dictionary<string, List<string>> m_dict = new Dictionary<string, List<string>>();
    private Dictionary<string, AsyncOperationHandle> modeleHandleDic = new Dictionary<string, AsyncOperationHandle>();

    public async Task<RoleStageContent> GetRoleStageContent(GGraph content)
    {
        try
        {
            var op = await ModelManager.Instance.AsyncCreateModel("RoleInfoContent");
            Logger.PrintDebug($"SetRoleStageContent  op={op.Result}");
            GameObject go = GameObject.Instantiate(op.Result);
            RoleStageContent model = go.GetComponent<RoleStageContent>();
            model.SetRoleStageContent(content);
            return model;
        }
        catch { 
        
            Logger.PrintError("GetRoleStageContent error");
        }
       return null;
    }

    public async Task<AsyncOperationHandle<GameObject>> AsyncCreateModel(string modelName)
    {
        if (modeleHandleDic.ContainsKey(modelName))
        {
            var handle = modeleHandleDic[modelName];
            // 检查句柄是否有效且加载成功
            if (handle.IsValid() && handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Convert<GameObject>();
            }
        }
        try
        {
            // 使用 Addressables 异步加载游戏对象
            var op = Addressables.LoadAssetAsync<GameObject>(modelName);
            await op.Task;

            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                // 记录加载句柄
                if (!modeleHandleDic.ContainsKey(modelName))
                {
                    modeleHandleDic.Add(modelName, op);
                }
                return op;
            }
            else
            {
                Debug.LogError($"加载模型 {modelName} 失败，状态: {op.Status}");
                return default;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"加载模型 {modelName} 时发生异常: {e.Message}");
            return default;
        }
    }
    public void DestroyModel(GameObject model)
    {
        if (model != null)
            GameObject.Destroy(model);
        
    }

    public void CreateModel(string packName, string modelName, Action<GameObject> onLoadend, Action<GameObject> callBackFun=null)
    {
        //packName = packName.ToLower();
        //modelName = modelName.ToLower();
        MainThread.Instance.StartCoroutine(AsyncCreateModel(packName, modelName, onLoadend));
    }
    public void CreateModel(string packName, string modelName, Action<GameObject> callBack, Action<GameObject, Action<GameObject>> onLoadend) {
        //packName = packName.ToLower();
        //modelName = modelName.ToLower();
        MainThread.Instance.StartCoroutine(AsyncCreateModelCallBack(packName, modelName, callBack, onLoadend));
    }


    private IEnumerator AsyncCreateModel(string packName, string modelName, Action<GameObject> onLoadend)
    {

        ResLoadManager.LoadAsync(AssetType.Model, packName, modelName, (relativePath, res) =>
        {
            if (!m_dict.ContainsKey(packName))
            {
                m_dict.Add(packName, new List<string>());
                m_dict[packName].Add(modelName);
            }
            else
            {
                if (!m_dict[packName].Contains(modelName))
                    m_dict[packName].Add(modelName);
            }
            if (onLoadend != null)
            {
                onLoadend.Invoke(res as GameObject);
            }
        });
        yield return null;
    }
    private IEnumerator AsyncCreateModelCallBack(string packName, string modelName, Action<GameObject> callBack, Action<GameObject, Action<GameObject>> onLoadend) {
        string abRelativePath2 = modelName;
        //string loadRootDir = Utility.Path.getLoadRootDir(packName, abRelativePath2);

        //string fullPath = Utility.Platform.ConnectStrs(loadRootDir, Path.PathWithENcrypt(abRelativePath2));

        //string getLoadRootDirStr = Path.getLoadRootDir(packName, abRelativePath2);
        //string PathWithENcryptStr = Path.PathWithENcrypt(abRelativePath2);
        //string fullPath2 = Utility.Platform.ConnectStrs(getLoadRootDirStr, PathWithENcryptStr);
        string abRelativePath = modelName;
        ResLoadManager.LoadAsync(AssetType.Model, packName, abRelativePath, (relativePath, res) => {
            if (!m_dict.ContainsKey(packName)) {
                m_dict.Add(packName, new List<string>());
                m_dict[packName].Add(abRelativePath);
            }
            else {
                if (!m_dict[packName].Contains(abRelativePath))
                    m_dict[packName].Add(abRelativePath);
            }
            if (onLoadend != null) {
                onLoadend.Invoke(res as GameObject, callBack);
            }
            if (callBack != null) {
                callBack(res as GameObject);
            }
        
        });
        yield return null;
    }

    //[BlackList]
    /// <summary>
    /// ??????????????????
    /// </summary>
    /// <param name="packageName"></param>
    public void DestroyPackageModel(string packageName)
    {
        if (!m_dict.ContainsKey(packageName))
            return;
        for (int i = 0, count = m_dict[packageName].Count; i < count; ++i)
        {
            //Logger.PrintLog(Utility.Platform.ConnectStrs("ж??????", m_dict[packageName][i]));
            AssetNodeManager.ReleaseNode(AssetType.Model, packageName, m_dict[packageName][i]);
        }
    }

    /// <summary>
    /// ???????????
    /// </summary>
    /// <param name="packageName"></param>
    public void DestroyAllPackageModel()
    {
        foreach (var item in m_dict) 
        { 
            if (item.Value != null)
            {
                int count = item.Value.Count;
                for (int i = 0 ; i < count; ++i)
                {
                    AssetNodeManager.ReleaseNode(AssetType.Model, item.Key, item.Value[i]);
                }
            }
           

         }
        foreach (var item in modeleHandleDic)
        {
            Addressables.Release(item.Value);
         }
        modeleHandleDic.Clear();
    }
}