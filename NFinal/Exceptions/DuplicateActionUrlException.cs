﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Exceptions
{
    public class DuplicateActionUrlException:Exception
    {
        private string message = null;
        public override string Message { get { return message; } }
        public DuplicateActionUrlException(string currentControllerName,string currentMethodName,string controllerName,string methodName)
        {
            message =string.Format("重复的请求路径！{0}下的{1}方法与{2}下的{3}方法拥有相同的请求路径定义。",currentControllerName,currentMethodName,controllerName,methodName);
        }
    }
}