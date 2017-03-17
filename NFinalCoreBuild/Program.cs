using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace NFinalBuild
{
    class Program
    {
        /// <summary>
        /// 待写入文件的log列表
        /// </summary>
        static List<string> Logs = new List<string>();

        /// <summary>
        /// 输出文件，成功与否
        /// </summary>
        static Dictionary<string, bool> OutputFiles = new Dictionary<string, bool>();

        static void Main(string[] args)
        {
            //命令行参数解析器
            CommandLineArgumentParser arguments = CommandLineArgumentParser.Parse(args);

            if (arguments.Has(ConfigInfo.Help) || arguments.Has(ConfigInfo.Question))
            {
                string helpFile =Resource.log;
                string[] contents = helpFile.Split('\n');
                foreach (string content in contents)
                {
                    Console.WriteLine(content);
                }

                return;
            }

            //解决方案路径
            string solutionUrl;
            if (arguments.Has(ConfigInfo.SolutionUrl))
            {
                solutionUrl = arguments.Get(ConfigInfo.SolutionUrl).Next;
            }
            else
            {
                solutionUrl = GetAppSetting(ConfigInfo.SolutionUrl);
            }

            //输出目录
            string outputDir;
            if (arguments.Has(ConfigInfo.OutputDir))
            {
                outputDir = arguments.Get(ConfigInfo.OutputDir).Next;
            }
            else
            {
                outputDir = GetAppSetting(ConfigInfo.OutputDir);
            }

            //编译属性
            string properties;
            if (arguments.Has(ConfigInfo.Properties))
            {
                properties = arguments.Get(ConfigInfo.Properties).Next;
            }
            else
            {
                properties = GetAppSetting(ConfigInfo.Properties);
            }

            Dictionary<string, string> keyValues;
            if (!string.IsNullOrEmpty(properties))
            {
                keyValues = new Dictionary<string, string>();
                IEnumerable<string> props = properties.Split(';').Where(t => !string.IsNullOrWhiteSpace(t));
                foreach (var item in props)
                {
                    string[] prop = item.Split('=');
                    keyValues.Add(prop[0], prop[1]);
                }
            }
            else
            {
                keyValues = null;
            }

            string logFile;
            if (arguments.Has(ConfigInfo.LogFile))
            {
                logFile = arguments.Get(ConfigInfo.LogFile).Next;
            }
            else
            {
                logFile = GetAppSetting(ConfigInfo.LogFile);
            }

            if (!File.Exists(solutionUrl))
            {
                AddFormatPrint("The file specified does not exist.");
                AddFormatPrint("FileName:" + solutionUrl);
            }
            else
            {
                AddFormatPrint("Start building solutions");
                AddFormatPrint();

                AddFormatPrint("Check output directory exists");
                if (!Directory.Exists(outputDir))
                {
                    AddFormatPrint("Create output directory:");
                    AddFormatPrint(outputDir);
                    Directory.CreateDirectory(outputDir);
                    AddFormatPrint("Output directory has been created successfully");
                }
                else
                {
                    AddFormatPrint("Output directory already exists");
                }
                AddFormatPrint();

                AddFormatPrint("Start compilation solution");
                AddFormatPrint();
                bool success = CompileSolution(solutionUrl, outputDir, keyValues);
                AddFormatPrint();

                if (success)
                {
                    AddFormatPrint("Compilation completed successfully.");
                }
                else
                {
                    AddFormatPrint("Compilation failed.");
                }
            }

            foreach (string fullPathName in OutputFiles.Where(t => t.Value == false).Select(t => t.Key))
            {
                try
                {
                    File.Delete(fullPathName);
                }
                catch
                {
                }
            }
            File.WriteAllLines(logFile, Logs);

#if DEBUG
            AddFormatPrint("Press the any key to exit.");
            Console.ReadKey();
#endif
        }

        /// <summary>
        /// 编译解决方案和输出项目bin文件
        /// </summary>
        /// <param name="solutionUrl"></param>
        /// <param name="outputDir"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        private static bool CompileSolution(string solutionUrl, string outputDir, Dictionary<string, string> keyValues = null)
        {
            bool success = true;

            MSBuildWorkspace workspace;
            if (keyValues != null && keyValues.Any())
            {
                workspace = MSBuildWorkspace.Create(keyValues);
            }
            else
            {
                workspace = MSBuildWorkspace.Create();
            }

            Solution solution = workspace.OpenSolutionAsync(solutionUrl).Result;

            ProjectDependencyGraph projectGraph = solution.GetProjectDependencyGraph();
            foreach (ProjectId projectId in projectGraph.GetTopologicallySortedProjects())
            {
                Project project = solution.GetProject(projectId);
                AddFormatPrint("Building: {0}", project.FilePath);
                try
                {
                    Compilation projectCompilation = project.GetCompilationAsync().Result;
                    if (null != projectCompilation && !string.IsNullOrEmpty(projectCompilation.AssemblyName))
                    {
                        string fileName = string.Format("{0}.dll", projectCompilation.AssemblyName);
                        string fullPathName = string.Format("{0}\\{1}", outputDir, fileName);
                        if (!OutputFiles.ContainsKey(fullPathName))
                        {
                            OutputFiles.Add(fullPathName, true);
                        }

                        var diagnostics = projectCompilation.GetDiagnostics();
                        var warnDiagnostics = diagnostics.Where(x => x.Severity == DiagnosticSeverity.Warning).ToArray();
                        var errorDiagnostics = diagnostics.Where(x => x.Severity == DiagnosticSeverity.Error).ToArray();

                        foreach (var e in errorDiagnostics.Concat(warnDiagnostics).ToArray())
                        {
                            AddFormatPrint("{0}: {1}", e.Severity.ToString(), e.ToString());
                        }

                        if (errorDiagnostics.Any())
                        {
                            OutputFiles[fullPathName] = false;
                            AddFormatPrint("Build failed.");
                            success = false;
                        }
                        else
                        {
                            AddFormatPrint("Build successfully.");

                            using (var stream = new MemoryStream())
                            {
                                EmitResult result = projectCompilation.Emit(stream);
                                AddFormatPrint("{0}  -->  {1}", project.Name, fullPathName);
                                if (result.Success)
                                {
                                    using (FileStream file = File.Create(fullPathName))
                                    {
                                        stream.Seek(0, SeekOrigin.Begin);
                                        stream.CopyTo(file);
                                    }
                                    AddFormatPrint("Output successfully.");
                                }
                                else
                                {
                                    OutputFiles[fullPathName] = false;
                                    AddFormatPrint("Output failed.");
                                    success = false;
                                }
                            }
                        }
                        AddFormatPrint();
                    }
                    else
                    {
                        AddFormatPrint("Build failed. {0}", project.FilePath);
                        success = false;
                    }
                }
                catch (AggregateException ex)
                {
                    foreach (var ie in ex.InnerExceptions)
                    {
                        AddFormatPrint(ie.Message);
                    }
                    success = false;
                }
                catch (Exception ex)
                {
                    AddFormatPrint(ex.Message);
                    success = false;
                }
                AddFormatPrint();
            }

            return success;
        }

        /// <summary>
        /// 添加消息记录和打印消息
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        private static void AddFormatPrint(string format = "", params object[] args)
        {
            if (format == string.Empty)
            {
                Logs.Add(string.Empty);
                Console.WriteLine();
            }
            else
            {
                string log = string.Format(format, args);
                Logs.Add(log);
                Console.WriteLine(log);
            }
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? ConfigurationManager.AppSettings[ConfigInfo.KeyValues[key]];
        }
    }

    public struct ConfigInfo
    {
        /// <summary>
        /// 解决方案路径
        /// </summary>
        public const string SolutionUrl = "-s";

        /// <summary>
        /// 输出目录
        /// </summary>
        public const string OutputDir = "-o";

        /// <summary>
        /// 编译属性
        /// </summary>
        public const string Properties = "-p";

        /// <summary>
        /// 日志文件名称
        /// </summary>
        public const string LogFile = "-l";

        /// <summary>
        /// 帮助
        /// </summary>
        public const string Help = "-h";

        /// <summary>
        /// 提问
        /// </summary>
        public const string Question = "-?";

        /// <summary>
        /// 全称键值对
        /// </summary>
        public static readonly Dictionary<string, string> KeyValues = new Dictionary<string, string> { { SolutionUrl, "solutionUrl" }, { OutputDir, "outputDir" }, { Properties, "properties" }, { LogFile, "logFile" } };
    }
}