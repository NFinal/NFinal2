
using System;
using System.Text;
using System.Collections.Generic;

namespace CSharpFormatter.Library.Parsers
{
  public class ParserStatus
  {
    public readonly Int32 TokenIndex;
    public readonly Int32 IndentLevel;
    public readonly List<String> Comments;

    public ParserStatus(Int32 tokenIndex, Int32 indentLevel, List<String> comments)
    {
      this.TokenIndex = tokenIndex;
      this.IndentLevel = indentLevel;
      this.Comments = comments;
    }
  }
}
