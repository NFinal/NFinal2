using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class WriterExtension
    {
        /// <summary>
        /// 输出字符串
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void Write(this NFinal.IO.IWriter writer,string value)
        {
            if (value != null)
            {
                byte[] buffer = NFinal.Constant.encoding.GetBytes(value);
                writer.Write(buffer, 0, buffer.Length);
            }
        }
        /// <summary>
        /// 输出反转义的Json字符串
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void WriteJsonReverseString(this NFinal.IO.IWriter writer,string value)
        {
            byte[] buffer = NFinal.Constant.encoding.GetBytes(value);
            buffer = NFinal.Utility.GetJsonString(buffer, 0, buffer.Length);
            writer.Write(buffer,0,buffer.Length);
        }
        public static void Write(this NFinal.IO.IWriter writer, dynamic value)
        {
            if (value != null)
            {
                writer.Write(((object)value).ToString());
            }
        }
        /// <summary>
        /// 输出字节
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="buffer"></param>
        public static void Write(this NFinal.IO.IWriter writer, byte[] buffer)
        {
            writer.Write(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// 输出Object.ToString();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void Write<T>(this NFinal.IO.IWriter writer, T value)
        {
            if (value != null)
            {
                writer.Write(value.ToString());
            }
        }
        /// <summary>
        /// 输出一行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="writer"></param>
        /// <param name="obj"></param>
        public static void WriteLine<T>(this NFinal.IO.IWriter writer, T obj)
        {
            writer.Write(obj.ToString());
            writer.Write(NFinal.Constant.Html_Br);
        }
    }
}
