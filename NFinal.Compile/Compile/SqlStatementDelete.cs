//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :DeleteStatement.cs
//        Description :删除声明类(匹配删除语句)
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
    public class SqlStatementDelete : SqlStatement
    {
        public static string deleteReg = @"delete\s+from\s+(@([_a-zA-Z0-9]+)\s*=\s*)?([_a-zA-Z0-9]+)";
        public SqlStatementDelete(string sql, DB.DBType dbType)
            : base(sql, dbType)
        { 
            
        }
        public new static string FormatSql(string sql)
        {
            Regex reg = new Regex(deleteReg, RegexOptions.IgnoreCase);
            Match mat = reg.Match(sql);
            if (mat.Success)
            {
                if (mat.Groups[1].Success)
                {
                    sql = sql.Remove(mat.Groups[3].Index,mat.Groups[3].Length);
                    sql = sql.Remove(mat.Groups[1].Index,mat.Groups[1].Length);
                    sql = sql.Insert(mat.Groups[1].Index,"\"+"+mat.Groups[2].Value+"+\"");
                }
            }
            return sql;
        }
        public void Parse()
        {
            Regex reg = new Regex(deleteReg, RegexOptions.IgnoreCase);
            Match mat = reg.Match(this.sqlInfo.sql);
            
            if (mat.Success)
            {
                SqlTable tab = GetTable(mat.Groups[3].Value);
                if (mat.Groups[1].Success)
                {
                    sqlInfo.sql = sqlInfo.sql.Remove(mat.Groups[3].Index, mat.Groups[3].Length);
                    sqlInfo.sql = sqlInfo.sql.Remove(mat.Groups[1].Index, mat.Groups[1].Length);
                    sqlInfo.sql = sqlInfo.sql.Insert(mat.Groups[1].Index, mat.Groups[3].Value);
                }
                this.sqlInfo.Tables.Add(tab);
                this.sqlInfo.sqlVarParameters = ParseVarName(sqlInfo.sql);
            }
        }
    }
}