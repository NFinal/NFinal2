//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :ConnectionString.cs
//        Description :配置实体类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;

namespace NFinal.Compile.DB
{
    /// <summary>
    /// Web.Config中连字符串实体类
    /// </summary>
    public class ConnectionString
    {
        public string name;
        public string value;
        public string provider;
        public DB.DBType type;
    }
}