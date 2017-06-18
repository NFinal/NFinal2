using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ActionExportAttribute:System.Attribute
    {
        public string methodName;
        public Type[] types;
        public Type viewBagType;
        /// <summary>
        /// Action导出接口
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="viewBagType">ViewBag类型</param>
        public ActionExportAttribute(string methodName,Type viewBagType=null)
        {
            if (viewBagType == null)
            {
                this.viewBagType = typeof(System.Dynamic.ExpandoObject);
            }
            else
            {
                this.viewBagType = viewBagType;
            }
            this.methodName = methodName;
            this.types = null;
        }
        public ActionExportAttribute(string methodName,Type[] types,Type viewBagType=null)
        {
            if (viewBagType == null)
            {
                this.viewBagType = typeof(System.Dynamic.ExpandoObject);
            }
            else
            {
                this.viewBagType = viewBagType;
            }
            this.methodName = methodName;
            this.types = types;
        }
    }
}
