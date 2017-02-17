using System;
using System.Collections.Generic;
using System.Web;

namespace NFinal.Compile
{
    /// <summary>
    /// 分析出csharp语句中相关的数据库函数,以及sql语句
    /// </summary>
    public struct DbFunctionData
    {
        public string sql;
        //Models.Entity.Common.TableName modelVarName;
        public string tableName;
        public string modelName;
        public bool hasSqlError;
        public string sqlError;
        public bool isDeclaration;
        public string type;
        public string varCommit;
        public string varName;
        public string transactionVarName;
        public string connectionVarName;
        public string connectionParameterName;
        public bool isTransaction;
        public string connectionName;
        public string functionName;
        public bool hasGenericType;
        public bool isSuperString;
        public string[] parameters;
        public string expression;
        public string convertMethodName;
        public int index;
        public int length;
        public System.Collections.Generic.List<DB.Coding.Table> tables;
        public System.Collections.Generic.List<DB.Coding.Field> fields;
        public System.Collections.Generic.List<NFinal.Compile.SqlVarParameter> sqlVarParameters;
    }
}