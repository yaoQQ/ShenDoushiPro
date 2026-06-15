using System.Collections.Generic;

// 定义一个无序的多值映射集合类，继承自 Dictionary<T, HashSet<K>>
public class UnOrderMultiMapSet<T, K> : Dictionary<T, HashSet<K>>
{
    // 获取或创建指定键对应的 HashSet
    public new HashSet<K> this[T t]
    {
        get
        {
            // 用于存储获取到的 HashSet
            HashSet<K> set;
            // 尝试从字典中获取指定键对应的 HashSet，如果未找到则返回 false
            if (!this.TryGetValue(t, out set))
            {
                // 若未找到，则创建一个新的 HashSet
                set = new HashSet<K>();
            }
            return set;
        }
    }

    // 获取当前对象的字典表示
    public Dictionary<T, HashSet<K>> GetDictionary()
    {
        return this;
    }

    // 向指定键对应的集合中添加一个值
    public void Add(T t, K k)
    {
        // 用于存储获取到的 HashSet
        HashSet<K> set;
        // 尝试从字典中获取指定键对应的 HashSet
        this.TryGetValue(t, out set);
        // 如果未获取到对应的 HashSet
        if (set == null)
        {
            // 创建一个新的 HashSet
            set = new HashSet<K>();
            // 将新的 HashSet 添加到字典中
            base[t] = set;
        }
        // 向 HashSet 中添加值
        set.Add(k);
    }

    // 从指定键对应的集合中移除一个值
    public bool Remove(T t, K k)
    {
        // 用于存储获取到的 HashSet
        HashSet<K> set;
        // 尝试从字典中获取指定键对应的 HashSet
        this.TryGetValue(t, out set);
        // 如果未获取到对应的 HashSet
        if (set == null)
        {
            // 返回移除失败
            return false;
        }
        // 尝试从 HashSet 中移除指定的值，如果移除失败
        if (!set.Remove(k))
        {
            // 返回移除失败
            return false;
        }
        // 如果移除后 HashSet 为空
        if (set.Count == 0)
        {
            // 从字典中移除该键
            this.Remove(t);
        }
        // 返回移除成功
        return true;
    }

    // 检查指定键对应的集合中是否包含指定的值
    public bool Contains(T t, K k)
    {
        // 用于存储获取到的 HashSet
        HashSet<K> set;
        // 尝试从字典中获取指定键对应的 HashSet
        this.TryGetValue(t, out set);
        // 如果未获取到对应的 HashSet
        if (set == null)
        {
            // 返回不包含
            return false;
        }
        // 返回 HashSet 中是否包含指定的值
        return set.Contains(k);
    }

    // 获取集合中所有值的总数
    public new int Count
    {
        get
        {
            // 用于存储值的总数
            int count = 0;
            // 遍历字典中的每个键值对
            foreach (KeyValuePair<T, HashSet<K>> kv in this)
            {
                // 将每个 HashSet 中的元素数量累加到总数中
                count += kv.Value.Count;
            }
            return count;
        }
    }
}