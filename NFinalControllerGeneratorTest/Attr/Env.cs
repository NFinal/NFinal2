using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFinalControllerGeneratorTest.Attr
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Env : Attribute
    {
        public Env(int a)
        {

        }
        public bool EnvironmentFilter(IDictionary<string, object> environment)
        {
            throw new NotImplementedException();
        }
    }
}
