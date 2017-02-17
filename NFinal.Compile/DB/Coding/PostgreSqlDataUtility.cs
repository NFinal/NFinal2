//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :SQLiteDataUtility.cs
//        Description :Sqlite数据库信息类
//
//        created by Lucas at  2015-6-30`
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace NFinal.Compile.DB.Coding
{
    /// <summary>
    /// Sqlite数据库信息类
    /// </summary>
    public class PostgreSqlDataUtility : DataUtility
    {
        public PostgreSqlDataUtility(string conStr)
            : base(conStr,NFinal.Compile.DB.DBType.PostgreSql)
        {
            //name(数据库名称)
            sql_getAllDataBase = "SELECT datname as name FROM pg_database;";
            //name(表名称)
            sql_getAllTables = "SELECT tablename as name FROM pg_tables WHERE tablename NOT LIKE 'pg%' AND tablename NOT LIKE 'sql_%' ORDER BY tablename;";
            //name(字段名称),position(字段位置),default_value(默认值),is_nullable(是否允许为空),data_type(数据类型),max_length(长度),oct_length(长度按字节)
            sql_getAllColumns = "select column_name as name,ordinal_position as position,column_default as default_value,is_nullable,data_type,character_maximum_length as max_length,character_octet_length as oct_length from information_schema.columns where table_name='{1}';";
            //name(主键名称),position(主键的位置)
            sql_getAllIds = @"select pg_attribute.attname as name,pg_constraint.conname as pk_name,pg_type.typname as typename from 
            pg_constraint inner join pg_class
            on pg_constraint.conrelid = pg_class.oid
            inner join pg_attribute on pg_attribute.attrelid = pg_class.oid
            and pg_attribute.attnum = pg_constraint.conkey[1]
            inner join pg_type on pg_type.oid = pg_attribute.atttypid
            where pg_class.relname = '{1}'
            and pg_constraint.contype = 'p'";
            create_note_table = @"CREATE TABLE IF NOT EXISTS nfinal_note (
               table_name VARCHAR(50) NOT NULL,
               field_name VARCHAR(50) NOT NULL ,
               field_note VARCHAR(255)
            ) ; ";
            if (conStr.IndexOf("|DataDirectory|") > 0)
            {
                conStr = conStr.Replace("|DataDirectory|", Frame.appRoot + "App_Data\\");
            }
            con = new Npgsql.NpgsqlConnection(conStr); 
        }
        public override string GetDbType(int dbType)
        {
            return ((NpgsqlTypes.NpgsqlDbType)dbType).ToString();
        }
        public override DbCommand GetDbCommand(string cmd, DbConnection con)
        {
            return new Npgsql.NpgsqlCommand(cmd, (Npgsql.NpgsqlConnection)con);
        }
        /// <summary>
        /// 数据库适配器类
        /// </summary>
        /// <param name="cmd">数据库命令</param>
        /// <param name="con">数据库连接</param>
        /// <returns>数据库适配类</returns>
        public override DbDataAdapter GetDataAdapter(string cmd, DbConnection con)
        {
            return new Npgsql.NpgsqlDataAdapter(cmd,(Npgsql.NpgsqlConnection)con);
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
            long temp = dr["max_length"] == DBNull.Value ? 0 : Convert.ToInt64(dr["max_length"]);
            if (temp < 0 || temp > int.MaxValue)
            {
                field.length = 0;
            }
            else
            {
                field.length = (int)temp;
            }
            temp = dr["oct_length"] == DBNull.Value ? 0 : Convert.ToInt64(dr["oct_length"]);
            if (temp < 0 || temp > int.MaxValue)
            {
                field.octLength = 0;
            }
            else
            {
                field.octLength = (int)temp;
            }
        }
    }
}
