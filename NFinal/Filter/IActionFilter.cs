using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Filter
{
    public interface IBeforeActionFilter
    {
        bool ActionFilter<TContext, TRequest>(NFinal.Action.AbstractAction<TContext, TRequest> action);
    }
    public interface IAfterActionFilter
    {
        bool ActionFilter<TContext, TRequest>(NFinal.Action.AbstractAction<TContext, TRequest> action);
    }
}
