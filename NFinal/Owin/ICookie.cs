﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal
{
    /// <summary>
    /// Cookie接口
    /// </summary>
    public interface ICookie<TContext>
    {
        string SessionId
        {
            get;
        }
        string this[string key]
        {
            get;
            set;
        }
    }
}