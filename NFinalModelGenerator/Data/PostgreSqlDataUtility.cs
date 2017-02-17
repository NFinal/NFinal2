using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFinalModelGenerator.Data
{
    public class PostgreSqlDataUtility : DataUtility
    {
        public PostgreSqlDataUtility(IDbConnection con) : base(con)
        {
            this.sql_getAllTables = "SELECT tablename as name FROM pg_tables WHERE tablename NOT LIKE 'pg%' AND tablename NOT LIKE 'sql_%' ORDER BY tablename;";
        }
    }
}
