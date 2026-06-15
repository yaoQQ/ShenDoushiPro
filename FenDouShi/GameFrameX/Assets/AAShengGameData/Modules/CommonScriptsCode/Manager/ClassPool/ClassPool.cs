// 引入 System.Collections.Generic 命名空间，用于使用 Stack 类
using System.Collections.Generic;

// 定义一个泛型类 ClassPool，实现 IClassPool 接口
// T 必须是引用类型，并且必须有一个无参构造函数
public class ClassPool<T> : IClassPool
    where T : class, new()
{
    // 用于存储对象的栈，栈中的元素类型为 T
    public Stack<T> stack = new();
    // 记录当前正在使用的对象数量
    public int usingCount;

    // 初始化对象池的数量，将对象池中的对象数量扩充到指定的 count
    public void InitPoolCount(int count)
    {
        // 从当前栈的数量开始，循环创建对象直到达到指定数量
        for (int i = stack.Count; i < count; i++)
        {
            // 创建一个新的 T 类型对象并压入栈中
            stack.Push(new T());
        }
    }

    // 从对象池中获取一个对象
    public T Get()
    {
        // 用于存储从栈中获取的对象
        T obj = null;
        // 当栈中还有对象时，尝试从栈中弹出对象
        while (stack.Count > 0)
        {
            // 从栈中弹出一个对象
            obj = stack.Pop();
            // 如果弹出的对象不为 null，则跳出循环
            if (obj != null)
                break;
        }

        // 如果从栈中没有获取到有效的对象，则创建一个新的对象
        if (obj == null)
        {
            obj = new T();
        }

        // 正在使用的对象数量加 1
        usingCount++;
        // 返回获取到的对象
        return obj;
    }

    // 将对象回收到对象池中
    public void Recycle(T obj)
    {
        // 如果要回收的对象不为 null
        if (obj != null)
        {
            // 尝试将对象转换为 IClassPoolItem 接口类型
            var classPoolItem = obj as IClassPoolItem;
            // 如果对象实现了 IClassPoolItem 接口
            if (classPoolItem != null)
            {
                // 调用对象的 OnRecycle 方法
                classPoolItem.OnRecycle();
            }

            // 正在使用的对象数量减 1
            usingCount--;
            // 将对象压入栈中
            stack.Push(obj);
        }
    }

    // 重写 ToString 方法，返回对象池的信息
    public override string ToString()
    {
        // 返回包含类型、池中对象数量和正在使用的对象数量的字符串
        return $"类型:{typeof(T)}, 池内数量:{stack.Count}, 使用数量:{usingCount}";
    }
}