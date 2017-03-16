using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Http
{
    /// <summary>
    /// Session
    /// </summary>
    public interface ISession
    {
        T Get<T>(string key);
        void Set<T>(string key, T t);
        object this[string key]
        {
            get;
            set;
        }
    }
}
