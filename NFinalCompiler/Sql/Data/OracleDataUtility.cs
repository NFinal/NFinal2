using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
namespace NFinalCompiler.Sql.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class OracleDataUtility : DataUtility
    {
        public OracleDataUtility(IDbConnection con) : base(con)
        {
            this.sql_getAllTables = @"SELECT TABLE_NAME AS name FROM user_tables where TABLE_NAME not like '%$%'";
        }
    }
}
