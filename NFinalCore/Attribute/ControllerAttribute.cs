using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal
{
    /// <summary>
    /// Controller控制器名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =true)]
    public class ControllerAttribute : System.Attribute
    {
        public string Name { get; set; }
        public ControllerAttribute()
        {
            this.Name = string.Empty;
        }
        public ControllerAttribute(string name)
        {
            this.Name = name;
        }
    }

}
