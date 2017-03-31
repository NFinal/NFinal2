using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace NFinal.Exceptions
{
    public class DataBaseNotSupportException:Exception
    {
        public DataBaseNotSupportException(string database):base(string.Format("数据库{0}不支持！", database))
        {
        }
    }
}
