//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : DBInfoHelper.cs
//        Description :获取数据库相关基本信息
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NFinal.Collections;

namespace NFinal.Model
{
    /// <summary>
    /// 数据库基本信息
    /// </summary>
    public class DBInfo
    {
        /// <summary>
        /// Id名称
        /// </summary>
        public string idName;
        /// <summary>
        /// 返回Id的sql语句
        /// </summary>
        public string selectIdSql;
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DBType dbType;
    }
    /// <summary>
    /// 获取数据库相关基本信息
    /// </summary>
    public class DBInfoHelper
    {
        /// <summary>
        /// 数据库缓存信息
        /// </summary>
        public static System.Collections.Concurrent.ConcurrentDictionary<string, DBInfo> DBInfoCache =
            new System.Collections.Concurrent.ConcurrentDictionary<string, DBInfo>();
        /// <summary>
        /// 获取数据库基本信息
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public static DBInfo GetDBInfo(IDbConnection con)
        {
            DBInfo dbInfo;
            if (!DBInfoCache.TryGetValue(con.ConnectionString,out dbInfo))
            {
                dbInfo = new DBInfo();
                dbInfo.idName = "id";
                if (con.ConnectionString.IndexOf("mysql", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    dbInfo.dbType = DBType.MySql;
                    dbInfo.selectIdSql = ";select @@IDENTITY";
                }
                else if (con.ConnectionString.IndexOf("sqlclient", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    dbInfo.dbType = DBType.SqlServer;
                    dbInfo.selectIdSql = ";select @@IDENTITY";
                }
                else if (con.ConnectionString.IndexOf("sqlite", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    dbInfo.dbType = DBType.Sqlite;
                    dbInfo.selectIdSql = ";select @@IDENTITY";
                }
                else if (con.ConnectionString.IndexOf("oracle", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    dbInfo.dbType = DBType.Oracle;
                    dbInfo.selectIdSql = ";select @@IDENTITY";
                }
                else if (con.ConnectionString.IndexOf("npgsql", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    dbInfo.dbType = DBType.PostgreSql;
                    dbInfo.selectIdSql = ";select @@IDENTITY";
                }
                else
                {
                    throw new NFinal.Exceptions.DataBaseNotSupportException(con.Database);
                }
                DBInfoCache.TryAdd(con.ConnectionString, dbInfo);
            }
            return dbInfo;
        }
    }
}
