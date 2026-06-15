using System;

public static class ClassPoolHelper
{
    public static T GetFromPool<T>(this T _)
        where T : class, new()
    {
        return ClassPoolManger.Instance.Get<T>();
    }

    public static void RecycleToPool<T>(this T obj)
        where T : class, new()
    {
        if (obj != null)
        {
            ClassPoolManger.Instance.Recycle(obj);
        }
    }
}