using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Compile
{
    /// <summary>
    /// csharp文件实体类
    /// </summary>
    public class ControllerFileData
    {
        public int start = 0;
        public int length = 0;
        public int position = 0;
        public string fileName = string.Empty;
        public string name = string.Empty;
        public string nameSpace = string.Empty;
        public string projectName = string.Empty;
        public string appName = string.Empty;
        public string csharpCode = string.Empty;
        public string Content = string.Empty;
        public Config config = null;

        public System.Collections.Generic.List<ControllerClassData> ClassDataList = new System.Collections.Generic.List<ControllerClassData>();
        public ControllerClassData GetClassData(string className)
        {
            if (ClassDataList.Count > 0)
            {
                for (int i = 0; i < ClassDataList.Count; i++)
                {
                    if (ClassDataList[i].name == className)
                    {
                        return ClassDataList[i];
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
