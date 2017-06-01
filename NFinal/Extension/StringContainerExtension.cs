//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : StringContainerExtension.cs
//        Description :字符串转任意基础类型
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
    /// 字符串转任意基础类型
    /// </summary>
    public static class StringContainerExtension
    {
        /// <summary>
        /// 字符串转任意基础类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static StringContainer AsVar(this string obj)
        {
            return new StringContainer(obj);
        }
    }
}
