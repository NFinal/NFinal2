using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFinalControllerGenerator
{
    public class DeclareData
    {
        public List<string> AttributeList;
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
