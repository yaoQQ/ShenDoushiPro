using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEnum
{
    BasePackage,
    shenDouShiPackage,
    None
}

public class ProjectControler :ScriptableObject
{
    public int curProjectIndex;
    public List<string> projects=new List<string>();

    //项目包名与项目的类名保持了一致 如BasePackage的资源包名与加载的类BasePackage.cs相同
    public const string basePackage = "BasePackage"; 
    public const string shenDouShiPackage = "ShenDouShiPackage";
    public string GetCurProjectName()
    {
        if(projects.Count>0)
        {
            return projects[curProjectIndex];
        }
        Debug.LogError("没有创建项目！！！！！！！！！！");
        return "";
    }
}
