
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_NamespaceSt
  {
    [Test]
    public void T_1()
    {
      var fp = new ParseFunc[]{
        NamespaceSt.Singleton()
      };
      var input = @"  namespace    Hoge  .  Foo  {   }  ";
      var expect = new String[]{
        @"namespace Hoge.Foo",
        @"{",
        @"}",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
