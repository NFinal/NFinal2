using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NFinal.Compile
{
    public struct SqlTransaction
    {
        public string connectionVarName;
        public string connectionName;
        public string varName;
        public bool isGet;
        public string parName;
        public static System.Collections.Generic.List<SqlTransaction> GetSqlTransactionList(string csharpCode, System.Collections.Generic.List<SqlConnection> sqlConnectionList)
        {
            System.Collections.Generic.List<SqlTransaction> sqlTransactionList = new System.Collections.Generic.List<SqlTransaction>();
            string sqlTransactionRegexStr = @"var\s+(\S+)\s*=\s*(\S+)\s*\.\s*(Get|Begin)Transaction\s*\(\s*(\S+)?\s*\)\s*;";
            Regex sqlTrasactionRegex = new Regex(sqlTransactionRegexStr);
            MatchCollection sqlTrasactionMac = sqlTrasactionRegex.Matches(csharpCode);
            SqlTransaction sqlTrasaction;
            if (sqlConnectionList == null)
            {
                sqlConnectionList = SqlConnection.GetSqlConnectionList(csharpCode);
            }
            for (int i = 0; i < sqlTrasactionMac.Count; i++)
            {
                if (sqlTrasactionMac[i].Success)
                {
                    sqlTrasaction = new SqlTransaction();
                    sqlTrasaction.varName = sqlTrasactionMac[i].Groups[1].Value;
                    sqlTrasaction.connectionVarName = sqlTrasactionMac[i].Groups[2].Value;
                    for (int j = 0; j < sqlConnectionList.Count; j++)
                    {
                        if (sqlTrasaction.connectionVarName == sqlConnectionList[j].varName)
                        {
                            sqlTrasaction.connectionName = sqlConnectionList[j].connectionName;
                            break;
                        }
                    }
                    sqlTrasaction.isGet = sqlTrasactionMac[i].Groups[3].Value == "Get";
                    sqlTrasaction.parName = sqlTrasactionMac[i].Groups[4].Value;
                    sqlTransactionList.Add(sqlTrasaction);
                }
            }
            return sqlTransactionList;
        }
    }
}
