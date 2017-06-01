//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : DBType.cs
//        Description :数据库类型
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Model
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DBType
    {
        /// <summary>
        /// sqlserver数据库
        /// </summary>
        SqlServer,
        /// <summary>
        /// Mysql数据库
        /// </summary>
        MySql,
        /// <summary>
        /// postgresql数据库
        /// </summary>
        PostgreSql,
        /// <summary>
        /// sqlite数据库
        /// </summary>
        Sqlite,
        /// <summary>
        /// oracle数据库
        /// </summary>
        Oracle
    }
}
