using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Razor
{
    public class BasePage<T>
    {
        public NFinal.IO.IWriter writer { get; }
        public T Model { get; set; }
        public virtual void Execute()
        {
            throw new NotImplementedException();
        }
    }
}