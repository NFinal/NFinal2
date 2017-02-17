using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

namespace NFinalModelGenerator
{
    public class SqlDocument
    {
        public Dictionary<string, string> varList = new Dictionary<string, string>();
        public bool useStruct = false;
        public string name = null;
        public string connectionString = null;
        public string providerName = null;
        public Dictionary<string, string> sqlList = new Dictionary<string, string>();
        public List<ModelFileData> modelFileDataList = new List<ModelFileData>();
        public List<string> fileNameList = new List<string>();
        public static string nameSpace;
        public SqlDocument(string filePath, string nameSpace, string sqlContent)
        {
            SqlDocument.nameSpace = nameSpace;
            string fileDirectory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }
            string fileName;
            NFinal.IO.StringWriter sw = null;
            ModelFileData modelData;
            System.Data.IDbConnection con = null;
            #region 读取配置
            Regex commentRegex = new Regex(@"/\*(.*?)\*/", RegexOptions.Singleline);
            Match commentMatch = commentRegex.Match(sqlContent);
            if (commentMatch.Success && commentMatch.Groups[1].Success)
            {
                string commentContent = commentMatch.Groups[1].Value;
                Regex varRegex = new Regex(@"set\s+@([_a-zA-Z0-9]+)=('.*?'|.*?)(\r|\n)*", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                MatchCollection varMac = varRegex.Matches(commentContent);
                string key = null;
                string value = null;
                foreach (Match varMat in varMac)
                {
                    if (varMat.Success)
                    {
                        key = varMat.Groups[1].Value;
                        value = varMat.Groups[2].Value.Trim().Trim('\'');
                        if (!string.IsNullOrEmpty(key))
                        {
                            if (key == "useStruct")
                            {
                                if (value == "true" || value == "1")
                                {
                                    useStruct = true;
                                }
                            }
                            else if (key == "name")
                            {
                                this.name = value;
                            }
                            else if (key == "connectionString")
                            {
                                this.connectionString = value;
                            }
                            else if (key == "providerName")
                            {
                                this.providerName = value;
                            }
                        }
                    }
                }
            }
            #endregion
            Data.DataUtility dataUtility = null;
            if (!string.IsNullOrEmpty(this.name) && !string.IsNullOrEmpty(this.connectionString) && !string.IsNullOrEmpty(this.providerName))
            {
                if (this.providerName.ToLower().IndexOf("mysql") > -1)
                {
                    con = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                    dataUtility = new Data.MySqlDataUtility(con);
                }
                else if (this.providerName.ToLower().IndexOf("sqlite") > -1)
                {
                    con = new System.Data.SQLite.SQLiteConnection(connectionString);
                    dataUtility = new Data.SQLiteDataUtility(con);
                }
                else if (this.providerName.ToLower().IndexOf("sqlclient") > -1)
                {
                    con = new System.Data.SqlClient.SqlConnection(connectionString);
                    dataUtility = new Data.SqlServerDataUtility(con);
                }
                else if (this.providerName.ToLower().IndexOf("Oracle") > -1)
                {
                    con = new Oracle.ManagedDataAccess.Client.OracleConnection(connectionString);
                    dataUtility = new Data.OracleDataUtility(con);
                }
                else if (this.providerName.ToLower().IndexOf("Npgsql") > -1)
                {
                    con = new Npgsql.NpgsqlConnection(connectionString);
                    dataUtility = new Data.PostgreSqlDataUtility(con);
                }
                dataUtility.Open();
                var tableDataList = dataUtility.GetDefaultTableData();
                if (dataUtility != null)
                {
                    //dataUtility.SetTableColumnData(tableDataList);
                    foreach (var tableData in tableDataList)
                    {
                        fileName = Path.Combine(fileDirectory, tableData.Name+".cs");
                        if (!File.Exists(fileName))
                        {
                            modelData = new ModelFileData();
                            modelData.name = tableData.Name;
                            modelData.nameSpace = nameSpace;
                            modelData.isDefaultTable = true;
                            dataUtility.SetTableColumnData(tableData);
                            sw = new NFinal.IO.StringWriter();
                            ModelTemplate.Render(sw, tableData);
                            modelData.content = sw.ToString();
                            modelData.fileName = fileName;
                            modelFileDataList.Add(modelData);
                        }
                    }
                }
            }
            sqlContent = sqlContent.Substring(commentMatch.Length);
            StringReader reader = new StringReader(sqlContent);
            bool isEnd = false;
            string line = null;
            string name = null;
            string sql = null;
            while (!isEnd)
            {
                line = reader.ReadLine();
                if (line == null)
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        sqlList.Add(name, sql);
                    }
                    break;
                }
                if (line.StartsWith("--"))
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        sqlList.Add(name, sql);
                    }
                    name = line.Substring(2).Trim();
                    sql = null;
                }
                else
                {
                    sql += line;
                }
            }
            
            foreach (var sqlData in sqlList)
            {
                fileName = Path.Combine(fileDirectory, sqlData.Key +".cs");
                if (!File.Exists(fileName))
                {
                    modelData = new ModelFileData();
                    modelData.fileName = fileName;
                    modelData.name = sqlData.Key;
                    modelData.sql = sqlData.Value;
                    modelData.nameSpace = nameSpace;
                    modelData.isDefaultTable = false;
                    Data.TableData tableData = new Data.TableData(modelData.name, modelData.sql);
                    dataUtility.SetTableColumnData(tableData);
                    sw = new NFinal.IO.StringWriter();
                    ModelTemplate.Render(sw, tableData);
                    modelData.content = sw.ToString();
                    modelFileDataList.Add(modelData);
                }
            }
            dataUtility.Close();
        }
    }
}
