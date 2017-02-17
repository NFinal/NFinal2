//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :DBType.cs
//        Description :数据库类型枚举类
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
    /// 数据库类型枚举类
    /// </summary>
    public enum DBType
    {
        MySql,
        SqlServer,
        Sqlite,
        Oracle,
        PostgreSql
    }
}