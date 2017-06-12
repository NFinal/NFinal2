using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Filter
{
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class)]
    public class ParameterFilterAttribute:System.Attribute
    {
        public bool ParameterFilter(NameValueCollection parameters)
        {
            return true;
        }
    }
    public interface IParameterFilter
    {
        bool ParameterFilter(NameValueCollection parameters);
    }
}
