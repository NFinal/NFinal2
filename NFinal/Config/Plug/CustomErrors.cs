using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Config.Plug
{
    public enum CustomErrorsMode
    {
        RemoteOnly,
        On,
        Off
    }
    public class Error
    {
        public int statusCode;
        public string redirect;
    }
    public class CustomErrors
    {
        public string defaultRedirect;
        public CustomErrorsMode mode;
        public Error[] errors;
    }
}
