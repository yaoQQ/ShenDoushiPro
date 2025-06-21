using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DelayTimeEffect : MonoBehaviour
{
    public float delayTime;
    private float time;
    public void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            this.gameObject.SetActive(false);
            time = delayTime;
        }
    }
}

