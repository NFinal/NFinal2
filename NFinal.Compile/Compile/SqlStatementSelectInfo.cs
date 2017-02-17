//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :SelectStatementInfo.cs
//        Description :select语句解析结果
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;

namespace NFinal.Compile
{
    public class SqlStatementSelectInfo
    {
        public string sql;
        public string selectSql;
        public string runnableSql;
        public string selectClause;
        public string fromClause;
        public string whereClause;
        public string otherClause;
        public string sqlWithOutSubSelect;
        public System.Collections.Generic.List<SqlStatementSelectInfo> selects;
        public SqlStatementSelectInfo(string sql,bool isFirst)
        {
            this.sql = sql;
            this.selectSql = sql;
            this.runnableSql = sql;
            this.sqlWithOutSubSelect = sql;
            if (isFirst)
            {
                isFirst = false;
                //对于from @tableName 和order by @columnName 的参数做一下特殊处理
                string orderByPattern = @"\s+(order\s+by|from)\s+(@([_a-zA-Z0-9]+)(?:\s*=\s*([_a-zA-Z0-9]+))?)";
                Regex orderByReg = new Regex(orderByPattern, RegexOptions.IgnoreCase);
                MatchCollection orderByMac = orderByReg.Matches(sql);
                foreach (Match orderByMat in orderByMac)
                {
                    if (orderByMat.Success)
                    {
                        runnableSql = runnableSql.Remove(orderByMat.Groups[2].Index, orderByMat.Groups[2].Length);
                        //如果有默认值，或者是from情况下。
                        if (orderByMat.Groups[4].Success || orderByMat.Groups[2].Value.ToLower() == "from")
                        {
                            runnableSql = runnableSql.Insert(orderByMat.Groups[2].Index, orderByMat.Groups[4].Value);
                        }
                        else
                        {
                            runnableSql = runnableSql.Insert(orderByMat.Groups[2].Index, "id");
                        }
                        selectSql = selectSql.Remove(orderByMat.Groups[2].Index, orderByMat.Groups[2].Length);
                        selectSql = selectSql.Insert(orderByMat.Groups[2].Index, string.Format("\" + {0} + \"", orderByMat.Groups[3].Value.TrimStart('@')));
                    }
                }
                this.sql = runnableSql;
                this.sqlWithOutSubSelect = runnableSql;
            }
        }
    }
}