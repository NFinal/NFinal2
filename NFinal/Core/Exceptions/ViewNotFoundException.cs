using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Exceptions
{
    public class ViewNotFoundException:System.Exception
    {
        private string message = null;
        public override string Message { get { return message; } }
        public ViewNotFoundException(string url)
        {
            message = "模板未找到！模板路径为:" + url;
        }
    }
}
