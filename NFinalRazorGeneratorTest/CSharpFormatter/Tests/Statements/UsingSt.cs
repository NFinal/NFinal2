
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_UsingSt
  {

    [Test]
    public void T1()
    {
      var fp = new ParseFunc[]{
        UsingSt.Singleton()
      };
      var input = @"    using System;";
      var expect = new String[]{
        @"using System;",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T2()
    {
      var fp = new ParseFunc[]{
        UsingSt.Singleton()
      };
      var input = @"   using NUnit.Framework;";
      var expect = new String[]{
        @"using NUnit.Framework;",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T3()
    {
      var fp = new ParseFunc[]{
        UsingSt.Singleton()
      };
      var input = @"   using System.IO.Path;";
      var expect = new String[]{
        @"using System.IO.Path;",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_Alias_1()
    {
      var fp = new ParseFunc[]{
        UsingSt.Singleton()
      };
      var input = @"    using A = System;";
      var expect = new String[]{
        @"using A = System;",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_Alias_2()
    {
      var fp = new ParseFunc[]{
        UsingSt.Singleton()
      };
      var input = @"   using A = NUnit.Framework;";
      var expect = new String[]{
        @"using A = NUnit.Framework;",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_Alias_3()
    {
      var fp = new ParseFunc[]{
        UsingSt.Singleton()
      };
      var input = @"   using A = System.IO.Path;";
      var expect = new String[]{
        @"using A = System.IO.Path;",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
