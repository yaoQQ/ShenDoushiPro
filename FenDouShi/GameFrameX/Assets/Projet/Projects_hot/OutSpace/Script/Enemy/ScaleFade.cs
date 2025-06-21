using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class ScaleFade : MonoBehaviour
{
    public float tScale;
    public float speend;

    private GameObject ship;
    public delegate void OverBack();
    public OverBack overbackFun;
    public void Awake()
    {
       
        ship = this.transform.GetChild(0).gameObject;
    }
    public void init()
    {
        this.enabled = true;
        this.transform.localScale = Vector3.one * 0.001f;
    }
    public void Update()
    {
        if ( this.transform.localScale.x>= tScale)
        {
            ship.SetActive(true);
            this.enabled = false;
            if (overbackFun != null)
            {
                overbackFun();
            }
            this.transform.GetChild(1).gameObject.SetActive(false);
            return;
        }
        float delet = this.transform.localScale.x + Time.deltaTime* speend;
        this.transform.localScale = Vector3.one* delet;
        

    }
}

