using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;


namespace NFinal.Logs
{
    public class SQLiteLogger : ILogger
    {
        public static string SqliteLoggerConnectionString;
        public static void Configaure(Func<string,System.Data.IDbConnection> getSqliteFunc)
        {
            
        }
        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Trace(string message)
        {
            throw new NotImplementedException();
        }

        public void Trace(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
