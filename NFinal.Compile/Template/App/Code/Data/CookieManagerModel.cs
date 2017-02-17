using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Compile.Template.App.Code.Data
{
    public struct CookieManagerModel
    {
        public string project;
        public string app;
        public List<CSharpDeclaration> declarations;
        public string cookiePrefix;
    }
}
