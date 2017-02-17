using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;

namespace NFinalControllerGenerator.Model
{
    public class GetFieldsUtility
    {
        public static void BuildStruct(TextWriter sw, List<KeyValuePair<string, DeclareData>> declares)
        {
            foreach (var de in declares)
            {
                sw.Write("\t\t");
                sw.Write("public ");
                sw.Write(de.Value.Type);
                sw.Write(" ");
                sw.Write(de.Value.Name);
                if (de.Value.IsAttribute)
                {
                    sw.WriteLine("{get;set;}");
                }
                else
                {
                    sw.WriteLine(";");
                }
            }
        }
        public static void  GetLocalVars(MethodDeclarationSyntax methodT, SemanticModel model, ref Dictionary<string, DeclareData> declares)
        {
            var methodSy = model.GetDeclaredSymbol(methodT);
            DeclareData data;
            TypeInfo typeInfo;
            var assignmentList = methodT.DescendantNodes().OfType<AssignmentExpressionSyntax>();
            foreach (var assignment in assignmentList)
            {
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
                                && thisMemberAccessExpressionSyntax.Expression.Kind() == SyntaxKind.ThisExpression)
                            {
                                data = new DeclareData();
                                data.IsAttribute = false;
                                data.Name = memberAccessExpressionSyntax.Name.Identifier.Text;
                                typeInfo = model.GetTypeInfo(assignment.Right);
                                data.Type = typeInfo.Type.ToString();
                                declares.Add(data.Name, data);
                            }
                        }
                        //ViewBag
                        if (memberAccessExpressionSyntax.Expression.Kind() == SyntaxKind.IdentifierName)
                        {
                            IdentifierNameSyntax leftIdentifierNameSyntax
                                = (IdentifierNameSyntax)(memberAccessExpressionSyntax.Expression);
                            if (leftIdentifierNameSyntax.Identifier.Text == "ViewBag")
                            {
                                data = new DeclareData();
                                data.Name = memberAccessExpressionSyntax.Name.Identifier.Text;
                                typeInfo = model.GetTypeInfo(assignment.Right);
                                data.Type = typeInfo.Type.ToString();
                                declares.Add(data.Name, data);
                            }
                        }
                    }
                }
            }
            foreach (var par in methodSy.Parameters)
            {
                data.IsAttribute = false;
                data.Type = par.Type.ToString();
                data.Name = par.Name;
            }
        }

        public static void GetAllFieldsAndPropertys(INamedTypeSymbol symbol, int level, ref Dictionary<string, DeclareData> declares)
        {
            if (symbol != null)
            {
                DeclareData data = new DeclareData();
                System.Collections.Immutable.ImmutableArray<AttributeData> attrArray;
                INamedTypeSymbol nameTypeSymbol = null;
                foreach (var filed in symbol.GetMembers().OfType<IFieldSymbol>())
                {
                    
                    if (filed.CanBeReferencedByName)
                    {
                        attrArray = filed.GetAttributes();
                        foreach (var attr in attrArray)
                        {
                            nameTypeSymbol = attr.AttributeClass;
                            if (nameTypeSymbol.Name == "ViewBagMemberAttribute")
                            {
                                data.IsAttribute = false;
                                data.Type = filed.Type.ToString();
                                data.Name = filed.Name;
                                if (!declares.ContainsKey(data.Name))
                                {
                                    if (level == 0)
                                    {
                                        declares.Add(data.Name, data);
                                    }
                                    else
                                    {
                                        if (filed.DeclaredAccessibility != Accessibility.Private
                                            && filed.DeclaredAccessibility != Accessibility.Internal)
                                        {
                                            declares.Add(data.Name, data);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (var property in symbol.GetMembers().OfType<IPropertySymbol>())
                {
                    attrArray = property.GetAttributes();
                    foreach (var attr in attrArray)
                    {
                        nameTypeSymbol = attr.AttributeClass;
                        if (nameTypeSymbol.Name == "ViewBagMemberAttribute")
                        {
                            data.IsAttribute = true;
                            data.Type = property.Type.ToString();
                            data.Name = property.Name;
                            if (!declares.ContainsKey(data.Name))
                            {
                                if (level == 0)
                                {
                                    declares.Add(data.Name, data);
                                }
                                else
                                {
                                    if (property.DeclaredAccessibility != Accessibility.Private
                                        && property.DeclaredAccessibility != Accessibility.Internal)
                                    {
                                        declares.Add(data.Name, data);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (symbol.BaseType != null)
            {
                level++;
                GetAllFieldsAndPropertys(symbol.BaseType, level, ref declares);
            }
        }
    }
}
