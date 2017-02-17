using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;

namespace NFinal.Compile
{
    public class ConnectionCompiler
    {
        public struct DbOperationObject
        {
            public int index;
            public int length;
            public string dbVarName;
            public string conVarName;
            public bool isConOrTran;
        }
        //解析连接字符串
        //var con=Models.Common.GetConnection();
        public Dictionary<string, DbOperationObject> GetAllDbOperationObject(string csharpCode)
        {
            string connectionPattern = @"var\s*(\S+)\s*=\s*Models\s*.\s*(\S+)\s*.\s*GetConnection\s*\(\s*\)\s*;";
            Regex connectionReg = new Regex(connectionPattern);
            MatchCollection connectionMac = connectionReg.Matches(csharpCode);
            string connectionVarName=null;
            string transactionVarName = null;

            Dictionary<string, DbOperationObject> dbOpertaonObjects = new Dictionary<string, DbOperationObject>();
            DbOperationObject connectionObject;
            DbOperationObject transactionObject;
            if (connectionMac.Count > 0)
            {
                for (int i = 0; i < connectionMac.Count; i++)
                {
                    connectionVarName = connectionMac[i].Groups[1].Value;
                    connectionObject = new DbOperationObject();
                    connectionObject.index = connectionMac[i].Index;
                    connectionObject.length = connectionMac[i].Length;
                    connectionObject.dbVarName = connectionMac[i].Groups[2].Value;
                    connectionObject.conVarName=connectionVarName;
                    connectionObject.isConOrTran = true;
                    dbOpertaonObjects.Add(connectionVarName, connectionObject);

                    string transactionPattern = string.Format(@"var\s*(\S+)\s*=\s*{0}\s*.\s*GetTransaction\s*\(\s*\)\s*;", connectionVarName);
                    Regex transactionReg = new Regex(transactionPattern);
                    MatchCollection transactionMac = transactionReg.Matches(csharpCode);
                    if (transactionMac.Count > 0)
                    {
                        for (int j = 0; j < transactionMac.Count; j++)
                        {
                            transactionVarName = transactionMac[j].Groups[1].Value;
                            transactionObject = new DbOperationObject();
                            transactionObject.index = transactionMac[j].Index;
                            transactionObject.length = transactionMac[i].Length;
                            transactionObject.dbVarName = connectionObject.dbVarName;
                            transactionObject.conVarName=connectionObject.conVarName; 
                            transactionObject.isConOrTran = false;
                            dbOpertaonObjects.Add(transactionVarName,transactionObject);
                        }
                    }
                }
            }
            return dbOpertaonObjects;
        }
        public void TransAllGetOperationObject(Dictionary<string, DbOperationObject> dbOpertaonObjects,string csharpCode)
        {
            //int relative_position = 0;
            for (int i = 0; i < dbOpertaonObjects.Count; i++)
            { 
                
            }
        }
        private static Regex dbFuncitonRegex = new Regex(@"(?:(\S+)\s+)?(\S+)\s*=\s*Models\s*.\s*([^\s\.]+)\s*.\s*([^\s\.]+)\s*\(\s*(?:@?""([^""]*)""(?:,\s*([^\s,\)]+))?)?\s*\)(?:\s*.\s*([^\(\)\s;.]+)\s*\(\s*\))?\s*;");
    }
}