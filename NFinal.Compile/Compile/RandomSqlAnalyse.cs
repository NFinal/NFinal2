//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :RandomSqlAnalyse.cs
//        Description :sql查询获取随记行集合类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;

namespace NFinal.Compile
{
    //选取随机行
    public class RandomSqlAnalyse
    {
        public string sql;
        public string randomSql;
        public DB.DBType dbType;
        public RandomSqlAnalyse(string sql,DB.DBType dbType)
        {
            this.sql = sql;
            this.dbType = dbType;
        }
        public void Parse()
        {
            string pattern = string.Empty;
            if (dbType == DB.DBType.MySql)
            {
                randomSql = sql + " order by rand() limit {0}";

            }
            else if (dbType == DB.DBType.SqlServer)
            {
                pattern = @"\s*(select)\s+";
                Regex selectReg = new Regex(pattern);
                Match selectMat = selectReg.Match(sql);
                if (selectMat.Success)
                {
                    randomSql = sql.Insert(selectMat.Index + selectMat.Length, " top {0} ");
                    randomSql += " order by newid()";
                }
            }
            else if (dbType == DB.DBType.Sqlite)
            {
                randomSql = sql + " order by random() limit {0}";
            }
            else if (dbType == DB.DBType.PostgreSql)
            {
                randomSql = sql + " order by random() limit {0}";
            }
            else if(dbType == DB.DBType.Oracle)
            {
                randomSql = "select * from (" + sql + " order by dbms_random.random) where rownum<={0}";
            }
        }
    }
}