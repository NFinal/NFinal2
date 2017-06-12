using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Filter
{
    public interface IExceptionFilter
    {
        System.Exception ExceptionHandler { get; set; }

        void ExceptionFilter<TContext,TRequest>(NFinal.Action.AbstractAction<TContext,TRequest> action);
    }
}
