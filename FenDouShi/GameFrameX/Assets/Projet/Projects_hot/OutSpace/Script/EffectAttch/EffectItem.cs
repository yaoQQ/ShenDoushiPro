using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EffectItem : ResourcePoolEnable
{
    public float liveTime = 1;
    private float totalTime;

    public void Awake()
    {
        totalTime = liveTime;
    }

    public virtual void Update()
    {
        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            this.gameObject.SetActive(false);
            if (!String.IsNullOrEmpty(prefabName))
                ResourceManagerPool.Instance.ReturnPoolObject(prefabName, ResourceType.effect, this.gameObject);
        }
    }
    public void disActive()
    {
        liveTime = totalTime;
    }
}

