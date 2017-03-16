using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal
{
    public static class StringContainerExtension
    {
        public static StringContainer AsVar(this string obj)
        {
            return new StringContainer(obj);
        }
    }
}
