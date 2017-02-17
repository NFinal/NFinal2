using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFinalModelGenerator.Data
{
    public class SqlServerDataUtility : DataUtility
    {
        public SqlServerDataUtility(IDbConnection con) : base(con)
        {
            this.sql_getAllTables = "SELECT name FROM {0}..SysObjects Where XType='U' AND name!='sysdiagrams' ORDER BY name";
        }
    }
}
