//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :SqlCompiler.cs
//        Description :csharp魔法函数语句分析和编译类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;

namespace NFinal.Compile
{

    /// <summary>
    /// csharp魔法函数语句分析和编译类
    /// </summary>
    public class SqlFunctionCompiler
    {
        private static Regex dbFuncitonRegex = new Regex(@"(?:(\S+)\s+)?(\S+)\s*=\s*Models\s*\.\s*([^\s\.]+)\s*\.\s*([^\s\.]+)\s*\(\s*(?:@?""([^""]*)""(?:,\s*([^\s,\)]+))?)?\s*\)(?:\s*\.\s*([^\(\)\s;\.]+)\s*\(\s*\))?\s*;");
        public SqlFunctionCompiler()
        {
        }
        /// <summary>
        /// 分析代码
        /// </summary>
        /// <param name="csharpCode">代码</param>
        /// <param name="index">代码开始的位置</param>
        /// <returns></returns>
        public System.Collections.Generic.List<DbFunctionData> Compile(string csharpCode)
        {
            //找出所有的Connection对象
            System.Collections.Generic.List<SqlConnection> sqlConnectionList = SqlConnection.GetSqlConnectionList(csharpCode);
            //找出所有的Trasaction对象
            System.Collections.Generic.List<SqlTransaction> sqlTransactionList = SqlTransaction.GetSqlTransactionList(csharpCode, sqlConnectionList);
            #region 找出所有的Connection与Trasaction执行的sql语句
            System.Collections.Generic.List<DbFunctionData> dbFunctionDataList = new System.Collections.Generic.List<DbFunctionData>();

            DbFunctionData dbFunctionData;
            string dbFunctionDataRegexStr= @"(?://([^\r\n]*?[\r\n]+[^\r\n\S]*))?(?:([^{};\s/]+)\s+)?(\S+)\s*=\s*([_a-zA-Z0-9]+)\s*\.\s*(QueryRandom|QueryAll|QueryTop|Page|QueryRow|QueryObject|Insert|Update|Delete|ExecuteNonQuery)\s*(?:<\s*(\S+)\s*>\s*)?\(\s*(\$)?""([^""]+)""(?:\s*,\s*([^\s,\)]+))?\s*\)\s*(\.\s*(ToByte|ToSByte|ToChar|ToDateTime|ToDecimal|ToDouble|ToSingle|ToBoolean|ToInt16|ToInt32|ToInt64|ToUInt16|ToUInt32|ToUInt64)\s*\(\s*\)\s*)?;";
            Regex dbFunctionDataRegex = new Regex(dbFunctionDataRegexStr);
            MatchCollection dbFunctionDataMac = dbFunctionDataRegex.Matches(csharpCode);
            for (int i = 0; i < dbFunctionDataMac.Count; i++)
            {
                if(dbFunctionDataMac[i].Success)
                {
                    dbFunctionData = new DbFunctionData();
                    dbFunctionData.expression = dbFunctionDataMac[i].Groups[0].Value;
                    dbFunctionData.varCommit = dbFunctionDataMac[i].Groups[1].Value;
                    dbFunctionData.type = dbFunctionDataMac[i].Groups[2].Value;
                    if (dbFunctionDataMac[i].Groups[2].Success)
                    {
                        dbFunctionData.isDeclaration = true;
                    }
                    dbFunctionData.varName = dbFunctionDataMac[i].Groups[3].Value;
                    dbFunctionData.connectionVarName = dbFunctionDataMac[i].Groups[4].Value;
                    for (int j = 0; j < sqlConnectionList.Count; j++)
                    {
                        if (sqlConnectionList[j].varName == dbFunctionData.connectionVarName)
                        {
                            dbFunctionData.connectionName = sqlConnectionList[j].connectionName;
                            dbFunctionData.connectionParameterName = sqlConnectionList[j].parName;
                            dbFunctionData.isTransaction = false;
                            break;
                        }
                    }
                    dbFunctionData.transactionVarName = dbFunctionDataMac[i].Groups[4].Value;
                    for (int j = 0; j < sqlTransactionList.Count; j++)
                    {
                        if (sqlTransactionList[j].varName == dbFunctionData.transactionVarName)
                        {
                            dbFunctionData.connectionVarName = sqlConnectionList[j].varName;
                            dbFunctionData.connectionName = sqlConnectionList[j].connectionName;
                            dbFunctionData.connectionParameterName = sqlConnectionList[j].parName;
                            dbFunctionData.isTransaction = true;
                            break;
                        }
                    }
                    dbFunctionData.functionName = dbFunctionDataMac[i].Groups[5].Value;
                    dbFunctionData.hasGenericType = dbFunctionDataMac[i].Groups[6].Success;
                    if (dbFunctionData.hasGenericType)
                    {
                        dbFunctionData.type = dbFunctionDataMac[i].Groups[6].Value;
                    }
                    dbFunctionData.isSuperString = dbFunctionDataMac[i].Groups[7].Success;
                    if (dbFunctionData.isSuperString)
                    {
                        dbFunctionData.sql = dbFunctionDataMac[i].Groups[8].Value.Replace("{","").Replace("}","");
                    }
                    else
                    {
                       dbFunctionData.sql = dbFunctionDataMac[i].Groups[8].Value;
                    }
                    if (dbFunctionData.functionName == "QueryAll" || dbFunctionData.functionName == "QueryTop" || dbFunctionData.functionName == "Page" || dbFunctionData.functionName == "QueryRandom" || dbFunctionData.functionName == "QueryRow")
                    {
                        //如果找不到select,该字符串就是tableName
                        if (dbFunctionData.sql.ToLower().IndexOf("select") < 0)
                        {
                            dbFunctionData.sql = string.Format("select * from {0}", dbFunctionData.sql);
                        }
                    }
                    if (dbFunctionDataMac[i].Groups[9].Success)
                    {
                        dbFunctionData.parameters = new string[2];
                        //select sql
                        dbFunctionData.parameters[0] = dbFunctionDataMac[i].Groups[8].Value;
                        //pageSize
                        dbFunctionData.parameters[1] = dbFunctionDataMac[i].Groups[9].Value;
                        if (dbFunctionData.functionName == "Insert" || dbFunctionData.functionName == "Update")
                        {
                            dbFunctionData.tableName = dbFunctionData.parameters[0];
                            dbFunctionData.modelName = dbFunctionData.parameters[1];
                            dbFunctionData.sql = string.Format("select * from {0}",dbFunctionData.tableName);
                        }
                    }
                    else
                    {
                        dbFunctionData.parameters = new string[1];
                        dbFunctionData.parameters[0] = dbFunctionDataMac[i].Groups[8].Value;
                    }
                    dbFunctionData.convertMethodName = dbFunctionDataMac[i].Groups[10].Value;
                    dbFunctionData.index = dbFunctionDataMac[i].Index;
                    dbFunctionData.length = dbFunctionDataMac[i].Length;
                    //如果没有标识类型，则默认填充类型
                    if (!dbFunctionData.hasGenericType)
                    {
                        if (dbFunctionData.functionName == "ExecuteNonQuery")
                        {
                            dbFunctionData.type = "int";
                        }
                        else if (dbFunctionData.functionName == "Insert")
                        {
                            dbFunctionData.type = "int";
                        }
                        else if (dbFunctionData.functionName == "QueryObject")
                        {
                            dbFunctionData.type = "object";
                        }
                        else if (dbFunctionData.functionName == "Delete")
                        {
                            dbFunctionData.type = "int";
                        }
                        else if (dbFunctionData.functionName == "Update")
                        {
                            dbFunctionData.type = "int";
                        }
                    }
                    dbFunctionDataList.Add(dbFunctionData);
                }
            }
            #endregion
            return dbFunctionDataList;
        }
        //在某段位置替换成另一段代码
        public int Replace(ref string str, int index, int length, string rep)
        {
            if (length > 0)
            {
                str = str.Remove(index, length);
            }
            if (index > 0)
            {
                str = str.Insert(index, rep);
            }
            return rep.Length - length;
        }
        private DB.ConnectionString GetConnectionString(string name)
        {
            if (Frame.ConnectionStrings.Count > 0)
            {
                for (int i = 0; i < Frame.ConnectionStrings.Count; i++)
                {
                    if (Frame.ConnectionStrings[i].name == name)
                    {
                        return Frame.ConnectionStrings[i];
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        public int SetMagicConnection(string methodName, ref string csharpCode, string appRoot)
        {
            if (methodName == "queryAll")
            {

            }
            System.Collections.Generic.List<SqlConnection> sqlConnectionList = SqlConnection.GetSqlConnectionList(csharpCode);
            DB.ConnectionString connectionString = null;
            //byte[] fileString = null;
            //RazorGenerator.Templating.RazorTemplateBase template = null;
            //JinianNet.JNTemplate.ITemplate template = null;
            string conCode = null;
            string dbName = null;
            int relative_position = 0;
            for (int i=0;i<sqlConnectionList.Count;i++)
            {
                connectionString = GetConnectionString(sqlConnectionList[i].connectionName);
                if (sqlConnectionList[i].isGet)
                {
                    //conCode = string.Empty;
                    if (connectionString.type == DB.DBType.MySql)
                    {
                        conCode = string.Format("var {0}=(MySql.Data.MySqlClient.MySqlConnection){1};", sqlConnectionList[i].varName, sqlConnectionList[i].parName);
                    }
                    else if (connectionString.type == DB.DBType.Oracle)
                    {
                        conCode = string.Format("var {0}=(Oracle.ManagedDataAccess.Client.OracleConnection){1};", sqlConnectionList[i].varName, sqlConnectionList[i].parName);
                    }
                    else if (connectionString.type == DB.DBType.Sqlite)
                    {
                        conCode = string.Format("var {0}=(System.Data.SQLite.SQLiteConnection){1};", sqlConnectionList[i].varName, sqlConnectionList[i].parName);
                    }
                    else if (connectionString.type == DB.DBType.SqlServer)
                    {
                        conCode = string.Format("var {0}=(System.Data.SqlClient.SqlConnection){1};", sqlConnectionList[i].varName, sqlConnectionList[i].parName);
                    }
                    else if (connectionString.type == DB.DBType.PostgreSql)
                    {
                        conCode = string.Format("var {0}=(Npgsql.NpgsqlConnection){1};", sqlConnectionList[i].varName, sqlConnectionList[i].parName);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(sqlConnectionList[i].parName))
                    {
                        dbName = "\"" + sqlConnectionList[i].connectionName + "\"";
                    }
                    else
                    {
                        dbName = sqlConnectionList[i].parName;
                    }
                    var OpenModel = new NFinal.Compile.SqlTemplate.Model.Open
                    {
                        functionName = methodName,
                        connectionVarName = sqlConnectionList[i].varName,
                        dbName = dbName
                    };
                    if (connectionString.type == DB.DBType.MySql)
                    {
                        //fileString = NFinal.Compile.SqlTemplate.mysql.mysql.Open;
                        var template =new NFinal.Compile.SqlTemplate.mysql.Open();
                        template.Model = OpenModel;
                        conCode= template.TransformText();
                    }
                    else if (connectionString.type == DB.DBType.Oracle)
                    {
                        //fileString = NFinal.Compile.SqlTemplate.oracle.oracle.Open;
                        var template = new NFinal.Compile.SqlTemplate.oracle.Open();
                        template.Model = OpenModel;
                        conCode = template.TransformText();
                    }
                    else if (connectionString.type == DB.DBType.Sqlite)
                    {
                        //fileString = NFinal.Compile.SqlTemplate.sqlite.sqlite.Open;
                        var template = new NFinal.Compile.SqlTemplate.sqlite.Open();
                        template.Model = OpenModel;
                        conCode = template.TransformText();
                    }
                    else if (connectionString.type == DB.DBType.SqlServer)
                    {
                        //fileString = NFinal.Compile.SqlTemplate.sqlserver.sqlserver.Open;
                        var template = new NFinal.Compile.SqlTemplate.sqlserver.Open();
                        template.Model = OpenModel;
                        conCode = template.TransformText();
                    }
                    else if(connectionString.type==DB.DBType.PostgreSql)
                    {
                        //fileString = NFinal.Compile.SqlTemplate.postgresql.postgresql.Open;
                        var template = new NFinal.Compile.SqlTemplate.postgresql.Open();
                        template.Model = OpenModel;
                        conCode = template.TransformText();
                    }
                    //template = new JinianNet.JNTemplate.Template(System.Text.Encoding.UTF8.GetString(fileString));
                    //template.Context.TempData["functionName"] = methodName;
                    //template.Context.TempData["connectionVarName"] = sqlConnectionList[i].varName;
                    //if (string.IsNullOrEmpty(sqlConnectionList[i].parName))
                    //{
                    //    template.Context.TempData["dbName"] = "\""+sqlConnectionList[i].connectionName+"\"";
                    //}
                    //else
                    //{
                    //    template.Context.TempData["dbName"] = sqlConnectionList[i].parName;
                    //}
                    //conCode = template.Render();
                    //conCode = RazorEngine.Engine.Razor.RunCompile(
                    //            System.Text.Encoding.UTF8.GetString(fileString),
                    //            string.Format("/SqlTemplate/{0}/Open.sql", connectionString.type),
                    //            OpenModel.GetType(),
                    //            OpenModel);
                }
                relative_position += Replace(ref csharpCode, sqlConnectionList[i].index + relative_position, sqlConnectionList[i].length, conCode);
            }
            return relative_position;
        }
        /// <summary>
        /// 执行数据库魔法函数
        /// </summary>
        /// <param name="methodName">Controller的函数名</param>
        /// <param name="csharpFileCode">Controller的函数内的代码</param>
        /// <param name="dbFunctionDatas">代码内分析出的魔法函数</param>
        /// <param name="appRoot">项目的根目录</param>
        /// <returns></returns>
        public int SetMagicFunction(string methodName, ref string csharpFileCode, int relative_position, System.Collections.Generic.List<DbFunctionData> dbFunctionDatas,string appRoot)
        {
            if(dbFunctionDatas.Count>0)
            {
                string webCsharpCode = "";
                //VTemplate.Engine.TemplateDocument doc=null;
                //JinianNet.JNTemplate.ITemplate tempalte = null;
                DB.ConnectionString connectionString=null;
                //string fileName = null;
 
                for (int i = 0; i < dbFunctionDatas.Count; i++)
                {
                    connectionString= GetConnectionString(dbFunctionDatas[i].connectionName);
                    if(connectionString!=null)
                    {
                        if (dbFunctionDatas[i].functionName == "ExecuteNonQuery")
                        {
                            var ExecuteNonQueryModel = new NFinal.Compile.SqlTemplate.Model.ExecuteNonQuery
                            {
                                functionName = methodName,
                                varName = dbFunctionDatas[i].varName,
                                hasGenericType = dbFunctionDatas[i].hasGenericType,
                                type = dbFunctionDatas[i].type,
                                isDeclaration = dbFunctionDatas[i].isDeclaration,
                                connectionVarName = dbFunctionDatas[i].connectionVarName,
                                isTransaction = dbFunctionDatas[i].isTransaction,
                                transactionVarName = dbFunctionDatas[i].transactionVarName,
                                dbName = dbFunctionDatas[i].connectionName,
                                sql = dbFunctionDatas[i].sql,
                                fields = dbFunctionDatas[i].fields,
                                sqlVarParameters = dbFunctionDatas[i].sqlVarParameters
                            };
                            //byte[] fileString = null;
                            if (connectionString.type == DB.DBType.MySql)
                            {
                                //fileString =NFinal.Compile.SqlTemplate.mysql.mysql.ExecuteNonQuery;
                                var template = new NFinal.Compile.SqlTemplate.mysql.ExecuteNonQuery();
                                template.Model = ExecuteNonQueryModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if(connectionString.type ==DB.DBType.Oracle)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.oracle.oracle.ExecuteNonQuery;
                                var template = new NFinal.Compile.SqlTemplate.oracle.ExecuteNonQuery();
                                template.Model = ExecuteNonQueryModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Sqlite)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlite.sqlite.ExecuteNonQuery;
                                var template = new NFinal.Compile.SqlTemplate.sqlite.ExecuteNonQuery();
                                template.Model = ExecuteNonQueryModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.SqlServer)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlserver.sqlserver.ExecuteNonQuery;
                                var template = new NFinal.Compile.SqlTemplate.sqlserver.ExecuteNonQuery();
                                template.Model = ExecuteNonQueryModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if(connectionString.type == DB.DBType.PostgreSql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.postgresql.postgresql.ExecuteNonQuery;
                                var template = new NFinal.Compile.SqlTemplate.postgresql.ExecuteNonQuery();
                                template.Model = ExecuteNonQueryModel;
                                webCsharpCode = template.TransformText();
                            }
                            
                            //webCsharpCode = RazorEngine.Engine.Razor.RunCompile(
                            //    System.Text.Encoding.UTF8.GetString(fileString),
                            //    string.Format("/SqlTemplate/{0}/ExecuteNonQuery.sql", connectionString.type),
                            //    ExecuteNonQueryModel.GetType(),
                            //    ExecuteNonQueryModel);

                            //tempalte = new JinianNet.JNTemplate.Template(fileString);
                            //tempalte.Context.TempData["functionName"] = methodName;
                            //tempalte.Context.TempData["varName"] = dbFunctionDatas[i].varName;
                            //tempalte.Context.TempData["hasGenericType"] = dbFunctionDatas[i].hasGenericType;
                            //tempalte.Context.TempData["type"] = dbFunctionDatas[i].type;
                            //tempalte.Context.TempData["isDeclaration"] = dbFunctionDatas[i].isDeclaration;
                            //tempalte.Context.TempData["connectionVarName"] = dbFunctionDatas[i].connectionVarName;
                            //tempalte.Context.TempData["isTransaction"] = dbFunctionDatas[i].isTransaction;
                            //tempalte.Context.TempData["transactionVarName"] = dbFunctionDatas[i].transactionVarName;
                            //tempalte.Context.TempData["dbName"] = dbFunctionDatas[i].connectionName;
                            //tempalte.Context.TempData["sql"] = dbFunctionDatas[i].sql;
                            //tempalte.Context.TempData["fields"] = dbFunctionDatas[i].fields;
                            //tempalte.Context.TempData["sqlVarParameters"] = dbFunctionDatas[i].sqlVarParameters;
                            //webCsharpCode = tempalte.Render();
                            relative_position += Replace(ref csharpFileCode,
                                relative_position + dbFunctionDatas[i].index,
                                dbFunctionDatas[i].length,
                                webCsharpCode);
                        }
                        if (dbFunctionDatas[i].functionName == "QueryAll")
                        {
                            var QueryAllModel = new NFinal.Compile.SqlTemplate.Model.QueryAll
                            {
                                functionName = methodName,
                                varName = dbFunctionDatas[i].varName,
                                hasGenericType = dbFunctionDatas[i].hasGenericType,
                                type = dbFunctionDatas[i].type,
                                isDeclaration = dbFunctionDatas[i].isDeclaration,
                                connectionVarName = dbFunctionDatas[i].connectionVarName,
                                isTransaction = dbFunctionDatas[i].isTransaction,
                                transactionVarName = dbFunctionDatas[i].transactionVarName,
                                dbName = dbFunctionDatas[i].connectionName,
                                sql = dbFunctionDatas[i].sql,
                                fields = dbFunctionDatas[i].fields,
                                sqlVarParameters = dbFunctionDatas[i].sqlVarParameters
                            };
                            //byte[] fileString = null;
                            if (connectionString.type == DB.DBType.MySql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.mysql.mysql.QueryAll;
                                var template = new NFinal.Compile.SqlTemplate.mysql.QueryAll();
                                template.Model = QueryAllModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Oracle)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.oracle.oracle.QueryAll;
                                var template = new NFinal.Compile.SqlTemplate.oracle.QueryAll();
                                template.Model = QueryAllModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Sqlite)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlite.sqlite.QueryAll;
                                var template = new NFinal.Compile.SqlTemplate.sqlite.QueryAll();
                                template.Model = QueryAllModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.SqlServer)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlserver.sqlserver.QueryAll;
                                var template = new NFinal.Compile.SqlTemplate.sqlserver.QueryAll();
                                template.Model = QueryAllModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if(connectionString.type == DB.DBType.PostgreSql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.postgresql.postgresql.QueryAll;
                                var template = new NFinal.Compile.SqlTemplate.postgresql.QueryAll();
                                template.Model = QueryAllModel;
                                webCsharpCode = template.TransformText();
                            }
                            
                            //webCsharpCode = RazorEngine.Engine.Razor.RunCompile(
                            //    System.Text.Encoding.UTF8.GetString(fileString),
                            //    string.Format("/SqlTemplate/{0}/QueryAll.sql", connectionString.type),
                            //    QueryAllModel.GetType(),
                            //    QueryAllModel);

                            //tempalte = new JinianNet.JNTemplate.Template(fileString);
                            //tempalte.Context.TempData["functionName"] = methodName;
                            //tempalte.Context.TempData["varName"] = dbFunctionDatas[i].varName;
                            //tempalte.Context.TempData["hasGenericType"] = dbFunctionDatas[i].hasGenericType;
                            //tempalte.Context.TempData["type"] = dbFunctionDatas[i].type;
                            //tempalte.Context.TempData["isDeclaration"] = dbFunctionDatas[i].isDeclaration;
                            //tempalte.Context.TempData["connectionVarName"] = dbFunctionDatas[i].connectionVarName;
                            //tempalte.Context.TempData["isTransaction"] = dbFunctionDatas[i].isTransaction;
                            //tempalte.Context.TempData["transactionVarName"] = dbFunctionDatas[i].transactionVarName;
                            //tempalte.Context.TempData["dbName"] = dbFunctionDatas[i].connectionName;
                            //tempalte.Context.TempData["sql"] = dbFunctionDatas[i].sql;
                            //tempalte.Context.TempData["fields"] = dbFunctionDatas[i].fields;
                            //tempalte.Context.TempData["sqlVarParameters"] = dbFunctionDatas[i].sqlVarParameters;
                            //webCsharpCode = tempalte.Render();
                            relative_position += Replace(ref csharpFileCode,
                                relative_position + dbFunctionDatas[i].index,
                                dbFunctionDatas[i].length,
                                webCsharpCode);
                        }
                        if (dbFunctionDatas[i].functionName == "QueryRow")
                        {
                            var QueryRowModel = new NFinal.Compile.SqlTemplate.Model.QueryRow
                            {
                                functionName = methodName,
                                varName = dbFunctionDatas[i].varName,
                                hasGenericType = dbFunctionDatas[i].hasGenericType,
                                type = dbFunctionDatas[i].type,
                                isDeclaration = dbFunctionDatas[i].isDeclaration,
                                connectionVarName = dbFunctionDatas[i].connectionVarName,
                                isTransaction = dbFunctionDatas[i].isTransaction,
                                transactionVarName = dbFunctionDatas[i].transactionVarName,
                                dbName = dbFunctionDatas[i].connectionName,
                                sql = dbFunctionDatas[i].sql,
                                fields = dbFunctionDatas[i].fields,
                                sqlVarParameters = dbFunctionDatas[i].sqlVarParameters
                            };
                            //byte[] fileString = null;
                            if (connectionString.type == DB.DBType.MySql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.mysql.mysql.QueryRow;
                                var template = new NFinal.Compile.SqlTemplate.mysql.QueryRow();
                                template.Model = QueryRowModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Oracle)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.oracle.oracle.QueryRow;
                                var template = new NFinal.Compile.SqlTemplate.oracle.QueryRow();
                                template.Model = QueryRowModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Sqlite)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlite.sqlite.QueryRow;
                                var template = new NFinal.Compile.SqlTemplate.sqlite.QueryRow();
                                template.Model = QueryRowModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.SqlServer)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlserver.sqlserver.QueryRow;
                                var template = new NFinal.Compile.SqlTemplate.sqlserver.QueryRow();
                                template.Model = QueryRowModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if(connectionString.type == DB.DBType.PostgreSql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.postgresql.postgresql.QueryRow;
                                var template = new NFinal.Compile.SqlTemplate.postgresql.QueryRow();
                                template.Model = QueryRowModel;
                                webCsharpCode = template.TransformText();
                            }
                            
                            //webCsharpCode = RazorEngine.Engine.Razor.RunCompile(
                            //    System.Text.Encoding.UTF8.GetString(fileString),
                            //    string.Format("/SqlTemplate/{0}/QueryRow.sql", connectionString.type),
                            //    QueryRowModel.GetType(),
                            //    QueryRowModel);

                            //tempalte = new JinianNet.JNTemplate.Template(fileString);
                            //tempalte.Context.TempData["functionName"] = methodName;
                            //tempalte.Context.TempData["varName"] = dbFunctionDatas[i].varName;
                            //tempalte.Context.TempData["hasGenericType"] = dbFunctionDatas[i].hasGenericType;
                            //tempalte.Context.TempData["type"] = dbFunctionDatas[i].type;
                            //tempalte.Context.TempData["isDeclaration"] = dbFunctionDatas[i].isDeclaration;
                            //tempalte.Context.TempData["connectionVarName"] = dbFunctionDatas[i].connectionVarName;
                            //tempalte.Context.TempData["isTransaction"] = dbFunctionDatas[i].isTransaction;
                            //tempalte.Context.TempData["transactionVarName"] = dbFunctionDatas[i].transactionVarName;
                            //tempalte.Context.TempData["dbName"] = dbFunctionDatas[i].connectionName;
                            //tempalte.Context.TempData["sql"] = dbFunctionDatas[i].sql;
                            //tempalte.Context.TempData["fields"] = dbFunctionDatas[i].fields;
                            //tempalte.Context.TempData["sqlVarParameters"] = dbFunctionDatas[i].sqlVarParameters;
                            //webCsharpCode = tempalte.Render();
                            relative_position += Replace(ref csharpFileCode,
                                relative_position + dbFunctionDatas[i].index,
                                dbFunctionDatas[i].length,
                                webCsharpCode);
                        }
                        if (dbFunctionDatas[i].functionName == "Insert")
                        {
                            var InsertModel = new NFinal.Compile.SqlTemplate.Model.Insert
                            {
                                functionName = methodName,
                                varName = dbFunctionDatas[i].varName,
                                hasGenericType = dbFunctionDatas[i].hasGenericType,
                                type = dbFunctionDatas[i].type,
                                isDeclaration = dbFunctionDatas[i].isDeclaration,
                                connectionVarName = dbFunctionDatas[i].connectionVarName,
                                isTransaction = dbFunctionDatas[i].isTransaction,
                                transactionVarName = dbFunctionDatas[i].transactionVarName,
                                dbName = dbFunctionDatas[i].connectionName,
                                sql = dbFunctionDatas[i].sql,
                                fields = dbFunctionDatas[i].fields,
                                sqlVarParameters = dbFunctionDatas[i].sqlVarParameters,
                                tableName = dbFunctionDatas[i].tableName
                            };
                            //byte[] fileString = null;
                            if (connectionString.type == DB.DBType.MySql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.mysql.mysql.Insert;
                                var template = new NFinal.Compile.SqlTemplate.mysql.Insert();
                                template.Model = InsertModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Oracle)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.oracle.oracle.Insert;
                                var template = new NFinal.Compile.SqlTemplate.oracle.Insert();
                                template.Model = InsertModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Sqlite)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlite.sqlite.Insert;
                                var template = new NFinal.Compile.SqlTemplate.sqlite.Insert();
                                template.Model = InsertModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.SqlServer)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlserver.sqlserver.Insert;
                                var template = new NFinal.Compile.SqlTemplate.sqlserver.Insert();
                                template.Model = InsertModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if(connectionString.type == DB.DBType.PostgreSql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.postgresql.postgresql.Insert;
                                var template = new NFinal.Compile.SqlTemplate.postgresql.Insert();
                                template.Model = InsertModel;
                                webCsharpCode = template.TransformText();
                            }
                            
                            //webCsharpCode = RazorEngine.Engine.Razor.RunCompile(
                            //    System.Text.Encoding.UTF8.GetString(fileString),
                            //    string.Format("/SqlTemplate/{0}/Insert.sql", connectionString.type),
                            //    InsertModel.GetType(),
                            //    InsertModel);

                            //tempalte = new JinianNet.JNTemplate.Template(fileString);
                            //tempalte.Context.TempData["functionName"] = methodName;
                            //tempalte.Context.TempData["varName"] = dbFunctionDatas[i].varName;
                            //tempalte.Context.TempData["hasGenericType"] = dbFunctionDatas[i].hasGenericType;
                            //tempalte.Context.TempData["type"] = dbFunctionDatas[i].type;
                            //tempalte.Context.TempData["isDeclaration"] = dbFunctionDatas[i].isDeclaration;
                            //tempalte.Context.TempData["connectionVarName"] = dbFunctionDatas[i].connectionVarName;
                            //tempalte.Context.TempData["isTransaction"] = dbFunctionDatas[i].isTransaction;
                            //tempalte.Context.TempData["transactionVarName"] = dbFunctionDatas[i].transactionVarName;
                            //tempalte.Context.TempData["dbName"] = dbFunctionDatas[i].connectionName;
                            //tempalte.Context.TempData["sql"] = dbFunctionDatas[i].sql;
                            ////tempalte.Context.TempData["tableName"] = dbFunctionDatas[i].tables[0].name;
                            //tempalte.Context.TempData["fields"] = dbFunctionDatas[i].fields;
                            //tempalte.Context.TempData["sqlVarParameters"] = dbFunctionDatas[i].sqlVarParameters;
                            //webCsharpCode = tempalte.Render();

                            relative_position += Replace(ref csharpFileCode,
                                relative_position + dbFunctionDatas[i].index,
                                dbFunctionDatas[i].length,
                                webCsharpCode);

                        }
                        if (dbFunctionDatas[i].functionName == "Update")
                        {
                            var UpdateModel = new NFinal.Compile.SqlTemplate.Model.Update
                            {
                                functionName = methodName,
                                varName = dbFunctionDatas[i].varName,
                                hasGenericType = dbFunctionDatas[i].hasGenericType,
                                type = dbFunctionDatas[i].type,
                                isDeclaration = dbFunctionDatas[i].isDeclaration,
                                connectionVarName = dbFunctionDatas[i].connectionVarName,
                                isTransaction = dbFunctionDatas[i].isTransaction,
                                transactionVarName = dbFunctionDatas[i].transactionVarName,
                                dbName = dbFunctionDatas[i].connectionName,
                                sql = dbFunctionDatas[i].sql,
                                fields = dbFunctionDatas[i].fields,
                                sqlVarParameters = dbFunctionDatas[i].sqlVarParameters
                            };
                            //byte[] fileString = null;
                            if (connectionString.type == DB.DBType.MySql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.mysql.mysql.Update;
                                var template = new NFinal.Compile.SqlTemplate.mysql.Update();
                                template.Model = UpdateModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Oracle)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.oracle.oracle.Update;
                                var template = new NFinal.Compile.SqlTemplate.oracle.Update();
                                template.Model = UpdateModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Sqlite)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlite.sqlite.Update;
                                var template = new NFinal.Compile.SqlTemplate.sqlite.Update();
                                template.Model = UpdateModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.SqlServer)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlserver.sqlserver.Update;
                                var template = new NFinal.Compile.SqlTemplate.sqlserver.Update();
                                template.Model = UpdateModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if(connectionString.type == DB.DBType.PostgreSql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.postgresql.postgresql.Update;
                                var template = new NFinal.Compile.SqlTemplate.postgresql.Update();
                                template.Model = UpdateModel;
                                webCsharpCode = template.TransformText();
                            }
                            
