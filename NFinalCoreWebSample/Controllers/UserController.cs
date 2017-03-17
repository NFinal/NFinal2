using System;
using System.Collections.Generic;
using System.Text;
using NFinal;

namespace NFinalCoreWebSample.Controllers
{
    public class UserController:BaseController<NFinal.EmptyMasterPageModel>
    {
        public void Login()
        {
            ViewBag.a = 1;
            ViewBag.b = "";
            Render();
        }
    }
}
