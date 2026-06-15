
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
public class ControlManager : Singleton<ControlManager>
{

    private Dictionary<string, Type> _controlDict = new Dictionary<string, Type>();
    ControlManager() { 
        _initControlDict();
    }  
    
    private void _initControlDict()
    {
        _controlDict.Clear();
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic); // 过滤动态程序集

        foreach (var assembly in assemblies)
        {
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException)
            {
                types = Array.Empty<Type>();
            }

            foreach (var type in types)
            {
                if (type.IsClass && !type.IsAbstract &&
                    type.GetCustomAttributes(typeof(ControlAttribute), false).Length > 0)
                {
                    Logger.PrintColor("green", $"========================================鍙戠幇鎺у埗鍣ㄧ被: {type.Name}");
                    _controlDict.Add(type.Name, type);
                }
            }
        }

        

    }

    /// <summary>
    /// 获取特定名称的属性（包括继承的）
    /// </summary>
    private  PropertyInfo GetProperty(Type type, string propertyName, BindingFlags bindingFlags = BindingFlags.Default)
    {
        bindingFlags |= BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        // 先尝试当前类型
        var property = type.GetProperty(propertyName, bindingFlags);
        if (property != null) return property;

        // 递归查找父类
        Type baseType = type.BaseType;
        while (baseType != null && baseType != typeof(object))
        {
            return GetProperty(baseType, propertyName, bindingFlags);
        }

        return null;
    }

    private MethodInfo GetMethod(Type type, string methodName, BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance)
    {
        MethodInfo initMethod = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (initMethod == null)
        {
            if (type.BaseType != null)
            {
                return GetMethod(type.BaseType, methodName, bindingFlags);
            }
        }
        return initMethod;
    }

    private void InvokeControlMethod(string methodName)
    {
        foreach (var item in _controlDict)
        {
            PropertyInfo instanceProperty = GetProperty(item.Value, "Instance", BindingFlags.Static);
            if (instanceProperty != null)
            {
                object singletonInstance = instanceProperty.GetValue(null);
                MethodInfo method = GetMethod(item.Value, methodName, BindingFlags.NonPublic | BindingFlags.Instance);
                if (method != null)
                    method.Invoke(singletonInstance, null);

            }

        }
    }

    /// <summary>
    /// 初始化所有控制器，在游戏开始时调用，防止出现网络监听未注册的情况
    /// </summary>
    public void Init()
    {
        InvokeControlMethod("init");
    }

    /// <summary>
    /// 清理所有控制器，在退出登录时调用，会将所有的登录缓存数据清理掉
    /// </summary>
    public void Clear()
    {
        InvokeControlMethod("clear");
    }

    public void LoginSuccess()
    {
        InvokeControlMethod("onLoginSuccess");
    }

    public void Dispose()
    {
        _controlDict.Clear();
    }
}
