using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal;
using Dapper;

namespace NFinalCorePlug.Controllers
{
    [NFinal.ActionExport("UpdateA")]
    public class IndexController : BaseController<int>
    {
        [Code.UserCheck]
        [Code.UserAuthroization]
        [Code.AfterAction]
        public void Default(Code.User model)
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
 