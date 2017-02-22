using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer.Controllers
{
    public class Index:BaseController<NFinal.EmptyMasterPageModel>
    {
        [Code.UserCheck]
        public void Default()
        {
            this.ViewBag.Message = "Hello World!";
            this.Render();
        }
    }
}
