using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Data;
using System.Text.RegularExpressions;

namespace NFinal.Model
{
    public delegate string[] GetColumnNamesDelegate();
    public class GRUDHelper
    {
        private static Dictionary<Type, GetColumnNamesDelegate> dic_GetColumnNamesDelegate = new Dictionary<Type, GetColumnNamesDelegate>();
        //private static Dictionary<Type, string> dic_SimpleInsertSql = new Dictionary<Type, string>();
        public static string GetInsertSql<TModel>(string sqlWhere,string selectIdSql)
        {
         
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into ");
            sb.Append(GetTableName<TModel>());
            sb.Append("(");
            string[] columnNames = GetColumnNames<TModel>();
            bool isFirstColumn = true;
            foreach (var column in columnNames)
            {
                if (isFirstColumn)
                {
                    isFirstColumn = false;
                }
                else
                {
                    sb.Append(",");
                }
                sb.Append(column);
            }
            sb.Append(")");
            sb.Append(" values(");
            isFirstColumn = true;
            foreach (var column in columnNames)
            {
                if (isFirstColumn)
                {
                    isFirstColumn = false;
                }
                else
                {
                    sb.Append(",");
                }
                sb.Append("@");
                sb.Append(column);
            }
            sb.Append(")");
            if (sqlWhere?.Length > 0)
            {
                sb.Append(" ");
                sb.Append(sqlWhere);
            }
            sb.Append(selectIdSql);
            return sb.ToString();
        }
        
