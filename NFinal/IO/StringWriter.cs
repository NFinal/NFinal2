//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : StringWriter.cs
//        Description :输出字符串
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.IO
{
    /// <summary>
    /// 输出字符串
    /// </summary>
    public class StringWriter : Writer
    {
        private StringBuilder sb = null;
        /// <summary>
        /// 初始化
        /// </summary>
        public StringWriter()
        {
            sb = new StringBuilder();
        }
        /// <summary>
        /// 写字节流
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            string value = System.Text.Encoding.UTF8.GetString(buffer,offset,count);
            sb.Append(value);
        }
        /// <summary>
        /// 返回写入的文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return sb.ToString();
        }
        /// <summary>
        /// 写字符串
        /// </summary>
        /// <param name="value"></param>
        public override void Write(string value)
        {
            sb.Append(value);
        }
    }
}
