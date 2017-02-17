//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :SQLDataUtility.cs
//        Description :sql数据库信息类
//
//        created by Lucas at  2015-6-30`
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace NFinal.Compile.DB.Coding
{
    /// <summary>
    /// Sql数据库信息类
    /// </summary>
    public class SQLDataUtility : DataUtility
    {
        public SQLDataUtility(string conStr)
            : base(conStr,DB.DBType.SqlServer)
        {

            //name(数据库名称)
            sql_getAllDataBase = "Select Name FROM Master..SysDatabases order by Name";
            //name(表名称)
            sql_getAllTables = "SELECT name FROM {0}..SysObjects Where XType='U' AND name!='sysdiagrams' ORDER BY name";
            //name(字段名称),position(字段位置),default_value(默认值),is_nullable(是否允许为空),data_type(数据类型),max_length(长度),oct_length(长度按字节)
            sql_getAllColumns = @"USE {0}
                SELECT COLUMN_NAME AS 'name',ORDINAL_POSITION AS 'position',COLUMN_DEFAULT AS 'default_value',IS_NULLABLE AS 'is_nullable',DATA_TYPE as 'data_type'
                ,CHARACTER_MAXIMUM_LENGTH as 'max_length',CHARACTER_OCTET_LENGTH as 'oct_length'
                  FROM INFORMATION_SCHEMA.COLUMNS WHERE  TABLE_NAME='{1}'";
            //name(主键名称),position(主键的位置)
            sql_getAllIds = "USE {0} SELECT COLUMN_NAME AS 'name',ORDINAL_POSITION AS 'position' FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME='{1}'";
            create_note_table = @"if object_id(N'nfinal_note',N'U') is  null
            begin
            CREATE TABLE [dbo].nfinal_note(
	            [table_name] [nvarchar](50) NOT NULL,
	            [field_name] [nvarchar](50) NOT NULL,
	            [field_note] [nvarchar](255) NULL
            ) ON [PRIMARY]
            end;";
            con =new System.Data.SqlClient.SqlConnection(conStr);
        }
        public override string GetDbType(int dbType)
        {
            return ((SqlDbType)dbType).ToString();
        }
        public override DbCommand GetDbCommand(string cmd, DbConnection con)
        {
            return new SqlCommand(cmd, (System.Data.SqlClient.SqlConnection)con);
        }
        /// <summary>
        /// 数据库适配器类
        /// </summary>
        /// <param name="cmd">数据库命令</param>
        /// <param name="con">数据库连接</param>
        /// <returns>数据库适配类</returns>
        public override DbDataAdapter GetDataAdapter(string cmd,DbConnection con)
        {
            return new SqlDataAdapter(cmd,(System.Data.SqlClient.SqlConnection) con);
        }
        /// <summary>
        /// 设置字段信息
        /// </summary>
        /// <param name="field">字段实体类</param>
        /// <param name="dr">表中一行</param>
        public override void SetField(ref Field field, DataRow dr)
        {
            //name(字段名称),position(字段位置),default_value(默认值),is_nullable(是否允许为空),data_type(数据类型),max_length(长度),oct_length(长度按字节)
            field.name = dr["name"].ToString();
            field.nameCs = GetNameCs(field.name);//csharp中的名称
            field.structFieldName = field.nameCs;
            field.nameJs = GetNameJs(field.name);//js中的名称
            field.position = Convert.ToInt32(dr["position"]);
            field.hasDefault = dr["default_value"] == DBNull.Value ? false : true;
            field.defautlValue = dr["default_value"].ToString();
            field.allowNull = dr["is_nullable"].ToString() == "0" ? false : true;
            field.sqlType = dr["data_type"].ToString();
            if (dr["max_length"] == DBNull.Value)
            {
                field.length = 0;
            }
            else
            {
                field.length =Convert.ToInt32(dr["max_length"])<0?0 : Convert.ToInt32(dr["max_length"]);
            }
            if (dr["oct_length"] == DBNull.Value)
            {
                field.octLength = 0;
            }
            else
            {
                field.octLength = Convert.ToInt32(dr["oct_length"]) < 0 ? 0 : Convert.ToInt32(dr["oct_length"]);
            }
        }
    }
}
