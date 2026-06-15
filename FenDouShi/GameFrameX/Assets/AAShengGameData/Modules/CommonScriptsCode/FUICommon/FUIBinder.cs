/** This is an automatically generated class by FUICodeSpawner. Please do not modify it. **/

using FairyGUI;
using login;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
namespace FairyGUI.Common
{
    public static partial class FUIBinder
    {
        public static void BindAll()
        {
            Logger.PrintColor("yellow", $"FUIBinder AutoBindAllAttriBinder() BindAll()");
            AutoBindAllAttriBinder();
        }

        //特定的包，绑定FGUI对象
        private static void AutoBindAllAttriBinderByPackage(string pakcageName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var binderTypes = assembly.GetTypes()
                .Where(t =>
                  t.GetCustomAttributes(typeof(UIBinderAttribute), true).Length > 0 &&
                  t.GetCustomAttribute<UIBinderAttribute>()?.PackageName == pakcageName &&
                  !t.IsInterface &&
                  !t.IsAbstract);
                foreach (var binderType in binderTypes)
                {
                    try
                    {
                        Logger.PrintColor("yellow", $"AutoBindAllAttriBinder() binderType={binderType}");
                        var attribute = binderType.GetCustomAttribute<UIBinderAttribute>();
                        var binderInstance = Activator.CreateInstance(binderType);
                        var bindMethod = binderType.GetMethod("BindAll");
                        bindMethod?.Invoke(binderInstance, null);
                        Logger.PrintColor("yellow", $"AutoBindAllAttriBinder() binderType={binderType} BindAll");
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Failed to bind {binderType.FullName}: {ex.Message}");
                    }
                }
            }
        }

        //当前域中所有UIBinderAttribute标签的类对象绑定
        private static void AutoBindAllAttriBinder()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var binderTypes = assembly.GetTypes()
                    .Where(t =>
                        t.GetCustomAttributes(typeof(UIBinderAttribute), true).Length > 0 &&
                        !t.IsInterface &&
                        !t.IsAbstract);


                foreach (var binderType in binderTypes)
                {
                    try
                    {
                        var attribute = binderType.GetCustomAttribute<UIBinderAttribute>();
                        var binderInstance = Activator.CreateInstance(binderType);
                        var bindMethod = binderType.GetMethod("BindAll");
                        bindMethod?.Invoke(binderInstance, null);
                        Logger.PrintColor("yellow", $"AutoBindAllAttriBinder() binderType={binderType} BindAll");
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Failed to bind {binderType.FullName}: {ex.Message}");
                    }
                }
            }
        }
        private static void AutoBindAllBinder()
        {
            // 获取所有程序集（包括动态加载的）
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var binderTypes = assembly.GetTypes()
                    .Where(t =>
                        t.GetInterfaces().Contains(typeof(IFUIBinder)) &&
                        !t.IsInterface &&
                        !t.IsAbstract);

                foreach (var binderType in binderTypes)
                {
                    try
                    {
                        var binderInstance = Activator.CreateInstance(binderType);
                        var bindMethod = binderType.GetMethod("BindAll");
                        bindMethod?.Invoke(binderInstance, null);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Failed to bind {binderType.FullName}: {ex.Message}");
                    }
                }
            }
        }
    }
  
    // 定义绑定器接口
    public interface IFUIBinder
    {
        void BindAll();
    }

}