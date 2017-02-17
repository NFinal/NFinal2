
using System;

namespace CSharpFormatter.Library.Exceptions
{
  class ParseFatalException : Exception
  {
    public ParseFatalException(String msg) : base(msg)
    {
    }
  }
}
