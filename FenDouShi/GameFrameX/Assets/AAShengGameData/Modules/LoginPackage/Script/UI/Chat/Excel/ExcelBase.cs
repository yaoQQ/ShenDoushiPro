using DataTableFrame;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class ExcelBase<T>
    where T : class
{
    public static Dictionary<int, T> DIC { get; private set; }
    public static T[] Arr { get; private set; }

    static ExcelBase()
    {
        DIC ??= ConfigMgr.Instance.GetConfig<T>();
        if (DIC == null)
        {
            Logger.PrintError($"[数据表]初始化数据表字典失败:{typeof(T).ToString()}");
        }
        Arr ??= DIC.Values.ToArray();
        if (Arr == null)
        {
            Logger.PrintError($"[数据表]初始化数据表数组失败:{typeof(T).ToString()}");
        }
    }

    public static int FindIndex(Predicate<T> func)
    {
        if (Arr == null) return -1;
        return Array.FindIndex(Arr, 0, func);
    }
}