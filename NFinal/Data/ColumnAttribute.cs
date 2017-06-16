using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Data
{
    /// <summary>
    /// 列设置
    /// </summary>
    public class ColumnAttribute :System.Attribute
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 列属性设置
        /// </summary>
        /// <param name="columnName"></param>
        public ColumnAttribute(string columnName)
        {
            this.ColumnName = columnName;
        }
    }
}
