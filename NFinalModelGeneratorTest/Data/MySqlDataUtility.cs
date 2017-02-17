using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFinalModelGenerator.Data
{
    public class MySqlDataUtility:DataUtility
    {
        
        public MySqlDataUtility(IDbConnection con):base(con)
        {
            this.sql_getAllTables= "SELECT TABLE_NAME AS name FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='{0}'";
        }
        //public override void SetColumnData(DataTable dt, DataRow dr,ref ColumnData columnData)
        //{
            
        //}
    }
}
