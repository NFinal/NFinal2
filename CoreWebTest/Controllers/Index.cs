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
        private void Main()
        {

        }
        public override void After()
        {
            base.After();
        }
        public void INN()
        {
            this.ViewBag = new ViewBagModel();
            this.ViewBag.a = "2";
            
            ViewBag.ddd = "sd";
        }
        public override bool Before()
        {
            return base.Before();
        }
        [ViewBagMember]
        public string a = "lucas";
        [Action("Show")]
        [ViewBag(typeof(ViewBagModel))]
        public void Show(int a, parameterModel model)
        {
            ViewBag.cc2 = DateTime.Now;
            ViewBag.a = "23";
            //this.Write(a);
            //this.Write(ViewBag.a);
            //this.Write("hello!");
            this.Render();
        }
        private void Render(NFinal.IO.IWriter writer,object obj)
        {
            Index_Model.Show m = (Index_Model.Show)obj;
            CoreWebTest.Views.Index.Render(this, m);
        }
    }
    public class parameterModel
    {
        public string a;
        public int b;
    }
    public class ViewBagModel
    {
        public string a;
        public int b;
    }
}