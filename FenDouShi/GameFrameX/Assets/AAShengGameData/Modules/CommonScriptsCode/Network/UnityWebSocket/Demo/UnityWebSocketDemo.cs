// 引入Unity引擎核心命名空间
using UnityEngine;

// 定义命名空间，用于组织代码
namespace UnityWebSocket.Demo
{
    // 定义一个继承自MonoBehaviour的类，用于Unity中的组件
    public class UnityWebSocketDemo : MonoBehaviour
    {
        // 定义WebSocket连接地址，默认使用wss://echo.websocket.events
        public string address = "wss://echo.websocket.events";
        // 定义要发送的文本消息，默认值为"Hello UnityWebSocket!"
        public string sendText = "Hello UnityWebSocket!";

        // 定义一个WebSocket对象
        private IWebSocket socket;

        // 控制是否记录消息日志
        private bool logMessage = true;
        // 存储日志消息的字符串
        private string log = "";
        // 记录发送消息的数量
        private int sendCount;
        // 记录接收消息的数量
        private int receiveCount;
        // 滚动视图的滚动位置
        private Vector2 scrollPos;
        // 定义绿色颜色，用于表示正常状态
        private Color green = new Color(0.1f, 1, 0.1f);
        // 定义红色颜色，用于表示关闭状态
        private Color red = new Color(1f, 0.1f, 0.1f);
        // 定义等待颜色，用于表示中间状态
        private Color wait = new Color(0.7f, 0.3f, 0.3f);

        // 每帧绘制GUI界面时调用
        private void OnGUI()
        {
            // 计算GUI的缩放比例，基于屏幕宽度
            var scale = Screen.width / 800f;
            // 设置GUI的矩阵，实现缩放效果
            GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(scale, scale, 1));
            // 定义GUI元素的宽度
            var width = GUILayout.Width(Screen.width / scale - 10);

            // 获取WebSocket的当前状态，如果socket为null则默认为Closed状态
            WebSocketState state = socket == null ? WebSocketState.Closed : socket.ReadyState;

            // 开始绘制水平布局，用于显示头部信息
            GUILayout.BeginHorizontal();
            // 显示SDK版本信息
            GUILayout.Label("SDK Version: 1", GUILayout.Width(Screen.width / scale - 100));
            // 设置GUI颜色为绿色
            GUI.color = green;
            // 显示当前FPS值，保留两位小数
            GUILayout.Label($"FPS: {fps:F2}", GUILayout.Width(80));
            // 恢复GUI颜色为白色
            GUI.color = Color.white;
            // 结束水平布局
            GUILayout.EndHorizontal();

            // 开始绘制水平布局，用于显示WebSocket状态
            GUILayout.BeginHorizontal();
            // 显示状态标签
            GUILayout.Label("State: ", GUILayout.Width(36));
            // 根据WebSocket状态设置GUI颜色
            GUI.color = WebSocketState.Closed == state ? red : WebSocketState.Open == state ? green : wait;
            // 显示当前WebSocket状态
            GUILayout.Label($"{state}", GUILayout.Width(120));
            // 恢复GUI颜色为白色
            GUI.color = Color.white;
            // 结束水平布局
            GUILayout.EndHorizontal();

            // 仅当WebSocket处于关闭状态时启用GUI元素
            GUI.enabled = state == WebSocketState.Closed;
            // 显示地址标签
            GUILayout.Label("Address: ", width);
            // 显示文本输入框，用于修改连接地址
            address = GUILayout.TextField(address, width);

            // 开始绘制水平布局，用于放置连接和关闭按钮
            GUILayout.BeginHorizontal();
            // 仅当WebSocket处于关闭状态时启用GUI元素
            GUI.enabled = state == WebSocketState.Closed;
            // 绘制连接按钮，根据状态显示不同文本
            if (GUILayout.Button(state == WebSocketState.Connecting ? "Connecting..." : "Connect"))
            {
                // 创建一个新的WebSocket对象
                socket = new WebSocket(address);
                // 注册连接成功事件处理函数
                socket.OnOpen += Socket_OnOpen;
                // 注册接收到消息事件处理函数
                socket.OnMessage += Socket_OnMessage;
                // 注册连接关闭事件处理函数
                socket.OnClose += Socket_OnClose;
                // 注册错误事件处理函数
                socket.OnError += Socket_OnError;
                // 添加连接中的日志信息
                AddLog(string.Format("Connecting..."));
                // 异步发起连接
                socket.ConnectAsync();
            }

            // 仅当WebSocket处于打开状态时启用GUI元素
            GUI.enabled = state == WebSocketState.Open;
            // 绘制关闭按钮，根据状态显示不同文本
            if (GUILayout.Button(state == WebSocketState.Closing ? "Closing..." : "Close"))
            {
                // 添加关闭中的日志信息
                AddLog(string.Format("Closing..."));
                // 异步关闭连接
                socket.CloseAsync();
            }
            // 结束水平布局
            GUILayout.EndHorizontal();

            // 显示消息标签
            GUILayout.Label("Message: ");
            // 显示文本输入区域，用于输入要发送的消息
            sendText = GUILayout.TextArea(sendText, GUILayout.MinHeight(50), width);

