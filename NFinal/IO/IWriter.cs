using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.IO
{
    public interface IWriter
    {
        /// <summary>
        /// 输出字节流
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        void Write(byte[] buffer, int offset, int count);
        /// <summary>
        /// 输出字符串
        /// </summary>
        /// <param name="value">字符串</param>
        void Write(string value);
    }
}
