using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Config
{
    public interface IConfigSection
    {
        T GetConfig<T>(string key);
    }
}
