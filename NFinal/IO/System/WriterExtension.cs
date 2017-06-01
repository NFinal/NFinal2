//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : WriterExtension.cs
//        Description :输出扩展函数
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal
{
    /// <summary>
    /// 输出扩展函数
    /// </summary>
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
            byte[] buffer = value.JsonEncodeBytes();
            writer.Write(buffer,0,buffer.Length);
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
        //public static void Write(this NFinal.IO.IWriter writer, NFinal.Validation.ValidResult result)
        //{
        //    foreach (var r in result)
        //    {
        //        writer.Write(r.message);
        //    }
        //}
        //public static void Write(this NFinal.IO.IWriter writer, ObjectContainer obj)
        //{
        //    writer.Write(obj.value.ToString());
        //}
        //public static void Write(this NFinal.IO.IWriter writer, StringContainer obj)
        //{
        //    writer.Write(obj.value);
        //}
    }
}
