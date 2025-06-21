using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyMat : MonoBehaviour
{
    
    // Start is called before the first frame update
    public Material skyMat;//Ìì¿ÕºÐµÄmaterial
    public float speed = 0.1f;
    public float rotate = 0;
    void Start()
    {
        // if(skyMat)
        Debug.Log("RotateSkyMat Start()");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (skyMat)
        {
            rotate = rotate + Time.deltaTime*speed;
            if (rotate >= 360)
            {
                rotate = 0;
            }
            skyMat.SetFloat("_Rotation", rotate);
        }
    }
}
