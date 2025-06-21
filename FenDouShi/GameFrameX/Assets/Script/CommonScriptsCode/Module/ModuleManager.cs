
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 模块系统 事件系统
/// </summary>
public class ModuleManager : Singleton<ModuleManager>
{
    Dictionary<ModuleEnum, BaseModule> moduleDic = new Dictionary<ModuleEnum, BaseModule>();
    Dictionary<Type, BaseModule> moduleTypeDic = new Dictionary<Type, BaseModule>();

    Dictionary<string, IModule> luaModuleDic = new Dictionary<string, IModule>();


    public void Init()
    {
        
        InitModule();

    }
    /// <summary>
    /// 每个模块在此注册一次;
    /// </summary>
    void InitModule()
    {
        //test@@@
       // RegisterModule<CommonModule>();
    }

    void RegisterModule<T>() where T : BaseModule
    {
        if (moduleTypeDic.ContainsKey(typeof(T)))
        {
            Debug.LogError("重复注册模块 ="+ typeof(T).Name);
            return;
        }
        BaseModule bm  = (BaseModule)Activator.CreateInstance(typeof(T), true);
        moduleDic.Add(bm.ModuleName(),bm);
        moduleTypeDic.Add(bm.GetType(),bm);
    }
    public void RegisterLuaModule(BaseModule luaModule)
    {
        ModuleEnum moduleName = luaModule.ModuleName();
        if (!moduleDic.ContainsKey(moduleName))
        {
            Debug.Log("@@@C# ModuleManager  RegisterLuaModule  Module.name=" + moduleName);
            luaModule.InitRegisterNet();
            moduleDic.Add(moduleName, luaModule);
        }
    }
    public void RegisterLuaModule(IModule luaModule) 
    {
        string moduleName = luaModule.getModuleName();
        if (!luaModuleDic.ContainsKey(moduleName))
        {
            Debug.Log("@@@C# ModuleManager  RegisterLuaModule  luaModule.name="+ moduleName);
            luaModule.initRegisterNet();
            luaModuleDic.Add(moduleName, luaModule);
        }
    }


    public void ExecuteNotificationHandle(string noticeType, BaseNotice vo)
    {
       // Logger.PrintColor("yellow", "ExecuteNotificationHandle noticeType=" + noticeType);
        foreach (BaseModule bm in moduleDic.Values)
        {
            List<string> notificationlist = bm.GetRegisterNotificationList();
            if (notificationlist.Contains(noticeType))
            {
                bm.OnNotificationLister(noticeType, vo);
            }
        }

        var buffer = new List<IModule>(luaModuleDic.Values);
        var enumerator = buffer.GetEnumerator();
         
        while (enumerator.MoveNext())
        {
            IModule lm = enumerator.Current;
            List<string> notificationlist = lm.getRegisterNotificationList();
            if (notificationlist.Contains(noticeType))
            {
                lm.onNotificationLister(noticeType, vo);
            }
        }
    }



    /// <summary>  
    /// 获取某个模块实例;
    /// </summary>
    public T GetModule<T>() where T : BaseModule
    {
        BaseModule bm = null;
        moduleTypeDic.TryGetValue(typeof(T),out bm);
        if (bm == null)
        {
            Debug.LogError("获取不存在的模块");
        }
        return (T)bm;
    }

    
}
