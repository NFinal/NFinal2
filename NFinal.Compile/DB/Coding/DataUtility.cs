//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :DataUtility.cs
//        Description :数据库信息类(获取所有的数据库信息)
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
    /// 数据库信息类
    /// </summary>
    public class DataUtility
    {
        public string conStr;
        public DbConnection con = null;
        public System.Collections.Generic.List<Table> tables = new System.Collections.Generic.List<Table>();
        //public Dictionary<string, CsTypeLink> csTypeDic = null;
        public Dictionary<string, TypeLink> sqlTypeDic = null;
        public Dictionary<int, TypeLink> dbTypeDic = null;
        public string[] dataBases;
        public string[] keywordsCs;
        public string[] keywordsJs;
        public string sql_getAllDataBase = "SELECT name FROM Master..SysDatabases order by Name";
        public string sql_getAllTables = "SELECT * FROM {0}..SysObjects Where XType='U' ORDER BY Name";
        public string sql_getAllColumns = "SELECT * FROM SysColumns WHERE id=Object_Id('{0}')";
        public string sql_getAllIds = "SELECT name FROM SysColumns WHERE id=Object_Id('{0}') and colid=(select  top 1 keyno from sysindexkeys where id=Object_Id('{0}'))";
        public string create_note_table = @"CREATE TABLE IF NOT EXISTS ""{0}"".""nfinal_note""
            (
            ""table_name""  TEXT,
            ""field_name""  TEXT,
            ""field_note""  TEXT
            );";
        public NFinal.Compile.DB.DBType dbType;
        /// <summary>
        /// 数据库信息类
        /// </summary>
        /// <param name="conStr">连接字符串</param>
        /// <param name="typeLinkFileName">数据库所对应的相关类型</param>
        /// <param name="csTypeLinkFileName">csharp所对应的相关类型</param>
        public DataUtility(string conStr, DB.DBType dbType)
        {
            this.conStr = conStr;
            this.dbType = dbType;
            TypeTable typeTable = new TypeTable(this.dbType);
            this.sqlTypeDic = typeTable.GetSqlTypeLinks();
            this.dbTypeDic = typeTable.GetDBTypeLinks();
            //CsTypeTable table = new CsTypeTable(this.dbType);
            //this.csTypeDic = table.GetCsTypeTableDic();
            //.net关键字列表
            keywordsCs = new string[] { 
                "abstract", "event", "new","struct",
                "as","explicit","null","switch",
                "base","extern","object","this",
                "bool","false","operator","throw",
                "break","finally","out","true",
                "byte","fixed","override","try",
                "case","float","params","typeof",
                "catch","for","private","uint",
                "char","foreach","protected","ulong",
                "checked","goto","public","unchecked",
                "class","if","readonly","unsafe",
                "const","implicit","ref","ushort",
                "continue","in","return","using",
                "decimal","int","sbyte","virtual",
                "default","interface","sealed","volatile",
                "delegate","internal","short","void",
                "do","is","sizeof","while",
                "double","lock","stackalloc",
                "else","long","static",
                "enum","namespace","string"
            };
            //javascript关键字列表
            keywordsJs = new string[] { 
                "break", "case", "catch", "continue", "debugger",
                "default", "delete", "do", "else", "false", "finally", 
                "for", "function", "if", "in", "instanceof", "new", 
                "null", "return", "switch", "this", "throw", "true", 
                "try", "typeof", "var", "void", "while", "with", 
                "abstract", "boolean", "byte", "char", "class", 
                "const", "double", "enum", "export", "extends", 
                "final", "float", "goto", "implements", "import", 
                "int", "interface", "long", "native", "package", 
                "private", "protected", "public", "short", "static", 
                "super", "synchronized", "throws", "transient", "volatile", 
                "arguments", "let", "yield"
            };
        }
        /// <summary>
        /// 修正JS字段名称
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>修正的名称</returns>
        public string GetNameJs(string name)
        {
            foreach (string key in keywordsJs)
            {
                if (name == key)
                {
                    return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToUpper(name);
                }
            }
            return name;
        }
        /// <summary>
        /// 修正CS字段名称
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>修正的名称</returns>
        public string GetNameCs(string name)
        {
            foreach (string key in keywordsCs)
            {
                if (name == key)
                {
                    return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToUpper(name);
                }
            }
            return name;
        }

        /// <summary>
        /// 获取所有的数据库名称
        /// </summary>
        /// <returns>数据库名数组</returns>
        public virtual string[] GetAllDataBase()
        {
            DbDataAdapter adp = GetDataAdapter(sql_getAllDataBase,con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataBases = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataBases[i] = dt.Rows[i]["name"].ToString();
            }
            dt.Dispose();
            adp.Dispose();
            return dataBases;
        }
        public virtual string GetDbType(int dbType)
        {
            return ((System.Data.SqlDbType)dbType).ToString();
        }
        public virtual DbCommand GetDbCommand(string cmd, DbConnection con)
        { 
            return new SqlCommand(cmd,(System.Data.SqlClient.SqlConnection)con);
        }
        /// <summary>
        /// 数据库适配器类
        /// </summary>
        /// <param name="cmd">数据库命令</param>
        /// <param name="con">数据库连接</param>
        /// <returns>数据库适配类</returns>
        public virtual DbDataAdapter GetDataAdapter(string cmd, DbConnection con)
        {
            return new SqlDataAdapter(cmd, (System.Data.SqlClient.SqlConnection)con);
        }
        /// <summary>
        /// 获取所有的表名
        /// </summary>
        /// <param name="dataBase">数据库名</param>
        /// <returns>所有的表名</returns>
        public virtual System.Collections.Generic.List<string> GetAllTableNames(string dataBase)
        {
            DbDataAdapter adp = GetDataAdapter(string.Format(sql_getAllTables, dataBase), con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            adp.Dispose();
            System.Collections.Generic.List<string> table_names = new System.Collections.Generic.List<string>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    table_names.Add(dt.Rows[i]["name"].ToString());
                }
            }
            dt.Dispose();
            return table_names;
        }
        /// <summary>
        /// 创建字段描述表
        /// </summary>
        public virtual void CreateNoteTable(string database)
        {
            DbCommand cmd = GetDbCommand(string.Format(create_note_table,database), con);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 获取所有的字段信息
        /// </summary>
        /// <param name="dataBase">数据库名称</param>
        /// <param name="table">表名</param>
        /// <returns></returns>
        public virtual DataTable GetAllColumns(string dataBase, string table)
        {
            DbDataAdapter adp = GetDataAdapter(string.Format(sql_getAllColumns, dataBase, table), con);
            DataTable dtCols = new DataTable();
            adp.Fill(dtCols);
            adp.Dispose();
            return dtCols;
        }
        /// <summary>
        /// 设置字段信息
        /// </summary>
        /// <param name="field">字段实体类</param>
        /// <param name="dr">表中一行</param>
        public virtual void SetField(ref Field field, DataRow dr)
        {
            //name(字段名称),position(字段位置),default_value(默认值),is_nullable(是否允许为空),data_type(数据类型),max_length(长度),oct_length(长度按字节)
            field.name = dr["name"].ToString();
            field.nameCs = GetNameCs(field.name);//csharp中的名称
            field.structFieldName = field.nameCs;
            field.nameJs = GetNameJs(field.name);//js中的名称
            field.position = Convert.ToInt32(dr["position"]);
            field.hasDefault = dr["default_value"] == DBNull.Value ? false : true;
            field.defautlValue = dr["default_value"].ToString();
            field.allowNull = dr["is_nullable"].ToString() == "0" || dr["is_nullable"].ToString()=="NO" ? false : true;
            field.sqlType = dr["data_type"].ToString();
            field.length =  dr["max_length"] == DBNull.Value ? 0 : Convert.ToInt32(dr["max_length"]);
            field.octLength = dr["oct_length"] == DBNull.Value ? 0 : Convert.ToInt32(dr["oct_length"]);
        }
        /// <summary>
        /// 获取表主键
        /// </summary>
        /// <param name="dataBase">数据库名</param>
        /// <param name="table">表名</param>
        /// <returns></returns>
        public virtual string GetId(string dataBase, string table)
        {
            DbDataAdapter adp = GetDataAdapter(string.Format(sql_getAllIds, dataBase, table), con);
            DataTable dtIdName = new DataTable();
            adp.Fill(dtIdName);
            string id = dtIdName.Rows.Count == 0 ? null : dtIdName.Rows[0][0].ToString();
            adp.Dispose();
            dtIdName.Dispose();
            return id;
        }
        /// <summary>
        /// 获取所有的表
        /// </summary>
        /// <param name="dataBase">数据库名</param>
        public void GetAllTables(string dataBase)
        {
            con.Open();
            TypeTable typeTable = new TypeTable(this.dbType);
            //Dictionary<string, TypeLink> links = typeTable.GetSqlTypeLinks();
            Table table;
            //获取所有的表
            System.Collections.Generic.List<string> dt_tables = GetAllTableNames(dataBase);
            DataTable dtCols = new DataTable();
            DataTable dtIdName = new DataTable();
            Field field;
            string idName = null;
            DbCommand cmdInsertNoteRecord = null;
            DbCommand cmdCount = null;
            DbCommand cmdGetNote = null;
            int noteCount = 0;
            if (dbType == DBType.Oracle)
            {
                try
                {
                    CreateNoteTable(dataBase);
                }
                catch(Exception) { }
            }
            else
            {
                CreateNoteTable(dataBase);
            }
            //循环查找所有的Table
            for (int i = 0; i < dt_tables.Count; i++)
            {
                table = new Table(dt_tables[i]);
                //相关参数
                //获取表名
                table.nameCs = GetNameCs(table.name);
                table.nameJs = GetNameJs(table.name);
                //获取表中所有的字段
                
                dtCols = GetAllColumns(dataBase, dt_tables[i]);

                //获取表中的主键值
                idName = GetId(dataBase, dt_tables[i]);
                table.hasId = idName != null;

                //添加列信息
                table.fields = new System.Collections.Generic.List<Field>();
                for (int j = 0; j < dtCols.Rows.Count; j++)
                {
                    field = new Field();
                    SetField(ref field, dtCols.Rows[j]);
                    field.nameCs = GetNameCs(field.name);
                    field.nameJs = GetNameJs(field.name);
                    //插入描述字段
                    cmdCount = GetDbCommand(string.Format("select count(*) from nfinal_note where table_name='{0}' and field_name='{1}'", table.name, field.name), con);
                    noteCount =Convert.ToInt32(cmdCount.ExecuteScalar());
                    if (noteCount < 1)
                    {
                        cmdInsertNoteRecord = GetDbCommand(string.Format("insert into nfinal_note(table_name,field_name,field_note) values('{0}','{1}','{2}')", table.name, field.name, string.Empty), con);
                        cmdInsertNoteRecord.ExecuteNonQuery();
                    }
                    else
                    {
                        cmdGetNote = GetDbCommand(string.Format("select field_note from nfinal_note where table_name='{0}' and field_name='{1}'", table.name, field.name), con);
                        field.description= cmdGetNote.ExecuteScalar().ToString();
                    }
                    if (idName == null)
                    {
                        field.isId = false;
                    }
                    else
                    {
                        field.isId = field.name == idName.ToString();
                    }
                    //如果出错，则说明此类型在csv中不存在，只需要该文件中添加相应的类型即可
                    field.sqlType = sqlTypeDic[field.sqlType].sqlType;
                    //csharp 基本类型
                    field.isValueType = sqlTypeDic[field.sqlType].isValueType;
                    field.csharpType = sqlTypeDic[field.sqlType].csharpType;
                    //reader.GetMethod();
                    field.GetMethodConvert = sqlTypeDic[field.sqlType].GetMethodConvert;
                    field.GetMethodName = sqlTypeDic[field.sqlType].GetMethodName;
                    field.GetMethodValue = sqlTypeDic[field.sqlType].GetMethodValue;
                    //csharp System.Data 中的类型
                    field.dbTypeInt = sqlTypeDic[field.sqlType].dbTypeInt;
                    field.dbType = sqlTypeDic[field.sqlType].dbType;
                    //数据转换时所标识的类型
                    field.jsonType = sqlTypeDic[field.sqlType].jsonType;
                    if (field.isId)
                    {
                        table.id = field;
                    }
                    //列类型所对应的数据转换成csharp成员时所对应的类型及转换函数
                    table.fields.Add(field);
                }
                dtCols.Dispose();
                dtIdName.Dispose();
                tables.Add(table);
            }
            con.Close();
        }
        /// <summary>
        /// 保存数据库信息
        /// </summary>
        public void Save()
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(List<Table>));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/base.xml", false, Encoding.UTF8);
            ser.Serialize(sw, tables);
            sw.Close();
            sw.Dispose();
        }
    }
}
