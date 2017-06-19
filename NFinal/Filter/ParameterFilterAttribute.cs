using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Filter
{
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class)]
    public abstract class ParameterFilterAttribute:System.Attribute
    {
        public abstract bool ParameterFilter(NameValueCollection parameters);
    }
}
