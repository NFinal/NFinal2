//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : GRUDExtension.cs
//        Description :数据库基本增删改查扩展函数
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;
using NFinal.Model;

namespace NFinal
{
    /// <summary>
    /// 数据库基本增删改查扩展函数
    /// </summary>
    public static class GRUDExtension
    {
        //public static string selectIdSql = ";select @@IDENTITY";
        //public static string idName = "id";
        /// <summary>
        /// 简单插入
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="con"></param>
        /// <param name="model"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static TKey SimpleInsert<TKey,TModel>(this IDbConnection con,TModel model,string sqlWhere=null, IDbTransaction transaction = null)
        {
            DBInfo dbInfo = DBInfoHelper.GetDBInfo(con);
            string sql= Model.GRUDHelper.GetInsertSql<TModel>(sqlWhere, dbInfo.selectIdSql);
            return con.ExecuteScalar<TKey>(sql + dbInfo.selectIdSql);
        }
        /// <summary>
        /// 简单更新
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="con"></param>
        /// <param name="model"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="transaction"></param>
        public static bool SimpleUpdate<TModel>(this IDbConnection con, TModel model, string sqlWhere=null, IDbTransaction transaction = null)
        {
            DBInfo dbInfo = DBInfoHelper.GetDBInfo(con);
            string sql = Model.GRUDHelper.GetUpdateSql<TModel>(sqlWhere, dbInfo.idName);
            return con.Execute(sql, model) > 0;
        }
        /// <summary>
        /// 简单删除
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="con"></param>
        /// <param name="model"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static bool SimpleDelete<TModel>(this IDbConnection con, TModel model, string sqlWhere=null, IDbTransaction transaction = null)
        {
            string sql = Model.GRUDHelper.GetDeleteSql<TModel>(sqlWhere);
            return con.Execute(sql, model,transaction)>0;
        }
        /// <summary>
        /// 简单获取
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="con"></param>
        /// <param name="model"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static TModel SimpleGet<TModel>(this IDbConnection con, TModel model, string sqlWhere = null, IDbTransaction transaction = null)
        {
            DBInfo dbInfo = DBInfoHelper.GetDBInfo(con);
            string sql = Model.GRUDHelper.GetSql<TModel>(sqlWhere, dbInfo.idName);
            return con.QueryFirstOrDefault<TModel>(sql, model, transaction);
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="con"></param>
        /// <param name="model"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IEnumerable<TModel> SimpleGetAll<TModel>(this IDbConnection con, TModel model, string sqlWhere = null, IDbTransaction transaction = null)
        {
            DBInfo dbInfo = DBInfoHelper.GetDBInfo(con);
            string sql = Model.GRUDHelper.GetSql<TModel>(sqlWhere, dbInfo.idName);
            return con.Query<TModel>(sql, model, transaction);
        }
        /// <summary>
        /// 获取前N行
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="con"></param>
        /// <param name="top"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IEnumerable<TModel> SimpleGetTop<TModel>(this IDbConnection con, int top,string sqlWhere = null, IDbTransaction transaction = null)
        {
            DBInfo dbInfo = DBInfoHelper.GetDBInfo(con);
            string sql = Model.GRUDHelper.GetTopSql<TModel>(top, dbInfo.dbType,sqlWhere);
            return con.Query<TModel>(sql, transaction);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="con"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IEnumerable<TModel> SimpleGetPage<TModel>(this IDbConnection con, int pageIndex,int pageSize,out int count, string sqlWhere = null, IDbTransaction transaction = null)
        {
            DBInfo dbInfo = DBInfoHelper.GetDBInfo(con);
            string countSql;
            string sql = Model.GRUDHelper.GetPageSql<TModel>(sqlWhere, dbInfo.idName, dbInfo.dbType, pageIndex, pageSize, out countSql);
            count = con.ExecuteScalar<int>(countSql);
            return con.Query<TModel>(sql, transaction);
        }
        /// <summary>
        /// 简单删除
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="con"></param>
        /// <param name="key"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="whereParam"></param>
        /// <returns></returns>
        public static bool SimpleDelete<TKey,TModel>(this IDbConnection con, TKey key,string sqlWhere=null,object whereParam=null)
        {
            string sql = Model.GRUDHelper.GetDeleteSql<TModel>(sqlWhere);
            return con.Execute(sql, whereParam) > 0;
        }
    }
}
