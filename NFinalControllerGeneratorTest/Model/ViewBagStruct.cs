using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NFinalControllerGeneratorTest.Model
{
    public class ViewBagStruct
    {
        public List<KeyValuePair<string, string>> fieldList = null;
        public ViewBagStruct(MethodDeclarationSyntax action,SemanticModel model)
        {
            TypeInfo typeInfo;
            string NameString = null;
            string TypeString = null;
            List<IdentifierNameSyntax> NameList = null;
            fieldList = new List<KeyValuePair<string, string>>();
            var assignment = action.DescendantNodes().OfType<AssignmentExpressionSyntax>();
            foreach (var memberAccesss in assignment)
            {
                if (memberAccesss.Kind() == SyntaxKind.SimpleAssignmentExpression)
                {
                    NameList = memberAccesss.Left.ChildNodes().OfType<IdentifierNameSyntax>().ToList();
                    if (NameList.Count > 1 && NameList[0].Identifier.Text == "ViewBag")
                    {
                        NameString = NameList[1].Identifier.Text;
                        typeInfo = model.GetTypeInfo(memberAccesss.Right);
                        TypeString = typeInfo.Type.ToString();
                        fieldList.Add(new KeyValuePair<string,string>(NameString, TypeString));
                    }
                    
                }
            }
        }
    }
}
