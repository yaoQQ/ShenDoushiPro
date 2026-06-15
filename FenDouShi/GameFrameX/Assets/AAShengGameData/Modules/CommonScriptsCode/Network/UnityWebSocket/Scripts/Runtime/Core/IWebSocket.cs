// 引入 System 命名空间
using System;

namespace UnityWebSocket
{
    /// <summary>
    /// <para>IWebSocket 表示一个网络连接。</para>
    /// <para>它可以处于连接中、已连接、关闭中或已关闭状态。</para>
    /// <para>您可以使用它来发送和接收消息。</para>
    /// <para>注册回调以处理消息。</para>
    /// <para> ----------------------------------------------------------- </para>
    /// <para>IWebSocket 表示一个网络连接，</para>
    /// <para>它可以是 connecting connected closing closed 状态，</para>
    /// <para>可以发送和接收消息，</para>
    /// <para>通过注册消息回调，来处理接收到的消息。</para>
    /// </summary>
    // 定义 IWebSocket 接口，用于表示一个网络连接
    public interface IWebSocket
    {
        /// <summary>
        /// 异步建立连接。
        /// </summary>
        /// <remarks>
        ///   <para>
        ///   此方法不会等待连接过程完成。
        ///   </para>
        ///   <para>
        ///   如果连接已经建立，此方法不执行任何操作。
        ///   </para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        ///   <para>
        ///   此实例不是客户端。
        ///   </para>
        ///   <para>
        ///   - 或 -
        ///   </para>
        ///   <para>
        ///   关闭过程正在进行中。
        ///   </para>
        ///   <para>
        ///   - 或 -
        ///   </para>
        ///   <para>
        ///   一系列的重新连接操作失败。
        ///   </para>
        /// </exception>
        // 异步建立连接的方法
        void ConnectAsync();

        /// <summary>
        /// 异步关闭连接。
        /// </summary>
        /// <remarks>
        ///   <para>
        ///   此方法不会等待关闭过程完成。
        ///   </para>
        ///   <para>
        ///   如果连接的当前状态为关闭中或已关闭，此方法不执行任何操作。
        ///   </para>
        /// </remarks>
        // 异步关闭连接的方法
        void CloseAsync();

        /// <summary>
        /// 使用 WebSocket 连接异步发送指定的数据。
        /// </summary>
        /// <remarks>
        /// 此方法不会等待发送过程完成。
        /// </remarks>
        /// <param name="data">
        /// 一个 <see cref="byte"/> 数组，表示要发送的二进制数据。
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// 连接的当前状态不是已打开。
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> 为 <see langword="null"/>。
        /// </exception>
        // 异步发送二进制数据的方法
        void SendAsync(byte[] data);

        /// <summary>
        /// 使用 WebSocket 连接发送指定的数据。
        /// </summary>
        /// <param name="text">
        /// 一个 <see cref="string"/>，表示要发送的文本数据。
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// 连接的当前状态不是已打开。
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="text"/> 为 <see langword="null"/>。
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="text"/> 无法进行 UTF-8 编码。
        /// </exception>
        // 异步发送文本数据的方法
        void SendAsync(string text);

        /// <summary>
        /// 获取要连接的地址。
        /// </summary>
        // 获取连接地址的属性
        string Address { get; }

        /// <summary>
        /// 获取子协议。
        /// </summary>
        // 获取子协议的属性
        string[] SubProtocols { get; }

        /// <summary>
        /// 获取连接的当前状态。
        /// </summary>
        /// <value>
        ///   <para>
        ///   <see cref="WebSocketState"/> 枚举值之一。
        ///   </para>
        ///   <para>
        ///   它表示连接的当前状态。
        ///   </para>
        ///   <para>
        ///   默认值为 <see cref="WebSocketState.Closed"/>。
        ///   </para>
        /// </value>
        // 获取连接当前状态的属性
        WebSocketState ReadyState { get; }

        /// <summary>
        /// 当 WebSocket 连接建立时发生。
        /// </summary>
        // 连接建立时触发的事件
        event EventHandler<OpenEventArgs> OnOpen;

        /// <summary>
        /// 当 WebSocket 连接关闭时发生。
        /// </summary>
        // 连接关闭时触发的事件
        event EventHandler<CloseEventArgs> OnClose;

        /// <summary>
        /// 当 <see cref="IWebSocket"/> 发生错误时发生。
        /// </summary>
        // 发生错误时触发的事件
        event EventHandler<ErrorEventArgs> OnError;

        /// <summary>
        /// 当 <see cref="IWebSocket"/> 接收到消息时发生。
        /// </summary>
        // 接收到消息时触发的事件
        event EventHandler<MessageEventArgs> OnMessage;
    }
}
