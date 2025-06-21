using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DollCamera:MonoBehaviour
{
    public RenderTexture renderCamera;
    public LayerMask CurrLayerMask_Out;
    public LayerMask CurrLayerMask_In;

    public void OnEnter()
    {
        CurrCamera.targetTexture = null;
        CurrCamera.cullingMask = CurrLayerMask_In;
    }
    public void OnExsist()
    {
        CurrCamera.targetTexture = renderCamera;
        CurrCamera.cullingMask = CurrLayerMask_Out;
    }

    public Camera CurrCamera
    {
        get
        {
           return this.gameObject.GetComponent<Camera>();
        }
    }
    
}

