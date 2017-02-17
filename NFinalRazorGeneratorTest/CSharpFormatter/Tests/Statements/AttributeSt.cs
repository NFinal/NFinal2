
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_Attribute
  {
    [Test]
    public void T_00()
    {
      var fp = new ParseFunc[]{
        AttributeSt.Singleton()
      };
      var input = @"[Test]";
      var expect = new String[]{
        @"[Test]",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_01()
    {
      var fp = new ParseFunc[]{
        AttributeSt.Singleton()
      };
      var input = @"[Hoge.Foo]";
      var expect = new String[]{
        @"[Hoge.Foo]",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_02()
    {
      var fp = new ParseFunc[]{
        AttributeSt.Singleton()
      };
      var input = @"[return:0]";
      var expect = new String[]{
        @"[return: 0]",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_03()
    {
      var fp = new ParseFunc[]{
        AttributeSt.Singleton()
      };
      var input = @"[Conditional(""DEBUG"")]";
      var expect = new String[]{
        @"[Conditional(""DEBUG"")]",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_04()
    {
      var fp = new ParseFunc[]{
        AttributeSt.Singleton()
      };
      var input = @"[Conditional(""DEBUG""),Conditional(""TEST"")]";
      var expect = new String[]{
        @"[Conditional(""DEBUG""), Conditional(""TEST"")]",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_05()
    {
      var fp = new ParseFunc[]{
        AttributeSt.Singleton()
      };
      var input = @"[assembly:DllImport(""msvcrt.dll"")]";
      var expect = new String[]{
        @"[assembly: DllImport(""msvcrt.dll"")]",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_06()
    {
      var fp = new ParseFunc[]{
        AttributeSt.Singleton()
      };
      var input = @"[AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct)]";
      var expect = new String[]{
        @"[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]",
      };
      TestUtils.Eq(fp, input, expect);
    }

    [Test]
    public void T_07()
    {
      var fp = new ParseFunc[]{
        AttributeSt.Singleton()
      };
      var input = @"[Author(""ami"",age=24)]";
      var expect = new String[]{
        @"[Author(""ami"", age = 24)]",
      };
      TestUtils.Eq(fp, input, expect);
    }
  }
}
