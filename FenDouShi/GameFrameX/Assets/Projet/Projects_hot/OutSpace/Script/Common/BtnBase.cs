
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnBase:MonoBehaviour
{


  public void SwitchSkybox()
    {
        OutSpaceCameraManager.Instance.isSwitchSkybox();
    }

    public void SwitchShake()
    {
        OutSpaceCameraManager.Instance.isSwitchShake();
        this.transform.Find("Text").GetComponent<Text>().text = "shakeCamera=" + OutSpaceCameraManager.Instance.isShake;
    }

    //返回主场景
    public void ReturnMainScene()
    {
        OutSpaceResourceManager.Instance.clearAll();
        GameCommonManager.Instance.ReturnMainScene(GameEnum.OutSpacePackage);
    }
}

