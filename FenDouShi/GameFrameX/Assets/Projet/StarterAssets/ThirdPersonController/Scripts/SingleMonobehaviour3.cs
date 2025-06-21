using UnityEngine;

public abstract class SingleMonobehaviour3<T> : MonoBehaviour where T : SingleMonobehaviour3<T>
{

    private static T instance = null;
    private static bool isInit = false;
    /// <summary>
    /// 线程锁
    /// </summary>
    private static readonly object _lock = new object();
    /// <summary>
    /// 是否为全局单例
    /// </summary>
    protected static bool isGolbal = false;

    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    //if (isQuit) {
                    //    return null;
                    //}
                    Object[] objs= GameObject.FindObjectsOfType(typeof(T));
                    Debug.Log("==============single objs.count=" + objs.Length+" type="+ typeof(T));
                    if (objs.Length > 0)
                    {
                        instance = ((T)objs[0]);
                        if(objs.Length > 1)
                        {
                            if (Debug.isDebugBuild)
                            {
                                Debug.LogWarning("[Singleton] " + typeof(T).Name + " should never be more than 1 in scene!");
                            }
                            return instance;
                        }
                    }

                    if (instance == null)
                    {
                       GameObject singletonObj = new GameObject("Singleton of " + typeof(T).ToString());
                        instance = singletonObj.AddComponent<T>();
                        if (isGolbal)
                        {
                            DontDestroyOnLoad(singletonObj);
                        }
                    }
                    if (!isInit)
                    {
                        instance.Init();
                        isInit = true;
                    }
                }
                return instance;
            }
        }
    }

    void Awake() {
        if (instance == null) {
            instance = this as T;
            if (!isInit) {
                instance.Init();
                isInit = true;
            }
        }
    }

    public virtual void Init() { }

    private void OnApplicationQuit() {
        instance = null;
        isInit = false;
    }


    public void OnDestroy() {
        instance = null;
        isInit = false;
      //  Debug.Log("<color='red'>"+ "Singleton of Destroy name=" + typeof(T).Name + "</color>");
    }
}