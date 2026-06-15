/*
using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIPackageMap : MonoBehaviour
{
    void Start()
    {
        FGUIAssetManager.Instance.Init();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 100), "Test1"))
        {
            Test1();
        }

        if (GUI.Button(new Rect(10, 200, 100, 50), "Test2"))
        {
            Test2();
        }
        if (GUI.Button(new Rect(10, 300, 100, 50), "Test3"))
        {
            Test3();
        }
    }
    private void Test1()
    {
        UIPackage.CreateObjectAsync("login", "LoginPage", (op) => {

            Logger.PrintColor("yellow", $"CreateObjectAsync login op=" + op);
            Stage.inst.AddChild(op.displayObject);
        });

    }
 
    private async void Test2()
    {
        var obj = await FGUIAssetManager.Instance.CreateObjectAsync("login", "LoginPage");
        Logger.PrintColor("yellow", $"LoginWindowToUIPanelMono login this.obj=" + obj);
        Stage.inst.AddChild(obj.displayObject);
    }

    private void Test3()
    {
        UIPackage.CreateObjectAsync("common", "TopTipsView", (op) => {

            Logger.PrintColor("yellow", $"Test3() CreateObjectAsync login op=" + op);
            Stage.inst.AddChild(op.displayObject);
        });

    }
}
*/