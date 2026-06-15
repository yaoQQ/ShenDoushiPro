using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
public class HttpPostManager : SingleMonobehaviour<HttpPostManager>
{
	//private static HttpPostManager m_instance;

 //   public static HttpPostManager Instance
 //   {
 //       get { return m_instance; }
 //   }

    //void Awake()
    //{
    //    m_instance = this;
    //}

    public void SendHttp(string url, string param, Action<string> callback)
    {
        StartCoroutine(SendHttpCoroutine(url, param, callback));
    }

    private IEnumerator SendHttpCoroutine(string url, string param, Action<string> callback)
    {
        string address = url + "?" + param;
        using (UnityWebRequest request = UnityWebRequest.Get(address))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (callback != null)
                {
                    callback(request.downloadHandler.text);
                }
            }
            else
            {
                Logger.PrintError("SendHttpCoroutine:" + request.error);
                if (callback != null)
                {
                    callback(null);
                }
            }
        }
    }

    public void SendHttpProtobuf(string url, byte[] protoBytes, Action<byte[]> callback)
    {
        StartCoroutine(SendHttpProtobufCoroutine(url, protoBytes, callback));
    }

    private IEnumerator SendHttpProtobufCoroutine(string url, byte[] protoBytes, Action<byte[]> callback)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            UploadHandlerRaw uploadHandler = new UploadHandlerRaw(protoBytes);
            uploadHandler.contentType = "application/x-protobuf";
            request.uploadHandler = uploadHandler;
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (callback != null)
                {
                    callback(request.downloadHandler.data);
                }
            }
            else
            {
                Logger.PrintError("SendHttpProtobufCoroutine:" + request.error);
                if (callback != null)
                {
                    callback(null);
                }
            }
        }
    }

    public void PostPhotoToHttpService(string url, string fileName, byte[] photoBytes, Action<bool> finishCallback)
    {
        Logger.PrintLog(Utility.Platform.ConnectStrs("上传照片：", fileName));
        StartCoroutine(PostPhoto(url, fileName, photoBytes, finishCallback));
    }

private IEnumerator PostPhoto(string url, string fileName, byte[] photoBytes, Action<bool> finishCallback)
{
#if UNITY_EDITOR
    Debug.Log(url);
#endif

    WWWForm form = new WWWForm();
    form.AddBinaryData("userphoto", photoBytes, fileName, "image/jpg");

    using (UnityWebRequest request = UnityWebRequest.Post(url, form))
    {
        // 发送请求并等待响应
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Logger.PrintLog("上传照片成功");
            if (finishCallback != null)
            {
                finishCallback(true);
            }
        }
        else
        {
            Logger.PrintError(Utility.Platform.ConnectStrs("上传照片失败(", request.url, ")：", request.error));
            if (finishCallback != null)
                finishCallback(false);
        }
    }
}

    public void DownloadPhoto(string url, bool isAllowCache, Action<Texture2D, string> finishCallback)
    {
        if (finishCallback == null)
            return;

        if (!isAllowCache)
        {
            TimeSpan span = (TimeSpan)(DateTime.UtcNow - new DateTime(0x7b2, 1, 1, 0, 0, 0, 0));
            url = Utility.Platform.ConnectStrs(url, "?", Convert.ToInt64(span.TotalSeconds).ToString());
        }

        StartCoroutine(DownloadPhotoCoroutine(url, finishCallback));
    }

    private IEnumerator DownloadPhotoCoroutine(string url, Action<Texture2D, string> finishCallback)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                if (finishCallback != null)
                {
                    finishCallback(texture, url);
                }
            }
            else
            {
                //Logger.PrintError("DownloadPhotoError(" + url + "):" + request.error);
                if (finishCallback != null)
                {
                    finishCallback(null, null);
                }
            }
        }
    }

    public void DownloadNetImg(string url, bool isAllowCache, Action<Texture2D, string> finishCallback) {
        if (finishCallback == null)
            return;

        if (!isAllowCache)
        {
            TimeSpan span = (TimeSpan)(DateTime.UtcNow - new DateTime(0x7b2, 1, 1, 0, 0, 0, 0));
            url = Utility.Platform.ConnectStrs(url, "?", Convert.ToInt64(span.TotalSeconds).ToString());
        }

        StartCoroutine(DownloadNetImgCoroutine(url, finishCallback));
    }

    private IEnumerator DownloadNetImgCoroutine(string url, Action<Texture2D, string> finishCallback) {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                if (finishCallback != null)
                {
                    finishCallback(texture, url);
                }
            }
            else
            {
                //Logger.PrintError("DownloadPhotoError(" + url + "):" + request.error);
                if (finishCallback != null)
                {
                    finishCallback(null, null);
                }
            }
        }
    }
}