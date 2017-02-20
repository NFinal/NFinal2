using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public class ViewBagAttribute:Attribute
    {
        public ViewBagAttribute(Type viewBagType)
        {
            this.ViewBagType = viewBagType;
        }
        public Type ViewBagType { get; }
    }
}
