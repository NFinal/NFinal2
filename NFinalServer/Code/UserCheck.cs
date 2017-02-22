using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal.Owin;

namespace NFinalServer.Code
{
    public class UserCheck : NFinal.Filter.IRequestFilter<NFinal.Owin.Request>
    {
        public bool RequestFilter(Request request)
        {
            return true;
        }
    }
}
