using System;
using System.Text;

namespace NFinal.Advanced
{
    /// <summary>
    /// 通过对byte[]和string扩展方法实现对数据按BASE64算法进行编码或解码。
    /// </summary>
    public static class Base64Tool
    {
        /// <summary>
        /// 将2进制数据进行BASE64编码，得到编码文本。
        /// </summary>
        /// <param name="data">源数据</param>
        /// <param name="isBreakLines">是否在编码结果中每76个字符使用换行符进行分隔。</param>
        /// <returns>BASE64编码文本</returns>
        public static string Base64Encode(this byte[] data, bool isBreakLines = false)
        {
            return Convert.ToBase64String(data, isBreakLines
                ? Base64FormattingOptions.InsertLineBreaks
                : Base64FormattingOptions.None);
        }

        /// <summary>
        /// 将2进制数据中的一部分进行BASE64编码，得到编码文本
        /// </summary>
        /// <param name="data">源数据</param>
        /// <param name="start">需要编码的数据起始索引</param>
        /// <param name="length">需要编码的数据长度</param>
        /// <param name="isBreakLines">是否在编码结果中每76个字符使用换行符进行分隔。</param>
        /// <returns>BASE64编码文本</returns>
        public static string Base64Encode(this byte[] data, int start, int length,
            bool isBreakLines = false)
        {
            return Convert.ToBase64String(data, start, length, isBreakLines
                ? Base64FormattingOptions.InsertLineBreaks
                : Base64FormattingOptions.None);
        }

        /// <summary>
        /// 将BASE64编码文本进行解码，得到原来的2进制数据
        /// </summary>
        /// <param name="base64">编码文本</param>
        /// <returns>解码出来的2进制数据</returns>
        public static byte[] Base64Decode(this string base64)
        {
            return Convert.FromBase64String(base64);
        }

        /// <summary>
        /// 将字符串进行BASE64编码，得到编码文本。
        /// </summary>
        /// <param name="source">需要编码的字符串</param>
        /// <param name="encoding">编码前将源字符串转换成2进制数据的编码方法</param>
        /// <param name="isBreakLines">是否在编码结果中每76个字符使用换行符进行分隔。</param>
        /// <returns>BASE64编码文本</returns>
        public static string Base64Encode(this string source, Encoding encoding = null,
            bool isBreakLines = false)
        {
            return string.IsNullOrEmpty(source)
                ? source
                : source.ToByteArray(encoding ?? Settings.Global.DefaultEncoding).Base64Encode(isBreakLines);
        }

        /// <summary>
        /// 将字符串进行BASE64编码，得到编码文本。
        /// </summary>
        /// <param name="source">需要编码的字符串</param>
        /// <param name="encodingName">编码前将源字符串转换成2进制数据的编码方法</param>
        /// <param name="isBreakLines">是否在编码结果中每76个字符使用换行符进行分隔。</param>
        /// <returns>BASE64编码文本</returns>
        /// <exception cref="ArgumentException">encoding不是有效的代码页名称</exception>
        public static string Base64Encode(this string source, string encodingName,
            bool isBreakLines = false)
        {
            return Base64Encode(source, Encoding.GetEncoding(encodingName), isBreakLines);
        }

        /// <summary>
        /// 将BASE64编码文本进行解码，并将得到的二进制数据按 <c>encoding</c> 指定的编码生成字符串
        /// </summary>
        /// <param name="base64">BASE64编码数据</param>
        /// <param name="encoding">原始数据字符串的编码</param>
        /// <returns></returns>
        public static string Base64Decode(this string base64, Encoding encoding)
        {
            return string.IsNullOrEmpty(base64)
                ? base64
                : base64.Base64Decode().GetString(encoding ?? Settings.Global.DefaultEncoding);
        }

        /// <summary>
        /// 将BASE64编码文本进行解码，并将得到的二进制数据按 <c>encoding</c> 指定的编码生成字符串
        /// </summary>
        /// <param name="base64">BASE64编码数据</param>
        /// <param name="encodingName">原始数据字符串的编码</param>
        /// <returns></returns>
        public static string Base64Decoee(this string base64, string encodingName)
        {
            return Base64Decode(base64, Encoding.GetEncoding(encodingName));
        }
    }
}
