using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Exceptions
{
    public class PlugAssemblyNotFoundException :System.Exception
    {
        public PlugAssemblyNotFoundException(string fileName) : base("插件程序集未找到!路径:"+fileName)
        {

        }
    }
}
