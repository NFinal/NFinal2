using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFinalControllerGeneratorTest.Action
{
    public class Index:NFinal.OwinAction<NFinal.EmptyMasterPageModel,dynamic,object>
    {
        [ViewBagMember]
        public string c = "3";
        [ViewBagMember]
        public int d { get; set; }
        public void Default()
        {
            this.ViewBag.a = 1;
            ViewBag.b = "2";
            this.ViewBag.e = 2;
        }
        private void This()
        {

        }
    }
}
