using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Mathfs : MonoBehaviour
{

    void Start()
    {
        
    }
    void OnLODLevelChanged()
    {
        Debug.Log(this.gameObject.name+" OnLODLevelChanged()");
    }
    void OnBecameVisible()
    {
        Debug.Log(this.gameObject.name + " OnBecameVisible()");
    }
}
