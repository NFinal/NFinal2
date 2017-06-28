using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Exceptions
{
    public class PlugConfigLoadException : System.Exception
    {
        public string content { get; set; }
        public PlugConfigLoadException(string content) : base("插件配置文件加载失败!请检查Json格式是否正确。")
        {
            this.content = content;
        }
    }
}
