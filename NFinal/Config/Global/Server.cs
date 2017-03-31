using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Config.Global
{
    public class Server
    {
        public string url;
        public string indexDocument;
        //public StaticContent staticContent;
    }
    public class StaticContent
    {
        public MimeMap[] mimeMap;
    }
    public class MimeMap
    {
        public string[] fileExtension;
        public string[] mimeType;
    }
}
