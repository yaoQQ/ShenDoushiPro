namespace UnityWebSocket
{
    /// <summary>
    /// 参考 HTML5 WebSocket 的 ReadyState 属性
    /// 表示 WebSocket 连接的状态。
    /// </summary>
    /// <remarks>
    /// 此枚举的值在以下文档中定义
    /// <see href="http://www.w3.org/TR/websockets/#dom-websocket-readystate">
    /// WebSocket API</see>。
    /// </remarks>
    // 定义 WebSocket 连接状态的枚举，使用 ushort 类型存储值
    public enum WebSocketState : ushort
    {
        /// <summary>
        /// 等效于数值 0。表示连接尚未建立。
        /// </summary>
        // 连接中状态
        Connecting = 0,
        /// <summary>
        /// 等效于数值 1。表示连接已建立，可以进行通信。
        /// </summary>
        // 已连接状态
        Open = 1,
        /// <summary>
        /// 等效于数值 2。表示连接正在进行关闭握手，或者已经调用了关闭方法。
        /// </summary>
        // 关闭中状态
        Closing = 2,
        /// <summary>
        /// 等效于数值 3。表示连接已关闭或无法建立。
        /// </summary>
        // 已关闭状态
        Closed = 3
    }
}
