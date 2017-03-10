using System;
using System.Collections.Generic;
using System.Text;

namespace NFinalCoreServer.Controllers
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
