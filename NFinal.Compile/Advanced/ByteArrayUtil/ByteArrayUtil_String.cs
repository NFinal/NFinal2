using System;
using System.Text;

namespace NFinal.Advanced
{
    partial class ByteArrayUtil
    {
        /// <summary>
        /// 将字符数组按指定编码转换为字符串。
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="encoding">编码，
        /// 如果为<c>null</c>则使用<see cref="P:Settings.Global.ByteArrayEncoding" />代替</param>
        /// <returns>通过转换生成的字符串</returns>
        public static string GetString(this byte[] bytes, Encoding encoding = null)
        {
            if (bytes == null) { return null; }
            if (bytes.Length == 0) { return string.Empty; }

            return (encoding ?? Settings.Global.DefaultEncoding).GetString(bytes);
        }

        /// <summary>
        /// 将字节数组按指定编码转换为字符串。
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="encodingName">编码名称，如果为<c>null</c>或<c>string.Empty</c>，
        /// 则使用<see cref="P:Settings.Global.ByteArrayEncoding" />编码</param>
        /// <returns>通过转换生成的字符串</returns>
        public static string GetString(this byte[] bytes, string encodingName)
        {
            if (bytes == null) { return null; }
            if (bytes.Length == 0) { return string.Empty; }

            var encoding = string.IsNullOrWhiteSpace(encodingName)
                ? Settings.Global.DefaultEncoding
                : Encoding.GetEncoding(encodingName);

            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 将字符串按指定的编号转换为字节数组。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="encoding">指定转换的编码格式，
        /// 如果<c>encoding</c>为null，则用<see cref="P:Settings.Global.ByteArrayEncoding" />代替</param>
        /// <returns><c>byte[]</c>表示的转换结果</returns>
        public static byte[] ToByteArray(this string s, Encoding encoding = null)
        {
            if (s == null) { return null; }
            if (s.Length == 0) { return EmptyByteArray; }

            return (encoding ?? Settings.Global.DefaultEncoding).GetBytes(s);
        }

        /// <summary>
        /// 将字符串按指定的编号转换为字节数组。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="encodingName">指定转换的编码格式，
        /// 如果<c>encoding</c>为null，则用<see cref="P:Settings.Global.ByteArrayEncoding" />代替</param>
        /// <returns><c>byte[]</c>表示的转换结果</returns>
        /// <exception cref="ArgumentException">
        /// <see cref="System.Text.Encoding.GetEncoding(string)"/></exception>
        public static byte[] ToByteArray(this string s, string encodingName)
        {
            if (s == null) { return null; }
            if (s.Length == 0) { return EmptyByteArray; }

            var encoding = string.IsNullOrWhiteSpace(encodingName)
                ? Settings.Global.DefaultEncoding
                : Encoding.GetEncoding(encodingName);
            return encoding.GetBytes(s);
        }
    }
}