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
        public ActionExportAttribute(string methodName,Type viewBagType)
        {
            this.methodName = methodName;
            this.types = null;
        }
        public ActionExportAttribute(string methodName,Type[] types,Type viewBagType)
        {
            this.methodName = methodName;
            this.types = types;
        }
    }
}
