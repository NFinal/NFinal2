﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NFinal.Compile.SqlTemplate.sqlserver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class Insert : RazorGenerator.Templating.RazorTemplateBase
    {
#line hidden
        #line 3 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
           
    public NFinal.Compile.SqlTemplate.Model.Insert Model { get; set; }

        #line default
        #line hidden
        
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("#region\tvar ");

            
            #line 6 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
              
            
            #line default
            #line hidden
            
            #line 6 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
         Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 6 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                            
            
            #line default
            #line hidden
WriteLiteral("; 插入并返回ID\r\n\t\t\tvar __");

            
            #line 7 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
           
            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
      Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                              
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 7 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                  
            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                             Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                
            
            #line default
            #line hidden
WriteLiteral("_command__ = new System.Data.SqlClient.SqlCommand(\"");

            
            #line 7 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                      
            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                 Write(Model.sql);

            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                
            
            #line default
            #line hidden
WriteLiteral(";select ");

WriteLiteral("@");

WriteLiteral("@IDENTITY;\", ");

            
            #line 7 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                           
            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                      Write(Model.connectionVarName);

            
            #line default
            #line hidden
            
            #line 7 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                                                   
            
            #line default
            #line hidden
WriteLiteral(");\r\n");

            
            #line 8 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
			
            
            #line default
            #line hidden
            
            #line 8 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
    if(Model.isTransaction){

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral("__");

            
            #line 9 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
         
            
            #line default
            #line hidden
            
            #line 9 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
    Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 9 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                            
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 9 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 9 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                           Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 9 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                              
            
            #line default
            #line hidden
WriteLiteral("_command__.Transaction=");

            
            #line 9 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                        
            
            #line default
            #line hidden
            
            #line 9 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                   Write(Model.transactionVarName);

            
            #line default
            #line hidden
            
            #line 9 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                 
            
            #line default
            #line hidden
WriteLiteral(";\r\n");

            
            #line 10 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
			}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

            
            #line 11 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
    if(Model.sqlVarParameters.Count>0){

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral("var __");

            
            #line 12 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
             
            
            #line default
            #line hidden
            
            #line 12 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
        Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 12 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 12 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                    
            
            #line default
            #line hidden
            
            #line 12 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                               Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 12 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                  
            
            #line default
            #line hidden
WriteLiteral("_parameters__=new System.Data.SqlClient.SqlParameter[");

            
            #line 12 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                          
            
            #line default
            #line hidden
            
            #line 12 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                     Write(Model.sqlVarParameters.Count);

            
            #line default
            #line hidden
            
            #line 12 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                       
            
            #line default
            #line hidden
WriteLiteral("];\r\n");

            
            #line 13 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
			}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

            
            #line 14 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
     int i=0;
            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 15 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
			
            
            #line default
            #line hidden
            
            #line 15 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
    foreach(var sqlVarParameter in Model.sqlVarParameters){
			if(sqlVarParameter.field.length>0){

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral("__");

            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
         
            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
    Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                            
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                           Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                              
            
            #line default
            #line hidden
WriteLiteral("_parameters__[");

            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                        Write(i);

            
            #line default
            #line hidden
WriteLiteral("] = new System.Data.SqlClient.SqlParameter(\"");

WriteLiteral("@");

            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                               
            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                          Write(sqlVarParameter.name);

            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                    
            
            #line default
            #line hidden
WriteLiteral("\",System.Data.SqlDbType.");

            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                                               
            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                                          Write(sqlVarParameter.field.dbType);

            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                                                                            
            
            #line default
            #line hidden
WriteLiteral(",");

            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                                                                                
            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                                                                           Write(sqlVarParameter.field.length);

            
            #line default
            #line hidden
            
            #line 17 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                                                                                                             
            
            #line default
            #line hidden
WriteLiteral(");\r\n");

            
            #line 18 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
				if(sqlVarParameter.field.allowNull){

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t");

WriteLiteral("__");

            
            #line 19 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
          
            
            #line default
            #line hidden
            
            #line 19 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
     Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 19 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                             
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 19 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                 
            
            #line default
            #line hidden
            
            #line 19 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                            Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 19 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                               
            
            #line default
            #line hidden
WriteLiteral("_parameters__[");

            
            #line 19 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                         Write(i);

            
            #line default
            #line hidden
WriteLiteral("].Value = ");

            
            #line 19 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                            
            
            #line default
            #line hidden
            
            #line 19 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                       Write(sqlVarParameter.csharpName);

            
            #line default
            #line hidden
            
            #line 19 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                       
            
            #line default
            #line hidden
WriteLiteral(".WithDBNull();\r\n");

            
            #line 20 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
				}else{

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t");

WriteLiteral("__");

            
            #line 21 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
          
            
            #line default
            #line hidden
            
            #line 21 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
     Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 21 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                             
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 21 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                 
            
            #line default
            #line hidden
            
            #line 21 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                            Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 21 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                               
            
            #line default
            #line hidden
WriteLiteral("_parameters__[");

            
            #line 21 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                         Write(i);

            
            #line default
            #line hidden
WriteLiteral("].Value = ");

            
            #line 21 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                            
            
            #line default
            #line hidden
            
            #line 21 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                       Write(sqlVarParameter.csharpName);

            
            #line default
            #line hidden
            
            #line 21 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                       
            
            #line default
            #line hidden
WriteLiteral(";\r\n");

            
            #line 22 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
				}
			}else{

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral("__");

            
            #line 24 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
         
            
            #line default
            #line hidden
            
            #line 24 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
    Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 24 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                            
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 24 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 24 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                           Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 24 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                              
            
            #line default
            #line hidden
WriteLiteral("_parameters__[");

            
            #line 24 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                        Write(i);

            
            #line default
            #line hidden
WriteLiteral("] = new System.Data.SqlClient.SqlParameter(\"");

WriteLiteral("@");

            
            #line 24 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                               
            
            #line default
            #line hidden
            
            #line 24 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                          Write(sqlVarParameter.name);

            
            #line default
            #line hidden
            
            #line 24 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                    
            
            #line default
            #line hidden
WriteLiteral("\",System.Data.SqlDbType.");

            
            #line 24 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                                               
            
            #line default
            #line hidden
            
            #line 24 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                                          Write(sqlVarParameter.field.dbType);

            
            #line default
            #line hidden
            
            #line 24 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                                                                                            
            
            #line default
            #line hidden
WriteLiteral(");\r\n");

            
            #line 25 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
				if(sqlVarParameter.field.allowNull){

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t");

WriteLiteral("__");

            
            #line 26 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
          
            
            #line default
            #line hidden
            
            #line 26 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
     Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 26 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                             
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 26 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                 
            
            #line default
            #line hidden
            
            #line 26 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                            Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 26 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                               
            
            #line default
            #line hidden
WriteLiteral("_parameters__[");

            
            #line 26 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                         Write(i);

            
            #line default
            #line hidden
WriteLiteral("].Value = ");

            
            #line 26 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                            
            
            #line default
            #line hidden
            
            #line 26 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                       Write(sqlVarParameter.csharpName);

            
            #line default
            #line hidden
            
            #line 26 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                       
            
            #line default
            #line hidden
WriteLiteral(".WithDBNull();\r\n");

            
            #line 27 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
				}else{

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t");

WriteLiteral("__");

            
            #line 28 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
          
            
            #line default
            #line hidden
            
            #line 28 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
     Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 28 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                             
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 28 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                 
            
            #line default
            #line hidden
            
            #line 28 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                            Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 28 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                               
            
            #line default
            #line hidden
WriteLiteral("_parameters__[");

            
            #line 28 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                         Write(i);

            
            #line default
            #line hidden
WriteLiteral("].Value = ");

            
            #line 28 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                            
            
            #line default
            #line hidden
            
            #line 28 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                       Write(sqlVarParameter.csharpName);

            
            #line default
            #line hidden
            
            #line 28 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                       
            
            #line default
            #line hidden
WriteLiteral(";\r\n");

            
            #line 29 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
				}
			}
			i++;
			}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

            
            #line 33 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
    if(Model.sqlVarParameters.Count>0){

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral("__");

            
            #line 34 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
         
            
            #line default
            #line hidden
            
            #line 34 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
    Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 34 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                            
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 34 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 34 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                           Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 34 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                              
            
            #line default
            #line hidden
WriteLiteral("_command__.Parameters.AddRange(__");

            
            #line 34 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                  
            
            #line default
            #line hidden
            
            #line 34 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                             Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 34 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                     
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 34 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                         
            
            #line default
            #line hidden
            
            #line 34 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                    Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 34 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                                                                       
            
            #line default
            #line hidden
WriteLiteral("_parameters__);\r\n");

            
            #line 35 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
			}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

            
            #line 36 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
    if(Model.isDeclaration){

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t");

WriteLiteral("var ");

            
            #line 37 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
            
            
            #line default
            #line hidden
            
            #line 37 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
       Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 37 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                          
            
            #line default
            #line hidden
WriteLiteral(" = __");

            
            #line 37 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                  
            
            #line default
            #line hidden
            
            #line 37 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                             Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 37 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                     
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 37 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                         
            
            #line default
            #line hidden
            
            #line 37 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                    Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 37 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                       
            
            #line default
            #line hidden
WriteLiteral("_command__.ExecuteScalar().AsVar();\r\n");

            
            #line 38 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
			}else{

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t");

            
            #line 39 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
        
            
            #line default
            #line hidden
            
            #line 39 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
   Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 39 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                      
            
            #line default
            #line hidden
WriteLiteral(" = __");

            
            #line 39 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                              
            
            #line default
            #line hidden
            
            #line 39 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                         Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 39 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                 
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 39 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                     
            
            #line default
            #line hidden
            
            #line 39 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 39 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                                                   
            
            #line default
            #line hidden
WriteLiteral("_command__.ExecuteScalar().AsVar();\r\n");

            
            #line 40 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
			}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t__");

            
            #line 41 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
       
            
            #line default
            #line hidden
            
            #line 41 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
  Write(Model.functionName);

            
            #line default
            #line hidden
            
            #line 41 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                          
            
            #line default
            #line hidden
WriteLiteral("_");

            
            #line 41 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                              
            
            #line default
            #line hidden
            
            #line 41 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                         Write(Model.varName);

            
            #line default
            #line hidden
            
            #line 41 "..\..\SqlTemplate\sqlserver\Insert.cshtml"
                                            
            
            #line default
            #line hidden
WriteLiteral("_command__.Dispose();\r\n\t\t\t#endregion\r\n\t\t\t");

        }
    }
}
#pragma warning restore 1591
