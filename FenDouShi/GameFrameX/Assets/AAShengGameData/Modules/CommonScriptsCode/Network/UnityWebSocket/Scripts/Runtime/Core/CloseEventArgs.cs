// 引入System命名空间
using System;

namespace UnityWebSocket
{
    /// <summary>
    /// 表示 <see cref="IWebSocket.OnClose"/> 事件的事件数据。
    /// </summary>
    /// <remarks>
    ///   <para>
    ///   当WebSocket连接已关闭时会触发该事件。
    ///   </para>
    ///   <para>
    ///   如果您想获取关闭的原因，应该访问 <see cref="Code"/> 或 <see cref="Reason"/> 属性。
    ///   </para>
    /// </remarks>
    // 定义CloseEventArgs类，继承自EventArgs类
    public class CloseEventArgs : EventArgs
    {
        #region 内部构造函数

        // 无参内部构造函数
        internal CloseEventArgs(WebSocket webSocket)
        {
            this.webSocket = webSocket;
        }

        // 带无符号短整型code参数的内部构造函数，调用另一个构造函数
        internal CloseEventArgs(ushort code, WebSocket webSocket)
          : this(code, null, webSocket)
        {
        }

        // 带CloseStatusCode类型code参数的内部构造函数，调用另一个构造函数
        internal CloseEventArgs(CloseStatusCode code, WebSocket webSocket)
          : this((ushort)code, null, webSocket)
        {
        }

        // 带CloseStatusCode类型code和字符串reason参数的内部构造函数，调用另一个构造函数
        internal CloseEventArgs(CloseStatusCode code, string reason, WebSocket webSocket)
          : this((ushort)code, reason, webSocket)
        {
        }

        // 带无符号短整型code和字符串reason参数的内部构造函数
        internal CloseEventArgs(ushort code, string reason,WebSocket webSocket)
        {
            // 初始化Code属性
            Code = code;
            // 初始化Reason属性
            Reason = reason;    
            this.webSocket = webSocket;
        }

        #endregion

        #region 公共属性

        public WebSocket webSocket { get; private set; }

        /// <summary>
        /// 获取关闭操作的状态码。
        /// </summary>
        /// <value>
        /// 一个 <see cref="ushort"/> 类型的值，表示关闭操作的状态码（如果有）。
        /// </value>
        // 公共属性，获取关闭操作的状态码，只能在内部设置
        public ushort Code { get; private set; }

        /// <summary>
        /// 获取关闭操作的原因。
        /// </summary>
        /// <value>
        /// 一个 <see cref="string"/> 类型的值，表示关闭操作的原因（如果有）。
        /// </value>
        // 公共属性，获取关闭操作的原因，只能在内部设置
        public string Reason { get; private set; }

        /// <summary>
        /// 获取一个值，该值指示连接是否已干净地关闭。
        /// </summary>
        /// <value>
        /// 如果连接已干净地关闭，则为 <c>true</c>；否则为 <c>false</c>。
        /// </value>
        // 公共属性，获取连接是否已干净地关闭，可在内部设置
        public bool WasClean { get; internal set; }

        /// <summary>
        /// 与Code相同的枚举值
        /// </summary>
        // 公共属性，获取与Code对应的CloseStatusCode枚举值
        public CloseStatusCode StatusCode
        {
            get
            {
                // 检查Code值是否在CloseStatusCode枚举中定义
                if (Enum.IsDefined(typeof(CloseStatusCode), Code))
                    // 如果定义了，则返回对应的枚举值
                    return (CloseStatusCode)Code;
                // 未定义则返回Unknown
                return CloseStatusCode.Unknown;
            }
        }

        #endregion
    }
}
