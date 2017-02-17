//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :SqlInfo.cs
//        Description :
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;

namespace NFinal.Compile
{
    public class SqlInfo
    {
        public System.Collections.Generic.List<SqlColumn> Columns = new System.Collections.Generic.List<SqlColumn>();
        public string ColumnsSql = "";
        public System.Collections.Generic.List<SqlTable> Tables = new System.Collections.Generic.List<SqlTable>();
        public string TablesSql = "";
        public System.Collections.Generic.List<NFinal.Compile.SqlVarParameter> sqlVarParameters = new System.Collections.Generic.List<SqlVarParameter>();

        public string sql;
        public char[] sqls;
        public DB.DBType dbType;

        public SqlInfo(string sql, DB.DBType dbType)
        {
            this.sql = sql;
            sqls = sql.ToCharArray();
            this.dbType = dbType;
        }
    }
}