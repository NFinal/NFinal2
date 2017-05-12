using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.IO
{
    public abstract class Writer : IWriter
    {
        ///// <summary>
        ///// 输出字符串
        ///// </summary>
        ///// <param name="writer"></param>
        ///// <param name="value"></param>
        //public void Write(string value)
        //{
        //    if (value != null)
        //    {
        //        byte[] buffer = NFinal.Constant.encoding.GetBytes(value);
        //        Write(buffer, 0, buffer.Length);
        //    }
        //}
        /// <summary>
        /// 输出反转义的Json字符串
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public void WriteJsonReverseString(string value)
        {
            byte[] buffer = value.JsonEncodeBytes();
            Write(buffer, 0, buffer.Length);
        }
        //public static void Write(this NFinal.IO.IWriter writer, dynamic value)
        //{
        //    if (value != null)
        //    {
        //        writer.Write(((object)value).ToString());
        //    }
        //}
        /// <summary>
        /// 输出字节
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="buffer"></param>
        public void Write(byte[] buffer)
        {
            Write(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// 输出Object.ToString();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public void Write<T>(NFinal.IO.IWriter writer, T value)
        {
            if (value != null)
            {
                Write(value.ToString());
            }
        }
        /// <summary>
        /// 输出一行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="writer"></param>
        /// <param name="obj"></param>
        public void WriteLine<T>(T obj)
        {
            Write(obj.ToString());
            Write(NFinal.Constant.Html_Br);
        }
        public void Write(StringContainer value)
        {
            Write(value.value);
        }

        public abstract void Write(byte[] buffer, int offset, int count);

        public abstract void Write(string value);
    }
}