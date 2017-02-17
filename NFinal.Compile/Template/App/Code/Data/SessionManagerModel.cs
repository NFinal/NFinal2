using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Compile.Template.App.Code.Data
{
    public struct SessionManagerModel
    {
        public string project;
        public string app;
        public List<CSharpDeclaration> declarations;
        public string cookiePrefix;
        public string sessionPrefix;
    }
}