                            //webCsharpCode = RazorEngine.Engine.Razor.RunCompile(
                            //    System.Text.Encoding.UTF8.GetString(fileString),
                            //    string.Format("/SqlTemplate/{0}/Update.sql", connectionString.type),
                            //    UpdateModel.GetType(),
                            //    UpdateModel);


                            //tempalte = new JinianNet.JNTemplate.Template(fileString);
                            //tempalte.Context.TempData["functionName"] = methodName;
                            //tempalte.Context.TempData["varName"] = dbFunctionDatas[i].varName;
                            //tempalte.Context.TempData["hasGenericType"] = dbFunctionDatas[i].hasGenericType;
                            //tempalte.Context.TempData["type"] = dbFunctionDatas[i].type;
                            //tempalte.Context.TempData["isDeclaration"] = dbFunctionDatas[i].isDeclaration;
                            //tempalte.Context.TempData["connectionVarName"] = dbFunctionDatas[i].connectionVarName;
                            //tempalte.Context.TempData["isTransaction"] = dbFunctionDatas[i].isTransaction;
                            //tempalte.Context.TempData["transactionVarName"] = dbFunctionDatas[i].transactionVarName;
                            //tempalte.Context.TempData["dbName"] = dbFunctionDatas[i].connectionName;
                            //tempalte.Context.TempData["sql"] = dbFunctionDatas[i].sql;
                            //tempalte.Context.TempData["fields"] = dbFunctionDatas[i].fields;
                            //tempalte.Context.TempData["sqlVarParameters"] = dbFunctionDatas[i].sqlVarParameters;
                            //webCsharpCode = tempalte.Render();

