using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Filter
{
    public interface IAfterActionFilter
    {
        bool ActionFilter<TContext, TRequest>(NFinal.Action.AbstractAction<TContext, TRequest> action);
    }
}