            // 开始绘制水平布局，用于放置发送消息按钮
            GUILayout.BeginHorizontal();
            // 绘制发送文本消息按钮，仅当输入内容不为空时可用
            if (GUILayout.Button("Send") && !string.IsNullOrEmpty(sendText))
            {
                // 异步发送文本消息
                socket.SendAsync(sendText);
                // 添加发送消息的日志信息
                AddLog(string.Format("Send: {0}", sendText));
                // 发送计数加1
                sendCount += 1;
            }
            // 绘制发送字节消息按钮，仅当输入内容不为空时可用
            if (GUILayout.Button("Send Bytes") && !string.IsNullOrEmpty(sendText))
            {
                // 将文本转换为UTF-8字节数组
                var bytes = System.Text.Encoding.UTF8.GetBytes(sendText);
                // 异步发送字节消息
                socket.SendAsync(bytes);
                // 添加发送字节消息的日志信息
                AddLog(string.Format("Send Bytes ({1}): {0}", sendText, bytes.Length));
                // 发送计数加1
                sendCount += 1;
            }
            // 绘制发送100次文本消息按钮，仅当输入内容不为空时可用
            if (GUILayout.Button("Send x100") && !string.IsNullOrEmpty(sendText))
            {
                // 循环100次发送消息
                for (int i = 0; i < 100; i++)
                {
                    // 构造带序号的消息文本
                    var text = (i + 1).ToString() + ". " + sendText;
                    // 异步发送文本消息
                    socket.SendAsync(text);
                    // 添加发送消息的日志信息
                    AddLog(string.Format("Send: {0}", text));
                    // 发送计数加1
                    sendCount += 1;
                }
            }
            // 绘制发送100次字节消息按钮，仅当输入内容不为空时可用
            if (GUILayout.Button("Send Bytes x100") && !string.IsNullOrEmpty(sendText))
            {
                // 循环100次发送字节消息
                for (int i = 0; i < 100; i++)
                {
                    // 构造带序号的消息文本
                    var text = (i + 1).ToString() + ". " + sendText;
                    // 将文本转换为UTF-8字节数组
                    var bytes = System.Text.Encoding.UTF8.GetBytes(text);
                    // 异步发送字节消息
                    socket.SendAsync(bytes);
                    // 添加发送字节消息的日志信息
                    AddLog(string.Format("Send Bytes ({1}): {0}", text, bytes.Length));
                    // 发送计数加1
                    sendCount += 1;
                }
            }
            // 结束水平布局
            GUILayout.EndHorizontal();

            // 启用所有GUI元素
            GUI.enabled = true;
            // 开始绘制水平布局，用于显示消息计数和日志开关
            GUILayout.BeginHorizontal();
            // 显示日志开关复选框
            logMessage = GUILayout.Toggle(logMessage, "Log Message");
            // 显示发送消息的数量
            GUILayout.Label(string.Format("Send Count: {0}", sendCount));
            // 显示接收消息的数量
            GUILayout.Label(string.Format("Receive Count: {0}", receiveCount));
            // 结束水平布局
            GUILayout.EndHorizontal();

            // 绘制清除按钮
            if (GUILayout.Button("Clear"))
            {
                // 清空日志内容
                log = "";
                // 重置接收消息计数
                receiveCount = 0;
                // 重置发送消息计数
                sendCount = 0;
            }

            // 开始绘制滚动视图，用于显示消息日志
            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.MaxHeight(Screen.height / scale - 270), width);
            // 显示日志内容
            GUILayout.Label(log);
            // 结束滚动视图
            GUILayout.EndScrollView();
        }

        // 添加日志信息的方法
        private void AddLog(string str)
        {
            // 如果不记录日志，则直接返回
            if (!logMessage) return;
            // 如果日志消息长度超过100，则截断并添加省略号
            if (str.Length > 100) str = str.Substring(0, 100) + "...";
            // 将日志消息添加到日志字符串中，并换行
            log += str + "\n";
            // 如果日志内容超过22KB，则截取最后22KB的内容
            if (log.Length > 22 * 1024)
            {
                log = log.Substring(log.Length - 22 * 1024);
            }
            // 将滚动视图滚动到最底部
            scrollPos.y = int.MaxValue;
        }

        // WebSocket连接成功时调用的事件处理函数
        private void Socket_OnOpen(object sender, OpenEventArgs e)
        {
            // 添加连接成功的日志信息
            AddLog(string.Format("Connected: {0}", address));
        }

        // 接收到WebSocket消息时调用的事件处理函数
        private void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            // 如果接收到的是二进制消息
            if (e.IsBinary)
            {
                // 添加接收二进制消息的日志信息
                AddLog(string.Format("Receive Bytes ({1}): {0}", e.Data, e.RawData.Length));
            }
            // 如果接收到的是文本消息
            else if (e.IsText)
            {
                // 添加接收文本消息的日志信息
                AddLog(string.Format("Receive: {0}", e.Data));
            }
            // 接收计数加1
            receiveCount += 1;
        }

        // WebSocket连接关闭时调用的事件处理函数
        private void Socket_OnClose(object sender, CloseEventArgs e)
        {
            // 添加连接关闭的日志信息，包含状态码和关闭原因
            AddLog(string.Format("Closed: StatusCode: {0}, Reason: {1}", e.StatusCode, e.Reason));
        }

        // WebSocket发生错误时调用的事件处理函数
        private void Socket_OnError(object sender, ErrorEventArgs e)
        {
            // 添加错误日志信息
            AddLog(string.Format("Error: {0}", e.Message));
        }

        // 记录帧数
        private int frame = 0;
        // 记录时间
        private float time = 0;
        // 记录FPS值
        private float fps = 0;
        // 每帧更新时调用
        private void Update()
        {
            // 帧数加1
            frame += 1;
            // 累加时间
            time += Time.deltaTime;
            // 当时间超过0.5秒时，计算FPS值
            if (time >= 0.5f)
            {
                // 计算FPS值
                fps = frame / time;
                // 重置帧数
                frame = 0;
                // 重置时间
                time = 0;
            }
        }
    }
}
