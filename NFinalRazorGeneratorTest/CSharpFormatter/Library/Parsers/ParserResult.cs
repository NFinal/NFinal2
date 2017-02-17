
using System;

namespace CSharpFormatter.Library.Parsers
{
  public class ParserResult
  {
    public readonly String Output;
    public readonly Boolean Success;

    public ParserResult(String output, Boolean success)
    {
      var len = output.Length;
      while (1 < len)
      {
        if (output[len - 2] == '\n' && output[len - 1] == '\n')
        {
          len--;
        }
        else
        {
          break;
        }
      }
      this.Output = output.Substring(0, len);
      this.Success = success;
    }
  }
}
