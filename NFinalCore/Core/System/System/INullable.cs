using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal
{
    public interface INullable
    {
        bool __Is_Null__
        {
            get;
            set;
        }
    }
}
