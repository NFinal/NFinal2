using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Exceptions
{
    public class InvalidControllerNameException : System.Exception
    {
        private string message = null;
        public override string Message { get { return message; } }
        public InvalidControllerNameException(string nameSpace, string name)
        {
            message = "控制器名称错误！必须为Controller后缀。当前名称为：" + name + ",所在命名空间："+nameSpace;
        }
    }
}
