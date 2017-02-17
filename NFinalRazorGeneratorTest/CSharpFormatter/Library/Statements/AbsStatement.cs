
using System;
using System.Text;
using CSharpFormatter.Library.Parsers;

namespace CSharpFormatter.Library.Statements
{
  public delegate StringBuilder ParseFunc(Parser psr, FormatterInfo fi);

  public abstract class AbsStatement<T> where T : AbsStatement<T>, new()
  {
    private static AbsStatement<T> singleton = new T();

    public static ParseFunc Singleton()
    {
      return singleton.Parse;
    }

    protected virtual StringBuilder Parse(Parser psr, FormatterInfo fi)
    {
      return new StringBuilder();
    }
  }
}
