using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Compile.SqlTemplate.Model
{
    public struct Delete
    {
        public string functionName;
        public string varName;
        public bool hasGenericType;
        public string type;
        public bool isDeclaration;
        public string connectionVarName;
        public bool isTransaction;
        public string transactionVarName;
        public string dbName;
        public string sql;
        public List<DB.Coding.Field> fields;
        public List<SqlVarParameter> sqlVarParameters;
    }
}
