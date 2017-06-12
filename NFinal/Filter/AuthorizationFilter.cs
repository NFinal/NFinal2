using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Filter
{ 
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class)]
    public class AuthorizationFilterAttribute:System.Attribute,NFinal.Filter.IAuthorizationFilter
    {
        public bool AuthorizationFilter<TContext, TRequest>(NFinal.Action.AbstractAction<TContext, TRequest> action)
        {
            return true;
        }
    }
}
