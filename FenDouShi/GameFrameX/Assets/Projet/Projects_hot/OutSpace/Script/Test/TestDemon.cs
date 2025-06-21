using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class GradientData
{
    public GradientColorKey[] colorKeys;
    public GradientAlphaKey[] alphaKeys;
    public GradientMode mode;

    public GradientData(Gradient gradient)
    {
        colorKeys = gradient.colorKeys;
        alphaKeys = gradient.alphaKeys;
        mode = gradient.mode;
    }
}
public class TestDemon : MonoBehaviour
{

    public Gradient gradient;

    public Gradient gradient2;

    private void Start()
    {
        GradientData data = new GradientData(gradient);
        string json = JsonConvert.SerializeObject(data);
        Debug.Log("TestDemon json=" + json);
        //gradient2 = JsonConvert.DeserializeObject<Gradient>(json);
    }

    public void Awake()
    {
        
     
    }

  

}
