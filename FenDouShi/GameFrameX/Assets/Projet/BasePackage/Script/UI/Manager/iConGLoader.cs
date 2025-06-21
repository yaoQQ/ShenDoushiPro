using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class iConGLoader
{
    public delegate void LoadCompleteCallback(NTexture texture);
    public delegate void LoadErrorCallback(string error);

    public class ExperimentTextureManager : MonoBehaviour
    {
        static ExperimentTextureManager _instance;

        public static ExperimentTextureManager inst
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("ExperimentTextureManager");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<ExperimentTextureManager>();
                }

                return _instance;
            }
        }

        public const int POOL_CHECK_TIME = 30;
        public const int MAX_POOL_SIZE = 0;

        List<LoadItem> _items;
        bool _started;
        Hashtable _pool;

        private Dictionary<string, AsyncOperationHandle<Texture2D>> texturePools;

        void Awake()
        {
            _items = new List<LoadItem>();
            _pool = new Hashtable();
            texturePools = new Dictionary<string, AsyncOperationHandle<Texture2D>>();

            //StartCoroutine(FreeIdleIcons());
        }

        public static void LoadIcon(string url,
            LoadCompleteCallback onSuccess,
            LoadErrorCallback onFail)
        {
            inst.loadIcon(url, onSuccess, onFail);
        }

        public void loadIcon(string url,
            LoadCompleteCallback onSuccess,
            LoadErrorCallback onFail)
        {
            LoadItem item = new LoadItem();
            item.url = url;
            item.onSuccess = onSuccess;
            item.onFail = onFail;
            _items.Add(item);
            if (!_started)
                StartCoroutine(Run());
        }

        public static void ReleaseIconAll()
        {
            inst.releaseIconAll();
        }

        void releaseIconAll()
        {
            ArrayList toRemove = null;
            foreach (DictionaryEntry de in _pool)
            {
                string key = (string)de.Key;
                NTexture texture = (NTexture)de.Value;

                if (texture.refCount == 0)
                {
                    if (toRemove == null)
                        toRemove = new ArrayList();

                    toRemove.Add(key);
                    texture.Dispose();

                    Addressables.Release(texturePools[key]);

                    //Debug.Log("释放资源：" + key);
                }
            }

            if (toRemove != null)
            {
                foreach (string key in toRemove)
                {
                    //Debug.Log("Remove资源：" + key);

                    _pool.Remove(key);
                    texturePools.Remove(key);
                }

            }

            Resources.UnloadUnusedAssets();
            Caching.ClearCache();
        }

        IEnumerator Run()
        {
            _started = true;

            LoadItem item = null;
            while (true)
            {
                if (_items.Count > 0)
                {
                    item = _items[0];
                    _items.RemoveAt(0);
                }
                else
                    break;

                if (_pool.ContainsKey(item.url))
                {
                    NTexture texture = (NTexture)_pool[item.url];

                    texture.refCount++;

                    if (item.onSuccess != null)
                        item.onSuccess(texture);

                    continue;
                }

                string url = item.url;

                if (texturePools.ContainsKey(url))
                {
                    NTexture texture = new NTexture(texturePools[url].Result);
                    texture.destroyMethod = DestroyMethod.Unload;

                    texture.refCount++;

                    _pool[item.url] = texture;

                    if (item.onSuccess != null)
                        item.onSuccess(texture);

                    continue;
                }

                AsyncOperationHandle<Texture2D> handle = Addressables.LoadAssetAsync<Texture2D>(url);

                yield return handle;

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    NTexture texture = new NTexture(handle.Result);
                    texture.destroyMethod = DestroyMethod.Unload;
                    texture.refCount++;

                    _pool[item.url] = texture;

                    if (item.onSuccess != null)
                        item.onSuccess(texture);

                    texturePools.Add(url, handle);
                }
                else
                {
                    if (item.onFail != null)
                        item.onFail(handle.OperationException.Message);
                }
            }

            _started = false;
        }

        IEnumerator FreeIdleIcons()
        {
            while (true)
            {
                yield return new WaitForSeconds(POOL_CHECK_TIME); //check the pool every 30 seconds

                int cnt = _pool.Count;
                if (cnt > MAX_POOL_SIZE)
                {
                    ArrayList toRemove = null;
                    foreach (DictionaryEntry de in _pool)
                    {
                        string key = (string)de.Key;
                        NTexture texture = (NTexture)de.Value;

                        if (texture.refCount == 0)
                        {
                            if (toRemove == null)
                                toRemove = new ArrayList();

                            toRemove.Add(key);
                            texture.Unload();

                            texture.Dispose();

                            //Addressables.ResourceManager.Release(texturePools[key]);
                            Addressables.Release(texturePools[key]);

                            Debug.Log("释放资源：" + key);

                            cnt--;
                            if (cnt <= 0)
                                break;
                        }
                    }

                    if (toRemove != null)
                    {
                        foreach (string key in toRemove)
                        {
                            Debug.Log("Remove资源：" + key);

                            _pool.Remove(key);
                            texturePools.Remove(key);
                        }

                    }

                    Resources.UnloadUnusedAssets();
                    Caching.ClearCache();
                }
            }
        }
    }

    public class LoadItem
    {
        public string url;
        public LoadCompleteCallback onSuccess;
        public LoadErrorCallback onFail;
    }

    public class ExperimentGLoader : GLoader
    {
        protected override void LoadExternal()
        {
            ExperimentTextureManager.LoadIcon(this.url, OnLoadSuccess, OnLoadFail);
        }

        protected override void FreeExternal(NTexture texture)
        {
            texture.refCount--;
        }

        void OnLoadSuccess(NTexture texture)
        {
            if (string.IsNullOrEmpty(this.url))
                return;

            this.onExternalLoadSuccess(texture);
        }

        void OnLoadFail(string error)
        {
            Debug.Log("load " + this.url + " failed: " + error);
            this.onExternalLoadFailed();
        }
    }
}