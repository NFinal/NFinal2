//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :UpdateStatement.cs
//        Description : 修改声明类(匹配更新语句)
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
    public class SqlStatementUpdate : SqlStatement
    {
        public static string updateRegStr = @"update\s+(?:@([_a-zA-Z0-9]+)\s*=\s*)?([_a-zA-Z0-9]+)\s+set";
        public static string columnNameReg = @"(?:,\s*|set\s+)([_a-zA-Z0-9]+(?:\s*\.\s*([_a-zA-Z0-9]+))*)\s*=";
        public SqlStatementUpdate(string sql, DB.DBType dbType)
            : base(sql, dbType)
        { }
        public static new string FormatSql(string sql)
        {
            Regex reg = new Regex(columnNameReg, RegexOptions.IgnoreCase);
            MatchCollection mac = reg.Matches(sql);
            int relative_position = 0;
            for (int i = 0; i < mac.Count; i++)
            {
                if (mac[i].Groups[2].Success)
                {
                    sql = sql.Remove(mac[i].Groups[1].Index+ relative_position, mac[i].Groups[1].Length);
                    sql = sql.Insert(mac[i].Groups[1].Index+ relative_position, mac[i].Groups[2].Value);
                    relative_position += mac[i].Groups[2].Length - mac[i].Groups[1].Length;
                }
            }
            Regex updateReg = new Regex(updateRegStr, RegexOptions.IgnoreCase);
            Match updateMat = updateReg.Match(sql);
            if (updateMat.Success)
            {
                if (updateMat.Groups[1].Success)
                {
                    sql = sql.Remove(updateMat.Index, updateMat.Length);
                    sql = sql.Insert(updateMat.Index, "update\"+" + updateMat.Groups[1].Value + "+\"set");
                }
            }
            return sql;
        }
        public void Parse()
        {
            Regex updateReg = new Regex(updateRegStr, RegexOptions.IgnoreCase);
            Match updateMat = updateReg.Match(this.sqlInfo.sql);
            if (updateMat.Success)
            {
                if (updateMat.Groups[1].Success)
                {
                    sqlInfo.sql = sqlInfo.sql.Remove(updateMat.Index, updateMat.Length);
                    sqlInfo.sql = sqlInfo.sql.Insert(updateMat.Index, updateMat.Groups[2].Value);
                }
                SqlTable tab = GetTable(updateMat.Groups[2].Value);
                this.sqlInfo.Tables.Add(tab);
                this.sqlInfo.sqlVarParameters=ParseVarName(sqlInfo.sql);
            }
        }
    }
}