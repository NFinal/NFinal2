
using System;
using System.Text;

namespace CSharpFormatter.Library
{
  public class FormatterInfo
  {
    public readonly String Path;
    public readonly Encoding Enc;
    public readonly Int32 Indent;
    public readonly Boolean UseTab;
    public readonly Boolean Overwrite;
    public readonly Boolean Crlf;
    public readonly Boolean SpaceBetweenKeywordAndParan;
    public readonly Boolean DebugMode;

    public FormatterInfo()
    {
      this.Path = @"";
      this.Enc = new UTF8Encoding(false);
      this.Indent = 2;
      this.UseTab = false;
      this.Overwrite = false;
      this.Crlf = false;
      this.SpaceBetweenKeywordAndParan = true;
      this.DebugMode = false;
    }

    public FormatterInfo(String p, Encoding e, Int32 i, Boolean t, Boolean w, Boolean crlf, Boolean sbkp, Boolean debugMode) : this()
    {
      this.Path = p;
      this.Enc = e;
      this.Indent = i;
      this.UseTab = t;
      this.Overwrite = w;
      this.Crlf = crlf;
      this.SpaceBetweenKeywordAndParan = sbkp;
      this.DebugMode = debugMode;
    }
  }
}
