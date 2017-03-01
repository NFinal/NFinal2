using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Filter
{
    public class FilterHelper
    {
        public static bool BaseFilter<TContext>(IBaseFilter<TContext>[] filters,TContext context)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (!filter.BaseFilter(context))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
            return true;
        }
        public static bool RequestFilter<TContext,TRequest>(IRequestFilter<TRequest>[] filters,TContext context,TRequest request)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (!filter.RequestFilter(request))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
            return true;
        }
        public static bool ResponseFilter(IResponseFilter[] filters, NFinal.Owin.Response response)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (!filter.ResponseFilter(response))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
            return true;
        }
    }
}
