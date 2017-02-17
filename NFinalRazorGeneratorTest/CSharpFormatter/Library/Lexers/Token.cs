
using System;

namespace CSharpFormatter.Library.Lexers
{
  public class Token
  {
    public readonly TokenType Type;
    public readonly String Text;
    public readonly String Path;
    public readonly Int32 Lnum;
    public readonly Int32 Col;
    public readonly String Line;

    private Token()
    {
      this.Type = TokenType.Unknown;
      this.Text = @"";
      this.Path = @"";
      this.Lnum = -1;
      this.Col = -1;
      this.Line = @"";
    }

    public Token(String str) : this()
    {
      this.Text = str;
    }

    public Token(TokenType type, String str) : this()
    {
      this.Type = type;
      this.Text = str;
    }

    public Token(TokenType type, String str, String path, Int32 lnum, Int32 col, String line) : this()
    {
      this.Type = type;
      this.Text = str;
      this.Path = path;
      this.Lnum = lnum;
      this.Col = col;
      this.Line = line;
    }

    public override String ToString()
    {
      return this.Text;
    }
  }
}