        public static string GetUpdateSql<TModel>(string sqlWhere,string idName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("update ");
            sb.Append(GetTableName<TModel>());
            sb.Append(" set ");
            string[] columnNames = GetColumnNames<TModel>();
            bool isFirstColumn = true;
            foreach (var column in columnNames)
            {
                if (isFirstColumn)
                {
                    isFirstColumn = false;
                }
                else
                {
                    sb.Append(",");
                }
                sb.Append(column);
                sb.Append("=@");
                sb.Append(column);
            }
            if (sqlWhere?.Length > 0)
            {
                sb.Append(" where ");
                sb.Append(" ");
                sb.Append(sqlWhere);
                //if (idName?.Length > 0)
                //{
                //    sb.Append(" and ");
                //    sb.Append(idName);
                //    sb.Append("=");
                //    sb.Append(idName);
                //}
            }
            else
            {
                if (idName?.Length > 0)
                {
                    sb.Append(" where ");
                    sb.Append(idName);
                    sb.Append("=@");
                    sb.Append(idName);
                }
            }
            return sb.ToString();
        }
        public static string GetSql<TModel>(string sqlWhere,string idName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from ");
            string tableName = GetTableName<TModel>();
            sb.Append(tableName);
            if (sqlWhere?.Length > 0)
            {
                sb.Append(" where ");
                sb.Append(sqlWhere);
            }
            else
            {
                sb.Append(" where ");
                sb.Append(idName);
                sb.Append("=@");
                sb.Append(idName);
            }
            return sb.ToString();
        }
        public static string GetAllSql<TModel>(string sqlWhere)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from ");
            string tableName = GetTableName<TModel>();
            sb.Append(tableName);
            if (sqlWhere?.Length > 0)
            {
                sb.Append(" where ");
                sb.Append(sqlWhere);
            }
            return sb.ToString();
        }
        public static string GetTopSql<TModel>(int top, DBType dbType, string sqlWhere)
        {
            string sql = GetAllSql<TModel>(sqlWhere);
            string topSql = null;
            //如果数据库是sqlserver
            if (dbType == DBType.SqlServer)
            {
                string selectFromParttern = @"(select\s+)";
                Regex selectFromReg = new Regex(selectFromParttern, RegexOptions.IgnoreCase);
                Match mat = selectFromReg.Match(sql);
                if (mat.Success)
                {
                    topSql = sql.Insert(mat.Index + mat.Length, " top {0} ");
                }
            }
            //如果数据库是mysql
            else if (dbType == DBType.MySql)
            {
                topSql = sql + " limit {0}";
            }
            //如果数据库是sqlite
            else if (dbType == DBType.Sqlite)
            {

                topSql = sql + " Limit {0}";
            }
            else if (dbType == DBType.Oracle)
            {
                topSql = sql + " where rownum<={0}";
            }
            else if (dbType == DBType.PostgreSql)
            {
                topSql = sql + " limit {0}";
            }
            return topSql;
        }
        public static string GetPageSql<TModel>(string sqlWhere,string idName,DBType dbType,int pageIndex,int pageSize,out string countSql)
        {
            string temp = string.Empty;
            string pageSql = null;
            string sql = GetAllSql<TModel>(sqlWhere);
      
            string selectFromParttern = @"select\s+([\s\S]+?)\s+from\s+";
            Regex selectFromReg = new Regex(selectFromParttern, RegexOptions.IgnoreCase);
            Match mat = selectFromReg.Match(sql);
            if (mat.Success)
            {
                countSql = sql.Remove(mat.Groups[1].Index, mat.Groups[1].Length);
                countSql = countSql.Insert(mat.Groups[1].Index, "count(*)");
            }
            else
            {
                countSql = "select count(*) from " + GetTableName<TModel>();
            }
            string orderBy = @" order by ";
            Regex orderByReg = new Regex(orderBy, RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
            Match orderByMat = orderByReg.Match(sql);
            bool hasOrderBy = orderByMat.Success;
            int p1=0,p2 = 0;
            //如果数据库是sqlserver
            if (dbType == DBType.SqlServer)
            {
                string selectParttern = @"\s*(select)\s+";
                Regex selectReg = new Regex(selectParttern, RegexOptions.IgnoreCase);
                Match selectMat = selectReg.Match(sql);

                string whereParttern = @"\s+where\s+";
                Regex whereReg = new Regex(whereParttern, RegexOptions.IgnoreCase);
                Match whereMat = whereReg.Match(sql);
                string fromParttern = @"\s+from\s+";
                Regex fromReg = new Regex(fromParttern, RegexOptions.IgnoreCase);
                Match fromMat = fromReg.Match(sql);
                
                //如果是select语句必定有select和from语句
                if (selectMat.Success)
                {
                    p1 = pageSize;
                    p2 = (pageIndex - 1) * pageSize;
                    //如果是select from where 的形式
                    if (whereMat.Success)
                    {
                        //如果没有order by排序
                        if (!hasOrderBy)
                        {
                            pageSql = $"{sql.Insert(selectMat.Index + selectMat.Length,$" top {p1} ")} and {idName} not in(select top {p2} {idName} {sql.Substring(fromMat.Groups[0].Index)})";
                        }
                        //如果有order by排序,则要把orderby语句放在最后面
                        else
                        {
                            pageSql = $"{sql.Insert(selectMat.Index + selectMat.Length, $" top {p1} ")} and {idName} not in (select top {p2} {idName} {sql.Substring(fromMat.Groups[0].Index)}) {orderByMat.Groups[0].Value}";
                        }
                    }
                    //如果是select from 的形式
                    else
                    {
                        //如果没有order by排序
                        if (!hasOrderBy)
                        {
                            pageSql = $"{sql.Insert(selectMat.Index + selectMat.Length,$" top {p1} ")} where {idName} not in(select top {p2} {idName} {sql.Substring(fromMat.Groups[0].Index)})";
                        }
                        //如果有order by排序,则要把orderby语句放在最后面
                        else
                        {
                            pageSql += $"{sql.Insert(selectMat.Index + selectMat.Length, $" top {p1} ")} where {idName} not in (select top {p2} {idName} {sql.Substring(fromMat.Groups[0].Index)}) {orderByMat.Groups[0].Value}";
                        }
                    }
                }
            }
            else if (dbType == DBType.Oracle)
            {
                p1 = pageIndex * pageSize;
                p2 = (pageIndex - 1) * pageSize + 1;
                string selectParttern = @"\s*(select)\s+";
                Regex selectReg = new Regex(selectParttern, RegexOptions.IgnoreCase);
                Match selectMat = selectReg.Match(sql);

                string whereParttern = @"\s+(where|from)\s+";
                Regex whereReg = new Regex(whereParttern, RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
                Match whereMat = whereReg.Match(sql);
                //如果是select语句必定有select和from语句
                if (selectMat.Success && whereMat.Success)
                {
                    //如果是select from where 的形式
                    if (whereMat.Groups[1].Value.ToLower() == "where")
                    {
                        temp = sql.Insert(whereMat.Index + whereMat.Length,
                                $" rownum<={p1} and ");
                        temp = temp.Insert(selectMat.Index + selectMat.Length,
                                " rownum rn,");
                        pageSql += string.Format($"select * from({p1}) where rn>={p2}", temp);
                    }
                    //如果是select from 的形式
                    else
                    {
                        temp = $"{sql} where rownum<={p1}";
                        temp = temp.Insert(selectMat.Index + selectMat.Length,
                                " rownum rn,");
                        pageSql += string.Format($"select * from({p1}) where rn>={p2}", temp);
                    }
                }
            }
            else if (dbType == DBType.PostgreSql)
            {
                p1 = pageSize;
                p2 = (pageIndex - 1) * pageSize;
                pageSql = $"{sql} Limit {p1} Offset {p2}";
            }
            //如果数据库是mysql
            else if (dbType == DBType.MySql)
            {
                p1 = pageSize;
                p2 = (pageIndex - 1) * pageSize;
                pageSql = $"{sql} limit {p2},{p1}";
            }
            //如果数据库是sqlite
            else if (dbType == DBType.Sqlite)
            {
                p1 = pageSize;
                p2 = (pageIndex - 1) * pageSize;
                pageSql = $"{sql} Limit {p1} Offset {p2}";
            }
            return string.Format(pageSql, p1, p2);
        }
        public static string GetDeleteSql<TModel>(string sqlWhere)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from ");
            sb.Append(GetTableName<TModel>());
            if (sqlWhere?.Length > 0)
            {
                sb.Append(" ");
                sb.Append(sqlWhere);
            }
            return sb.ToString();
        }
        public static string GetTableName<TModel>()
        {
            return typeof(TModel).Name;
        }
        public static string[] GetColumnNames<TModel>()
        {
            GetColumnNamesDelegate getColumnNamesDelegate = null;
            if (!dic_GetColumnNamesDelegate.TryGetValue(typeof(TModel), out getColumnNamesDelegate))
            {
                getColumnNamesDelegate = BuildColumnNamesDelegate<TModel>();
            }
            return getColumnNamesDelegate();
        }
        public static GetColumnNamesDelegate BuildColumnNamesDelegate<TModel>()
        {
            Type modelType = typeof(TModel);
            var properties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            DynamicMethod method = new DynamicMethod("GetColumnNames", typeof(string[]), Type.EmptyTypes);
            var methodIL= method.GetILGenerator();
            methodIL.Emit(OpCodes.Ldc_I4, properties.Length);
            methodIL.Emit(OpCodes.Newarr,typeof(System.String));
            
            for(int i=0;i<properties.Length;i++)
            {
                methodIL.Emit(OpCodes.Dup);
                methodIL.Emit(OpCodes.Ldc_I4,i);
                methodIL.Emit(OpCodes.Ldstr,properties[i].Name);
                methodIL.Emit(OpCodes.Stelem_Ref);
            }
            methodIL.Emit(OpCodes.Ret);

            return (GetColumnNamesDelegate)method.CreateDelegate(typeof(GetColumnNamesDelegate));
        }
        //public string[] getColumnNamesSample()
        //{
        //    return new string[] {"a","b" };
        //}
    }
}
