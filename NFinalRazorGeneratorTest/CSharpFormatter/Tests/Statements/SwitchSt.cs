
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_SwitchSt
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        SwitchSt.Singleton()
      };
      var input = @" switch (aaa  )  { case @""TRUE"": if(true){continue;} case @""FALSE"": throw ex; default: break; } ";
      var expect = new String[]{
        @"switch (aaa)",
        @"{",
        @"  case @""TRUE"":",
        @"    if (true)",
        @"    {",
        @"      continue;",
        @"    }",
        @"  case @""FALSE"":",
        @"    throw ex;",
        @"  default:",
        @"    break;",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
