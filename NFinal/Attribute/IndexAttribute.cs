using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class IndexAttribute : System.Attribute
    {
        public IndexAttribute() { }
    }
}
