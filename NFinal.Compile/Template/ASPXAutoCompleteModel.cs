using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Compile.Template
{
    public struct ASPXAutoCompleteModel
    {
        public string aspxPageNameSpace;
        public string aspxPageClassName;
        public string baseName;
        public string StructDatas;
        public List<ControllerParameterData> parameterDataList;
        public string BaseClassName;
        public string methodName;
        public List<DbFunctionData> functionDataList;
        public List<CSharpDeclaration> csharpDeclarationList;
    }
}
