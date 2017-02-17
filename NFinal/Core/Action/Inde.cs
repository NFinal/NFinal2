using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Core.Action
{
    public class BaseController<TViewBag> : NFinal.OwinAction<TViewBag,object>
    {
        
    }
    #region 控制器文件
    public class Index : BaseController<dynamic>
    {
        public void ShowA()
        {
            ViewBag.a = 1;
            ViewBag.b = "a";
            this.AjaxReturn();
        }
        public void ShowB()
        {
            ViewBag.c = 1;
            ViewBag.d = 2;
            this.Render();
        }
    }
    #endregion

    #region ShowA Action文件
    public class Index_ShowA_Model
    {
        public int a = 0;
        public string b = "";
    }
    public class Index_ShowA_Action : OwinAction<Index_ShowA_Model,object>
    {
        public void ShowA()
        {
            ViewBag.a = 1;
            ViewBag.b = "a";
            this.AjaxReturn();
        }
    }
    #endregion
    #region ShowA JSON文件
    /*
    var Index_ShowA_Model = {
        "a":0,
        "b":""
    };
    */
    #endregion

    #region ShowB Action文件
    public class Index_ShowB_Model
    {
        public string c = "";
        public float d = 0;
    }
    public class Index_ShowB_Action : OwinAction<Index_ShowB_Model,object>
    {
        public void ShowB()
        {
            ViewBag.c = "1";
            ViewBag.d = 2;
            this.Render();
        }
    }
    #endregion
    #region ShowB JSON文件
    /*
    var Index_ShowA_Model = {
        "c":"",
        "d":0
    };
    */
    #endregion

}
