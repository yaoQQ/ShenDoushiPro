using System.Collections.Generic;
using UnityEngine;

public class ResourceManagerPool : Singleton<ResourceManagerPool>
{
    private Dictionary<string, ResourcePoolItem> _poolItemCache = new Dictionary<string, ResourcePoolItem>();
    private static GameObject _poolManagerContent = null;



   
    //attachPrefab 是否已预制件设置初始化
    public GameObject GetPoolObject(string name, ResourceType type, bool attachPrefab = false)
    {
        string fullPath = name;

        ResourcePoolItem poolItem = null;
        if (_poolItemCache.ContainsKey(fullPath))
            poolItem = _poolItemCache[fullPath];
        else
        {
            poolItem = new ResourcePoolItem(fullPath);
            _poolItemCache[fullPath] = poolItem;
        }
        GameObject resultObj = poolItem.GetFromPool(attachPrefab, name, type);
      
       // if (resultObj == null) 
       //     return null;
        resultObj.name = name;

        ResourcePoolEnable poolEnable = resultObj.GetComponent<ResourcePoolEnable>();
        if (poolEnable != null)
        {
            poolEnable.prefabName = name;
            poolEnable.Active();
        }

        return resultObj;
    
    }
  

   
    public void ReturnPoolObject(string name, ResourceType type, GameObject obj)
    {
        if (_poolManagerContent == null)
        {
            _poolManagerContent = new GameObject("_poolManagerContent");
        }
        // string fullPath = GetPrefabFullPath(name, type);
        if (!_poolItemCache.ContainsKey(name))
        {
            Logger.PrintError("_poolItemCache 不存在name=【" + name + "】的PoolItem return");
            return;

        }

        obj.transform.parent = _poolManagerContent.transform;
        _poolItemCache[name].AddToPool(obj);
    }
    
    public bool RemoveGameObject(GameObject obj)
    {
        string fullPath = obj.name;
        if (_poolItemCache.ContainsKey(fullPath))
        {
            ResourcePoolItem poolItem = _poolItemCache[fullPath];

            poolItem.removeObj(obj);
            return true;
        }
        return false;
    }

    //public static string GetPrefabFullPath(string prefabName, ResourceType type)
    //{
    //    string path = "";
    //    if (type == ResourceType.bullet)
    //    {

    //        path = "Prefabs/Shoot/Bullet/";
    //    }
    //    else if (type == ResourceType.effect)
    //    {
    //        path = "Prefabs/Shoot/Effects/";
    //    }
    //    else if (type == ResourceType.gun)
    //    {
    //        path = "Prefabs/Shoot/Gun/";
    //    }
    //    else if (type == ResourceType.ship)
    //    {
    //        path = "Prefabs/Shoot/Model/Ships/";
    //    }
    //    else if (type == ResourceType.UI)
    //    {
    //        path = "Prefabs/Shoot/UI/";
    //    }
    //    else if (type == ResourceType.scene)
    //    {
    //        path = "Prefabs/Scenes/";
    //    }
    //    string fullPath = path + prefabName;
    //    return fullPath;
    //}

    //下一关卡调用
    private void CheckResource()
    {
        foreach (ResourcePoolItem pair in _poolItemCache.Values)
        {
            pair.CheckResourcePoolItem();
        }
    }
    //下一关卡调用
    public void Clear()
    {
        foreach(ResourcePoolItem pair in _poolItemCache.Values)
        {
            pair.Clear();
        }
        _poolItemCache.Clear();
        _poolManagerContent = null;
        
    }
   

}