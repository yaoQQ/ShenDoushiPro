using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking; // 引入UnityWebRequest命名空间

public class WWWLoader
{
    private bool m_isTimeOut;
    public int autoRetryCount;
    public Action<string> errorCallback;
    public bool isRetry;
    public float lastLoadTime;
    public static Action<string> openRetryFunc;
    public float progress;
    public Action<float> progressCallback;
    // 修改回调参数类型为UnityWebRequest
    public Action<UnityWebRequest> succeedCallback; 
    public Action timeoutCallback;
    public string url;
    // 将WWW类型替换为UnityWebRequest
    public UnityWebRequest unityWebRequest; 

    public WWWLoader(string url)
    {
        this.url = url;
    }

    public static void SetRetryFunc(Action<string> func)
    {
        openRetryFunc = func;
    }

    public void checkTimeout()
    {
        if ((unityWebRequest != null) && !m_isTimeOut)
        {
            float num = Time.realtimeSinceStartup;
            float num2 = unityWebRequest.downloadProgress;
            if (progressCallback != null)
            {
                progressCallback(num2);
            }
            if (num2 > progress)
            {
                progress = num2;
                lastLoadTime = num;
            }
            else if ((num - lastLoadTime) > 15f)
            {
                Logger.PrintLog("加载(" + url + ")超时");
                m_isTimeOut = true;
                if (autoRetryCount > 0)
                {
                    autoRetryCount--;
                    WWWManager.Instance.retryLoader(url);
                }
                else if (isRetry)
                {
                    if (openRetryFunc != null)
                        openRetryFunc(url);
                }
                else
                {
                    if (timeoutCallback != null)
                    {
                        timeoutCallback();
                    }
                    WWWManager.Instance.removeLoader(url);
                }
            }
        }
    }

    public WWWLoader clone()
    {
        return new WWWLoader(url)
        {
            isRetry = isRetry,
            succeedCallback = succeedCallback,
            errorCallback = errorCallback,
            timeoutCallback = timeoutCallback,
            progressCallback = progressCallback
        };
    }

    public IEnumerator wwwLoad()
    {
        lastLoadTime = Time.realtimeSinceStartup;
        // 使用UnityWebRequest.Get创建请求
        unityWebRequest = UnityWebRequest.Get(url); 
        // 发送请求并等待完成
        unityWebRequest.SendWebRequest(); 

        while (!unityWebRequest.isDone && !m_isTimeOut)
        {
            checkTimeout();
            yield return null;
        }

        if (!m_isTimeOut)
        {
            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                Logger.PrintLog("加载(" + url + ")失败：" + unityWebRequest.error);
                if (autoRetryCount > 0)
                {
                    autoRetryCount--;
                    WWWManager.Instance.retryLoader(url);
                }
                else if (isRetry)
                {
                    if (openRetryFunc != null)
                        openRetryFunc(url);
                }
                else
                {
                    if (errorCallback != null)
                    {
                        errorCallback(unityWebRequest.error);
                    }
                    WWWManager.Instance.removeLoader(url);
                }
            }
            else
            {
                if (succeedCallback != null)
                {
                    succeedCallback(unityWebRequest);
                }
                WWWManager.Instance.removeLoader(url);
            }
        }
        // 释放请求资源
        unityWebRequest.Dispose(); 
    }

    public bool isTimeOut
    {
        get { return m_isTimeOut; }
    }
}
