//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :Column.cs
//        Description :从SQL语句中分析的列信息类
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
    //select (*.*.)* from (*.*.)a,(*.*.)b
    //select a as a1,b as b1 from c,d
    //select count(*),a+b as id from a,b
    /// <summary>
    /// 从SQL语句中分析的列信息
    /// </summary>
    public class SqlColumn
    {
        public string sql = string.Empty;
        public string functionName = string.Empty;
        public string returnType = null;
        public string dataBaseName = string.Empty;
        public string tableName = string.Empty;
        public string name = string.Empty;
        public string fullName = string.Empty;
        public string asName = string.Empty;
    }
}