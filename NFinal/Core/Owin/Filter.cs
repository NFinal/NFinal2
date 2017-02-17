using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Owin
{
    public delegate Response ResponseFilter(Response response);
    public class Filter
    {
        public event ResponseFilter responseFilter;
        public void Main()
        {
            responseFilter += Filter_responseFilter;
        }

        private Response Filter_responseFilter(Response response)
        {
            return new Response();
        }
    }
}
