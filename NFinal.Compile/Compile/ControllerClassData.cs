using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Compile
{
    /// <summary>
    /// 类实体类
    /// </summary>
    public class ControllerClassData
    {
        //类头开始,及长度
        public int start = 0;
        public int length = 0;

        //类内容开始,及内容
        public int position = 0;
        public string Content = string.Empty;
        public string AttributeString = string.Empty;
        public System.Collections.Generic.List<ControllerAttributeData> Attributes;
        public string baseUrl = string.Empty;
        public string name = string.Empty;
        public string baseName = string.Empty;

        public string fullName = string.Empty;
        //relativeName="/Manage/IndexController"
        public string relativeName = string.Empty;
        //RelativeDotName=".Manage.IndexController"
        public string relativeDotName = string.Empty;

        public System.Collections.Generic.List<ControllerMethodData> MethodDataList = new System.Collections.Generic.List<ControllerMethodData>();
    }
}
