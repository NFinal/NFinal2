using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Config
{
    /// <summary>
    /// 数据库连接字符串集合类
    /// </summary>
    public class ConnectionStringSettingsCollection : Dictionary<string, ConnectionStringSettings>
    {
        public ConnectionStringSettingsCollection()
        {

        }
        public void Add(ConnectionStringSettings settings)
        {
            this.Add(settings.Name, settings);
        }
    }
}
