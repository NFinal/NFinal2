//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :SelectStatement.cs
//        Description :select语句解析类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NFinal.Compile
{

    public class SqlStatementSelect : SqlStatement
    {
        public DB.Coding.DataUtility dataUtility;
        //语句分三部分,select+columns+fromTables
        //select(?:\s+distinct|\s+top\s+[0-9]+|\s+top\s+[0-9]+\s+percent)?
        //((?:\s+[^,\s]+)(?:\s+as\s+(?:[^,\s]+))?(?:\s*,\s*(?:[^,\s]+)(?:\s+as\s+(?:[^,\s]+))?)*)
        //(?:\s+from((?:\s+[^,\s]+)(?:\s+as\s+(?:[^,\s]+))?(?:\s*,\s*(?:[^,\s]+)(?:\s+as\s+(?:[^,\s]+))?)*))?
        //static string selectStatementReg = @"select(?:\s+distinct|\s+top\s+\S+|\s+top\s+\S+\s+percent)?((?:\s+[^,\s]+)(?:\s+as\s+(?:[^,\s]+))?(?:\s*,\s*(?:[^,\s]+)(?:\s+as\s+(?:[^,\s]+))?)*)(?:\s+from((?:\s+[^,\s]+)(?:\s+as\s+(?:[^,\s]+))?(?:\s*,\s*(?:[^,\s]+)(?:\s+as\s+(?:[^,\s]+))?)*))?";
        public SqlStatementSelect(string sql, DB.Coding.DataUtility dataUtility)
            : base(sql, dataUtility.dbType)
        {
            this.dataUtility = dataUtility;
        }
        /// <summary>
        /// 解析出SQL中的select,from,where子语句
        /// </summary>
        public void ParseSQL(ref SqlStatementSelectInfo selectStatement)
        {
            //string sql="select count(*),id,name from (select * from users) right on a where id in(select id from users)";
            Regex selectReg = new Regex(@"\s*select\s+", RegexOptions.IgnoreCase);
            Match selectMat = selectReg.Match(selectStatement.sql);
            int LeftBracket = 0;
            int RightBracket = 0;
            char[] sqlString = selectStatement.sql.ToCharArray();
            bool[] InBracket = new bool[sqlString.Length];
            for (int i = 0; i < sqlString.Length; i++)
            {
                if (sqlString[i] == '(')
                {
                    LeftBracket++;
                }
                else if (sqlString[i] == ')')
                {
                    RightBracket++;
                }
                if (LeftBracket == RightBracket)
                {
                    InBracket[i] = false;
                }
                else
                {
                    InBracket[i] = true;
                }
            }
            Regex fromReg = new Regex(@"\s+from\s+", RegexOptions.IgnoreCase);
            MatchCollection fromMac = fromReg.Matches(selectStatement.sql);
            Match fromMat = null;
            for (int i = 0; i < fromMac.Count; i++)
            {
                if (!InBracket[fromMac[i].Index])
                {
                    fromMat = fromMac[i];
                    break;
                }
            }
            Regex whereReg = new Regex(@"\s+where\s+", RegexOptions.IgnoreCase);
            MatchCollection whereMac = whereReg.Matches(selectStatement.sql);
            Match whereMat = null;
            for (int i = 0; i < whereMac.Count; i++)
            {
                if (!InBracket[whereMac[i].Index])
                {
                    whereMat = whereMac[i];
                    break;
                }
            }
            Regex otherReg = new Regex(@"\s+(group\s+by|having|order\s+by|with|limit)\s+");
            if (fromMat != null)
            {
                selectStatement.selectClause = selectStatement.sql.Substring(selectMat.Index + selectMat.Length, fromMat.Index - selectMat.Index - selectMat.Length);
            }
            else
            {
                selectStatement.selectClause = selectStatement.sql.Substring(selectMat.Index + selectMat.Length);
            }
            //如果where不存在
            if (whereMat == null)
            {
                if (fromMat != null)
                {
                    selectStatement.fromClause = selectStatement.sql.Substring(fromMat.Index + fromMat.Length);
                    Match otherMat = otherReg.Match(selectStatement.fromClause);
                    if (otherMat.Success)
                    {
                        selectStatement.fromClause = selectStatement.fromClause.Substring(0, otherMat.Index);
                    }
                }
                else
                {
                    selectStatement.fromClause = null;
                }
                selectStatement.selects = new System.Collections.Generic.List<SqlStatementSelectInfo>();
                selectStatement.whereClause = null;
            }
            //如果where存在
            else
            {
                selectStatement.fromClause = selectStatement.sql.Substring(fromMat.Index + fromMat.Length, whereMat.Index - fromMat.Index - fromMat.Length);
                selectStatement.whereClause = selectStatement.sql.Substring(whereMat.Index + whereMat.Length);
                Match otherMat = otherReg.Match(selectStatement.whereClause);
                if (otherMat.Success)
                {
                    selectStatement.whereClause = selectStatement.whereClause.Substring(0, otherMat.Index);
                }
                selectStatement.selects = new System.Collections.Generic.List<SqlStatementSelectInfo>();
                ParseSubSelect(ref selectStatement);
            }
        }
        public void ParseSubSelect(ref SqlStatementSelectInfo MainSelecteStatement)
        {
            string subSelectPattern = @"\(\s*select\s+((?<open>\()|(?<-open>\))|[\s\S])*(?(open)(?!))\)";
            Regex subSelectReg = new Regex(subSelectPattern, RegexOptions.IgnoreCase);
            MatchCollection mac = subSelectReg.Matches(MainSelecteStatement.sql);
            string subSelectSql = null;
            int relative_position = 0;
            MainSelecteStatement.sqlWithOutSubSelect = MainSelecteStatement.sql;
            for (int i = 0; i < mac.Count; i++)
            {
                //去掉查到的子查询
                MainSelecteStatement.sqlWithOutSubSelect = MainSelecteStatement.sqlWithOutSubSelect.Remove(mac[i].Index + relative_position, mac[i].Length);
                relative_position -= mac[i].Length;
                //去掉两边的括号
                subSelectSql = mac[i].Value.Substring(1, mac[i].Value.Length - 2);
                SqlStatementSelectInfo selectStatement = new SqlStatementSelectInfo(subSelectSql,false);
                ParseSQL(ref selectStatement);
                MainSelecteStatement.selects.Add(selectStatement);
            }
        }

        public System.Collections.Generic.List<SqlVarParameter> GetParameters(DB.Coding.DataUtility dataUtility, SqlStatementSelectInfo selectStatement, ref System.Collections.Generic.List<SqlVarParameter> allSqlVarParameters)
        {
            System.Collections.Generic.List<SqlVarParameter> sqlVarParameters = ParseVarName(selectStatement.sqlWithOutSubSelect);
            if (sqlVarParameters.Count > 0)
            {
                string sqlParametersSelect = "select ";
                bool hasValue = false;
                for (int i = 0; i < sqlVarParameters.Count; i++)
                {
                    if (sqlVarParameters[i].fullName != null)
                    {
                        hasValue = true;
                        if (i != 0)
                        {
                            sqlParametersSelect += ",";
                        }
                        sqlParametersSelect += sqlVarParameters[i].fullName;
                    }
                    else
                    {
                        SqlVarParameter parameter = new SqlVarParameter();
                        parameter.name = sqlVarParameters[i].name;
                        allSqlVarParameters.Add(parameter);
                    }
                }
                if (hasValue)
                {
                    sqlParametersSelect += " from " + selectStatement.fromClause + " where 1>2 ";
                    Regex fromReg = new Regex(@"\s+from\s+", RegexOptions.IgnoreCase);

                    System.Data.Common.DbCommand cmd = dataUtility.GetDbCommand(FormatSql(sqlParametersSelect), dataUtility.con);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    System.Data.Common.DbDataReader reader = null;
                    if (dataUtility.dbType == DB.DBType.SqlServer)
                    {
                        reader = cmd.ExecuteReader(System.Data.CommandBehavior.KeyInfo);
                    }
                    else
                    {
                        reader = cmd.ExecuteReader();

                    }
                    dt = reader.GetSchemaTable();
                    reader.Read();

                    Regex nameReg = new Regex(@"^[_0-9a-zA-Z]+$");
                    bool findColumn = false;
                    for (int i = 0; i < sqlVarParameters.Count;i++)
                    {
                        SqlVarParameter parameter = sqlVarParameters[i];
                        DB.Coding.Field field = new DB.Coding.Field();
                        field.name = dt.Rows[i]["BaseColumnName"].ToString();
                        field.structFieldName = dt.Rows[i]["ColumnName"].ToString();
                        field.length = Convert.ToInt32(dt.Rows[i]["ColumnSize"]);
                        field.position = Convert.ToInt32(dt.Rows[i]["ColumnOrdinal"]);
                        if (dt.Columns.Contains("IsIdentity"))
                        {
                            field.isId = Convert.ToBoolean(dt.Rows[i]["IsIdentity"] == DBNull.Value ? false : dt.Rows[i]["IsIdentity"]);
                        }
                        else
                        {
                            field.isId = Convert.ToBoolean(dt.Rows[i]["IsKey"] == DBNull.Value ? false : dt.Rows[i]["IsKey"]);
                        }
                        //查看名称是否合法,不合法则重命名.
                        if (!nameReg.IsMatch(field.name))
                        {
                            field.name = "_column" + i.ToString();
                        }
                        if (!nameReg.IsMatch(field.structFieldName))
                        {
                            field.structFieldName = "_column" + i.ToString();
                        }
                        field.allowNull = Convert.ToBoolean(dt.Rows[i]["AllowDBNull"]);
                        if (dataUtility.dbType == DB.DBType.Sqlite)
                        {
                            field.allowNull = Convert.ToBoolean(dt.Rows[i]["AllowDBNull"]);
                            field.sqlType = dt.Rows[i]["DataTypeName"].ToString().ToLower();
                            if (string.IsNullOrEmpty(field.sqlType))
                            {
                                field.dbTypeInt = Convert.ToInt32(dt.Rows[i]["ProviderType"]);
                                field.GetMethodConvert = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodConvert;
                                field.GetMethodName = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodName;
                                field.GetMethodValue = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodValue;
                                field.csharpType = dataUtility.dbTypeDic[field.dbTypeInt].csharpType;
                                field.isValueType = dataUtility.dbTypeDic[field.dbTypeInt].isValueType;
                                field.jsonType = dataUtility.dbTypeDic[field.dbTypeInt].jsonType;
                                field.sqlType = dataUtility.dbTypeDic[field.dbTypeInt].sqlType;
                                field.dbType = dataUtility.dbTypeDic[field.dbTypeInt].dbType;
                            }
                            else
                            {
                                field.GetMethodConvert = dataUtility.sqlTypeDic[field.sqlType].GetMethodConvert;
                                field.GetMethodName = dataUtility.sqlTypeDic[field.sqlType].GetMethodName;
                                field.GetMethodValue = dataUtility.sqlTypeDic[field.sqlType].GetMethodValue;
                                field.csharpType = dataUtility.sqlTypeDic[field.sqlType].csharpType;
                                field.isValueType = dataUtility.sqlTypeDic[field.sqlType].isValueType;
                                field.jsonType = dataUtility.sqlTypeDic[field.sqlType].jsonType;
                                field.dbTypeInt = dataUtility.sqlTypeDic[field.sqlType].dbTypeInt;
                                field.dbType = dataUtility.sqlTypeDic[field.sqlType].dbType;
                            }
                            //查找注释 
                            findColumn = false;
                            for (int j = 0; j < dataUtility.tables.Count; j++)
                            {
                                if (dataUtility.tables[j].name == dt.Rows[i]["BaseTableName"].ToString())
                                {
                                    for (int k = 0; k < dataUtility.tables[j].fields.Count; k++)
                                    {
                                        if (dataUtility.tables[j].fields[k].name == dt.Rows[i]["BaseColumnName"].ToString())
                                        {
                                            field.description = dataUtility.tables[j].fields[k].description;
                                            findColumn = true; break;
                                        }
                                    }
                                }
                                if (findColumn)
                                {
                                    break;
                                }
                            }
                        }
                        else if (dataUtility.dbType == DB.DBType.SqlServer)
                        {
                            field.allowNull = Convert.ToBoolean(dt.Rows[i]["AllowDBNull"]);
                            field.sqlType = dt.Rows[i]["DataTypeName"].ToString().ToLower();
                            if (field.sqlType.IndexOf('.') > -1)
                            {
                                field.sqlType = field.sqlType.Substring(field.sqlType.LastIndexOf('.') + 1);
                            }
                            field.GetMethodConvert = dataUtility.sqlTypeDic[field.sqlType].GetMethodConvert;
                            field.GetMethodName = dataUtility.sqlTypeDic[field.sqlType].GetMethodName;
                            field.GetMethodValue = dataUtility.sqlTypeDic[field.sqlType].GetMethodValue;
                            field.csharpType = dataUtility.sqlTypeDic[field.sqlType].csharpType;
                            field.isValueType = dataUtility.sqlTypeDic[field.sqlType].isValueType;
                            field.jsonType = dataUtility.sqlTypeDic[field.sqlType].jsonType;
                            field.dbTypeInt = dataUtility.sqlTypeDic[field.sqlType].dbTypeInt;
                            field.dbType = dataUtility.sqlTypeDic[field.sqlType].dbType;
                            //查找注释 
                            findColumn = false;
                            for (int j = 0; j < dataUtility.tables.Count; j++)
                            {
                                if (dataUtility.tables[j].name == dt.Rows[i]["BaseTableName"].ToString())
                                {
                                    for (int k = 0; k < dataUtility.tables[j].fields.Count; k++)
                                    {
                                        if (dataUtility.tables[j].fields[k].name == dt.Rows[i]["BaseColumnName"].ToString())
                                        {
                                            field.description = dataUtility.tables[j].fields[k].description;
                                            findColumn = true; break;
                                        }
                                    }
                                }
                                if (findColumn)
                                {
                                    break;
                                }
                            }
                        }
                        else if (dataUtility.dbType == DB.DBType.MySql)
                        {
                            field.allowNull = Convert.ToBoolean(dt.Rows[i]["AllowDBNull"]);
                            field.dbTypeInt = Convert.ToInt32(dt.Rows[i]["ProviderType"]);
                            field.GetMethodConvert = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodConvert;
                            field.GetMethodName = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodName;
                            field.GetMethodValue = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodValue;
                            field.csharpType = dataUtility.dbTypeDic[field.dbTypeInt].csharpType;
                            field.isValueType = dataUtility.dbTypeDic[field.dbTypeInt].isValueType;
                            field.jsonType = dataUtility.dbTypeDic[field.dbTypeInt].jsonType;
                            field.sqlType = dataUtility.dbTypeDic[field.dbTypeInt].sqlType;
                            field.dbType = dataUtility.dbTypeDic[field.dbTypeInt].dbType;
                            //查找注释 
                            findColumn = false;
                            for (int j = 0; j < dataUtility.tables.Count; j++)
                            {
                                if (dataUtility.tables[j].name == dt.Rows[i]["BaseTableName"].ToString())
                                {
                                    for (int k = 0; k < dataUtility.tables[j].fields.Count; k++)
                                    {
                                        if (dataUtility.tables[j].fields[k].name == dt.Rows[i]["BaseColumnName"].ToString())
                                        {
                                            field.description = dataUtility.tables[j].fields[k].description;
                                            findColumn = true; break;
                                        }
                                    }
                                }
                                if (findColumn)
                                {
                                    break;
                                }
                            }
                        }
                        else if (dataUtility.dbType == DB.DBType.PostgreSql)
                        {
                            field.allowNull = Convert.ToBoolean(dt.Rows[i]["AllowDBNull"]);
                            field.sqlType = dt.Rows[i]["ProviderType"].ToString();
                            if (string.IsNullOrEmpty(field.sqlType))
                            {
                                field.sqlType = "text";
                            }
                            field.GetMethodConvert = dataUtility.sqlTypeDic[field.sqlType].GetMethodConvert;
                            field.GetMethodName = dataUtility.sqlTypeDic[field.sqlType].GetMethodName;
                            field.GetMethodValue = dataUtility.sqlTypeDic[field.sqlType].GetMethodValue;
                            field.csharpType = dataUtility.sqlTypeDic[field.sqlType].csharpType;
                            field.isValueType = dataUtility.sqlTypeDic[field.sqlType].isValueType;
                            field.jsonType = dataUtility.sqlTypeDic[field.sqlType].jsonType;
                            field.dbTypeInt = dataUtility.sqlTypeDic[field.sqlType].dbTypeInt;
                            field.dbType = dataUtility.sqlTypeDic[field.sqlType].dbType;
                            //查找注释 
                            findColumn = false;
                            for (int j = 0; j < dataUtility.tables.Count; j++)
                            {
                                if (dataUtility.tables[j].name == dt.Rows[i]["BaseTableName"].ToString())
                                {
                                    for (int k = 0; k < dataUtility.tables[j].fields.Count; k++)
                                    {
                                        if (dataUtility.tables[j].fields[k].name == dt.Rows[i]["BaseColumnName"].ToString())
                                        {
                                            field.description = dataUtility.tables[j].fields[k].description;
                                            findColumn = true; break;
                                        }
                                    }
                                }
                                if (findColumn)
                                {
                                    break;
                                }
                            }
                        }
                        else if (dataUtility.dbType == DB.DBType.Oracle)
                        {
                            field.name = dt.Rows[i]["BaseColumnName"].ToString().ToLower();
                            field.structFieldName = dt.Rows[i]["ColumnName"].ToString().ToLower();
                            field.allowNull = Convert.ToBoolean(dt.Rows[i]["AllowDBNull"]);
                            field.dbTypeInt = Convert.ToInt32(dt.Rows[i]["ProviderType"]);
                            field.GetMethodConvert = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodConvert;
                            field.GetMethodName = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodName;
                            field.GetMethodValue = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodValue;
                            field.csharpType = dataUtility.dbTypeDic[field.dbTypeInt].csharpType;
                            field.isValueType = dataUtility.dbTypeDic[field.dbTypeInt].isValueType;
                            field.jsonType = dataUtility.dbTypeDic[field.dbTypeInt].jsonType;
                            field.sqlType = dataUtility.dbTypeDic[field.dbTypeInt].sqlType;
                            field.dbType = dataUtility.dbTypeDic[field.dbTypeInt].dbType;
                            //查找注释 
                            findColumn = false;
                            for (int j = 0; j < dataUtility.tables.Count; j++)
                            {
                                if (dataUtility.tables[j].name == dt.Rows[i]["BaseTableName"].ToString())
                                {
                                    for (int k = 0; k < dataUtility.tables[j].fields.Count; k++)
                                    {
                                        if (dataUtility.tables[j].fields[k].name == dt.Rows[i]["BaseColumnName"].ToString())
                                        {
                                            field.description = dataUtility.tables[j].fields[k].description;
                                            findColumn = true; break;
                                        }
                                    }
                                }
                                if (findColumn)
                                {
                                    break;
                                }
                            }
                        }

                        parameter.field = field;
                        allSqlVarParameters.Add(parameter);
                    }
                    dt.Dispose();
                    reader.Dispose();
                    cmd.Dispose();
                }
            }
            //分析子查询里的参数
            for (int i = 0; i < selectStatement.selects.Count; i++)
            {
                GetParameters(dataUtility, selectStatement.selects[i], ref allSqlVarParameters);
            }
            return sqlVarParameters;
        }
        public System.Collections.Generic.List<DB.Coding.Field> GetFields(DB.Coding.DataUtility dataUtility, SqlStatementSelectInfo selectStatement)
        {
            System.Collections.Generic.List<DB.Coding.Field> fields = new System.Collections.Generic.List<DB.Coding.Field>();
            string selectFiledsSql = string.Format("select {0} from {1} where 1>2", selectStatement.selectClause, selectStatement.fromClause);
            System.Data.Common.DbCommand cmd = dataUtility.GetDbCommand(FormatSql(selectFiledsSql), dataUtility.con);
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.Common.DbDataReader reader = null;
            int RowCount = 0;
            try
            {
                if (dataUtility.dbType == DB.DBType.SqlServer)
                {
                    System.Data.Common.DbDataReader readerForCount = cmd.ExecuteReader();
                    System.Data.DataTable dtForCount = readerForCount.GetSchemaTable();
                    RowCount = dtForCount.Rows.Count;
                    dt.Dispose();
                    readerForCount.Dispose();
                    reader = cmd.ExecuteReader(System.Data.CommandBehavior.KeyInfo);
                    dt = reader.GetSchemaTable();
                }
                else
                {
                    reader = cmd.ExecuteReader();
                    dt = reader.GetSchemaTable();
                    RowCount = dt.Rows.Count;
                }
            }
            catch(Exception ex)
            { 
               
            }
            
            reader.Read();

            Regex nameReg = new Regex(@"^[_0-9a-zA-Z]+$");
            bool findColumn = false;
            for (int i = 0; i < RowCount; i++)
            {
                DB.Coding.Field field = new DB.Coding.Field();
                field.name = dt.Rows[i]["BaseColumnName"].ToString();
                field.structFieldName = dt.Rows[i]["ColumnName"].ToString();
                field.length = Convert.ToInt32(dt.Rows[i]["ColumnSize"]);
                field.position = Convert.ToInt32(dt.Rows[i]["ColumnOrdinal"]);
                if (dt.Columns.Contains("IsIdentity"))
                {
                    field.isId = Convert.ToBoolean(dt.Rows[i]["IsIdentity"] == DBNull.Value ? false : dt.Rows[i]["IsIdentity"]);
                }
                else
                {
                    field.isId = Convert.ToBoolean(dt.Rows[i]["IsKey"] == DBNull.Value ? false : dt.Rows[i]["IsKey"]);
                }
                //查看名称是否合法,不合法则重命名.
                if (!nameReg.IsMatch(field.name))
                {
                    field.name = "_column" + i.ToString();
                }
                if (!nameReg.IsMatch(field.structFieldName))
                {
                    field.structFieldName = "_column" + i.ToString();
                }
                field.allowNull = Convert.ToBoolean(dt.Rows[i]["AllowDBNull"]);
                if (dataUtility.dbType == DB.DBType.Sqlite)
                {
                    field.allowNull = Convert.ToBoolean(dt.Rows[i]["AllowDBNull"]);
                    field.sqlType = dt.Rows[i]["DataTypeName"].ToString().ToLower();
                    if (string.IsNullOrEmpty(field.sqlType))
                    {
                        field.dbTypeInt =Convert.ToInt32(dt.Rows[i]["ProviderType"]);
                        field.GetMethodConvert = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodConvert;
                        field.GetMethodName = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodName;
                        field.GetMethodValue = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodValue;
                        field.csharpType = dataUtility.dbTypeDic[field.dbTypeInt].csharpType;
                        field.isValueType = dataUtility.dbTypeDic[field.dbTypeInt].isValueType;
                        field.jsonType = dataUtility.dbTypeDic[field.dbTypeInt].jsonType;
                        field.sqlType = dataUtility.dbTypeDic[field.dbTypeInt].sqlType;
                        field.dbType = dataUtility.dbTypeDic[field.dbTypeInt].dbType;
                    }
                    else
                    {
                        field.GetMethodConvert = dataUtility.sqlTypeDic[field.sqlType].GetMethodConvert;
                        field.GetMethodName = dataUtility.sqlTypeDic[field.sqlType].GetMethodName;
                        field.GetMethodValue = dataUtility.sqlTypeDic[field.sqlType].GetMethodValue;
                        field.csharpType = dataUtility.sqlTypeDic[field.sqlType].csharpType;
                        field.isValueType = dataUtility.sqlTypeDic[field.sqlType].isValueType;
                        field.jsonType = dataUtility.sqlTypeDic[field.sqlType].jsonType;
                        field.dbTypeInt = dataUtility.sqlTypeDic[field.sqlType].dbTypeInt;
                        field.dbType = dataUtility.sqlTypeDic[field.sqlType].dbType;
                    }
                    //查找注释 
                    findColumn = false;
                    for (int j = 0; j < dataUtility.tables.Count; j++)
                    {
                        if (dataUtility.tables[j].name == dt.Rows[i]["BaseTableName"].ToString())
                        {
                            for (int k = 0; k < dataUtility.tables[j].fields.Count; k++)
                            {
                                if (dataUtility.tables[j].fields[k].name == dt.Rows[i]["BaseColumnName"].ToString())
                                {
                                    field.description = dataUtility.tables[j].fields[k].description;
                                    findColumn = true; break;
                                }
                            }
                        }
                        if (findColumn)
                        {
                            break;
                        }
                    }
                }
                else if (dataUtility.dbType == DB.DBType.SqlServer)
                {
                    field.allowNull = Convert.ToBoolean(dt.Rows[i]["AllowDBNull"]);
                    field.sqlType = dt.Rows[i]["DataTypeName"].ToString().ToLower();
                    if (field.sqlType.IndexOf('.') > -1)
                    {
                        field.sqlType = field.sqlType.Substring(field.sqlType.LastIndexOf('.') + 1);
                    }
                    field.GetMethodConvert = dataUtility.sqlTypeDic[field.sqlType].GetMethodConvert;
                    field.GetMethodName = dataUtility.sqlTypeDic[field.sqlType].GetMethodName;
                    field.GetMethodValue = dataUtility.sqlTypeDic[field.sqlType].GetMethodValue;
                    field.csharpType = dataUtility.sqlTypeDic[field.sqlType].csharpType;
                    field.isValueType = dataUtility.sqlTypeDic[field.sqlType].isValueType;
                    field.jsonType = dataUtility.sqlTypeDic[field.sqlType].jsonType;
                    field.dbTypeInt = dataUtility.sqlTypeDic[field.sqlType].dbTypeInt;
                    field.dbType = dataUtility.sqlTypeDic[field.sqlType].dbType;
                    //查找注释 
                    findColumn = false;
                    for (int j = 0; j < dataUtility.tables.Count; j++)
                    {
                        if (dataUtility.tables[j].name == dt.Rows[i]["BaseTableName"].ToString())
                        {
                            for (int k = 0; k < dataUtility.tables[j].fields.Count; k++)
                            {
                                if (dataUtility.tables[j].fields[k].name == dt.Rows[i]["BaseColumnName"].ToString())
                                {
                                    field.description = dataUtility.tables[j].fields[k].description;
                                    findColumn = true; break;
                                }
                            }
                        }
                        if (findColumn)
                        {
                            break;
                        }
                    }
                }
                else if (dataUtility.dbType == DB.DBType.MySql)
                {
                    field.allowNull = Convert.ToBoolean(dt.Rows[i]["AllowDBNull"]);
                    field.dbTypeInt = Convert.ToInt32(dt.Rows[i]["ProviderType"]);
                    field.GetMethodConvert = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodConvert;
                    field.GetMethodName = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodName;
                    field.GetMethodValue = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodValue;
                    field.csharpType = dataUtility.dbTypeDic[field.dbTypeInt].csharpType;
                    field.isValueType = dataUtility.dbTypeDic[field.dbTypeInt].isValueType;
                    field.jsonType = dataUtility.dbTypeDic[field.dbTypeInt].jsonType;
                    field.sqlType = dataUtility.dbTypeDic[field.dbTypeInt].sqlType;
                    field.dbType = dataUtility.dbTypeDic[field.dbTypeInt].dbType;
                    //查找注释 
                    findColumn = false;
                    for (int j = 0; j < dataUtility.tables.Count; j++)
                    {
                        if (dataUtility.tables[j].name == dt.Rows[i]["BaseTableName"].ToString())
                        {
                            for (int k = 0; k < dataUtility.tables[j].fields.Count; k++)
                            {
                                if (dataUtility.tables[j].fields[k].name == dt.Rows[i]["BaseColumnName"].ToString())
                                {
                                    field.description = dataUtility.tables[j].fields[k].description;
                                    findColumn = true; break;
                                }
                            }
                        }
                        if (findColumn)
                        {
                            break;
                        }
                    }
                }
                else if (dataUtility.dbType == DB.DBType.PostgreSql)
                {
                    field.allowNull = Convert.ToBoolean(dt.Rows[i]["AllowDBNull"]);
                    field.sqlType = dt.Rows[i]["ProviderType"].ToString();
                    if (string.IsNullOrEmpty(field.sqlType))
                    {
                        field.sqlType = "text";
                    }
                    field.GetMethodConvert = dataUtility.sqlTypeDic[field.sqlType].GetMethodConvert;
                    field.GetMethodName = dataUtility.sqlTypeDic[field.sqlType].GetMethodName;
                    field.GetMethodValue = dataUtility.sqlTypeDic[field.sqlType].GetMethodValue;
                    field.csharpType = dataUtility.sqlTypeDic[field.sqlType].csharpType;
                    field.isValueType = dataUtility.sqlTypeDic[field.sqlType].isValueType;
                    field.jsonType = dataUtility.sqlTypeDic[field.sqlType].jsonType;
                    field.dbTypeInt = dataUtility.sqlTypeDic[field.sqlType].dbTypeInt;
                    field.dbType = dataUtility.sqlTypeDic[field.sqlType].dbType;
                    //查找注释 
                    findColumn = false;
                    for (int j = 0; j < dataUtility.tables.Count; j++)
                    {
                        if (dataUtility.tables[j].name == dt.Rows[i]["BaseTableName"].ToString())
                        {
                            for (int k = 0; k < dataUtility.tables[j].fields.Count; k++)
                            {
                                if (dataUtility.tables[j].fields[k].name == dt.Rows[i]["BaseColumnName"].ToString())
                                {
                                    field.description = dataUtility.tables[j].fields[k].description;
                                    findColumn = true; break;
                                }
                            }
                        }
                        if (findColumn)
                        {
                            break;
                        }
                    }
                }
                else if (dataUtility.dbType == DB.DBType.Oracle)
                {
                    field.name = dt.Rows[i]["BaseColumnName"].ToString().ToLower();
                    field.structFieldName = dt.Rows[i]["ColumnName"].ToString().ToLower();
                    field.allowNull = Convert.ToBoolean(dt.Rows[i]["AllowDBNull"]);
                    field.dbTypeInt = Convert.ToInt32(dt.Rows[i]["ProviderType"]);
                    field.GetMethodConvert = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodConvert;
                    field.GetMethodName = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodName;
                    field.GetMethodValue = dataUtility.dbTypeDic[field.dbTypeInt].GetMethodValue;
                    field.csharpType = dataUtility.dbTypeDic[field.dbTypeInt].csharpType;
                    field.isValueType = dataUtility.dbTypeDic[field.dbTypeInt].isValueType;
                    field.jsonType = dataUtility.dbTypeDic[field.dbTypeInt].jsonType;
                    field.sqlType = dataUtility.dbTypeDic[field.dbTypeInt].sqlType;
                    field.dbType = dataUtility.dbTypeDic[field.dbTypeInt].dbType;
                    //查找注释 
                    findColumn = false;
                    for (int j = 0; j < dataUtility.tables.Count; j++)
                    {
                        if (dataUtility.tables[j].name == dt.Rows[i]["BaseTableName"].ToString())
                        {
                            for (int k = 0; k < dataUtility.tables[j].fields.Count; k++)
                            {
                                if (dataUtility.tables[j].fields[k].name == dt.Rows[i]["BaseColumnName"].ToString())
                                {
                                    field.description = dataUtility.tables[j].fields[k].description;
                                    findColumn = true; break;
                                }
                            }
                        }
                        if (findColumn)
                        {
                            break;
                        }
                    }
                }
                fields.Add(field);
            }
            dt.Dispose();
            reader.Dispose();
            cmd.Dispose();
            return fields;
        }
        /// <summary>
        /// 复杂SQL解析,解析出所有的列
        /// </summary>
        /// <param name="functionData"></param>
        /// <returns></returns>
        public DbFunctionData GetFunctionData(DbFunctionData functionData)
        {

            dataUtility.con.Open();
            SqlStatementSelectInfo selectStatement = new SqlStatementSelectInfo(sqlInfo.sql,true);
            functionData.sql = selectStatement.selectSql;
            //把执行的sql算出来，包含order by @columnName的转换成ordery by id
            ParseSQL(ref selectStatement);
            functionData.fields = GetFields(dataUtility, selectStatement);
            functionData.sqlVarParameters = new System.Collections.Generic.List<SqlVarParameter>();
            GetParameters(dataUtility, selectStatement, ref functionData.sqlVarParameters);
            dataUtility.con.Close();
            //Models.Entity.tableName
            //var id=con.Insert("tableName",modelName);
            if (!string.IsNullOrEmpty(functionData.modelName))
            {
                SqlVarParameter sqlVarParameter = null;
                if (functionData.functionName == "Insert")
                {
                    StringBuilder insertSql = new StringBuilder();
                    insertSql.Append("insert into ");
                    insertSql.Append(functionData.tableName);
                    insertSql.Append("(");
                    bool isFirst = true;
                    for (int i = 0; i < functionData.fields.Count; i++)
                    {
                        if (functionData.fields[i].name.ToLower() != "id")
                        {
                            if (!isFirst)
                            {
                                insertSql.Append(",");
                            }
                            else
                            {
                                isFirst = false;
                            }
                            insertSql.Append(functionData.fields[i].name);
                        }
                    }
                    insertSql.Append(")");
                    insertSql.Append(" values(");
                    isFirst = true;
                    for (int i = 0; i < functionData.fields.Count; i++)
                    {
                        if (functionData.fields[i].name.ToLower()!="id")
                        {
                            sqlVarParameter = new SqlVarParameter();
                            sqlVarParameter.name = functionData.modelName.Replace('.', '_') + '_' + functionData.fields[i].name;
                            sqlVarParameter.csharpName = functionData.modelName+"."+ functionData.fields[i].name;
                            sqlVarParameter.field = functionData.fields[i];
                            functionData.sqlVarParameters.Add(sqlVarParameter);
                            if (!isFirst)
                            {
                                insertSql.Append(",");
                            }
                            else
                            {
                                isFirst = false;
                            }
                            insertSql.Append("@");
                            insertSql.Append(sqlVarParameter.name);
                        }
                    }
                    insertSql.Append(")");
                    functionData.sql = insertSql.ToString();
                }
                else if (functionData.functionName == "Update")
                {
                    StringBuilder updateSql = new StringBuilder();
                    updateSql.Append("update ");
                    updateSql.Append(functionData.tableName);
                    updateSql.Append(" set ");
                    bool isFirst = true;
                    for (int i = 0; i < functionData.fields.Count; i++)
                    {
                        sqlVarParameter = new SqlVarParameter();
                        sqlVarParameter.name = functionData.modelName.Replace('.', '_') + '_' + functionData.fields[i].name;
                        sqlVarParameter.csharpName = functionData.modelName+"."+ functionData.fields[i].name;
                        sqlVarParameter.field = functionData.fields[i];
                        functionData.sqlVarParameters.Add(sqlVarParameter);
                        if (functionData.fields[i].name.ToLower() != "id")
                        {
                            if (!isFirst)
                            {
                                updateSql.Append(',');
                            }
                            else
                            {
                                isFirst = false;
                            }
                            updateSql.Append(functionData.fields[i].name);
                            updateSql.Append("=@");
                            updateSql.Append(sqlVarParameter.name);
                        }
                    }
                    updateSql.Append(" where id=@"+ functionData.modelName.Replace('.', '_') + "_id");
                    functionData.sql = updateSql.ToString();
                }
            }
            //转换为可执行的SQL
            functionData.sql = FormatSql(functionData.sql);
            return functionData;
        }
        public System.Collections.Generic.List<SqlColumn> getColumns()
        {
            //System.Data.Common.DbDataAdapter dataAdapter= dataUtility.GetDataAdapter(FormatSql(sqlInfo.sql),dataUtility.con);
            dataUtility.con.Open();
            System.Data.Common.DbCommand cmd = new System.Data.SQLite.SQLiteCommand(FormatSql(sqlInfo.sql), (System.Data.SQLite.SQLiteConnection)dataUtility.con);
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Collections.Generic.List<SqlColumn> columns = new System.Collections.Generic.List<SqlColumn>();
            System.Data.Common.DbDataReader reader = cmd.ExecuteReader();
            dt = reader.GetSchemaTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SqlColumn column = new SqlColumn();
                column.name = dt.Rows[i]["BaseColumnName"].ToString();
                column.asName = dt.Rows[i]["ColumnName"].ToString();
                column.tableName = dt.Rows[i]["BaseTableName"].ToString();
                column.returnType = dt.Rows[i]["DataType"].ToString();
                columns.Add(column);
            }
            dt.Dispose();
            reader.Dispose();
            cmd.Dispose();
            dataUtility.con.Close();
            return columns;
        }
        /// <summary>
        /// 从SQL语句中获取表信息
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>表信息</returns>
        public System.Collections.Generic.List<SqlTable> ParseTable(string sql)
        {
            System.Collections.Generic.List<SqlTable> tables = new System.Collections.Generic.List<SqlTable>();
            string[] tableSqls = sql.Split(',');
            Regex tableStatementReg = new Regex(tableStatement, RegexOptions.IgnoreCase);
            Match tableStatementM = null;

            for (int i = 0; i < tableSqls.Length; i++)
            {
                SqlTable tab = new SqlTable();

                tableStatementM = tableStatementReg.Match(tableSqls[i]);
                if (tableStatementM.Success)
                {
                    //前面变量相关的字符串
                    tab.sql = tableStatementM.Groups[1].Value;
                    //asName相关的字符串
                    tab.asName = tableStatementM.Groups[2].Value;
                    tab = GetTable(tab.sql);
                }
                tables.Add(tab);
            }
            return tables;
        }
    }
}