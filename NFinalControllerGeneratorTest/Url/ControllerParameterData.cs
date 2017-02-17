using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalControllerGenerator.Url
{
    /// <summary>
    /// 方法参数类
    /// </summary>
    public class ControllerParameterData
    {
        public string parameterCommit = string.Empty;
        public int position = 0;
        public string type = string.Empty;
        public bool isArray = false;
        public string name = string.Empty;
        public bool isUrlParameter = false;
        public int urlParameterIndex = 1;
        public bool hasDefaultValue = false;
        public string defaultValue = string.Empty;
        public string getParamterCode = string.Empty;
        public string AttributeString = string.Empty;
        public System.Collections.Generic.List<ControllerAttributeData> Attributes = null;
    }
}
