using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Middleware.Config
{
    public enum Mode
    {
        On,
        Off
    }
    public class CustomErrors
    {
        public Mode mode = Mode.Off;
        public string defaultRedirect = string.Empty;
    }
}
