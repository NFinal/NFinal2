using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false,Inherited =false)]
    public class ActionAttribute:Attribute
    {
        public ActionAttribute(string name){
            this.Name = name;
        }
        public string Name { get; set; }
    }
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false,Inherited =false)]
    public class IndexAttribute : Attribute
    {
        public IndexAttribute() { }
    }
}
