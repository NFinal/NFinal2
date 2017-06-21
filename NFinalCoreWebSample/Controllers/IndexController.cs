using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NFinal;

namespace NFinalCoreWebSample.Controllers
{
    public class IndexController:BaseController
    {
        public void Index()
        {
            this.Write("Hello World!");
        }
    }
}
