using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NFinal.Collections
{
    public class CodeNode
    {
        /// <summary>
        /// 对比的数字
        /// </summary>
        public long compareValue;
        /// <summary>
        /// if 语句节点
        /// </summary>
        public CodeNode ifCase;
        /// <summary>
        /// else语句节点
        /// </summary>
        public CodeNode elseCase;
        /// <summary>
        /// 节点的类型
        /// </summary>
        public CodeNodeType nodeType;
        /// <summary>
        /// 对比的列数
        /// </summary>
        public int charIndex;
        /// <summary>
        /// 当只有一个元素时，返回该元素的索引
        /// </summary>
        public int arrayIndex;
    }
}
