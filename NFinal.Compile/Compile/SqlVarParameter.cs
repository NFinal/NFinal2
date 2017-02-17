//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :SqlVarParameter.cs
//        Description : 从SQL语句中分析的参数信息
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
    /// <summary>
    /// 从SQL语句中分析的参数信息
    /// </summary>
    public class SqlVarParameter
    {
        public string sql;
        public string name;
        public string tableName;
        public string fullName;
        public string csharpType;
        public string csharpName;
        public string columnName;
        //public DB.Coding.CsTypeLink link = null;
        public bool hasSqlError;
        public string sqlError;
        public DB.Coding.Field field;
    }
}