using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourcePoolItem
{
    private  int POOL_MAX = 12;

    private GameObject _resPrefab = null;
    private List<GameObject> _objList = null;
 

    public ResourcePoolItem(string prefabFullPath)
    {
        //玩家最常用数量最多的子弹类型,特殊处理
        if(prefabFullPath== "BulletFast")
        {
            POOL_MAX = 20;
        }

        _resPrefab= OutSpaceResourceManager.Instance.getPrefabDirect(prefabFullPath);
        
        _objList = new List<GameObject>();
    }
    
    public void AddToPool(GameObject obj)
    {

        if (_objList.Contains(obj))
            return;
        if (_objList.Count >= POOL_MAX) 
		{
			GameObject.Destroy(obj);
			return;
		}
        obj.SetActive(false);
        if (obj == null)
        {
            Debug.LogError("AddToPool getObj == null");
        }
        _objList.Add(obj);
    }

    public GameObject GetFromPool(bool attachPrefab, string name, ResourceType type)
    {
         // Debug.Log(_resPrefab.name+" totalPool,count="+ _objList.Count);
      
        GameObject getObj = null;
        int poolCount = _objList.Count;
        if (poolCount > 0)
        {
            getObj = _objList[poolCount - 1];
            if (getObj == null)
            {
                CheckResourcePoolItem();
                poolCount = _objList.Count;
                if (poolCount > 0)
                {
                    getObj = _objList[poolCount - 1];
                }
            }
            _objList.RemoveAt(poolCount - 1);

        }
        else
        {
            getObj = GameObject.Instantiate(_resPrefab) as GameObject;
            if (attachPrefab)
            {
                getObj.transform.localPosition = _resPrefab.transform.position;
                getObj.transform.localEulerAngles = _resPrefab.transform.eulerAngles;
                getObj.transform.localScale = _resPrefab.transform.localScale;
            }

            if (getObj == null)
            {
                Debug.LogError("GameObject.Instantiate getObj == null");
            }
        }
        if (getObj == null)
        {
            Debug.Log(_resPrefab.name + " totalPool,count=" + _objList.Count);
            Debug.LogError("getObj == null");
        }
        //if (getObj != null)
        //{
            getObj.name =  name;
            getObj.SetActive(true);
       // }
    
       
        // Debug.Log(_resPrefab.name + "_objList.count=" + _objList.Count);
        return getObj;

    }
    public void removeObj(GameObject obj)
    {
        if (_objList == null|| obj==null)
        {
            return;
        }
        _objList.Remove(obj);
    }

    public void CheckResourcePoolItem()
    {
        Logger.PrintColor("blue", "=================CheckResourcePoolItem() name=" + _resPrefab.name + "====================");
        Logger.PrintColor("blue", "=================总数 _objList.count=" + _objList.Count);
        for (int i = _objList.Count - 1; i >= 0; i--)
        {
            if (_objList[i] == null)
            {
                Logger.PrintColor("red", "=================错误 CheckResourcePoolItem() i=" + i + "==null");
                _objList.RemoveAt(i);
            }
        }
    }
    public void Clear()
    {
        _resPrefab = null;
        _objList.Clear();
        _objList = null;
       
    }
    public int ItemCount()
    {
        if (_objList == null)
        {
            return 0;
        }
        return _objList.Count;
    }




}