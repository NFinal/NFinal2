using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Exceptions
{
    public class IConnectionNotImplementedException : NotImplementedException
    {
        private string message = null;
        public override string Message { get { return message; } }
        public IConnectionNotImplementedException(Type type)
        {
            message = string.Format("控制器类型{0}.{1}必须继承NFinal.Action.IConnection接口，并重写GetDbConnection方法。",type.Namespace,type.Name);
        }
    }
}