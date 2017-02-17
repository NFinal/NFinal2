//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :DBBase.cs
//        Description : 数据库魔法函数类,此类只为自动提示时使用,并不编译执行
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Data;
using System.Web;
using System.Collections.Generic;

namespace NFinal.Tips
{
    /// <summary>
    /// 数据库魔法函数类,此类只为自动提示时使用,并不编译执行
    /// </summary>
    public class DBBase
    {
        /// <summary>
        /// 执行SQL并返回相应集合数的NFinal.List对象
        /// </summary>
        /// <param name="sql">选取所有行对应的sql语句</param>
        /// <param name="top">选取的行数</param>
        /// <returns>NFinal.List集合</returns>
        public List<dynamic> QueryRandom(string sql,int top)
        {
            return new List<dynamic>();
        }
        /// <summary>
        /// 执行SQL并返回相应集合数的NFinal.List对象
        /// </summary>
        /// <param name="sql">选取所有行对应的sql语句</param>
        /// <param name="top">选取的行数</param>
        /// <returns>NFinal.List集合</returns>
        public List<T> QueryRandom<T>(string sql, int top) where T : IModel
        {
            return new List<T>();
        }
        /// <summary>
        /// 执行SQL并返回相应的NFinal.List对象
        /// </summary>
        /// <param name="sql">选取所有行对应的sql语句</param>
        /// <returns>NFinal.List集合</returns>
        public List<dynamic> QueryAll(string sql)
        {
            return new List<dynamic>();
        }
        /// <summary>
        /// 执行SQL并返回相应的NFinal.List对象
        /// </summary>
        /// <param name="sql">选取所有行对应的sql语句</param>
        /// <returns>NFinal.List集合</returns>
        public List<T> QueryAll<T>(string sql) where T : IModel
        {
            return new List<T>();
        }
        /// <summary>
        /// 执行SQL并返回相应的NFinal.List对象
        /// </summary>
        /// <param name="sql">选取所有行对应的sql语句</param>
        /// <param name="topQty">选取的行数</param>
        /// <returns>NFinal.List集合</returns>
        public List<dynamic> QueryTop(string sql,int topQty)
        {
            return new List<dynamic>();
        }
        /// <summary>
        /// 执行SQL并返回相应的NFinal.List对象
        /// </summary>
        /// <param name="sql">选取所有行对应的sql语句</param>
        /// <param name="topQty">选取的行数</param>
        /// <returns>NFinal.List集合</returns>
        public List<T> QueryTop<T>(string sql, int topQty) where T : IModel
        {
            return new List<T>();
        }
        /// <summary>
        /// 执行Sql并返回对应的分页后的NFinal.List对象
        /// </summary>
        /// <param name="sql">选取所有记录的SQL语句</param>
        /// <param name="page">NFinal.Page分页对象</param>
        /// <returns>NFinal.List对象</returns>
        public List<dynamic> Page(string sql,NFinal.Page page)
        {
            return new List<dynamic>();
        }
        /// <summary>
        /// 执行Sql并返回对应的分页后的NFinal.List对象
        /// </summary>
        /// <param name="sql">选取所有记录的SQL语句</param>
        /// <param name="page">NFinal.Page分页对象</param>
        /// <returns>NFinal.List对象</returns>
        public List<T> Page<T>(string sql, NFinal.Page page) where T:IModel
        {
            return new List<T>();
        }
        /// <summary>
        /// 执行SQL并返回NFinal.Struct对象
        /// </summary>
        /// <param name="sql">选取一行对应的sql语句</param>
        /// <returns>NFinal.Struct对象</returns>
        public dynamic QueryRow(string sql)
        {
            dynamic obj = null;
            return obj;
        }
        /// <summary>
        /// 执行SQL并返回NFinal.Struct对象
        /// </summary>
        /// <param name="sql">选取一行对应的sql语句</param>
        /// <returns>NFinal.Struct对象</returns>
        public T QueryRow<T>(string sql) where T:IModel
        {
            dynamic obj = null;
            return obj;
        }
        /// <summary>
        /// 执行sql并返回object对象
        /// </summary>
        /// <param name="sql">选取某行某列的sql语句</param>
        /// <returns>object对象</returns>
        public ObjectContainer QueryObject(string sql)
        {
            return new ObjectContainer();
        }
        /// <summary>
        /// 执行SQL并返回ID
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="str">数据库实体</param>
        /// <returns>id</returns>
        public ObjectContainer Insert(string tableName,NFinal.IModel str)
        {
            return new ObjectContainer();
        }
        /// <summary>
        /// 执行SQL并返回ID
        /// </summary>
        /// <param name="sql">sql插入行记录语句</param>
        /// <returns>id</returns>
        public ObjectContainer Insert(string sql)
        {
            return new ObjectContainer();
        }
        /// <summary>
        /// 执行SQL并返回更新行数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="str">数据库实体</param>
        /// <returns>更新行数</returns>
        public int Update(string tableName,NFinal.IModel str)
        {
            return 0;
        }
        /// <summary>
        /// 执行SQL并返回更新行数
        /// </summary>
        /// <param name="sql">sql更新行记录语句</param>
        /// <returns>更新行数</returns>
        public int Update(string sql)
        {
            return 0;
        }
        /// <summary>
        /// 执行SQL并返回删除行数
        /// </summary>
        /// <param name="sql">sql删除行记录语句</param>
        /// <returns>删除的行数</returns>
        public int Delete(string sql)
        {
            return 0;
        }
        /// <summary>
        /// 执行SQL并返回受影响行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>受影响行数</returns>
        public int ExecuteNonQuery(string sql)
        {
            return 0;
        } 
    }
}