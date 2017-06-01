//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ViewAttribute.cs
//        Description :对RazorView实现类设置View路径信息的特性
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal
{
    /// <summary>
    /// 对RazorView实现类设置View路径信息的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewAttribute : System.Attribute
    {
        /// <summary>
        /// View路径
        /// </summary>
        public string viewUrl;
        /// <summary>
        /// 设置View路径
        /// </summary>
        /// <param name="url">View路径</param>
        public ViewAttribute(string url) {
            viewUrl = url;
        }
    }
}