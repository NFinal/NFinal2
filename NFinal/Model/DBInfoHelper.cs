using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NFinal.Model
{
    public class DBInfo
    {
        public string idName;
        public string selectIdSql;
        public DBType dbType;
    }
    public class DBInfoHelper
    {
        public static Dictionary<string, DBInfo> DBInfoCache = new Dictionary<string, DBInfo>();
        public static DBInfo GetDBInfo(IDbConnection con)
        {
            DBInfo dbInfo;
            if (!DBInfoCache.TryGetValue(con.ConnectionString,out dbInfo))
            {
                dbInfo = new DBInfo();
                dbInfo.idName = "id";
                if (con.ConnectionString.IndexOf("mysql", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    dbInfo.dbType = DBType.MySql;
                    dbInfo.selectIdSql = ";select @@IDENTITY";
                }
                else if (con.ConnectionString.IndexOf("sqlclient", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    dbInfo.dbType = DBType.SqlServer;
                    dbInfo.selectIdSql = ";select @@IDENTITY";
                }
                else if (con.ConnectionString.IndexOf("sqlite", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    dbInfo.dbType = DBType.Sqlite;
                    dbInfo.selectIdSql = ";select @@IDENTITY";
                }
                else if (con.ConnectionString.IndexOf("oracle", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    dbInfo.dbType = DBType.Oracle;
                    dbInfo.selectIdSql = ";select @@IDENTITY";
                }
                else if (con.ConnectionString.IndexOf("npgsql", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    dbInfo.dbType = DBType.PostgreSql;
                    dbInfo.selectIdSql = ";select @@IDENTITY";
                }
                else
                {
                    throw new NFinal.Exceptions.DataBaseNotSupportException(con.Database);
                }
                DBInfoCache.Add(con.ConnectionString, dbInfo);
            }
            return dbInfo;
        }
    }
}
