using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFinalControllerGenerator.Model
{
    public struct DeclareData
    {
        public string Type;
        public string Name;
        public bool IsAttribute;
        public override string ToString()
        {
            if (IsAttribute)
            {
                return $"\t{Type} {Name} {{get;set;}}";
            }
            else
            {
                return $"\t{Type} {Name};";
            }
        }
    }
}
