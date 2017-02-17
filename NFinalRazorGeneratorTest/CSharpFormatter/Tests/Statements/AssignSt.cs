
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_AssignSt
  {
    [Test]
    public void T_01()
    {
      var fp = new ParseFunc[]{
        AssignSt.Singleton()
      };
      var input = @"x = 1 + 4 + f(4, g());";
      var expect = new String[]{
        @"x = 1 + 4 + f(4, g());",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_02()
    {
      var fp = new ParseFunc[]{
        AssignSt.Singleton()
      };
      var input = @"x += 1 + 4 + f(4, g());";
      var expect = new String[]{
        @"x += 1 + 4 + f(4, g());",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_03()
    {
      var fp = new ParseFunc[]{
        AssignSt.Singleton()
      };
      var input = @"x -= 1 + 4 + f(4, g());";
      var expect = new String[]{
        @"x -= 1 + 4 + f(4, g());",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_04()
    {
      var fp = new ParseFunc[]{
        AssignSt.Singleton()
      };
      var input = @"x *= 1 + 4 + f(4, g());";
      var expect = new String[]{
        @"x *= 1 + 4 + f(4, g());",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_05()
    {
      var fp = new ParseFunc[]{
        AssignSt.Singleton()
      };
      var input = @"x /= 1 + 4 + f(4, g());";
      var expect = new String[]{
        @"x /= 1 + 4 + f(4, g());",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_06()
    {
      var fp = new ParseFunc[]{
        AssignSt.Singleton()
      };
      var input = @"x %= 1 + 4 + f(4, g());";
      var expect = new String[]{
        @"x %= 1 + 4 + f(4, g());",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_07()
    {
      var fp = new ParseFunc[]{
        AssignSt.Singleton()
      };
      var input = @"x ^= 1 + 4 + f(4, g());";
      var expect = new String[]{
        @"x ^= 1 + 4 + f(4, g());",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_08()
    {
      var fp = new ParseFunc[]{
        AssignSt.Singleton()
      };
      var input = @"x &= 1 + 4 + f(4, g());";
      var expect = new String[]{
        @"x &= 1 + 4 + f(4, g());",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_09()
    {
      var fp = new ParseFunc[]{
        AssignSt.Singleton()
      };
      var input = @"x |= 1 + 4 + f(4, g());";
      var expect = new String[]{
        @"x |= 1 + 4 + f(4, g());",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
