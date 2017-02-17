//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :Table.cs
//        Description : 从SQL语句中分析的表信息
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
    /// 从SQL语句中分析的表信息
    /// </summary>
    public class SqlTable
    {
        public string sql = string.Empty;
        public string name = string.Empty;
        public string dataBaseName = string.Empty;
        public string dboName = string.Empty;
        public string fullName = string.Empty;
        public string asName = string.Empty;
    }
}