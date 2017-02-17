using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Core.System.System
{
    public class Run
    {
        public static void R(IDictionary<string, object> env)
        {

        }
        public delegate string GetUrlDelegate(params StringContainer[] pars);
        public string GetUrl(params StringContainer[] pars)
        {
            return string.Empty;
        }
        public void Index()
        {
            GetUrl("", 4.5, 5.6);
        }
    }
}
