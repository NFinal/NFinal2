using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewAttribute : System.Attribute
    {
        public string viewUrl;
        public ViewAttribute(string url) {
            viewUrl = url;
        }
    }
}
