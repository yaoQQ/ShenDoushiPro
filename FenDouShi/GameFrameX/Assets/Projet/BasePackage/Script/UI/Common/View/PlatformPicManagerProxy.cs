using UnityEngine;
using UIWidget;
using System.Collections.Generic;

public class PlatformPicManagerProxy:Singleton<PlatformPicManagerProxy>
{
    Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();
    private  void downloadImage(string picName, ImageWidget picLocateImg)
    {
        if (picLocateImg == null)
        {
            return;
        }
        if (string.IsNullOrEmpty(picName))
        {
            picLocateImg.SetPng(null);
            return;
        }

        if (spriteDic.ContainsKey(picName))
        {
            if (picLocateImg.loadingImg != null)
            {
                picLocateImg.loadingImg.gameObject.SetActive(false);
            }
         //    Debug.Log(picLocateImg.Img.name+"  ContainsKey spriteDic[" + picName+"]="+ spriteDic[picName]);
            picLocateImg.SetPng(spriteDic[picName]);
            return;
        }
        picLocateImg.SetPng(null);
        if (picLocateImg.loadingImg != null)
        {
            picLocateImg.loadingImg.gameObject.SetActive(true);
        }
        PhotoManager.Instance.downloadNetImg(picName, (texture2D,url) =>
        {
            if (texture2D != null)
            {
                Sprite sprite = ImageUtil.CreateSpriteByTexture(texture2D);
                picLocateImg.SetPng(sprite);
                addSprite(picName, sprite);
                if (picLocateImg.loadingImg != null)
                {
                    picLocateImg.loadingImg.gameObject.SetActive(false);
                }
                Debug.Log("downloadNetImg() 下载成功 picName="+ picName);
            }
        });
        if (!picLocateImg.Img.enabled)
        {
            picLocateImg.Img.enabled = true;
        }
    }
    public void downloadGameIcon(string name ,ImageWidget picLocateImg)
    {
        string netPath = "http://yaomvp.oss-cn-shenzhen.aliyuncs.com/"+ "GameIcon/" + name;
      //  Debug.LogFormat("downloadGameIcon() 尝试下载网络图:  netPath=" + netPath + " picLocateImg=" + (picLocateImg));
        downloadImage(netPath, picLocateImg);
    }
    public void downloadMerchantHead(string name, ImageWidget picLocateImg)
    {
        string netPath = "http://yaomvp.oss-cn-shenzhen.aliyuncs.com/" + "MerchantHead/" + name;
        downloadImage(netPath, picLocateImg);
    }
    private void addSprite(string name,Sprite sprite)
    {
        if (spriteDic.ContainsKey(name))
        {
            return;
        }
        spriteDic[name] = sprite;
    }
}