//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :TopSqlAnalyse.cs
//        Description : 分析获取前几行sql语句类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace NFinal.Compile
{
    /// <summary>
    /// 选取前N行sql语句的分析
    /// </summary>
    public class TopSqlAnalyse
    {
        public string topSql;
        public string sql;
        public DB.DBType dbType;

        public TopSqlAnalyse(string sql, DB.DBType dbType)
        {
            this.sql = sql;
            this.dbType = dbType;
        }

        public void Parse()
        {
            //如果数据库是sqlserver
            if (dbType == DB.DBType.SqlServer)
            {
                string selectFromParttern = @"(select\s+)";
                Regex selectFromReg = new Regex(selectFromParttern, RegexOptions.IgnoreCase);
                Match mat = selectFromReg.Match(sql);
                if (mat.Success)
                {
                    topSql = sql.Insert(mat.Index + mat.Length, " top {0} ");
                }
            }
            //如果数据库是mysql
            else if (dbType == DB.DBType.MySql)
            {
                topSql = sql + " limit {0}";
            }
            //如果数据库是sqlite
            else if (dbType == DB.DBType.Sqlite)
            {

                topSql = sql + " Limit {0}";
            }
            else if (dbType == DB.DBType.Oracle)
            {
                topSql = sql + " where rownum<={0}";
            }
            else if (dbType == DB.DBType.PostgreSql)
            {
                topSql = sql + " limit {0}";
            }
        }
    }
}