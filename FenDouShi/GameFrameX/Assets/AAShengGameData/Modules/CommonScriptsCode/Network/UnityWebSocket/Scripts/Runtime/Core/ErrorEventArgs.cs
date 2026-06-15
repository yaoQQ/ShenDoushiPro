// 引入System命名空间
using System;

namespace UnityWebSocket
{
    /// <summary>
    /// 表示 <see cref="IWebSocket.OnError"/> 事件的事件数据。
    /// </summary>
    /// <remarks>
    ///   <para>
    ///   当 <see cref="IWebSocket"/> 发生错误时，会触发该事件。
    ///   </para>
    ///   <para>
    ///   如果您想获取错误消息，应该访问 <see cref="Message"/> 属性。
    ///   </para>
    ///   <para>
    ///   如果错误是由异常引起的，您可以通过访问 <see cref="Exception"/> 属性来获取该异常。
    ///   </para>
    /// </remarks>
    // 定义错误事件参数类，继承自EventArgs
    public class ErrorEventArgs : EventArgs
    {
        #region 内部构造函数

        // 内部构造函数，仅传入错误消息
        internal ErrorEventArgs(string message,WebSocket webSocket)
          : this(message, null, webSocket)
        {
        }

        // 内部构造函数，传入错误消息和异常对象
        internal ErrorEventArgs(string message, Exception exception, WebSocket webSocket)
        {
            // 初始化错误消息属性
            this.Message = message;
            // 初始化异常属性
            this.Exception = exception;
            //当前网络连接
            this.webSocket = webSocket;
        }

        #endregion

        #region 公共属性
        //当前网络连接
        public WebSocket webSocket { get; private set; }
        /// <summary>
        /// 获取导致错误的异常。
        /// </summary>
        /// <value>
        /// 如果错误是由异常引起的，则为表示错误原因的 <see cref="System.Exception"/> 实例；否则为 <see langword="null"/>。
        /// </value>
        // 公共属性，获取导致错误的异常对象
        public Exception Exception { get; private set; }

        /// <summary>
        /// 获取错误消息。
        /// </summary>
        /// <value>
        /// 表示错误消息的 <see cref="string"/>。
        /// </value>
        // 公共属性，获取错误消息
        public string Message { get; private set; }

        #endregion
    }
}
