using common;
using FairyGUI;
using System;
using System.Collections.Generic;

public class RedPointPool
{
    GComponent parent;
    Stack<GComponent> redPointGos = new();

    public RedPointPool()
    {
        parent = new GComponent();
        parent.gameObjectName = typeof(RedPointPool).ToString();
    }

    public void Get(Action<GComponent> complete)
    {
        GComponent go = null;
        while (redPointGos.Count > 0)
        {
            go = redPointGos.Pop();
            if (go != null)
            {
                complete?.Invoke(go);
                return;
            }
        }

        // TODO:取消流程
        FGUIAssetManager.Instance.CreateObjectAsync(G_RedPoint.PACKAGE_NAME, G_RedPoint.COMPONENT_NAME, x =>
        {
            complete?.Invoke(x.asCom);
        });
    }

    public void Recycle(GComponent go)
    {
        if (go != null && go.displayObject.gameObject && parent != null)
        {
            parent.AddChild(go);
            go.visible = false;
            redPointGos.Push(go);
        }
    }
}