using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Dapper;

namespace NFinalCompiler.Sql.Data
{
    public class DataUtility
    {
        public bool hasGetOrdinal = false;
        public int ColumnNameOrdinal = -1;
        public int DataTypeOrdinal = -1;
        public int AllowDBNullOrdinal = -1;
        public string sql_getAllTables = string.Empty;
        public IDbConnection con = null;
        public DataUtility(IDbConnection con)
        {
            this.con = con;
        }
        public void Open()
        {
            this.con.Open();
        }
        public TableNameModel[] GetAllTableName()
        {
            var tables= con.Query<TableNameModel>(string.Format(sql_getAllTables, con.Database));
            return tables.ToArray();
        }
        public List<TableData> GetDefaultTableData()
        {
            List<TableData> tableDataList = new List<Data.TableData>();
            TableNameModel[] tableNameArray = GetAllTableName();
            TableData tableData = null;
            for (int i = 0; i < tableNameArray.Length; i++)
            {
                tableData = new TableData(tableNameArray[i].name, "select * from " + tableNameArray[i].name);
                tableDataList.Add(tableData);
            }
            return tableDataList;
        }
        public void SetTableColumnData(TableData tableData)
        {
            using (IDbCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = tableData.ExecutableSql;
                using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SequentialAccess | CommandBehavior.SchemaOnly | CommandBehavior.SingleResult))
                {
                    using (DataTable dt = reader.GetSchemaTable())
                    {
                        GetAllColomnData(tableData,dt);
                    }
                }
            }
        }
        public void SetTableColumnData(List<TableData> tableDataList)
        {   
            for (int i = 0; i < tableDataList.Count; i++)
            {
                SetTableColumnData(tableDataList[i]);
            }
        }
        public void GetAllColomnData(TableData tableData, DataTable dt)
        {
            ColumnData columnData = null;
            bool hasSameName = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!hasGetOrdinal)
                {
                    if (ColumnNameOrdinal == -1)
                    {
                        ColumnNameOrdinal = dt.Columns["ColumnName"].Ordinal;
                    }
                    if (DataTypeOrdinal == -1)
                    {
                        DataTypeOrdinal = dt.Columns["DataType"].Ordinal;
                    }
                    if (AllowDBNullOrdinal == -1)
                    {
                        AllowDBNullOrdinal = dt.Columns["AllowDBNull"].Ordinal;
                    }
                    hasGetOrdinal = true;
                }
                hasSameName = false;
                for (int j = 0; j < tableData.ColumnDataList.Count; j++)
                {
                    if (tableData.ColumnDataList[j].Name == dt.Rows[i][ColumnNameOrdinal].ToString())
                    {
                        hasSameName = true;
                    }
                }
                if (!hasSameName)
                {
                    columnData = new ColumnData();
                    SetColumnData(dt, dt.Rows[i], ref columnData);
                    tableData.ColumnDataList.Add(columnData);
                }
            }
        }
        public virtual void  SetColumnData(DataTable dt,DataRow dr,ref ColumnData columnData)
        {
            
            //base.SetColumnData(dt,dr,ref columnData);
            columnData.Name = dr[ColumnNameOrdinal].ToString();
            columnData.BaseType = (Type)dr[DataTypeOrdinal];
            columnData.AllowDBNull = (bool)dr[AllowDBNullOrdinal];
            columnData.TypeString = columnData.BaseType.ToString();
            if (columnData.BaseType.IsValueType)
            {
                if (columnData.AllowDBNull)
                {
                    columnData.TypeString = columnData.BaseType.ToString() + "?";
                }
            }
        }
        public void Close()
        {
            this.con.Close();
        }
    }
}
