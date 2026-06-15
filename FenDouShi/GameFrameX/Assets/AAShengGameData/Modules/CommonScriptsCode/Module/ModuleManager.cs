using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 模块系统 事件系统
/// </summary>
// 模块管理器类，继承自单例类，确保全局只有一个实例
public class ModuleManager : Singleton<ModuleManager>
{
    // 存储模块枚举与基础模块实例的字典
    Dictionary<ModuleEnum, BaseModule> moduleDic = new Dictionary<ModuleEnum, BaseModule>();
    // 存储模块类型与基础模块实例的字典
    Dictionary<Type, BaseModule> moduleTypeDic = new Dictionary<Type, BaseModule>();
    // 存储模块名称与接口模块实例的字典
    Dictionary<string, IModule> IModuleDic = new Dictionary<string, IModule>();

    // 初始化模块管理器
    public void Init()
    {
        // 调用初始化模块的方法
        InitModule();
    }

    /// <summary>
    /// 每个模块在此注册一次;
    /// </summary>
    // 初始化模块的方法，用于注册各个模块
    void InitModule()
    {
        //注册全局的协议模块
        RegisterModule<SystemCommonModule>();
    }

    // 泛型方法，用于注册指定类型的模块
    void RegisterModule<T>() where T : BaseModule
    {
        // 检查该类型的模块是否已经注册过
        if (moduleTypeDic.ContainsKey(typeof(T)))
        {
            // 若已注册，输出错误日志
            Debug.LogError("重复注册模块 =" + typeof(T).Name);
            return;
        }
        // 创建指定类型模块的实例
        BaseModule bm = (BaseModule)Activator.CreateInstance(typeof(T), true);
        // 将模块添加到模块枚举与基础模块实例的字典中
        bm.InitRegisterNet();
        moduleDic.Add(bm.ModuleName(), bm);
        // 将模块添加到模块类型与基础模块实例的字典中
        moduleTypeDic.Add(bm.GetType(), bm);
    }

    // 注册基础模块的方法
    public void RegisterModule(BaseModule module)
    {
        // 获取模块的枚举名称
        ModuleEnum moduleName = module.ModuleName();
        // 检查该模块是否未注册过
        if (!moduleDic.ContainsKey(moduleName))
        {
            // 输出注册模块的日志信息
            Debug.Log("@@@C# ModuleManager  RegisterModule  Module.name=" + moduleName);
            // 初始化模块的网络注册
            module.InitRegisterNet();
            // 将模块添加到模块枚举与基础模块实例的字典中
            moduleDic.Add(moduleName, module);
        }
    }

    // 执行通知处理的方法
    public void ExecuteNotificationHandle(EventSysArgsBase vo)
    {
        // 遍历模块枚举与基础模块实例字典中的所有模块
        foreach (BaseModule bm in moduleDic.Values)
        {
            // 获取模块注册的通知列表
            List<int> notificationlist = bm.GetRegisterNotificationList();
            // 检查通知列表是否包含当前事件类型
            if (notificationlist.Contains(vo.eventType))
            {
                // 若包含，则调用模块的通知处理方法
                bm.OnNotificationLister(vo.eventType, vo);
            }
        }

        // 将接口模块字典中的所有值转换为列表
        var buffer = new List<IModule>(IModuleDic.Values);
        // 获取列表的枚举器
        var enumerator = buffer.GetEnumerator();

        // 遍历接口模块列表
        while (enumerator.MoveNext())
        {
            // 获取当前的接口模块实例
            IModule lm = enumerator.Current;
            // 获取接口模块注册的通知列表
            List<EEventType> notificationlist = lm.getRegisterNotificationList();
            // 检查通知列表是否包含当前事件类型
            if (notificationlist.Contains((EEventType)vo.eventType))
            {
                // 若包含，则调用接口模块的通知处理方法
                lm.onNotificationLister((EEventType)vo.eventType, vo);
            }
        }
    }

    /// <summary>  
    /// 获取某个模块实例;
    /// </summary>
    // 泛型方法，用于获取指定类型的模块实例
    public T GetModule<T>() where T : BaseModule
    {
        // 用于存储获取到的模块实例
        BaseModule bm = null;
        // 尝试从模块类型与基础模块实例的字典中获取指定类型的模块
        moduleTypeDic.TryGetValue(typeof(T), out bm);
        // 若未获取到模块实例
        if (bm == null)
        {
            // 输出错误日志
            Debug.LogError("获取不存在的模块");
        }
        // 返回获取到的模块实例
        return (T)bm;
    }

    #region 所有网络模块 通用方法
    public void OnLoginSuccess() 
    { 
        foreach (BaseModule module in moduleDic.Values)
        {
            module.OnLoginSuccess();
        }
    }    // 登录游戏

    public  void OnReconnect() 
    { 
        foreach (BaseModule module in moduleDic.Values)
        {
            module.OnReconnect();
        }
    }       // 断线重连

    public  void OnLogoutSuccess() 
    { 
        foreach (BaseModule module in moduleDic.Values)
        {
            module.OnLogoutSuccess();
        }
    }   // 换号/退出登录/退出游戏

    public  void OnRefreshOnZero() 
    { 
        foreach (BaseModule module in moduleDic.Values)
        {
            module.OnRefreshOnZero();
        }
    }   // 凌晨0点刷新
    #endregion

    // 清除指定枚举名称模块的方法
    public void Clear(ModuleEnum moduleEnum)
    {
        // 从模块枚举与基础模块实例的字典中移除指定模块
        moduleDic.Remove(moduleEnum);
    }

    public void Dispose()
    {
        moduleDic.Clear();
        moduleTypeDic.Clear();
        IModuleDic.Clear();
    }
}
