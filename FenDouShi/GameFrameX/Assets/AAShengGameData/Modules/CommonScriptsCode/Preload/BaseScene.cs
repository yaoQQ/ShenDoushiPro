using UnityEngine;
using System.Collections;
using System;

//Scene基类
public class BaseScene : IScene
{
    private bool isInit = false;

    public virtual string getSceneName()
    {
       return "";
    }

    public bool getIsInit()
    {
        return isInit;
    }
    //--结束加载后调用
    protected void endInit()
    {
        this.isInit = true;
    }

    public virtual void onEnter()
    {
        

    }
    public virtual void onReset()
    {

    }
    public virtual void onLeave()
    {
        isInit = false;
        SceneManager.Instance.Clear();
    }

}
