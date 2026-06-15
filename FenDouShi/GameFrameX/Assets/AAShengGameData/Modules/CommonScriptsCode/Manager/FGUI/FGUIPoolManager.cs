//using FairyGUI;
//using System;
//using System.Collections.Generic;

//public class FGUIPoolManager : Singleton<FGUIPoolManager>
//{
//    GComponent parent;
//    Dictionary<Type, Stack<GComponent>> pools = new();

//    public void Init()
//    {
//        parent = new GComponent();
//        parent.name = typeof(FGUIPoolManager).ToString();
//    }

//    public T Get<T>()
//        where T : GComponent
//    {
//        GComponent go = null;
//        if (pools.TryGetValue(typeof(T), out var stack))
//        {
//            while (stack.Count > 0)
//            {
//                go = stack.Pop();
//                if (go != null)
//                    break;
//            }
//        }
//        return go as T;
//    }

//    public void Recycle<T>(GComponent go)
//        where T : GComponent
//    {
//        if (go != null)
//        {
//            parent.AddChild(go);
//            go.visible = false;
//            redPointGos.Push(go);
//        }
//    }
//}