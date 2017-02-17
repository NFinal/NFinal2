using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Owin;

namespace NFinal.Filter
{
    public interface IContextFilter:IBaseFilter<IOwinContext>
    {
    }
}
