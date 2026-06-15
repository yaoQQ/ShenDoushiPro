// 引入System命名空间，提供常用的基础类型和功能
using System;
// 引入System.Text命名空间，提供文本编码解码相关功能
using System.Text;

namespace UnityWebSocket
{
    // 定义消息事件参数类，继承自EventArgs基类
    public class MessageEventArgs : EventArgs
    {
        // 存储原始的字节数据
        private byte[] _rawData;
        // 存储字符串形式的数据
        private string _data;

        // 构造函数，使用操作码和原始字节数据初始化对象
        internal MessageEventArgs(Opcode opcode, byte[] rawData)
        {
            Opcode = opcode;  // 设置操作码
            _rawData = rawData;  // 设置原始字节数据
        }

        // 构造函数，使用操作码和字符串数据初始化对象
        internal MessageEventArgs(Opcode opcode, string data)
        {
            Opcode = opcode;  // 设置操作码
            _data = data;  // 设置字符串数据
        }

        /// <summary>
        /// 获取消息的操作码。
        /// </summary>
        /// <value>
        /// <see cref="Opcode.Text"/>、<see cref="Opcode.Binary"/>。
        /// </value>
        // 内部属性，获取消息的操作码，只能在类内部设置
        internal Opcode Opcode { get; private set; }

        /// <summary>
        /// 将消息数据作为 <see cref="string"/> 类型获取。
        /// </summary>
        /// <value>
        /// 如果消息类型为文本且成功解码为字符串，则为表示消息数据的 <see cref="string"/>；
        /// 否则为 <see langword="null"/>。
        /// </value>
        // 公共属性，获取字符串形式的消息数据
        public string Data
        {
            get
            {
                SetData();  // 设置字符串数据
                return _data;  // 返回字符串数据
            }
        }

        /// <summary>
        /// 将消息数据作为 <see cref="byte"/> 数组获取。
        /// </summary>
        /// <value>
        /// 表示消息数据的 <see cref="byte"/> 数组。
        /// </value>
        // 公共属性，获取原始字节形式的消息数据
        public byte[] RawData
        {
            get
            {
                SetRawData();  // 设置原始字节数据
                return _rawData;  // 返回原始字节数据
            }
        }

        /// <summary>
        /// 获取一个值，该值指示消息类型是否为二进制。
        /// </summary>
        /// <value>
        /// 如果消息类型为二进制，则为 <c>true</c>；否则为 <c>false</c>。
        /// </value>
        // 公共属性，判断消息类型是否为二进制
        public bool IsBinary
        {
            get
            {
                return Opcode == Opcode.Binary;  // 判断操作码是否为二进制类型
            }
        }

        /// <summary>
        /// 获取一个值，该值指示消息类型是否为文本。
        /// </summary>
        /// <value>
        /// 如果消息类型为文本，则为 <c>true</c>；否则为 <c>false</c>。
        /// </value>
        // 公共属性，判断消息类型是否为文本
        public bool IsText
        {
            get
            {
                return Opcode == Opcode.Text;  // 判断操作码是否为文本类型
            }
        }

        // 私有方法，设置字符串数据
        private void SetData()
        {
            if (_data != null) return;  // 如果字符串数据已存在，则直接返回

            if (RawData == null)  // 如果原始字节数据为空
            {
                return;  // 直接返回
            }

            _data = Encoding.UTF8.GetString(RawData);  // 将原始字节数据解码为字符串
        }

        // 私有方法，设置原始字节数据
        private void SetRawData()
        {
            if (_rawData != null) return;  // 如果原始字节数据已存在，则直接返回

            if (_data == null)  // 如果字符串数据为空
            {
                return;  // 直接返回
            }

            _rawData = Encoding.UTF8.GetBytes(_data);  // 将字符串数据编码为字节数组
        }
    }
}