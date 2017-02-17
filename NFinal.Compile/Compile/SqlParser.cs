//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :SqlParser.cs
//        Description :解析出sql语句中所有的表名与列名
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;///
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NFinal.Compile
{
    //解析出sql语句中所有的表名与列名
    public class SqlParser
    {
        public DB.Coding.DataUtility dataUtility;
        public SqlParser(DB.Coding.DataUtility dataUtility)
        {
            this.dataUtility = dataUtility;
        }

        public System.Collections.Generic.List<SqlInfo> Parse(string sql)
        {
            string temp=sql.Trim().ToLower();
            System.Collections.Generic.List<SqlInfo> sqlInfos = null;
            if( temp.StartsWith("select"))
            {
                SqlStatementSelect select = new SqlStatementSelect(sql,dataUtility);
                sqlInfos = new System.Collections.Generic.List<SqlInfo>();
            }
            else if (temp.StartsWith("insert"))
            {
                SqlStatementInsert insert = new SqlStatementInsert(sql, dataUtility.dbType);
                insert.Parse();
                sqlInfos = new System.Collections.Generic.List<SqlInfo>();
                sqlInfos.Add(insert.sqlInfo);
            }
            else if (temp.StartsWith("update"))
            {
                SqlStatementUpdate update = new SqlStatementUpdate(sql, dataUtility.dbType);
                update.Parse();
                sqlInfos = new System.Collections.Generic.List<SqlInfo>();
                sqlInfos.Add(update.sqlInfo);
            }
            else if (temp.StartsWith("delete"))
            {
                SqlStatementDelete delete = new SqlStatementDelete(sql, dataUtility.dbType);
                delete.Parse();
                sqlInfos = new System.Collections.Generic.List<SqlInfo>();
                sqlInfos.Add(delete.sqlInfo);
            }
            else
            {
                sqlInfos=new System.Collections.Generic.List<SqlInfo>();
            }
            //如果是Oracle数据库,则要把所有的表名转为大写
            if (dataUtility.dbType == DB.DBType.Oracle)
            {
                if (sqlInfos.Count > 0)
                {
                    for (int i = 0; i < sqlInfos.Count; i++)
                    {
                        if (sqlInfos[i].Tables.Count > 0)
                        {
                            //则要把所有的表名转为小写
                            for (int j = 0; j < sqlInfos[i].Tables.Count; j++)
                            {
                                sqlInfos[i].Tables[j].name=sqlInfos[i].Tables[j].name.ToLower();
                                sqlInfos[i].Tables[j].fullName = sqlInfos[i].Tables[j].fullName.ToLower();
                                sqlInfos[i].Tables[j].asName = sqlInfos[i].Tables[j].asName.ToLower();
                            }
                            //把所有列名转为小写
                            for (int j = 0; j < sqlInfos[i].Columns.Count; j++)
                            {
                                sqlInfos[i].Columns[j].name = sqlInfos[i].Columns[j].name.ToLower();
                                sqlInfos[i].Columns[j].fullName = sqlInfos[i].Columns[j].fullName.ToLower();
                                sqlInfos[i].Columns[j].asName = sqlInfos[i].Columns[j].asName.ToLower();
                            }
                        }
                    }
                }
            }

            return sqlInfos;
        }
    }
}