                            relative_position += Replace(ref csharpFileCode,
                                relative_position + dbFunctionDatas[i].index,
                                dbFunctionDatas[i].length,
                                webCsharpCode);

                        }
                        if (dbFunctionDatas[i].functionName == "Delete")
                        {
                            var DeleteModel = new NFinal.Compile.SqlTemplate.Model.Delete
                            {
                                functionName = methodName,
                                varName = dbFunctionDatas[i].varName,
                                hasGenericType = dbFunctionDatas[i].hasGenericType,
                                type = dbFunctionDatas[i].type,
                                isDeclaration = dbFunctionDatas[i].isDeclaration,
                                connectionVarName = dbFunctionDatas[i].connectionVarName,
                                isTransaction = dbFunctionDatas[i].isTransaction,
                                transactionVarName = dbFunctionDatas[i].transactionVarName,
                                dbName = dbFunctionDatas[i].connectionName,
                                sql = dbFunctionDatas[i].sql,
                                fields = dbFunctionDatas[i].fields,
                                sqlVarParameters = dbFunctionDatas[i].sqlVarParameters
                            };
                            //byte[] fileString = null;
                            if (connectionString.type == DB.DBType.MySql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.mysql.mysql.Delete;
                                var template = new NFinal.Compile.SqlTemplate.mysql.Delete();
                                template.Model = DeleteModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Oracle)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.oracle.oracle.Delete;
                                var template = new NFinal.Compile.SqlTemplate.oracle.Delete();
                                template.Model = DeleteModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Sqlite)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlite.sqlite.Delete;
                                var template = new NFinal.Compile.SqlTemplate.sqlite.Delete();
                                template.Model = DeleteModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.SqlServer)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlserver.sqlserver.Delete;
                                var template = new NFinal.Compile.SqlTemplate.sqlserver.Delete();
                                template.Model = DeleteModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if(connectionString.type==DB.DBType.PostgreSql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.postgresql.postgresql.Delete;
                                var template = new NFinal.Compile.SqlTemplate.postgresql.Delete();
                                template.Model = DeleteModel;
                                webCsharpCode = template.TransformText();
                            }
                            
                            //webCsharpCode = RazorEngine.Engine.Razor.RunCompile(
                            //    System.Text.Encoding.UTF8.GetString(fileString),
                            //    string.Format("/SqlTemplate/{0}/Delete.sql", connectionString.type),
                            //    DeleteModel.GetType(),
                            //    DeleteModel);

                            //tempalte = new JinianNet.JNTemplate.Template(fileString);
                            //tempalte.Context.TempData["functionName"] = methodName;
                            //tempalte.Context.TempData["varName"] = dbFunctionDatas[i].varName;
                            //tempalte.Context.TempData["hasGenericType"] = dbFunctionDatas[i].hasGenericType;
                            //tempalte.Context.TempData["type"] = dbFunctionDatas[i].type;
                            //tempalte.Context.TempData["isDeclaration"] = dbFunctionDatas[i].isDeclaration;
                            //tempalte.Context.TempData["connectionVarName"] = dbFunctionDatas[i].connectionVarName;
                            //tempalte.Context.TempData["isTransaction"] = dbFunctionDatas[i].isTransaction;
                            //tempalte.Context.TempData["transactionVarName"] = dbFunctionDatas[i].transactionVarName;
                            //tempalte.Context.TempData["dbName"] = dbFunctionDatas[i].connectionName;
                            //tempalte.Context.TempData["sql"] = dbFunctionDatas[i].sql;
                            //tempalte.Context.TempData["fields"] = dbFunctionDatas[i].fields;
                            //tempalte.Context.TempData["sqlVarParameters"] = dbFunctionDatas[i].sqlVarParameters;
                            //webCsharpCode = tempalte.Render();
                            relative_position += Replace(ref csharpFileCode,
                                relative_position + dbFunctionDatas[i].index,
                                dbFunctionDatas[i].length,
                                webCsharpCode);

                        }
                        if (dbFunctionDatas[i].functionName == "QueryObject")
                        {
                            var QueryObjectModel = new NFinal.Compile.SqlTemplate.Model.QueryObject
                            {
                                functionName = methodName,
                                varName = dbFunctionDatas[i].varName,
                                hasGenericType = dbFunctionDatas[i].hasGenericType,
                                type = dbFunctionDatas[i].type,
                                isDeclaration = dbFunctionDatas[i].isDeclaration,
                                connectionVarName = dbFunctionDatas[i].connectionVarName,
                                isTransaction = dbFunctionDatas[i].isTransaction,
                                transactionVarName = dbFunctionDatas[i].transactionVarName,
                                dbName = dbFunctionDatas[i].connectionName,
                                sql = dbFunctionDatas[i].sql,
                                fields = dbFunctionDatas[i].fields,
                                sqlVarParameters = dbFunctionDatas[i].sqlVarParameters,
                                convertMethodName = dbFunctionDatas[i].convertMethodName
                            };
                            //byte[] fileString = null;
                            if (connectionString.type == DB.DBType.MySql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.mysql.mysql.QueryObject;
                                var template = new NFinal.Compile.SqlTemplate.mysql.QueryObject();
                                template.Model = QueryObjectModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Oracle)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.oracle.oracle.QueryObject;
                                var template = new NFinal.Compile.SqlTemplate.oracle.QueryObject();
                                template.Model = QueryObjectModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Sqlite)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlite.sqlite.QueryObject;
                                var template = new NFinal.Compile.SqlTemplate.sqlite.QueryObject();
                                template.Model = QueryObjectModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.SqlServer)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlserver.sqlserver.QueryObject;
                                var template = new NFinal.Compile.SqlTemplate.sqlserver.QueryObject();
                                template.Model = QueryObjectModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if(connectionString.type == DB.DBType.PostgreSql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.postgresql.postgresql.QueryObject;
                                var template = new NFinal.Compile.SqlTemplate.postgresql.QueryObject();
                                template.Model = QueryObjectModel;
                                webCsharpCode = template.TransformText();
                            }
                            
                            //webCsharpCode = RazorEngine.Engine.Razor.RunCompile(
                            //    System.Text.Encoding.UTF8.GetString(fileString),
                            //    string.Format("/SqlTemplate/{0}/QueryObject.sql", connectionString.type),
                            //    QueryObjectModel.GetType(),
                            //    QueryObjectModel);

                            //tempalte = new JinianNet.JNTemplate.Template(fileString);
                            //tempalte.Context.TempData["functionName"] = methodName;
                            //tempalte.Context.TempData["varName"] = dbFunctionDatas[i].varName;
                            //tempalte.Context.TempData["hasGenericType"] = dbFunctionDatas[i].hasGenericType;
                            //tempalte.Context.TempData["type"] = dbFunctionDatas[i].type;
                            //tempalte.Context.TempData["isDeclaration"] = dbFunctionDatas[i].isDeclaration;
                            //tempalte.Context.TempData["connectionVarName"] = dbFunctionDatas[i].connectionVarName;
                            //tempalte.Context.TempData["isTransaction"] = dbFunctionDatas[i].isTransaction;
                            //tempalte.Context.TempData["transactionVarName"] = dbFunctionDatas[i].transactionVarName;
                            //tempalte.Context.TempData["dbName"] = dbFunctionDatas[i].connectionName;
                            //tempalte.Context.TempData["sql"] = dbFunctionDatas[i].sql;
                            //tempalte.Context.TempData["fields"] = dbFunctionDatas[i].fields;
                            //tempalte.Context.TempData["sqlVarParameters"] = dbFunctionDatas[i].sqlVarParameters;
                            //tempalte.Context.TempData["convertMethodName"] = dbFunctionDatas[i].convertMethodName;
                            //webCsharpCode = tempalte.Render();

                            relative_position += Replace(ref csharpFileCode,
                                relative_position + dbFunctionDatas[i].index,
                                dbFunctionDatas[i].length,
                                webCsharpCode);
                        }
                        if (dbFunctionDatas[i].functionName == "Page")
                        {
                            //分页参数解析
                            PageSqlAnalyse pageStatement = new PageSqlAnalyse(dbFunctionDatas[i].sql, connectionString.type);
                            pageStatement.Parse();
                            var PageModel = new NFinal.Compile.SqlTemplate.Model.Page
                            {
                                functionName = methodName,
                                varName = dbFunctionDatas[i].varName,
                                hasGenericType = dbFunctionDatas[i].hasGenericType,
                                type = dbFunctionDatas[i].type,
                                isDeclaration = dbFunctionDatas[i].isDeclaration,
                                connectionVarName = dbFunctionDatas[i].connectionVarName,
                                isTransaction = dbFunctionDatas[i].isTransaction,
                                transactionVarName = dbFunctionDatas[i].transactionVarName,
                                dbName = dbFunctionDatas[i].connectionName,
                                fields = dbFunctionDatas[i].fields,
                                pageSql = pageStatement.pageSql,
                                countSql = pageStatement.countSql,
                                pageVarName = dbFunctionDatas[i].parameters[1],
                                sqlVarParameters = dbFunctionDatas[i].sqlVarParameters,
                                convertMethodName = dbFunctionDatas[i].convertMethodName
                            };
                            //byte[] fileString = null;
                            if (connectionString.type == DB.DBType.MySql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.mysql.mysql.Page;
                                var template = new NFinal.Compile.SqlTemplate.mysql.Page();
                                template.Model = PageModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Oracle)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.oracle.oracle.Page;
                                var template = new NFinal.Compile.SqlTemplate.oracle.Page();
                                template.Model = PageModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Sqlite)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlite.sqlite.Page;
                                var template = new NFinal.Compile.SqlTemplate.sqlite.Page();
                                template.Model = PageModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.SqlServer)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlserver.sqlserver.Page;
                                var template = new NFinal.Compile.SqlTemplate.sqlserver.Page();
                                template.Model = PageModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if(connectionString.type == DB.DBType.PostgreSql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.postgresql.postgresql.Page;
                                var template = new NFinal.Compile.SqlTemplate.postgresql.Page();
                                template.Model = PageModel;
                                webCsharpCode = template.TransformText();
                            }

                            //tempalte = new JinianNet.JNTemplate.Template(fileString);
                            //tempalte.Context.TempData["functionName"] = methodName;
                            //tempalte.Context.TempData["varName"] = dbFunctionDatas[i].varName;
                            //tempalte.Context.TempData["hasGenericType"] = dbFunctionDatas[i].hasGenericType;
                            //tempalte.Context.TempData["type"] = dbFunctionDatas[i].type;
                            //tempalte.Context.TempData["isDeclaration"] = dbFunctionDatas[i].isDeclaration;
                            //tempalte.Context.TempData["connectionVarName"] = dbFunctionDatas[i].connectionVarName;
                            //tempalte.Context.TempData["isTransaction"] = dbFunctionDatas[i].isTransaction;
                            //tempalte.Context.TempData["transactionVarName"] = dbFunctionDatas[i].transactionVarName;
                            //tempalte.Context.TempData["dbName"] = dbFunctionDatas[i].connectionName;
                            //tempalte.Context.TempData["fields"] = dbFunctionDatas[i].fields;
                            

                            
                            //tempalte.Context.TempData["pageSql"] = pageStatement.pageSql;
                            //tempalte.Context.TempData["countSql"] = pageStatement.countSql;
                            //tempalte.Context.TempData["pageVarName"] = dbFunctionDatas[i].parameters[1];

                            //tempalte.Context.TempData["sqlVarParameters"] = dbFunctionDatas[i].sqlVarParameters;
                            //tempalte.Context.TempData["convertMethodName"] = dbFunctionDatas[i].convertMethodName;
                            //webCsharpCode = tempalte.Render();

                            
                            //webCsharpCode = RazorEngine.Engine.Razor.RunCompile(
                            //    System.Text.Encoding.UTF8.GetString(fileString),
                            //    string.Format("/SqlTemplate/{0}/Page.sql", connectionString.type),
                            //    PageModel.GetType(),
                            //    PageModel);


                            relative_position += Replace(ref csharpFileCode,
                                relative_position + dbFunctionDatas[i].index,
                                dbFunctionDatas[i].length,
                                webCsharpCode);
                        }
                        if (dbFunctionDatas[i].functionName == "QueryRandom")
                        {
                            RandomSqlAnalyse random = new RandomSqlAnalyse(dbFunctionDatas[i].sql, connectionString.type);
                            random.Parse();
                            var QueryRandom = new NFinal.Compile.SqlTemplate.Model.QueryRandom
                            {
                                functionName = methodName,
                                varName = dbFunctionDatas[i].varName,
                                hasGenericType = dbFunctionDatas[i].hasGenericType,
                                type = dbFunctionDatas[i].type,
                                isDeclaration = dbFunctionDatas[i].isDeclaration,
                                connectionVarName = dbFunctionDatas[i].connectionVarName,
                                isTransaction = dbFunctionDatas[i].isTransaction,
                                transactionVarName = dbFunctionDatas[i].transactionVarName,
                                dbName = dbFunctionDatas[i].connectionName,
                                topNumber = dbFunctionDatas[i].parameters[1],
                                sql = random.randomSql,
                                fields = dbFunctionDatas[i].fields,
                                sqlVarParameters = dbFunctionDatas[i].sqlVarParameters
                            };
                            //byte[] fileString = null;
                            if (connectionString.type == DB.DBType.MySql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.mysql.mysql.QueryRandom;
                                var template = new NFinal.Compile.SqlTemplate.mysql.QueryRandom();
                                template.Model = QueryRandom;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Oracle)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.oracle.oracle.QueryRandom;
                                var template = new NFinal.Compile.SqlTemplate.oracle.QueryRandom();
                                template.Model = QueryRandom;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Sqlite)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlite.sqlite.QueryRandom;
                                var template = new NFinal.Compile.SqlTemplate.sqlite.QueryRandom();
                                template.Model = QueryRandom;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.SqlServer)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlserver.sqlserver.QueryRandom;
                                var template = new NFinal.Compile.SqlTemplate.sqlserver.QueryRandom();
                                template.Model = QueryRandom;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.PostgreSql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.postgresql.postgresql.QueryRandom;
                                var template = new NFinal.Compile.SqlTemplate.postgresql.QueryRandom();
                                template.Model = QueryRandom;
                                webCsharpCode = template.TransformText();
                            }
                            //tempalte = new JinianNet.JNTemplate.Template(fileString);
                            //tempalte.Context.TempData["functionName"] = methodName;
                            //tempalte.Context.TempData["varName"] = dbFunctionDatas[i].varName;
                            //tempalte.Context.TempData["hasGenericType"] = dbFunctionDatas[i].hasGenericType;
                            //tempalte.Context.TempData["type"] = dbFunctionDatas[i].type;
                            //tempalte.Context.TempData["isDeclaration"] = dbFunctionDatas[i].isDeclaration;
                            //tempalte.Context.TempData["connectionVarName"] = dbFunctionDatas[i].connectionVarName;
                            //tempalte.Context.TempData["isTransaction"] = dbFunctionDatas[i].isTransaction;
                            //tempalte.Context.TempData["transactionVarName"] = dbFunctionDatas[i].transactionVarName;
                            //tempalte.Context.TempData["dbName"] = dbFunctionDatas[i].connectionName;
                            //select 语句 转为选取某随机行的语句

                            

                            //tempalte.Context.TempData["topNumber"] = dbFunctionDatas[i].parameters[1];
                            //tempalte.Context.TempData["sql"] = random.randomSql;
                            //tempalte.Context.TempData["fields"] = dbFunctionDatas[i].fields;
                            //tempalte.Context.TempData["sqlVarParameters"] = dbFunctionDatas[i].sqlVarParameters;
                            //webCsharpCode = tempalte.Render();
                            
                            //webCsharpCode = RazorEngine.Engine.Razor.RunCompile(
                            //    System.Text.Encoding.UTF8.GetString(fileString),
                            //    string.Format("/SqlTemplate/{0}/QueryRandom.sql", connectionString.type),
                            //    QueryRandom.GetType(),
                            //    QueryRandom);

                            relative_position += Replace(ref csharpFileCode,
                                relative_position + dbFunctionDatas[i].index,
                                dbFunctionDatas[i].length,
                                webCsharpCode);
                        }
                        if (dbFunctionDatas[i].functionName == "QueryTop")
                        {
                            TopSqlAnalyse top = new TopSqlAnalyse(dbFunctionDatas[i].sql, connectionString.type);
                            top.Parse();
                            var QueryTopModel = new NFinal.Compile.SqlTemplate.Model.QueryTop
                            {
                                functionName = methodName,
                                varName = dbFunctionDatas[i].varName,
                                hasGenericType = dbFunctionDatas[i].hasGenericType,
                                type = dbFunctionDatas[i].type,
                                isDeclaration = dbFunctionDatas[i].isDeclaration,
                                connectionVarName = dbFunctionDatas[i].connectionVarName,
                                isTransaction = dbFunctionDatas[i].isTransaction,
                                transactionVarName = dbFunctionDatas[i].transactionVarName,
                                dbName = dbFunctionDatas[i].connectionName,
                                topNumber = dbFunctionDatas[i].parameters[1],
                                sql = top.topSql,
                                fields = dbFunctionDatas[i].fields,
                                sqlVarParameters = dbFunctionDatas[i].sqlVarParameters
                            };
                            //byte[] fileString = null;
                            if (connectionString.type == DB.DBType.MySql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.mysql.mysql.QueryTop;
                                var template = new NFinal.Compile.SqlTemplate.mysql.QueryTop();
                                template.Model = QueryTopModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Oracle)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.oracle.oracle.QueryTop;
                                var template = new NFinal.Compile.SqlTemplate.oracle.QueryTop();
                                template.Model = QueryTopModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.Sqlite)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlite.sqlite.QueryTop;
                                var template = new NFinal.Compile.SqlTemplate.sqlite.QueryTop();
                                template.Model = QueryTopModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if (connectionString.type == DB.DBType.SqlServer)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.sqlserver.sqlserver.QueryTop;
                                var template = new NFinal.Compile.SqlTemplate.sqlserver.QueryTop();
                                template.Model = QueryTopModel;
                                webCsharpCode = template.TransformText();
                            }
                            else if(connectionString.type == DB.DBType.PostgreSql)
                            {
                                //fileString = NFinal.Compile.SqlTemplate.postgresql.postgresql.QueryTop;
                                var template = new NFinal.Compile.SqlTemplate.mysql.QueryTop();
                                template.Model = QueryTopModel;
                                webCsharpCode = template.TransformText();
                            }
                            //tempalte = new JinianNet.JNTemplate.Template(fileString);
                            //tempalte.Context.TempData["functionName"] = methodName;
                            //tempalte.Context.TempData["varName"] = dbFunctionDatas[i].varName;
                            //tempalte.Context.TempData["hasGenericType"] = dbFunctionDatas[i].hasGenericType;
                            //tempalte.Context.TempData["type"] = dbFunctionDatas[i].type;
                            //tempalte.Context.TempData["isDeclaration"] = dbFunctionDatas[i].isDeclaration;
                            //tempalte.Context.TempData["connectionVarName"] = dbFunctionDatas[i].connectionVarName;
                            //tempalte.Context.TempData["isTransaction"] = dbFunctionDatas[i].isTransaction;
                            //tempalte.Context.TempData["transactionVarName"] = dbFunctionDatas[i].transactionVarName;
                            //tempalte.Context.TempData["dbName"] = dbFunctionDatas[i].connectionName;
                            //select 语句 转为选取某随机行的语句

                            

                            //tempalte.Context.TempData["topNumber"] = dbFunctionDatas[i].parameters[1];
                            //tempalte.Context.TempData["sql"] = top.topSql;
                            //tempalte.Context.TempData["fields"] = dbFunctionDatas[i].fields;
                            //tempalte.Context.TempData["sqlVarParameters"] = dbFunctionDatas[i].sqlVarParameters;
                            //webCsharpCode = tempalte.Render();
                            
                            //webCsharpCode = RazorEngine.Engine.Razor.RunCompile(
                            //    System.Text.Encoding.UTF8.GetString(fileString),
                            //    string.Format("/SqlTemplate/{0}/QueryTop.sql", connectionString.type),
                            //    QueryTopModel.GetType(),
                            //    QueryTopModel);

                            relative_position += Replace(ref csharpFileCode,
                                relative_position + dbFunctionDatas[i].index,
                                dbFunctionDatas[i].length,
                                webCsharpCode);
                        }
                    }
                }
            }
            return relative_position;
        }
        /// <summary>
        /// 数据库函数替换类
        /// </summary>
        /// <param name="methodName">函数名</param>
        /// <param name="dbFunctionData">函数信息</param>
        /// <param name="appRoot">网站根目录</param>
        /// <returns></returns>
        public string SetMagicStruct(string methodName,DbFunctionData dbFunctionData, System.Collections.Generic.List<NFinal.Compile.StructField> structFieldList,string appRoot)
        {
            string result = null;
            if (dbFunctionData.functionName == "QueryAll" || dbFunctionData.functionName == "QueryRow" 
                || dbFunctionData.functionName == "Page" || dbFunctionData.functionName == "QueryRandom"
                || dbFunctionData.functionName =="QueryTop")
            {
                DB.ConnectionString connectionString = null;
                connectionString = GetConnectionString(dbFunctionData.connectionName);
                var StructModel = new NFinal.Compile.SqlTemplate.Model.Struct
                {
                    functionName = methodName,
                    varName = dbFunctionData.varName,
                    hasGenericType = dbFunctionData.hasGenericType,
                    type = dbFunctionData.type,
                    isDeclaration = dbFunctionData.isDeclaration,
                    connectionVarName = dbFunctionData.connectionVarName,
                    isTransaction = dbFunctionData.isTransaction,
                    transactionVarName = dbFunctionData.transactionVarName,
                    fields = dbFunctionData.fields,
                    structFields = structFieldList
                };
                //JinianNet.JNTemplate.ITemplate template = null;
                byte[] fileString = null;
                if (connectionString.type == DB.DBType.MySql)
                {
                    //fileString = NFinal.Compile.SqlTemplate.mysql.mysql.Struct;
                    var template = new NFinal.Compile.SqlTemplate.mysql.Struct();
                    template.Model = StructModel;
                    result = template.TransformText();
                }
                else if (connectionString.type == DB.DBType.Oracle)
                {
                    //fileString = NFinal.Compile.SqlTemplate.oracle.oracle.Struct;
                    var template = new NFinal.Compile.SqlTemplate.oracle.Struct();
                    template.Model = StructModel;
                    result = template.TransformText();
                }
                else if (connectionString.type == DB.DBType.Sqlite)
                {
                    //fileString = NFinal.Compile.SqlTemplate.sqlite.sqlite.Struct;
                    var template = new NFinal.Compile.SqlTemplate.sqlite.Struct();
                    template.Model = StructModel;
                    result = template.TransformText();
                }
                else if (connectionString.type == DB.DBType.SqlServer)
                {
                    //fileString = NFinal.Compile.SqlTemplate.sqlserver.sqlserver.Struct;
                    var template = new NFinal.Compile.SqlTemplate.sqlserver.Struct();
                    template.Model = StructModel;
                    result = template.TransformText();
                }
                else if (connectionString.type == DB.DBType.PostgreSql)
                {
                    //fileString = NFinal.Compile.SqlTemplate.postgresql.postgresql.Struct;
                    var template = new NFinal.Compile.SqlTemplate.postgresql.Struct();
                    template.Model = StructModel;
                    result = template.TransformText();
                }
                //template = new JinianNet.JNTemplate.Template(fileString);
                //template.Context.TempData["functionName"] = methodName;
                //template.Context.TempData["varName"] = dbFunctionData.varName;
                //template.Context.TempData["hasGenericType"] = dbFunctionData.hasGenericType;
                //template.Context.TempData["type"] = dbFunctionData.type;
                //template.Context.TempData["isDeclaration"] = dbFunctionData.isDeclaration;
                //template.Context.TempData["connectionVarName"] = dbFunctionData.connectionVarName;
                //template.Context.TempData["isTransaction"] = dbFunctionData.isTransaction;
                //template.Context.TempData["transactionVarName"] = dbFunctionData.transactionVarName;
                //template.Context.TempData["fields"] = dbFunctionData.fields;
                //template.Context.TempData["structFields"] = structFieldList;
                //result = template.Render();
                
                //result = RazorEngine.Engine.Razor.RunCompile(
                //                System.Text.Encoding.UTF8.GetString(fileString),
                //                string.Format("/SqlTemplate/{0}/Struct.sql", connectionString.type),
                //                StructModel.GetType(),
                //                StructModel);
            }
            return result;
        }
       
    }
}