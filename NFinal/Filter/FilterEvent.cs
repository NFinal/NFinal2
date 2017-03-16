using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Filter
{
    public delegate bool EnvironmentFilter(IDictionary < string, object > environment);
    public delegate bool RequestFilter(NFinal.Owin.Request request);
    public delegate string ResponseFilter(NFinal.Owin.Response response);
}
