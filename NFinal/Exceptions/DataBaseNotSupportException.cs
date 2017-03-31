using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace NFinal.Exceptions
{
    public class DataBaseNotSupportException:Exception
    {
        private string message = null;
        public override string Message { get { return message; } }
        public DataBaseNotSupportException(string database)
        {
            message = string.Format("数据库{0}不支持！", database);
        }
    }
}
