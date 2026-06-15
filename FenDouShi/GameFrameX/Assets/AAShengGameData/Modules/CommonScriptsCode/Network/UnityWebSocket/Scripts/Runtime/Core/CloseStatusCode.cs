namespace UnityWebSocket
{
    /// <summary>
    /// 指示WebSocket连接关闭的状态码。
    /// </summary>
    /// <remarks>
    ///   <para>
    ///   此枚举的值在 
    ///   <see href="http://tools.ietf.org/html/rfc6455#section-7.4">
    ///   RFC 6455的第7.4节</see> 中定义。
    ///   </para>
    ///   <para>
    ///   “保留值” 不能由端点在关闭握手时作为状态码发送。
    ///   </para>
    /// </remarks>
    // 定义WebSocket连接关闭状态码的枚举，使用ushort类型
    public enum CloseStatusCode : ushort
    {
        // 未知状态码，值为65534
        Unknown = 65534,
        /// <summary>
        /// 相当于关闭状态1000。表示正常关闭。
        /// </summary>
        // 正常关闭状态
        Normal = 1000,
        /// <summary>
        /// 相当于关闭状态1001。表示某个端点正在断开连接。
        /// </summary>
        // 端点断开连接状态
        Away = 1001,
        /// <summary>
        /// 相当于关闭状态1002。表示某个端点因协议错误而终止连接。
        /// </summary>
        // 协议错误导致连接终止状态
        ProtocolError = 1002,
        /// <summary>
        /// 相当于关闭状态1003。表示某个端点因接收到无法接受的数据类型而终止连接。
        /// </summary>
        // 接收到不支持的数据导致连接终止状态
        UnsupportedData = 1003,
        /// <summary>
        /// 相当于关闭状态1004。仍然未定义。一个保留值。
        /// </summary>
        // 未定义状态，保留值
        Undefined = 1004,
        /// <summary>
        /// 相当于关闭状态1005。表示实际上没有状态码存在。一个保留值。
        /// </summary>
        // 无状态码状态，保留值
        NoStatus = 1005,
        /// <summary>
        /// 相当于关闭状态1006。表示连接异常关闭。一个保留值。
        /// </summary>
        // 连接异常关闭状态，保留值
        Abnormal = 1006,
        /// <summary>
        /// 相当于关闭状态1007。表示某个端点因接收到与消息类型不一致的数据而终止连接。
        /// </summary>
        // 接收到无效数据导致连接终止状态
        InvalidData = 1007,
        /// <summary>
        /// 相当于关闭状态1008。表示某个端点因接收到违反其策略的消息而终止连接。
        /// </summary>
        // 接收到违反策略的消息导致连接终止状态
        PolicyViolation = 1008,
        /// <summary>
        /// 相当于关闭状态1009。表示某个端点因接收到太大而无法处理的消息而终止连接。
        /// </summary>
        // 接收到过大消息导致连接终止状态
        TooBig = 1009,
        /// <summary>
        /// 相当于关闭状态1010。表示客户端因期望服务器协商一个或多个扩展，但服务器在握手响应中未返回这些扩展而终止连接。
        /// </summary>
        // 客户端期望的扩展未协商导致连接终止状态
        MandatoryExtension = 1010,
        /// <summary>
        /// 相当于关闭状态1011。表示服务器因遇到意外情况而无法完成请求而终止连接。
        /// </summary>
        // 服务器遇到错误导致连接终止状态
        ServerError = 1011,
        /// <summary>
        /// 相当于关闭状态1015。表示连接因TLS握手失败而关闭。一个保留值。
        /// </summary>
        // TLS握手失败导致连接关闭状态，保留值
        TlsHandshakeFailure = 1015,
    }
}
