//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : Url.cs
//        Description :Url配置
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Config.Plug
{
    /// <summary>
    /// Url配置
    /// </summary>
    public class Url
    {
        /// <summary>
        /// Url前缀
        /// </summary>
        public string prefix;
        /// <summary>
        /// Url默认后缀，如.php,.aspx,.html等
        /// </summary>
        public string extension;
        /// <summary>
        /// 默认首页
        /// </summary>
        public string defaultDocument;
    }
}
