using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal;

namespace NFinalCoreServer.Controllers
{
    public class Index:BaseController<NFinal.EmptyMasterPageModel>
    {
        [Code.UserCheck]
        public void Default()
        {
            this.ViewBag.Message = "Hello World!";
            this.ViewBag.Title = "Title";
            this.Render();
        }
        public void Ajax()
        {
            this.ViewBag.Message = "Hello Json!";
            this.ViewBag.id = 2;
            this.ViewBag.time = DateTime.Now;
            this.AjaxReturn();
        }
    }
}
