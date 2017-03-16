using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal
{
    /// <summary>
    /// 区域
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AreaAttribute : System.Attribute
    {
        public string Name { get; set; }
        public AreaAttribute(string name)
        {
            this.Name = name;
        }
    }
}
