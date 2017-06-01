//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : Debug.cs
//        Description :调试信息，在开发时调用。
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace NFinal.Config.Global
{
    /// <summary>
    /// 调试信息
    /// </summary>
    public class Debug
    {
        /// <summary>
        /// 是否打开调试
        /// </summary>
        public bool enable;
        /// <summary>
        /// 调试页面
        /// </summary>
        public string url;
        /// <summary>
        /// 调试html目录
        /// </summary>
        public string directory;
    }
}
