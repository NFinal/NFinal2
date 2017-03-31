using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Config.Plug
{
    public enum SessionStateMode
    {
        Off,
        Redis,
        InProc
    }
    public class SessionState
    {
        public string cookieName;
        public SessionStateMode mode;
        public string stateConnectionString;
        public int timeout;
        public string prefix;
    }
}
