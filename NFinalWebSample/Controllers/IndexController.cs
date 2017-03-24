using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NFinal;

namespace NFinalWebSample.Controllers
{
    
    [SubDomain("www")]
    public class IndexController : NFinal.OwinAction<NFinal.User.User>
    {
        [GetHtml("/Index-{a}.html")]
        public void INN(int ac)
        {
            this.ViewBag.ab = "2";
            this.ViewBag.ddd = "sd";
            Write(a.ToString());
        }
        [ViewBagMember]
        public string a= "lucas";
        [Action("Show")]
        public void Show(int a, parameterModel model)
        {
            ViewBag.cc2 = DateTime.Now;
            ViewBag.a = "23";
            this.Render("/NFinalWebSample/Views/Index.cshtml");
        }
    }
    public class parameterModel
    {
        public string a;
        public int b;
    }
}