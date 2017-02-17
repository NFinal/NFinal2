using System;
//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :UserControl.cs
//        Description :用户控件父类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System.Collections.Generic;
using System.Web;

namespace NFinal
{
    /// <summary>
    /// 用户控件类
    /// </summary>
    public class UserControl
    {
        /// <summary>
        /// 输出控件对应的html的方法代理
        /// </summary>
        public delegate void __Render__();
        /// <summary>
        /// 输出控件对应的html的方法
        /// </summary>
        public __Render__ __render__ = null;
        /// <summary>
        /// 二级域名
        /// </summary>
        public string _subdomain;//二级域名
        /// <summary>
        /// 模块名
        /// </summary>
        public string _app;
        /// <summary>
        /// 请求的URL
        /// </summary>
        public string _url;
    }
}