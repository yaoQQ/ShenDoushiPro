namespace UnityWebSocket
{
    /// <summary>
    /// 指示 WebSocket 帧的类型。
    /// </summary>
    /// <remarks>
    /// 此枚举的值在 RFC 6455 的
    /// <see href="http://tools.ietf.org/html/rfc6455#section-5.2">
    /// 第 5.2 节</see> 中定义。
    /// </remarks>
    // 定义 WebSocket 帧类型的枚举，使用 byte 作为底层类型
    public enum Opcode : byte
    {
        /// <summary>
        /// 等效于数值 1。表示文本帧。
        /// </summary>
        // 文本帧类型，值为 0x1
        Text = 0x1,
        /// <summary>
        /// 等效于数值 2。表示二进制帧。
        /// </summary>
        // 二进制帧类型，值为 0x2
        Binary = 0x2,
        /// <summary>
        /// 等效于数值 8。表示连接关闭帧。
        /// </summary>
        // 连接关闭帧类型，值为 0x8
        Close = 0x8,
    }
}
