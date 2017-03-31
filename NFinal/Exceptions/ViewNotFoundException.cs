using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Exceptions
{
    public class ViewNotFoundException:System.Exception
    {
        public ViewNotFoundException(string url)
            :base("模板未找到！模板路径为:" + url)
        {
        }
    }
}
