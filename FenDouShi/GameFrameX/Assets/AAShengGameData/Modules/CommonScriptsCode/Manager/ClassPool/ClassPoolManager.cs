// 引入 System 命名空间，提供常用的基础类型和功能
using System;
// 引入 System.Collections.Generic 命名空间，提供泛型集合类
using System.Collections.Generic;
// 引入 System.Diagnostics 命名空间，提供调试和跟踪功能
using System.Diagnostics;
// 引入 System.Text 命名空间，提供处理文本的功能
using System.Text;

// 定义 ClassPoolManger 类，继承自 Singleton<ClassPoolManger>
public class ClassPoolManger : Singleton<ClassPoolManger>
{
    // 定义一个字典，用于存储不同类型的对象池，键为对象类型，值为 IClassPool 接口实例
    public Dictionary<Type, IClassPool> pools = new();

    // 初始化方法，当前为空实现
    public void Init() { }

    // 初始化指定类型对象池的方法
    // <typeparam name="T">对象池存储的对象类型，必须是引用类型且具有无参构造函数</typeparam>
    // <param name="poolItemCount">对象池初始对象数量</param>
    public void InitPool<T>(int poolItemCount)
        where T : class, new()
    {
        // 获取或创建指定类型的对象池
        ClassPool<T> pool = GetOrCreatePool<T>();
        // 初始化对象池的数量
        pool.InitPoolCount(poolItemCount);
    }

    // 获取或创建指定类型对象池的方法
    // <typeparam name="T">对象池存储的对象类型，必须是引用类型且具有无参构造函数</typeparam>
    // <returns>指定类型的对象池实例</returns>
    public ClassPool<T> GetOrCreatePool<T>()
        where T : class, new()
    {
        // 尝试从字典中获取指定类型的对象池
        if (!pools.TryGetValue(typeof(T), out var iPool))
        {
            // 若未找到，则创建一个新的指定类型的对象池
            iPool = new ClassPool<T>();
            // 将新创建的对象池添加到字典中
            pools.Add(typeof(T), iPool);
        }

        // 将获取到的对象池转换为指定类型并返回
        return iPool as ClassPool<T>;
    }

    // 从指定类型的对象池中获取一个对象的方法
    // <typeparam name="T">要获取的对象类型，必须是引用类型且具有无参构造函数</typeparam>
    // <returns>从对象池中获取的对象实例</returns>
    public T Get<T>()
        where T : class, new()
    {
        // 获取或创建指定类型的对象池
        ClassPool<T> pool = GetOrCreatePool<T>();
        // 从对象池中获取一个对象并返回
        return pool.Get();
    }

    // 将对象回收到指定类型的对象池中的方法
    // <typeparam name="T">要回收的对象类型，必须是引用类型且具有无参构造函数</typeparam>
    // <param name="obj">要回收的对象实例</param>
    public void Recycle<T>(T obj)
        where T : class, new()
    {
        // 获取或创建指定类型的对象池
        ClassPool<T> pool = GetOrCreatePool<T>();
        // 将对象回收到对象池中
        pool.Recycle(obj);
    }

    // 仅在 Unity 编辑器环境下执行的日志输出方法
    [Conditional("UNITY_EDITOR")]
    public void Log()
    {
        // 创建一个 StringBuilder 实例，用于构建日志信息
        StringBuilder sb = new StringBuilder("[������]��������Ϣ:\n");
        // 遍历存储对象池的字典
        foreach (var pool in pools)
        {
            // 将每个对象池的信息添加到日志信息中
            sb.AppendLine($"{pool.Value.ToString()}");
        }
        // 调用 Logger 类的 PrintLog 方法输出日志信息
        Logger.PrintLog(sb.ToString());
    }
}