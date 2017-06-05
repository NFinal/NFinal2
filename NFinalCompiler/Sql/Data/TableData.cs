using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace NFinalCompiler.Sql.Data
{
    public class ColumnData
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 基本类型
        /// </summary>
        public Type BaseType { get; set; }
        /// <summary>
        /// 可为Nullable的类型
        /// </summary>
        public string TypeString { get; set; }
        /// <summary>
        /// 是否可为null
        /// </summary>
        public bool AllowDBNull { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefautValue { get; set; }
        /// <summary>
        /// 是否是引用类型
        /// </summary>
        public bool ReferenceType { get; set; }
        public bool CustomeType { get; set; } = false;
    }
    public class TableData
    {
        public string Name { get; set; }
        public string Sql { get; set; }
        public string ExecutableSql { get; set; }
        public List<ColumnData> ColumnDataList = new List<ColumnData>();
        public TableData(string name, string sql)
        {
            this.Name = name;
            this.Sql = sql;
            this.ExecutableSql = sql;
            Regex reg = new Regex(@"'([\S]+)'\s*([aA][Ss]\s*'\s*\(\s*([^\)]+)\s*\)')");
            MatchCollection mac= reg.Matches(this.ExecutableSql);
            ColumnData columnData;
            if (mac.Count > 0)
            {
                for (int i = mac.Count - 1; i >= 0; i--)
                {
                    this.ExecutableSql= this.ExecutableSql.Remove(mac[i].Groups[2].Index, mac[i].Groups[2].Length);
                    columnData = new ColumnData();
                    columnData.TypeString = mac[i].Groups[3].Value;
                    columnData.Name = mac[i].Groups[1].Value;
                    columnData.CustomeType = true;
                    this.ColumnDataList.Add(columnData);
                }
            }
        }
    }
}
