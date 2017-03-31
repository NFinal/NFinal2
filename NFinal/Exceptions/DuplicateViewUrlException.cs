using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Exceptions
{
    public class DuplicateViewUrlException:Exception
    {
        public DuplicateViewUrlException(string oldViewClassName,string viewClassName)
            :base(string.Format("重复的模板路径！{0}下的Render方法与{1}下的Render方法拥有相同的View定义。", oldViewClassName, viewClassName))
        {
        }
    }
}
