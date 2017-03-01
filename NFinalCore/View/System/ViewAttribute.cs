using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewAttribute : Attribute
    {
        public string viewUrl;
        public ViewAttribute(string url) {
            viewUrl = url;
        }
    }
}
