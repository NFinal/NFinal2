using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Exceptions
{
    public class IConnectionNotImplementedException : NotImplementedException
    {
        public IConnectionNotImplementedException(Type type)
            :base(string.Format("控制器类型{0}.{1}必须继承NFinal.Action.IConnection接口，并重写GetDbConnection方法。", type.Namespace, type.Name))
        {

        }
    }
}