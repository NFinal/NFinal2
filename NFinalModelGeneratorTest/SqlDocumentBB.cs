using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
        public NFinal.Compile.DB.DBType dbType = NFinal.Compile.DB.DBType.MySql;
        public Dictionary<string, string> sqlList = new Dictionary<string, string>();
        public List<ModelFileData> modelFileDataList = new List<ModelFileData>();
        public SqlDocument(string filePath,string nameSpace,string sqlContent)
        {
            string fileDirectory= Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath));
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }
            StringWriter sw = null;
            ModelFileData modelData;
            NFinal.Compile.DB.Coding.DataUtility dataUtility = null;
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
                
                if (!string.IsNullOrEmpty(this.name) && !string.IsNullOrEmpty(this.connectionString) && !string.IsNullOrEmpty(this.providerName))
                {
                    
                    if (this.providerName.ToLower().IndexOf("mysql") > -1)
                    {
                        dataUtility = new NFinal.Compile.DB.Coding.MySQLDataUtility(this.connectionString);
                        dataUtility.GetAllTables(dataUtility.con.Database);
                    }
                    else if (this.providerName.ToLower().IndexOf("sqlite") > -1)
                    {
                        dataUtility = new NFinal.Compile.DB.Coding.SQLiteDataUtility(this.connectionString);
                        dataUtility.GetAllTables(dataUtility.con.Database);
                    }
                    else if (this.providerName.ToLower().IndexOf("sqlclient") > -1)
                    {
                        dataUtility = new NFinal.Compile.DB.Coding.SQLDataUtility(this.connectionString);
                        dataUtility.GetAllTables(dataUtility.con.Database);
                    }
                    else if (this.providerName.ToLower().IndexOf("Oracle") > -1)
                    {
                        dataUtility = new NFinal.Compile.DB.Coding.OracleDataUtility(this.connectionString);
                        dataUtility.GetAllTables(dataUtility.con.Database);
                    }
                    else if (this.providerName.ToLower().IndexOf("Npgsql") > -1)
                    {
                        dataUtility = new NFinal.Compile.DB.Coding.PostgreSqlDataUtility(this.connectionString);
                        dataUtility.GetAllTables(dataUtility.con.Database);
                    }
                    if (dataUtility != null)
                    {
                        foreach (NFinal.Compile.DB.Coding.Table table in dataUtility.tables)
                        {
                            try
                            {
                                modelData = new ModelFileData();
                                modelData.name = table.name;
                                modelData.nameSpace = nameSpace;
                                modelData.isDefaultTable = true;
                                modelData.fileName = System.IO.Path.Combine(fileDirectory, table.name + ".cs");
                                sw = new StringWriter();
                                var EntityModel = new NFinal.Compile.Template.App.Models.EntityModel
                                {
                                    nameSpace = nameSpace,
                                    table = table
                                };
                                var EntityTemplate = new NFinal.Compile.Template.App.Models.Entity();
                                EntityTemplate.Model = EntityModel;
                                sw.Write(EntityTemplate.TransformText());
                                modelData.content = sw.ToString();
                                sw.Dispose();
                                modelFileDataList.Add(modelData);
                            }
                            catch
                            { }
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
            string fileName = null;
            dataUtility.con.Open();
            foreach (var sqlData in sqlList)
            {
                try
                {
                    modelData = new NFinalModelGenerator.ModelFileData();
                    modelData.fileName = Path.Combine(fileDirectory, sqlData.Key + ".cs");
                    modelData.name = sqlData.Key;
                    modelData.sql = sqlData.Value;
                    modelData.nameSpace = nameSpace;
                    modelData.isDefaultTable = false;
                    sw = new StringWriter();
                    NFinal.Compile.DB.Coding.Table table = new NFinal.Compile.DB.Coding.Table();
                    table.name = sqlData.Key;

                    NFinal.Compile.SqlStatementSelectInfo selectStatement = new NFinal.Compile.SqlStatementSelectInfo(sqlData.Value, true);

                    NFinal.Compile.SqlStatementSelect select = new NFinal.Compile.SqlStatementSelect(sqlData.Value, dataUtility);
                    select.ParseSQL(ref selectStatement);
                    table.fields = select.GetFields(dataUtility, selectStatement);
                    var EntityModel = new NFinal.Compile.Template.App.Models.EntityModel
                    {
                        nameSpace = nameSpace,
                        table = table
                    };
                    var EntityTemplate = new NFinal.Compile.Template.App.Models.Entity();
                    EntityTemplate.Model = EntityModel;
                    sw.Write(EntityTemplate.TransformText());
                    modelData.content = sw.ToString();
                    sw.Dispose();
                    modelFileDataList.Add(modelData);
                }
                catch { }
            }
            dataUtility.con.Close();
        }
    }
}
