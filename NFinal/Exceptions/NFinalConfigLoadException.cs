using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Exceptions
{
    public class NFinalConfigLoadException : System.Exception
    {
        public string content { get; set; }
        public NFinalConfigLoadException(string content):base("nfinal配置文件加载失败!请检查Json格式是否正确。")
        {
            this.content = content;
        }
    }
}
