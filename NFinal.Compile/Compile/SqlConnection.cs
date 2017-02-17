using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NFinal.Compile
{
    public struct SqlConnection
    {
        public string varName;
        public string connectionName;
        public bool isGet;
        public string parName;
        public int index;
        public int length;
        public static System.Collections.Generic.List<SqlConnection> GetSqlConnectionList(string csharpCode)
        {
            System.Collections.Generic.List<SqlConnection> sqlConnectionList = new System.Collections.Generic.List<SqlConnection>();
            string sqlConnectionRegexStr = @"var\s+(\S+)\s*=\s*Models\s*\.\s*(\S+)\s*\.\s*(Open|Get)Connection\s*\(\s*(\S+)?\s*\)\s*;";
            Regex sqlConnectionRegex = new Regex(sqlConnectionRegexStr);
            MatchCollection sqlConnectionMac = sqlConnectionRegex.Matches(csharpCode);
            SqlConnection sqlConnection;
            for (int i = 0; i < sqlConnectionMac.Count; i++)
            {
                if (sqlConnectionMac[i].Success)
                {
                    sqlConnection = new SqlConnection();
                    sqlConnection.index = sqlConnectionMac[i].Index;
                    sqlConnection.length = sqlConnectionMac[i].Length;
                    sqlConnection.varName = sqlConnectionMac[i].Groups[1].Value;
                    sqlConnection.connectionName = sqlConnectionMac[i].Groups[2].Value;
                    sqlConnection.isGet = sqlConnectionMac[i].Groups[3].Value == "Get";
                    sqlConnection.parName = sqlConnectionMac[i].Groups[4].Value;
                    sqlConnectionList.Add(sqlConnection);
                }
            }
            return sqlConnectionList;
        }
    }
}
