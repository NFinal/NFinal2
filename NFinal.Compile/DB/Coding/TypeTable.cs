//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :TypeTable.cs
//        Description :读取配置对应类型
//
//        created by Lucas at  2015-6-30`
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NFinal.Compile.DB.Coding
{
    /// <summary>
    /// 字段类型表
    /// </summary>
    public class TypeTable
    {
        public string fileString = "";
        public TypeTable(DB.DBType dbType)
        {
            switch (dbType)
            {
                case DBType.MySql: fileString = NFinal.Compile.SqlTemplate.SqlTemplate.mysql; break;
                case DBType.Sqlite: fileString = NFinal.Compile.SqlTemplate.SqlTemplate.sqlite; break;
                case DBType.SqlServer: fileString = NFinal.Compile.SqlTemplate.SqlTemplate.sqlserver; break;
                case DBType.Oracle: fileString = NFinal.Compile.SqlTemplate.SqlTemplate.oracle; break;
                case DBType.PostgreSql: fileString = NFinal.Compile.SqlTemplate.SqlTemplate.postgresql; break;
            }
        }
        public DB.Coding.JsonType GetJsonType(string JsonType)
        {
            switch (JsonType)
            {
                case "String":
                    return DB.Coding.JsonType.String;
                case "Bool":
                    return DB.Coding.JsonType.Bool;
                case "Time":
                    return DB.Coding.JsonType.Time;
                case "Object":
                    return DB.Coding.JsonType.Object;
                case "Number":
                    return DB.Coding.JsonType.Number;
                case "Base64":
                    return DB.Coding.JsonType.Base64;
                default:
                    return DB.Coding.JsonType.Object;
            }
        }
        public Dictionary<string, TypeLink> GetSqlTypeLinks()
        {
            Dictionary<string, TypeLink> links = new Dictionary<string, TypeLink>();
            System.IO.StringReader sr = new System.IO.StringReader(fileString);
            TypeLink link = new TypeLink();
            string[] row;
            string line;
            string[] GetMethod = null;
            line = sr.ReadLine();
            row = line.Split(',');
            bool isNumber = false;
            while (!string.IsNullOrEmpty(line))
            {
                row = line.Split(',');
                link = new TypeLink();
                //数据库中的类型
                link.sqlType = row[0].ToLower();
                //csharp 基本类型
                link.csharpType = row[1];
                link.isValueType = row[2] == "1";
                //reader.GetMethod();
                GetMethod = row[3].Split(':');
                link.GetMethodConvert = GetMethod[0];
                link.GetMethodName = GetMethod[1];
                link.GetMethodValue = GetMethod[2];
                //JsonType
                link.jsonType = row[4];
                link.dbType = row[6];
                //csharp System.Data 中的类型
                isNumber = int.TryParse(row[5],out link.dbTypeInt);
                if (isNumber)
                {
                    links.Add(link.sqlType, link);
                }
                line = sr.ReadLine();
            }
            sr.Close();
            return links;
        }
        public Dictionary<int, TypeLink> GetDBTypeLinks()
        {
            Dictionary<int, TypeLink> links = new Dictionary<int, TypeLink>();
            System.IO.StringReader sr = new System.IO.StringReader(fileString);
            TypeLink link = new TypeLink();
            string[] row;
            string line;
            string[] GetMethod = null;
            line = sr.ReadLine();
            row = line.Split(',');
            bool isNumber = false;
            while (!string.IsNullOrEmpty(line))
            {
                row = line.Split(',');
                link = new TypeLink();
                //数据库中的类型
                link.sqlType = row[0].ToLower();
                //csharp 基本类型
                link.csharpType = row[1];
                link.isValueType = row[2] == "1";
                //reader.GetMethod();
                GetMethod = row[3].Split(':');
                link.GetMethodConvert = GetMethod[0];
                link.GetMethodName = GetMethod[1];
                link.GetMethodValue = GetMethod[2];
                //JsonType
                link.jsonType = row[4];
                //csharp System.Data 中的类型
                isNumber = int.TryParse(row[5],out link.dbTypeInt);
                link.dbType = row[6];
                if (isNumber && !links.ContainsKey(link.dbTypeInt))
                {
                    links.Add(link.dbTypeInt, link);
                }
                line = sr.ReadLine();
            }
            sr.Close();
            return links;
        }
    }
}
