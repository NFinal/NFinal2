using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal.Filter;
using NFinal.Owin;

namespace NFinal
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true,Inherited =true)]
    public abstract class EnvironmentFilterAttribute : System.Attribute, IEnvironmentFilter
    {
        public EnvironmentFilterAttribute()
        {
        }
        public abstract bool BaseFilter(IDictionary<string, object> environment);
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true,Inherited =true)]
    public abstract class OwinRequestFilterAttribute : System.Attribute, NFinal.Filter.IRequestFilter<NFinal.Owin.Request>
    {
        public OwinRequestFilterAttribute()
        {
        }
        public abstract bool RequestFilter(Request request);
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true,Inherited =true)]
    public abstract class ResponseFilterAttribute : System.Attribute, NFinal.Filter.IResponseFilter
    {
        public ResponseFilterAttribute()
        {
        }
        public abstract bool ResponseFilter(Response response);
    }
}
