using UnityEngine;
using System.Collections;
using System;

public class PhotoManager : Singleton<PhotoManager>
{

    public void downloadNetPhoto(string url, Action<Texture2D, string> finishCallback)
    {
        HttpPostManager.Instance.DownloadPhoto(url, true, finishCallback);
    }
    public void downloadNetImg(string url, Action<Texture2D, string> finishCallback)
    {

        HttpPostManager.Instance.DownloadNetImg(url, true, finishCallback);
    }
}
