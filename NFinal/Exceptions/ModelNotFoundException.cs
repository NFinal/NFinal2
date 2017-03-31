using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Exceptions
{
    public class ModelNotFoundException: System.Exception
    {
        public ModelNotFoundException(string fullName)
            :base("ViewBag实体类未找到！\r\n实体类型为:" + fullName)
        {
        }
    }
}
