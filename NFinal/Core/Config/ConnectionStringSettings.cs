using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Config
{
    /// <summary>
    /// 数据库连接字符串设置类
    /// </summary>
    public class ConnectionStringSettings
    {
        public string Name;
        public string ConnectionString;
        public string ProviderName;
        public ConnectionStringSettings()
        { }
        public ConnectionStringSettings(string name, string connectionString)
        {
            this.Name = name;
        }
        public ConnectionStringSettings(string name, string connectionString, string providerName)
        {
            this.Name = name;
            this.ConnectionString = connectionString;
            this.ProviderName = providerName;
        }
    }
}
