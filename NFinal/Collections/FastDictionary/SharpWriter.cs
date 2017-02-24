using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFinal.Collections
{
    public class SharpWriter
    {
        public void WriteGetTempCode(StringBuilder sb, int charIndex)
        {
            sb.AppendLine("    #region GetTemp");
            sb.AppendLine("    //[0][1][2][3],[4][5][ ][ ],[ ][ ][ ][ ],");
            sb.AppendLine(string.Format("    if ({0} < length)", charIndex));
            sb.AppendLine("    {");
            sb.AppendLine(string.Format("        switch (length - {0})", charIndex));
            sb.AppendLine("        {");
            sb.AppendLine("            case 0: temp = *(long*)(pt + charIndex); break;");
            sb.AppendLine("            case 1: temp = *(short*)(pt + charIndex); break;");
            sb.AppendLine("            case 2: temp = *(int*)(pt + charIndex); break;");
            sb.AppendLine("            case 3: temp = *(long*)(pt + charIndex-1); break;");
            sb.AppendLine("            default: temp = *(long*)(pt + charIndex); break;");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("    else");
            sb.AppendLine("    {");
            sb.AppendLine("        temp = 0;");
            sb.AppendLine("    }");
            sb.AppendLine("    #endregion");
        }
        public void WriteCode(StringBuilder sb, CodeNode codeNode, int level)
        {
            level++;
            if (level == 1)
            {
                sb.AppendLine("int i = 0;");
                sb.AppendLine("int charIndex = 4;");
                sb.AppendLine("long temp = 0;");
                sb.AppendLine("fixed (char* p = key)");
                sb.AppendLine("{");
                sb.AppendLine("    char * pt = p;");
                WriteCode(sb, codeNode, level);
                sb.AppendLine("}");
            }
            else
            {
                if (codeNode != null)
                {
                    if (codeNode.nodeType == CodeNodeType.CompareCreaterThan)
                    {
                        WriteGetTempCode(sb, codeNode.charIndex);
                        sb.AppendLine(String.Format("if( temp>{0}){{", codeNode.compareValue));
                        WriteCode(sb, codeNode.ifCase, level);
                        sb.AppendLine("}");
                        sb.AppendLine("else");
                        sb.AppendLine("{");
                        WriteCode(sb, codeNode.elseCase, level);
                        sb.AppendLine("}");
                    }
                    else if (codeNode.nodeType == CodeNodeType.CompareLessThan)
                    {
                        WriteGetTempCode(sb, codeNode.charIndex);
                        sb.AppendLine(String.Format("if( temp<{0}){{", codeNode.compareValue));
                        WriteCode(sb, codeNode.ifCase, level);
                        sb.AppendLine("}");
                        sb.AppendLine("else");
                        sb.AppendLine("{");
                        WriteCode(sb, codeNode.elseCase, level);
                        sb.AppendLine("}");
                    }
                    else if (codeNode.nodeType == CodeNodeType.SetIndex)
                    {
                        sb.AppendLine(String.Format("    i={0};", codeNode.arrayIndex));
                    }
                }
            }
        }
    }
}
