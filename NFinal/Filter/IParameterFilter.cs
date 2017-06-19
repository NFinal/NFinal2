using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Filter
{
    public interface IParameterFilter
    {
        bool ParameterFilter(NameValueCollection parameters);
    }
}
