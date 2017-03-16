using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false,Inherited =false)]
    public class ActionAttribute:System.Attribute
    {
        public ActionAttribute(string name){
            this.Name = name;
        }
        public string Name { get; set; }
    }
    
}
