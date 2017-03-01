using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if (NET40 || NET451 || NET461)
using Microsoft.Owin;
#endif

namespace NFinal.Filter
{
#if (NET40 || NET451 || NET461)
    public interface IContextFilter:IBaseFilter<IOwinContext>
    {
    }
#endif
}
