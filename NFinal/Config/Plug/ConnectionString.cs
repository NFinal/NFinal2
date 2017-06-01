//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ConnectionString.cs
//        Description :数据库连接字符串信息
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

namespace NFinal.Config.Plug
{
    /// <summary>
    /// 数据库连接字符串信息
    /// </summary>
    public class ConnectionString
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string connectionString;
        /// <summary>
        /// 数据库提供程序
        /// </summary>
        public string providerName;
    }
}
