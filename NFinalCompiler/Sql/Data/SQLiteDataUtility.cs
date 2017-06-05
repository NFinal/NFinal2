using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFinalCompiler.Sql.Data
{
    public class SQLiteDataUtility : DataUtility
    {
        public SQLiteDataUtility(IDbConnection con) : base(con)
        {
            this.sql_getAllTables = "select name from {0}.sqlite_master where type='table' and name!= 'sqlite_sequence' and name not like '/_%' escape '/'";
        }
    }
}
