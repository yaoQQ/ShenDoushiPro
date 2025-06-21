using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderProCloseBtn : MonoBehaviour
{
    public void ReturnMainScene()
    {
       //释放其他项目的资源对象
        GameCommonManager.Instance.ReturnMainScene(GameEnum.ShaderProPackage);

    }
}
