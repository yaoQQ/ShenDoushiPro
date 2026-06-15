using FairyGUI;
using UnityEngine;

public class UIObjPool : Singleton<UIObjPool>
{
    private GObjectPool mPool;
    private Transform _poolRoot;
    private Transform poolRoot
    {
        get
        {
            if (_poolRoot != null) return _poolRoot;
            var gameObject = new GameObject("poolRoot");
            _poolRoot = gameObject.transform;
            if (Application.isPlaying)
            {
                UnityEngine.Object.DontDestroyOnLoad(gameObject);
            }
            gameObject.hideFlags = 0;
            gameObject.SetActive(false);
            return _poolRoot;
        }
    }
    private GObject GetObjectInner(string url)
    {
        mPool ??= new GObjectPool(poolRoot);
        return mPool.GetObject(url);
    }

    public GObject GetObject(string url)
    {
        return GetObjectInner(url);
    }

    public GObject GetObject(string package, string compoment)
    {
        var url = UIPackage.NormalizeURL(UIHelper.GetFguiUrl(package, compoment));
        return GetObjectInner(url);
    }

    public void ReturnObject(GObject gameObj)
    {
        if (gameObj == null)
            return;
        if (string.IsNullOrEmpty(gameObj.resourceURL))
        {
            gameObj.Dispose();
            return;
        }
        mPool ??= new GObjectPool(poolRoot);
        mPool.ReturnObject(gameObj);
    }

    /// <summary>
    /// 清除对象池
    /// </summary>
    public void Clear()
    {
        if (mPool != null)
        {
            mPool.Clear();
            mPool = null;
        }
        if (_poolRoot != null)
        {
            UnityEngine.Object.Destroy(_poolRoot.gameObject);
            _poolRoot = null;
        }
    }
}
