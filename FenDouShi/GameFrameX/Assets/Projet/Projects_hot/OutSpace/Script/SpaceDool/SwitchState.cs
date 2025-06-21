using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchState : MonoBehaviour
{
    public DollCamera CurrCamera;
    public DollCamera TargetCamera;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "  hit " + this.gameObject.name);
        switchState();
    }


    public void switchState()
    {
        DollCamera temCamera = CurrCamera;
        CurrCamera = TargetCamera;
        TargetCamera = temCamera;
        CurrCamera.OnEnter();
        TargetCamera.OnExsist();

    }

}
