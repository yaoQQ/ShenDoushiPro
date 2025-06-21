using System;
using System.Collections;
using System.Net;
using System.IO;
using UnityEngine;
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
        ThreadManager.RunThread(() =>
        {
            string str = PostToHttpService(url, param);
            if (callback != null)
            {
                ThreadManager.RunMainThread(() =>
                {
                    callback(str);
                });
            }
        });
    }

    public string PostToHttpService(string url, string param)
    {
        string address = url + "?" + param;
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(address);
        request.Method = "GET";

        HttpWebResponse response = null;
        string responseDatas = string.Empty;
        string getString = "";
        try
        {
            response = (HttpWebResponse)request.GetResponse();
            int len = (int)response.ContentLength;
            Stream streamResponse = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(streamResponse);
            getString = streamReader.ReadToEnd();
        }
        catch (Exception e)
        {
            Logger.PrintError("PostToHttpService2:" + e.Message);
            request.Abort();
        }
        finally
        {
            if (response != null)
            {
                try
                {
                    response.Close();
                }
                catch
                {
                    request.Abort();
                }
            }
        }
        return getString;
    }

    public void SendHttpProtobuf(string url, byte[] protoBytes, Action<byte[]> callback)
    {
        ThreadManager.RunThread(() =>
        {
            byte[] rsp = PostToHttpServiceProtobuf(url, protoBytes);
            if (callback != null)
            {
                ThreadManager.RunMainThread(() =>
                {
                    callback(rsp);
                });
            }
        });
    }

    public byte[] PostToHttpServiceProtobuf(string url, byte[] protoBytes)
    {
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/x-protobuf";
        request.KeepAlive = false;
        byte[] datas = protoBytes;
        request.ContentLength = datas.Length;

        try
        {
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(datas, 0, datas.Length);
            requestStream.Close();
        }
        catch (Exception e)
        {
            Logger.PrintError("PostToHttpService1:" + e.Message);
            request.Abort();
            return null;
        }
        
        HttpWebResponse response = null;
        string responseDatas = string.Empty;
        byte[] buffer = null;
        try
        {
            response = (HttpWebResponse)request.GetResponse();
            int len = (int)response.ContentLength;
            Stream streamResponse = response.GetResponseStream();
            int offset = 0;
            buffer = new byte[len];
            while (len > 0)
            {
                int n = streamResponse.Read(buffer, offset, len);
                if (n == 0) break;
                len -= n;
                offset += n;
            }
        }
        catch (Exception e)
        {
            Logger.PrintError("PostToHttpService2:" + e.Message);
            request.Abort();
        }
        finally
        {
            if (response != null)
            {
                try
                {
                    response.Close();
                }
                catch
                {
                    request.Abort();
                }
            }
        }
        return buffer;
    }

    public void PostPhotoToHttpService(string url, string fileName, byte[] photoBytes, Action<bool> finishCallback)
    {
        Logger.PrintLog(UtilMethod.ConnectStrs("上传照片：", fileName));
        StartCoroutine(PostPhoto(url, fileName, photoBytes, finishCallback));
    }

    private IEnumerator PostPhoto(string url, string fileName, byte[] photoBytes, Action<bool> finishCallback)
    {
#if UNITY_EDITOR
        Debug.Log(url);
#endif

        WWWForm form = new WWWForm();
        form.AddBinaryData("userphoto", photoBytes, fileName, "image/jpg");

        WWW www = new WWW(url, form);
        yield return www;

        if (www.error == null)
        {
            Logger.PrintLog("上传照片成功");
            if (finishCallback != null)
            {
                finishCallback(true);
            }
        }
        else
        {
            Logger.PrintError(UtilMethod.ConnectStrs("上传照片失败(", www.url, ")：", www.error));
            if (finishCallback != null)
                finishCallback(false);
        }
    }

    public void DownloadPhoto(string url, bool isAllowCache, Action<Texture2D, string> finishCallback)
    {
        if (finishCallback == null)
            return;

        if (!isAllowCache)
        {
            TimeSpan span = (TimeSpan)(DateTime.UtcNow - new DateTime(0x7b2, 1, 1, 0, 0, 0, 0));
            url = UtilMethod.ConnectStrs(url, "?", Convert.ToInt64(span.TotalSeconds).ToString());
        }

        WWWManager.Instance.load(url, (www) =>
        {
            finishCallback(www.texture, www.url);
        }, (error) =>
        {
            //Logger.PrintError("DownloadPhotoError(" + url + "):" + error);
            finishCallback(null, null);
        }, () =>
        {
            //Logger.PrintError("DownloadPhotoTimeout(" + url + ")");
            finishCallback(null, null);
        });
    }

    public void DownloadNetImg(string url, bool isAllowCache, Action<Texture2D, string> finishCallback) {
        //  string url = "https://vlive-api.1plustore.com/vlin-api/upload/files/20180907/0/9e344433-bf99-48e4-8809-7c2b22977754.jpg";
        if (finishCallback == null)
            return;
        //ThreadManager.RunMainThread(() => {
        //    StartCoroutine(Load(url, finishCallback));
        //});
        WWWManager.Instance.load(url, (www) => {
            finishCallback(www.texture, www.url);
        }, (error) => {
            //Logger.PrintError("DownloadPhotoError(" + url + "):" + error);
            finishCallback(null, null);
        }, () => {
            //Logger.PrintError("DownloadPhotoTimeout(" + url + ")");
            finishCallback(null, null);
        });
    }
    IEnumerator Load(string url, Action<Texture2D, string> finishCallback) {
        WWW www = new WWW(url);
        yield return www;
        if (www != null && string.IsNullOrEmpty(www.error)) {
            if (finishCallback != null) {
                finishCallback.Invoke(www.texture, www.url);
            }
        }
        else {
            Debug.LogWarningFormat("Failed with error load: {0}", www.error);
            if (finishCallback != null) {
                finishCallback.Invoke(null, null);
            }
        }
    }
    //private void finishCallback(Texture2D texture, string url) {
    //    if (texture == null) {
    //        return;
    //    }
    //    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    //    Debug.Log("sprite=" + sprite);
    //}
}