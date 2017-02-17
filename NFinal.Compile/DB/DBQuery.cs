//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :DBObject.cs
//        Description :执行SQL并返回行对象数组
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;

namespace NFinal.Compile.DB
{
    public class DBQuery : DBCommand
    {
        public void OpenConnection()
        { }
        public void CloseConnection()
        {

        }
        /// <summary>
        /// 执行SQL并返回相应行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<dynamic> QueryRandom(string sql, int top)
        {
            return new List<dynamic>();
        }
        /// <summary>
        /// 执行SQL并返回行对象数组
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<dynamic> QueryAll(string sql)
        {
            return new List<dynamic>();
        }
        /// <summary>
        /// 执行SQL并返回行对象数组
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<dynamic> QueryTop(string sql, int topQty)
        {
            return new List<dynamic>();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sql">选取所有记录的SQL语句</param>
        /// <param name="pageIndexVarName">传送页码所需的变量,默认为pageIndex</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public List<dynamic> Page(string sql, int pageSize)
        {
            return new List<dynamic>();
        }

        /// <summary>
        /// 执行SQL并返回行对象
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public dynamic QueryRow(string sql)
        {
            dynamic a = 0;
            return a;
        }
        public SqlObject QueryObject(string sql)
        {
            return new SqlObject(null);
        }
    }
}