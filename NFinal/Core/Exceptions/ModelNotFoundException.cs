using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Exceptions
{
    public class ModelNotFoundException: System.Exception
    {
        private string message = null;
        public override string Message { get { return message; } }
        public ModelNotFoundException(string fullName)
        {
            message = "ViewBag实体类未找到！\r\n实体类型为:" + fullName;
        }
    }
}
