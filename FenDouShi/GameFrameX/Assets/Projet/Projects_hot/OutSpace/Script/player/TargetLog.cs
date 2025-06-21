using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//瞄准器脚本
public class TargetLog : MonoBehaviour
{

    private Transform cachTransform;
    public float scaleSpeed = 0.008f;//逐渐减小速度
    private bool isMotion = false;
    private void Awake()
    {
        cachTransform = this.transform;
    }

    public void Update()
    {
        if (!isMotion)
        {
            return;
        }
        if (cachTransform.localScale.x > 1)
        {
            cachTransform.localScale = Vector3.one * (cachTransform.localScale.x - scaleSpeed);
            //  Debug.Log("update TargetLog=" + this.transform.localScale);
            if (cachTransform.localScale.x > 1.3f)
            {
                cachTransform.localScale = Vector3.one * 1.3f;
            }
           
        }
        else if (cachTransform.localScale.x < 1)
        {
            cachTransform.localScale = Vector3.one;
            isMotion = false;
        }
    }
    //不断增大
    public void shoot()
    {
        this.transform.localScale = Vector3.one * (this.transform.localScale.x + 0.025f);
        isMotion = true;
    }

}

