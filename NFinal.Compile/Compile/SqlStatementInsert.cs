//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :InsertStatement.cs
//        Description :插入声明类(匹配插入语句)
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace NFinal.Compile
{
    public class SqlStatementInsert : SqlStatement
    {
        //INSERT INTO 表名称 VALUES (值1, 值2,....)
        public static string sqlReg = @"insert\s+into\s+(@([_a-zA-Z0-9]+)\s*=\s*)?([_a-zA-Z0-9]+)(?:\s*\(\s*((?:[^,\s\(\)]+)(?:\s*,\s*[^,\s\(\)]+)*)\s*\))?\s*values\s*\(\s*((?:[^,\s\(\)]+)(?:\s*,\s*[^,\s\(\)]+)*)\s*\)";
        public string varNameReg = @"@([^,\s]+)";
        public SqlStatementInsert(string sql, DB.DBType dbType)
            : base(sql, dbType)
        { 
            
        }
        public static new string FormatSql(string sql)
        {
            Regex reg = new Regex(sqlReg, RegexOptions.IgnoreCase);
            Match mat = reg.Match(sql);
            if (mat.Success)
            {
                //把@user.id,@user.name替换为@user_id,@user_name
                int indexPos = 0;
                string columns=mat.Groups[4].Value;
                if (columns.IndexOf('.') > -1)
                {
                    columns = string.Empty;
                    string[] varNames = mat.Groups[4].Value.Split(',');
                    for (int i = 0; i < varNames.Length; i++)
                    {
                        indexPos = varNames[i].LastIndexOf('.');
                        if (i != 0)
                        {
                            columns += ",";
                        }
                        if (indexPos > -1)
                        {
                            columns += varNames[i].Substring(indexPos + 1);
                        }
                        else
                        {
                            columns += varNames[i];
                        }
                    }
                    sql= sql.Remove(mat.Groups[4].Index, mat.Groups[4].Length);
                    sql = sql.Insert(mat.Groups[4].Index,columns);
                }
                if (mat.Groups[1].Success)
                {
                    sql = sql.Remove(mat.Groups[3].Index, mat.Groups[3].Length);
                    sql = sql.Remove(mat.Groups[1].Index, mat.Groups[1].Length);
                    sql = sql.Insert(mat.Groups[1].Index, "\"+"+mat.Groups[2].Value + "+\"");
                }
            }
            
            return sql;
        }
        public void Parse()
        {
            Regex reg=new Regex(sqlReg,RegexOptions.IgnoreCase);
            Match mat = reg.Match(sqlInfo.sql);
            SqlTable table =null;
            if (mat.Success)
            {
                table = GetTable(mat.Groups[3].Value);
                this.sqlInfo.Tables.Add(table);

                this.sqlInfo.ColumnsSql = mat.Groups[4].Value;
                this.sqlInfo.Columns= ParseColumn(mat.Groups[4].Value);
                string[] varNames = mat.Groups[5].Value.Split(',');
                if (sqlInfo.Columns.Count == varNames.Length)
                {
                    SqlVarParameter sqlVarParameter = null;
                    Regex varReg = new Regex(varNameReg);
                    Match varMat = null;
                    for (int i = 0; i < sqlInfo.Columns.Count; i++)
                    {
                        varMat= varReg.Match(varNames[i]);
                        if (varMat.Success)
                        {
                            sqlVarParameter = new SqlVarParameter();
                            sqlVarParameter.sql=varNames[i];
                            sqlVarParameter.name= varMat.Groups[1].Value.Replace('.','_');
                            sqlVarParameter.csharpName = varMat.Groups[1].Value;
                            sqlVarParameter.columnName = sqlInfo.Columns[i].name;
                            sqlInfo.sqlVarParameters.Add(sqlVarParameter);
                        }
                    }
                }
            }
        }
    }
}