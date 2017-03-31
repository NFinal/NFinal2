using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;
using NFinal.Model;

namespace NFinal
{
    public static class GRUDExtension
    {
        //public static string selectIdSql = ";select @@IDENTITY";
        //public static string idName = "id";
        public static TKey SimpleInsert<TKey,TModel>(this IDbConnection con,TModel model,string sqlWhere=null, IDbTransaction transaction = null)
        {
            DBInfo dbInfo = DBInfoHelper.GetDBInfo(con);
            string sql= Model.GRUDHelper.GetInsertSql<TModel>(sqlWhere, dbInfo.selectIdSql);
            return con.ExecuteScalar<TKey>(sql + dbInfo.selectIdSql);
        }
        public static bool SimpleUpdate<TModel>(this IDbConnection con, TModel model, string sqlWhere=null, IDbTransaction transaction = null)
        {
            DBInfo dbInfo = DBInfoHelper.GetDBInfo(con);
            string sql = Model.GRUDHelper.GetUpdateSql<TModel>(sqlWhere, dbInfo.idName);
            return con.Execute(sql, model) > 0;
        }
        public static bool SimpleDelete<TModel>(this IDbConnection con, TModel model, string sqlWhere=null, IDbTransaction transaction = null)
        {
            string sql = Model.GRUDHelper.GetDeleteSql<TModel>(sqlWhere);
            return con.Execute(sql, model,transaction)>0;
        }
        public static TModel SimpleGet<TModel>(this IDbConnection con, TModel model, string sqlWhere = null, IDbTransaction transaction = null)
        {
            DBInfo dbInfo = DBInfoHelper.GetDBInfo(con);
            string sql = Model.GRUDHelper.GetSql<TModel>(sqlWhere, dbInfo.idName);
            return con.QueryFirstOrDefault<TModel>(sql, model, transaction);
        }
        public static IEnumerable<TModel> SimpleGetAll<TModel>(this IDbConnection con, TModel model, string sqlWhere = null, IDbTransaction transaction = null)
        {
            DBInfo dbInfo = DBInfoHelper.GetDBInfo(con);
            string sql = Model.GRUDHelper.GetSql<TModel>(sqlWhere, dbInfo.idName);
            return con.Query<TModel>(sql, model, transaction);
        }
        public static IEnumerable<TModel> SimpleGetTop<TModel>(this IDbConnection con, int top,string sqlWhere = null, IDbTransaction transaction = null)
        {
            DBInfo dbInfo = DBInfoHelper.GetDBInfo(con);
            string sql = Model.GRUDHelper.GetTopSql<TModel>(top, dbInfo.dbType,sqlWhere);
            return con.Query<TModel>(sql, transaction);
        }
        public static IEnumerable<TModel> SimpleGetPage<TModel>(this IDbConnection con, int pageIndex,int pageSize,out int count, string sqlWhere = null, IDbTransaction transaction = null)
        {
            DBInfo dbInfo = DBInfoHelper.GetDBInfo(con);
            string countSql;
            string sql = Model.GRUDHelper.GetPageSql<TModel>(sqlWhere, dbInfo.idName, dbInfo.dbType, pageIndex, pageSize, out countSql);
            count = con.ExecuteScalar<int>(countSql);
            return con.Query<TModel>(sql, transaction);
        }
        public static bool SimpleDelete<TKey,TModel>(this IDbConnection con, TKey key,string sqlWhere=null,object whereParam=null)
        {
            string sql = Model.GRUDHelper.GetDeleteSql<TModel>(sqlWhere);
            return con.Execute(sql, whereParam) > 0;
        }
    }
}
