using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Filter
{
    public interface IAuthorizationFilter
    {
        bool AuthorizationFilter<TContext, TRequest>(NFinal.Action.AbstractAction<TContext, TRequest> action);
    }
}
