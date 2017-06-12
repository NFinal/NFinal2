using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NFinal;

namespace NFinalPlug.Controllers
{
    
    public class IndexController : NFinal.OwinAction
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
            //Controllers.IndexController_Model.Show moo = new IndexController_Model.Show();
            //Views.Index indexView = new Views.Index(this, moo);
            //indexView.Execute();
            this.Render("/NFinalPlug/Views/Index.cshtml");
        }
    }
    public class parameterModel
    {
        public string a;
        public int b;
    }
}