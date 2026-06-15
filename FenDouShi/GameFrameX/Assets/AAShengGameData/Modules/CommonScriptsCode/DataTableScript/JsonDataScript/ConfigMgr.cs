using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ConfigMgr : Singleton<ConfigMgr>
{
    // 存储配置数据的字典
    private Dictionary<Type, object> configDataDic = new Dictionary<Type, object>();
    public const string label = "config";

    private bool isLoadAll = false;

    public void LoadAllConfig()
    {
        if (isLoadAll)
        {
            return;
        }
        List<string> strList = ConfigConst.ConfigList;
        for (int i = 0; i < strList.Count; i++)
        {
            ConfigMgr.Instance.LoadConfigText(strList[i]);
        }
        isLoadAll = true;
    }

    /// <summary>
    /// 加载指定名称的Excel配置文本
    /// </summary>
    /// <param name="excelName">Excel文件名</param>
    public void LoadConfigText(string excelName)
    {
        string className = excelName + "Vo";
        className = CapitalizeFirstLetter(className);
        Type type = Type.GetType(className);
        if (type == null)
        {
            Logger.PrintColor("red", "未找到类型: " + className);
            return;
        }
        if (configDataDic.ContainsKey(type))
        {
            Logger.PrintColor("red", "excelName: " + className + " 已经加载过了");
            return;
        }

        //   Logger.PrintDebug("excelType=" + type+ " excelName = "+ excelName);
        Addressables.LoadAssetsAsync<TextAsset>(new List<string> { excelName, label }, null, Addressables.MergeMode.Intersection).Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                //object obj = DataTableFrame.CongfigUtility.Json.ToObject(op.Result.text);
                // 将配置数据存入字典
                configDataDic[type] = op.Result[0].text;
                Logger.PrintGreen("配置加载完成: " + excelName);

            }
            else
            {
                Logger.PrintError("加载配置失败: " + excelName + " 错误: " + op.OperationException);
            }
            Addressables.Release(op);
        };
    }

    /// <summary>
    /// 获取指定Excel的配置数据
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="excelName">Excel文件名</param>
    /// <returns>配置数据字典</returns>
    public Dictionary<int, T> GetConfig<T>()
    {
        // 获取类型T的名称
        if (configDataDic.TryGetValue(typeof(T), out object data))
        {
            if (data is string jsonStr)
            {
                var dic = DataTableFrame.CongfigUtility.Json.ToObject<Dictionary<int, T>>(jsonStr);
                configDataDic[typeof(T)] = dic;
            }
            return configDataDic[typeof(T)] as Dictionary<int, T>;
        }
        string typeName = typeof(T).Name;
        Logger.PrintError("red", "未找到配置: " + typeName);
        return null;
    }

    /// <summary>
    /// 获取指定Excel的配置数据
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="excelName">Excel文件名</param>
    /// <returns>配置数据字典</returns>
    public Dictionary<string, T> GetConfigVos<T>()
    {
        // 获取类型T的名称
        if (configDataDic.TryGetValue(typeof(T), out object data))
        {
            if (data is string jsonStr)
            {
                var dic = DataTableFrame.CongfigUtility.Json.ToObject<Dictionary<string, T>>(jsonStr);
                configDataDic[typeof(T)] = dic;
            }
            return configDataDic[typeof(T)] as Dictionary<string, T>;
        }
        string typeName = typeof(T).Name;
        Logger.PrintError("red", "未找到配置: " + typeName);
        return null;
    }
    /// <summary>
    /// 获取指定Excel的配置数据
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="excelName">Excel文件名</param>
    /// <returns>配置数据字典</returns>
    public List<T> GetConfigList<T>()
    {
        // 获取类型T的名称
        if (configDataDic.TryGetValue(typeof(T), out object data))
        {
            if (data is string jsonStr)
            {
                var dic = DataTableFrame.CongfigUtility.Json.ToObject<Dictionary<int, T>>(jsonStr);
                configDataDic[typeof(T)] = dic;
            }
           var dicResult = configDataDic[typeof(T)] as Dictionary<int, T>;
            List<T> list = new List<T>(dicResult.Values);
            return list;
        }
        string typeName = typeof(T).Name;
        Logger.PrintError("red", "未找到配置: " + typeName);
        return null;
    }
    /// <summary>
    /// 通过配置ID获取配置数据
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="Key">唯一ID</param>
    /// <returns></returns>
    public T GetConfigVoById<T>(string Key)
    {
        Dictionary<string, T> cfgDic = GetConfigVos<T>();
        if (cfgDic == null)
            return default(T);
        if (!cfgDic.ContainsKey(Key))
            return default(T);
        T configVo = cfgDic[Key];
        return configVo;
    }

    /// <summary>
    /// 通过配置ID获取配置数据
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="Key">唯一ID</param>
    /// <returns></returns>
    public T GetConfigVoById<T>(int Key)
    {
        Dictionary<int, T> cfgDic = GetConfig<T>();
        if (cfgDic == null)
            return default(T);
        if (!cfgDic.ContainsKey(Key))
            return default(T);
        T configVo = cfgDic[Key];
        return configVo;
    }
    

    // 首字母大写 _->大写 by jack
    private static string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("input 不能为空");
        }
        string[] cstrList = input.Split('_');
        if (cstrList.Length == 0)
        {
            return char.ToUpper(input[0]) + input.Substring(1);
        }
        string str = "";
        foreach (string c in cstrList)
        {
            str += char.ToUpper(c[0]) + c.Substring(1);
        }
        return str;
    }
    public void Dispose()
    {
        configDataDic.Clear();
        isLoadAll = false;
    }

    /// <summary>
    /// 获取游戏常量
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static List<int> GetGameConst(string key)
    {
        var configVo = ConfigMgr.Instance.GetConfigVoById<MiscVo>(key);
        if (configVo != null)
        {
            return configVo.Value;
        }
        return new List<int>();
    }

}