
using System;
using System.Text;
using System.Collections.Generic;
using CSharpFormatter.Library;
using CSharpFormatter.Library.Lexers;
using CSharpFormatter.Library.Parsers;

namespace CSharpFormatter
{
  class CSharpFormatterMain
  {
    public static void Main(String[] args)
    {
      try
      {
        var showHelp = false;
        if (0 == args.Length)
        {
          showHelp = true;
        }
        else if (1 == args.Length)
        {
          switch (args[0])
          {
            case @"--help":
              showHelp = true;
              break;
          }
        }
        if (showHelp)
        {
          const String urlAppveyorSvg = @"https://ci.appveyor.com/api/projects/status/n000mdm0bj1pxd35/branch/master?svg=true";
          const String urlAppveyor = @"https://ci.appveyor.com/project/rbtnn/csharp-formatter/branch/master";
          const String urlTravisSvg = @"https://travis-ci.org/rbtnn/csharp-formatter.svg?branch=master";
          const String urlTravis = @"https://travis-ci.org/rbtnn/csharp-formatter";
          IO.PrintNormal(@"");
          IO.PrintYellow(@"# CSharp-Formatter");
          IO.PrintHide(String.Format(@"[![Build status]({0})]({1})", urlAppveyorSvg, urlAppveyor));
          IO.PrintHide(String.Format(@"[![Build status]({0})]({1})", urlTravisSvg, urlTravis));
          IO.PrintNormal(@"");
          IO.PrintNormal(@"This is a C# formatter tool without Visual Studio using .NET Framework's csc.exe.  ");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"");
          IO.PrintGreen(@"## BUILD");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"__Windows__  ");
          IO.PrintNormal(@"");
          IO.PrintDarkGray(@"> MSBuild.exe msbuild.xml");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"__Mac__  ");
          IO.PrintNormal(@"");
          IO.PrintDarkGray(@"> xbuild msbuild.xml");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"");
          IO.PrintGreen(@"## USAGE");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"__Windows__  ");
          IO.PrintNormal(@"");
          IO.PrintDarkGray(@"> .\bin\CSharpFormatter.exe {options} {.cs file}");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"__Mac__  ");
          IO.PrintNormal(@"");
          IO.PrintDarkGray(@"> mono ./bin/CSharpFormatter.exe {options} {.cs file}");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"");
          IO.PrintGreen(@"## OPTIONS");
          IO.PrintNormal(@"");
          IO.PrintRed(@"### --tab");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"Use tabs when indenting.  ");
          IO.PrintNormal(@"(default: __off__)  ");
          IO.PrintNormal(@"");
          IO.PrintRed(@"### --indent {count}");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"Use {count} spaces per indent level.  ");
          IO.PrintNormal(@"(default: __2__)  ");
          IO.PrintNormal(@"");
          IO.PrintRed(@"### --encoding {encoding}");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"Read {.cs file} by {encoding}. {encoding} is one of following encodings.  ");
          IO.PrintNormal(@"(default: __utf-8n__)  ");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"    cp932");
          IO.PrintNormal(@"    shift_jis");
          IO.PrintNormal(@"    utf-16");
          IO.PrintNormal(@"    iso-2022-jp");
          IO.PrintNormal(@"    euc-jp");
          IO.PrintNormal(@"    utf-7");
          IO.PrintNormal(@"    utf-8");
          IO.PrintNormal(@"    utf-8n");
          IO.PrintNormal(@"");
          IO.PrintRed(@"### --overwrite");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"Do not print reformatted sources to standard output.  ");
          IO.PrintNormal(@"Overwrite {.cs file} By LF with CSharpFormatter's version.  ");
          IO.PrintNormal(@"(default: __off__)  ");
          IO.PrintNormal(@"");
          IO.PrintRed(@"### --crlf");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"Overwrite {.cs file} by CRLF. --overwrite only.  ");
          IO.PrintNormal(@"(default: __off__)  ");
          IO.PrintNormal(@"");
          IO.PrintRed(@"### --debug");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"Enable Debug mode.  ");
          IO.PrintNormal(@"(default: __off__)  ");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"");
          IO.PrintGreen(@"## VIM PLUGIN");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"Provide the following Vim command:  ");
          IO.PrintNormal(@"");
          IO.PrintDarkGray(@"> :CSFmt");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"This is to format C# codes in current buffer.  ");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"To install using Vundle, add the following to your vimrc:  ");
          IO.PrintNormal(@"");
          IO.PrintDarkGray(@"> :Plugin rbtnn/csharp-formatter");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"");
          IO.PrintGreen(@"## LICENSE");
          IO.PrintNormal(@"");
          IO.PrintNormal(@"Distributed under MIT License. See LICENSE.  ");
          IO.PrintNormal(@"");
        }
        else
        {
          var fis = new List<FormatterInfo>(){};
          var defaultFI = new FormatterInfo();
          Encoding enc = defaultFI.Enc;
          var indent = defaultFI.Indent;
          var useTab = defaultFI.UseTab;
          var overwrite = defaultFI.Overwrite;
          var crlf = defaultFI.Crlf;
          var sbkp = defaultFI.SpaceBetweenKeywordAndParan;
          var debugMode = defaultFI.DebugMode;

          var optionName = @"";
          foreach (var arg in args)
          {
            switch (arg)
            {
              case @"--overwrite":
                overwrite = true;
                break;
              case @"--indent":
                optionName = arg;
                break;
              case @"--tab":
                useTab = true;
                break;
              case @"--crlf":
                crlf = true;
                break;
              case @"--debug":
                debugMode = true;
                break;
              case @"--encoding":
                optionName = arg;
                break;
              default:
                switch (optionName)
                {
                  case @"":
                    if (IO.ExistsFile(arg))
                    {
                      fis.Add(new FormatterInfo(arg, enc, indent, useTab, overwrite, crlf, sbkp, debugMode));
                    }
                    else
                    {
                      throw new Exception(String.Format(@"No such file: '{0}'", arg));
                    }
                    break;
                  case @"--indent":
                    var temp = 0;
                    if (Int32.TryParse(arg, out temp))
                    {
                      indent = temp;
                    }
                    else
                    {
                      throw new Exception(String.Format(@"Invalid argument: '{0}'", arg));
                    }
                    break;
                  case @"--encoding":
                    switch (arg)
                    {
                      case @"cp932":
                      case @"shift_jis":
                        enc = Encoding.GetEncoding(932);
                        break;
                      case @"utf-16":
                        enc = Encoding.GetEncoding(1200);
                        break;
                      case @"iso-2022-jp":
                        enc = Encoding.GetEncoding(50222);
                        break;
                      case @"euc-jp":
                        enc = Encoding.GetEncoding(51932);
                        break;
                      case @"utf-7":
                        enc = Encoding.GetEncoding(65000);
                        break;
                      case @"utf-8":
                        enc = new UTF8Encoding(true);
                        break;
                      case @"utf-8n":
                        enc = new UTF8Encoding(false);
                        break;
                      default:
                        throw new Exception(String.Format(@"Invalid encoding: '{0}'", arg));
                    }
                    break;
                }
                optionName = @"";
                break;
            }
          }
          foreach (var fi in fis)
          {
            var ts = Lexer.LexerFile(fi);
            var psr = new Parser(ts, fi);
            var result = psr.Evalute();
            if (result.Success && fi.Overwrite)
            {
              if (fi.Crlf)
              {
                IO.WriteFileCRLF(fi.Path, result.Output, fi.Enc);
              }
              else
              {
                IO.WriteFileLF(fi.Path, result.Output, fi.Enc);
              }
            }
            else
            {
              IO.PrintRed(result.Output);
            }
          }
        }
      }
      catch (Exception ex)
      {
        IO.PrintRed(String.Format(@"{0}", ex.ToString()));
      }
    }
  }
}
