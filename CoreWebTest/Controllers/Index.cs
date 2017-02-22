using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NFinal.Owin;

namespace CoreWebTest.Controllers
{
    
    [SubDomain("www")]
    public class Index : NFinal.OwinAction<NFinal.EmptyMasterPageModel, dynamic>
    {
        [GetHtml("/Index-{a}.html")]
        public void INN(int a)
        {
            this.ViewBag.a = "2";
            ViewBag.ddd = "sd";
            Write(a.ToString());
        }
        [ViewBagMember]
        public string a = "lucas";
        [Action("Show")]
        public void Show(int a, parameterModel model)
        {
            ViewBag.cc2 = DateTime.Now;
            ViewBag.a = "23";
            this.Render("/CoreWebTest/Views/Index.cshtml");
        }
    }
    public class parameterModel
    {
        public string a;
        public int b;
    }
}