//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :UnCode.cs
//        Description :字符串反转义
//
//        created by Lucas at  2015-6-30`
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Compile.DB.Coding
{
    /// <summary>
    /// 字符串反转义
    /// </summary>
    class UnCode
    {
        /// <summary>
        /// 添加字符串
        /// </summary>
        /// <param name="sb">字符串类</param>
        /// <param name="strValue">要添加的字符串</param>
        /// <returns></returns>
        private string AddString(StringBuilder sb,string strValue)
        {
            for (int i = 0; i < strValue.Length; i++)
            {
                switch (strValue[i])
                {
                    case '\"': sb.Append("\\\"");break;
                    case '\r': sb.Append("\'"); break;
                    case '\n': sb.Append("\n"); break;
                    case '\v': break;
                    case '\f': break;
                    case '\b': break;
                    default: sb.Append(strValue[i]); break;
                }
            }
            return sb.ToString();
        }
    }
}