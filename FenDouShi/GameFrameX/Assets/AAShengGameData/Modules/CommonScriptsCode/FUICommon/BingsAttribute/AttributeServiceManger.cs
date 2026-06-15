using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
public class AttributeBindManger : Singleton<AttributeBindManger>
{
    private readonly UnOrderMultiMapSet<Type, Type> _viewTypes = new();
    //当前域中所有UIBinderAttribute标签的类对象绑定
    public void AutoBindAllViewAttriBinder()
    {
        _viewTypes.Clear();
        var validAssemblies = AppDomain.CurrentDomain.GetAssemblies() .Where(a => !a.IsDynamic); // 过滤动态程序集
        foreach (var assembly in validAssemblies)
        {
            var typesWithAttribute = assembly.GetTypes()
                .Where(t => !t.IsAbstract && t.GetCustomAttributes<FGUIViewAttribute>().Any());

            foreach (var type in typesWithAttribute)
            {
                _viewTypes.Add(typeof(FGUIViewAttribute), type);
                Logger.PrintDebug($"FGUIViewAttribute: {typeof(FGUIViewAttribute)} -> {type}");
            }
        }
    }
    public HashSet<Type> GetTypes(Type systemAttributeType)
    {
        if (!this._viewTypes.ContainsKey(systemAttributeType))
        {
            return new HashSet<Type>();
        }

        return this._viewTypes[systemAttributeType];
    }

    public void Dispose()
    {
        _viewTypes.Clear();
    }
}