
using Cysharp.Text;
using System;

public static partial class Utility
{
    /// <summary>
    /// 字符相关的实用函数。
    /// </summary>
    public static class Text
    {
        public static string ConnectStrs(params string[] strs)
        {
            using (var sb = ZString.CreateStringBuilder())
            {
                int len = strs.Length;
                for (int i = 0; i < len; ++i)
                {
                    sb.Append(strs[i]);
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// 获取格式化字符串。
        /// </summary>
        /// <typeparam name="T">字符串参数的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg">字符串参数。</param>
        /// <returns>格式化后的字符串。</returns>
        public static string Format<T>(string format, T arg)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            return ZString.Format(format, arg);
        }

        /// <summary>
        /// 获取格式化字符串。
        /// </summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <returns>格式化后的字符串。</returns>
        public static string Format<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            return ZString.Format(format, arg1, arg2);
        }

        /// <summary>
        /// 获取格式化字符串。
        /// </summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <returns>格式化后的字符串。</returns>
        public static string Format<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            return ZString.Format(format, arg1, arg2, arg3);
        }

        /// <summary>
        /// 获取格式化字符串。
        /// </summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <param name="arg4">字符串参数 4。</param>
        /// <returns>格式化后的字符串。</returns>
        public static string Format<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            return ZString.Format(format, arg1, arg2, arg3, arg4);
        }
        public static string Format<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            return ZString.Format(format, arg1, arg2, arg3, arg4, arg5);
        }
        public static string Format<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            return ZString.Format(format, arg1, arg2, arg3, arg4, arg5, arg6);
        }
        public static string Format<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            return ZString.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
    }
}


