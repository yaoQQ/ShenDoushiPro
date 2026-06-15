#if !NET_LEGACY && (UNITY_EDITOR || !UNITY_WEBGL)
// 引入泛型集合命名空间
using System.Collections.Generic;
// 引入Unity引擎核心命名空间
using UnityEngine;

namespace UnityWebSocket
{
    // 禁止在同一个游戏对象上添加多个此组件
    [DisallowMultipleComponent]
    // 设置该组件的默认执行顺序为 -10000
    [DefaultExecutionOrder(-10000)]
    // 定义一个内部类 WebSocketManager，继承自 MonoBehaviour
    internal class WebSocketManager:MonoBehaviour
    {
        // 定义根游戏对象的名称常量
        private const string rootName = "[UnityWebSocket]";
        // 定义静态的单例实例字段
        private static WebSocketManager _instance;
        // 定义静态的单例实例属性
        public static WebSocketManager Instance
        {
            get
            {
                // 如果实例不存在，则创建实例
                if (!_instance) CreateInstance();
                // 返回单例实例
                return _instance;
            }
        }

        // 当脚本实例被加载时调用
        private void Awake()
        {
            // 确保游戏对象在场景切换时不被销毁
            DontDestroyOnLoad(gameObject);
        }

        // 静态方法，用于创建单例实例
        public static void CreateInstance()
        {
            // 根据根名称查找游戏对象
            GameObject go = GameObject.Find("/" + rootName);
            // 如果未找到，则创建一个新的游戏对象
            if (!go) go = new GameObject(rootName);
            // 尝试从游戏对象上获取 WebSocketManager 组件
            _instance = go.GetComponent<WebSocketManager>();
            // 如果未获取到，则添加 WebSocketManager 组件
            if (!_instance) _instance = go.AddComponent<WebSocketManager>();
        }

        // 定义一个只读的 WebSocket 列表，用于存储所有的 WebSocket 实例
        private readonly List<WebSocket> sockets = new List<WebSocket>();

        // 向列表中添加一个 WebSocket 实例
        public void Add(WebSocket socket)
        {
            // 如果列表中不包含该 WebSocket 实例，则添加
            if (!sockets.Contains(socket))
                sockets.Add(socket);
        }

        // 从列表中移除一个 WebSocket 实例
        public void Remove(WebSocket socket)
        {
            // 如果列表中包含该 WebSocket 实例，则移除
            if (sockets.Contains(socket))
                sockets.Remove(socket);
        }



        // 每帧更新时调用
        private void Update()
        {
            // 如果列表中没有 WebSocket 实例，则直接返回
            if (sockets.Count <= 0) return;
            // 遍历所有 WebSocket 实例，调用其 Update 方法
            for (int i = sockets.Count - 1; i >= 0; i--)
            {
                sockets[i].Update();
            }
        }

        // 当组件被禁用时调用
        private void OnDisable()
        {
            // 调用 SocketAbort 方法中止所有 WebSocket 连接
            SocketAbort();
        }

        // 中止所有 WebSocket 连接的方法
        private void SocketAbort()
        {
            // 遍历所有 WebSocket 实例，调用其 Abort 方法
            for (int i = sockets.Count - 1; i >= 0; i--)
            {
                sockets[i].Abort();
            }
        }
        private void SocketDispose()
        {
            // 遍历所有 WebSocket 实例，调用其 Abort 方法
            for (int i = sockets.Count - 1; i >= 0; i--)
            {
                sockets[i].SocketDispose();
            }
        }
        public void OnApplicationQuit()
        {
            SocketAbort();
            SocketDispose();
        }
    }
}
#endif