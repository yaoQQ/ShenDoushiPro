using FairyGUI;
using System;

// TODO:取消资源加载
public class FGUILoadHandler : IUISource
{
    public string packageName;
    public string componentName;

    public string fileName
    {
        get => componentName;
        set => componentName = value;
    }

    public FGUILoadHandler(string packageName, string componentName)
    {
        this.packageName = packageName;
        this.componentName = componentName;
    }

    public bool loaded => throw new System.NotImplementedException();

    public void Load(UILoadCallback callback)
    {
        //加载界面
        UIPackage.CreateObjectAsync(packageName, componentName, LoadFinish);
    }

    public void LoadFinish(GObject result)
    {

    }

    public void Cancel()
    {
        throw new System.NotImplementedException();
    }
}