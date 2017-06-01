//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : Server.cs
//        Description :服务器配置
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

namespace NFinal.Config.Global
{
    /// <summary>
    /// 服务器配置信息
    /// </summary>
    public class Server
    {
        /// <summary>
        /// 请求的Url根。例如http://localhost:8080
        /// </summary>
        public string url;
        /// <summary>
        /// 首页。例如/Index.html
        /// </summary>
        public string indexDocument;
        //public StaticContent staticContent;
    }
    /// <summary>
    /// 静态文件类型
    /// </summary>
    public class StaticContent
    {
        /// <summary>
        /// 默认web文档类型
        /// </summary>
        public MimeMap[] mimeMap;
    }
    /// <summary>
    /// 文档类型
    /// </summary>
    public class MimeMap
    {
        /// <summary>
        /// 文件后缀
        /// </summary>
        public string[] fileExtension;
        /// <summary>
        /// 文档类型
        /// </summary>
        public string[] mimeType;
    }
}
