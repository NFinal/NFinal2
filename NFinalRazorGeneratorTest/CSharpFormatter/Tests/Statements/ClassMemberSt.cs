
using System;
using NUnit.Framework;
using CSharpFormatter.Library.Statements;

namespace CSharpFormatter.Tests
{
  [TestFixture]
  class Test_ClassMemberSt
  {

    [Test]
    public void T_public()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"public String str;";
      var expect = new String[]{
        @"public String str;",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_private()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"private String str;";
      var expect = new String[]{
        @"private String str;",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_protected()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"protected String str;";
      var expect = new String[]{
        @"protected String str;",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_internal()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"internal String str;";
      var expect = new String[]{
        @"internal String str;",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_protected_internal()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"protected internal String str;";
      var expect = new String[]{
        @"protected internal String str;",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_none()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"String str;";
      var expect = new String[]{
        @"String str;",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_readonly()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"readonly String str;";
      var expect = new String[]{
        @"readonly String str;",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_static()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"static String str;";
      var expect = new String[]{
        @"static String str;",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_readonly_and_static()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"static readonly String str;";
      var expect = new String[]{
        @"static readonly String str;",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_initialize()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"public static String str = @""vim"";";
      var expect = new String[]{
        @"public static String str = @""vim"";",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_getter()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"public static String T{ get; }";
      var expect = new String[]{
        @"public static String T",
        @"{",
        @"  get;",
        @"}",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_getter_block()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"public static String T{ get{ return this.Sum;} }";
      var expect = new String[]{
        @"public static String T",
        @"{",
        @"  get",
        @"  {",
        @"    return this.Sum;",
        @"  }",
        @"}",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_getter_and_setter1()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"public static String T{ set;get{ return this.Sum;} }";
      var expect = new String[]{
        @"public static String T",
        @"{",
        @"  set;",
        @"  get",
        @"  {",
        @"    return this.Sum;",
        @"  }",
        @"}",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_getter_and_setter2()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"public static String T{ set{ this.Sum = value;}get{ return this.Sum;} }";
      var expect = new String[]{
        @"public static String T",
        @"{",
        @"  set",
        @"  {",
        @"    this.Sum = value;",
        @"  }",
        @"  get",
        @"  {",
        @"    return this.Sum;",
        @"  }",
        @"}",
      };
      TestUtils.Eq(pf, input, expect);
    }

    [Test]
    public void T_getter_and_setter3()
    {
      var pf = new ParseFunc[]{
        ClassMemberSt.Singleton()
      };
      var input = @"public static String T{ protected set{ this.Sum = value;}get{ return this.Sum;} }";
      var expect = new String[]{
        @"public static String T",
        @"{",
        @"  protected set",
        @"  {",
        @"    this.Sum = value;",
        @"  }",
        @"  get",
        @"  {",
        @"    return this.Sum;",
        @"  }",
        @"}",
      };
      TestUtils.Eq(pf, input, expect);
    }
  }
}
