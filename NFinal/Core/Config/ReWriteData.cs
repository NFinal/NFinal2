using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Config
{
    /// <summary>
    /// url重写配置
    /// </summary>
    public struct ReWriteData
    {
        public List<RewriteDirectory> rewriteDirectoryList;
        public List<RewriteFile> rewriteFileList;
    }
}
