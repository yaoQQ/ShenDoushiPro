using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEnum
{
    BasePackage,
    LittlePrincePackage,
    ShaderProPackage,
    CameraPostPackage,
    OutSpacePackage,
    ProceduralPlanetPackage,
    ARProPackage,


    None
}

public class ProjectControler :ScriptableObject
{
    public int curProjectIndex;
    public List<string> projects=new List<string>();

    //项目包名与项目的类名保持了一致 如BasePackage的资源包名与加载的类BasePackage.cs相同
    public const string basePackage = "BasePackage"; 
    public const string littlePrincePackage = "LittlePrincePackage";
    public const string shaderPro = "ShaderProPackage";
    public const string cameraPostPro = "CameraPostPackage";
    public const string OutSpacePro = "OutSpacePackage";
    public const string PhythmMusicScript = "PhythmMusicScript";
    public const string ProceduralPlanetPackage = "ProceduralPlanetPackage";
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
