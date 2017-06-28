using System;
using System.Collections.Generic;
using System.Text;

namespace $safeprojectname$.Controllers
{
    public class IndexController : BaseController
    {
        public void Index()
        {
            this.Write("Hello World!");
        }
        public void Template()
        {
            this.ViewBag.Message = "Hello World!";
            this.Render();
        }
    }
}
