using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Filter
{
    public interface IBaseFilter<TContext>
    {
        bool BaseFilter(TContext context);
    }
}
