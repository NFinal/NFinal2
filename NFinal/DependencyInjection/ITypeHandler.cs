using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.DependencyInjection
{
    public interface ITypeHandler
    {
        RuntimeTypeHandle ImplementationTypeHandle { get; set; }
        void Configaure(params object[] options);
    }
}
