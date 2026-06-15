using FairyGUI;
using System;
using System.Collections.Generic;

public class GComponentPool<T> : Singleton<GComponentPool<T>>
    where T : GComponent
{
    public string PackageName;
    public string ComponentName;

    GComponent parent;
    Stack<GComponent> gComponents = new();

    public int allCount => usingCount + gComponents.Count;
    public int usingCount;

    public GComponentPool()
    {
        parent = new GComponent();
        parent.gameObjectName = typeof(T).ToString();
    }

    public void Init(string packageName, string componentName)
    {
        PackageName = packageName;
        ComponentName = componentName;
    }

    public void Get(Action<T, object> complete, object data)
    {
        GComponent go = null;
        while (gComponents.Count > 0)
        {
            go = gComponents.Pop();
            if (go != null)
                break;
        }
        if (go != null && go is T com)
        {
            usingCount++;
            go.visible = true;
            complete?.Invoke(go as T, data);
            return;
        }

        // TODO:取消加载资源
        UIPackage.CreateObjectAsync(PackageName, ComponentName, x =>
        {
            T obj = x as T;
            if (obj == null)
            {
                Logger.PrintError($"[GComponentPool]加载FGUI资源失败:{PackageName},{ComponentName}");
            }
            usingCount++;
            complete?.Invoke(obj, data);
        });
    }

    public void Recycle(GComponent go)
    {
        if (go == null) return;

        usingCount--;
        if (usingCount < 0)
        {
            Logger.PrintError($"[GComponentPool]池中物体数量为{usingCount},是否回收了非对象池生成的物体");
        }

        parent.AddChild(go);
        go.visible = false;
        gComponents.Push(go);
    }
}