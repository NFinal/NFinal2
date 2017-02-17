using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.LanguageServices;
using NFinalControllerGenerator.Model;
using NFinalControllerGenerator.Url;

namespace NFinalControllerGeneratorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MSBuildWorkspace msbw = null;
            Project proj = null;
            if (msbw == null)
            {
                msbw = MSBuildWorkspace.Create(new Dictionary<string, string> { { "DesignTimeBuild", "true" },
                { "IntelliSenseBuild", "true" },
                { "BuildingInsideVisualStudio", "true" }});
            }
            string projectDirectory = AppContext.BaseDirectory.GetDirectoryName().GetDirectoryName().GetDirectoryName().GetDirectoryName();
            string projectName = Path.Combine(projectDirectory, "NFinalControllerGeneratorTest", "NFinalControllerGeneratorTest.csproj");
            if (proj == null)
            {
                proj = msbw.OpenProjectAsync(projectName).Result;
            }
            string controllerFileName = Path.Combine(projectDirectory, "NFinalControllerGeneratorTest", "Action","Index.cs");
            CSharpCompilation cSharpCompilation = null;
            Compilation compilation = null;
            if (cSharpCompilation == null)
            {
                if (proj.TryGetCompilation(out compilation))
                {
                    cSharpCompilation = (CSharpCompilation)compilation;
                }
                else
                {
                    cSharpCompilation = (CSharpCompilation)proj.GetCompilationAsync().Result;
                }
            }
            var document = proj.Documents.Single(doc => { return doc.FilePath == controllerFileName; });
            SyntaxTree tree = null;
            if (document != null)
            {
                tree = document.GetSyntaxTreeAsync().Result;
                SyntaxNode root = tree.GetRoot();
                SemanticModel model = cSharpCompilation.GetSemanticModel(tree);
                StructModel structModel = new NFinalControllerGenerator.Model.StructModel();
                StringWriter sw = new StringWriter();
                structModel.WriteDocument(sw, tree, model);
                string structModelString = sw.ToString();
                sw.Dispose();
                var controllers = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
                sw = new StringWriter();
                if (controllers.Count()>0)
                {
                    var controller = controllers.First();
                    var actions = controller.DescendantNodes().OfType<MethodDeclarationSyntax>();
                    foreach (var action in actions)
                    {
                        var assignmentList= action.DescendantNodes().OfType<AssignmentExpressionSyntax>();
                        foreach (var assignment in assignmentList)
                        {
                            bool isViewBag = false;
                            if (assignment.Kind() == SyntaxKind.SimpleAssignmentExpression)
                            {
                                if (assignment.Left.Kind() == SyntaxKind.SimpleMemberAccessExpression)
                                {
                                    //this.ViewBag.b | ViewBag.b
                                    MemberAccessExpressionSyntax memberAccessExpressionSyntax = (MemberAccessExpressionSyntax)(assignment.Left);
                                    //this.ViewBag
                                    if (memberAccessExpressionSyntax.Expression.Kind() == SyntaxKind.SimpleMemberAccessExpression)
                                    {
                                        //this.ViewBag
                                        MemberAccessExpressionSyntax thisMemberAccessExpressionSyntax 
                                            = (MemberAccessExpressionSyntax)(memberAccessExpressionSyntax.Expression);
                                        if (thisMemberAccessExpressionSyntax.Name.Identifier.Text == "ViewBag"
                                            && thisMemberAccessExpressionSyntax.Expression.Kind()==SyntaxKind.ThisExpression)
                                        {
                                            var stringName = memberAccessExpressionSyntax.Name.Identifier.Text;
                                            var typeInfo = model.GetTypeInfo(assignment.Right);
                                            var typeName = typeInfo.Type.ToString();
                                        }
                                    }
                                    //ViewBag
                                    if (memberAccessExpressionSyntax.Expression.Kind() == SyntaxKind.IdentifierName)
                                    {
                                        IdentifierNameSyntax leftIdentifierNameSyntax
                                            = (IdentifierNameSyntax)(memberAccessExpressionSyntax.Expression);
                                        if (leftIdentifierNameSyntax.Identifier.Text == "ViewBag")
                                        {
                                            var stringName = memberAccessExpressionSyntax.Name.Identifier.Text;
                                            var typeInfo = model.GetTypeInfo(assignment.Right);
                                            var typeName = typeInfo.Type.ToString();
                                        }
                                    }
                                }
     
                                //var NameList= assignment.Left.ChildNodes().OfType<IdentifierNameSyntax>().ToList();
                                //if (NameList.Count>1 && NameList[0].Identifier.Text == "ViewBag")
                                //{
                                    
                                //    string Name= NameList[1].Identifier.Text;
                                //}
                                //var typeInfo = model.GetTypeInfo(assignment.Right);
                                //var typeName = typeInfo.Type.ToString();
                            }
                        }
                        NFinalControllerGenerator.Execute.ExecuteMethod executeMethod = new NFinalControllerGenerator.Execute.ExecuteMethod();
                        executeMethod.WriteMethod(sw, model, controller, action);
                        string result = sw.ToString();
                        UrlParser parser = new UrlParser(model, controller, action);
                    }
                }
            }
        }
    }
    public static class StringExtension
    {
        public static string GetDirectoryName(this string filePath)
        {
            return System.IO.Path.GetDirectoryName(filePath);
        }
    }
}
