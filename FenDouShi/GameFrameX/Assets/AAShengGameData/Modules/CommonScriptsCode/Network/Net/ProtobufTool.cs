using UnityEngine;
using System.IO;
using ProtoBuf;
using System;

public static class ProtobufTool
{
    // 序列化对象 → 字节数组
    public static byte[] PSerializer(object entity)
    {
      //  Logger.PrintColor("white", $"发送协议数据 ProtobufTool PSerializer({entity})");
        if (entity == null)
        {
            Debug.LogWarning("ProtobufTool.Serialize: 对象为Null");
            return new byte[0];
        }

        try
        {
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, entity);
                return ms.ToArray();
            }
        }
        catch (ProtoException ex)
        {
            Debug.LogError($"序列化失败: {ex.Message}");
            return null;
        }

        //Serialize
        ////	byte[] buffer = null;
        //	using (MemoryStream memoryStream = new MemoryStream())
        //	{
        //		Serializer.Serialize(memoryStream, entity);
        //           //m.Position = 0;
        //           //int length = (int)m.Length;
        //           //buffer = new byte[length];
        //           //m.Read(buffer, 0, length);
        //           return memoryStream.ToArray(); // 直接获取字节数组，避免手动读取
        //       }
    }

    // 反序列化字节数组 → 对象
    public static T PDeserialize<T>(byte[] buffer)
    {
        if (buffer == null || buffer.Length == 0)
        {
          //  Debug.LogWarning("ProtobufTool.Deserialize: 数据为空");
            // 尝试创建类型 T 的实例，而不是返回 default(T)
            try
            {
                return Activator.CreateInstance<T>();
            }
            catch (Exception ex)
            {
                Debug.LogWarning("无法创建类型 T 的实例，返回 default(T): " + ex.Message);
                return default(T);
            }
        }

        try
        {
            using (var ms = new MemoryStream(buffer))
            {
                return Serializer.Deserialize<T>(ms);
            }
        }
        catch (ProtoException ex)
        {
            Debug.LogError($"反序列化失败: ex.Message:{ex.Message}  字节长度buffer.Length={buffer.Length}");
            return default(T);
        }
    }
}


