using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Data
{
    /// <summary>
    /// 表设置
    /// </summary>
    public class TableAttribute : System.Attribute
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 表属性设置
        /// </summary>
        /// <param name="tableName"></param>
        public TableAttribute(string tableName)
        {
            this.TableName = tableName;
        }
    }
}
