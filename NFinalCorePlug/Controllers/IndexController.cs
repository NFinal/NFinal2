using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal;
using Dapper;

namespace NFinalCorePlug.Controllers
{
    [NFinal.ActionExport("UpdateA")]
    public class IndexController : BaseController
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
        public void SetSession()
        {
            Code.User user = new Code.User();
            user.Name = "lucas";
            this.Session.SetUser(user);
        }
        public void WriteSession()
        {
            var user = this.Session.GetUser<Code.User>();

            Write(user.Name);

        }
    }
}
 