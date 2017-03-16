using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal
{
    public class ViewData
    {
        public ViewData(string path, object t)
        {
        }
        public string Path
        {
            get; set;
        }

        public object ViewBag
        {
            get; set;
        }
    }
}
