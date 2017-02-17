using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
namespace System
{
	/// <summary>
    /// string转其它基本类型的扩展类
    /// </summary>
    public static class TypeExtension
    {
		public static Dictionary<Type,bool> BaseTypeDictionary=new Dictionary<Type,bool>(){
			{typeof(string),false}
						,{typeof(SByte),true}
						,{typeof(Byte),true}
						,{typeof(Int16),true}
						,{typeof(UInt16),true}
						,{typeof(Int32),true}
						,{typeof(UInt32),true}
						,{typeof(Int64),true}
						,{typeof(UInt64),true}
						,{typeof(Boolean),true}
						,{typeof(Char),true}
						,{typeof(Decimal),true}
						,{typeof(Double),true}
						,{typeof(Single),true}
						,{typeof(DateTime),true}
						,{typeof(DateTimeOffset),true}
									,{typeof(SByte?),true}
						,{typeof(Byte?),true}
						,{typeof(Int16?),true}
						,{typeof(UInt16?),true}
						,{typeof(Int32?),true}
						,{typeof(UInt32?),true}
						,{typeof(Int64?),true}
						,{typeof(UInt64?),true}
						,{typeof(Boolean?),true}
						,{typeof(Char?),true}
						,{typeof(Decimal?),true}
						,{typeof(Double?),true}
						,{typeof(Single?),true}
						,{typeof(DateTime?),true}
						,{typeof(DateTimeOffset?),true}
					};
		public static bool IsBaseType(this Type t)
		{
			if(BaseTypeDictionary.ContainsKey(t))
			{
				return true;
			}
			return false;
		}
		    //eg: symbol.IsFullNameEquals("List`1", "Generic", "Collections", "System")
    internal static bool IsFullNameEquals(this ISymbol symbol, params string[] nameParts) {
        if (symbol == null) throw new ArgumentNullException("symbol");
        if (nameParts == null || nameParts.Length == 0) throw new ArgumentNullException("nameParts");
        var idx = 0;
        for (; symbol != null; symbol = symbol.ContainingSymbol) {
            var name = symbol.MetadataName;
            if (string.IsNullOrEmpty(name)) break;
            if (idx == nameParts.Length) return false;
            if (name != nameParts[idx]) return false;
            idx++;
        }
        return idx == nameParts.Length;
    }

    //eg: var idx = symbol.MatchFullName(new []{"List`1", "Dictionary`2"}, new []{"Generic", "Collections", "System"});
    //return value: -1: none; 0: symbol is List`1; 1: symbol is Dictionary`2 
    internal static int MatchFullName(this ISymbol symbol, string[] typeNames, string[] outerNameParts) {
        if (symbol == null) throw new ArgumentNullException("symbol");
        if (typeNames == null || typeNames.Length == 0) throw new ArgumentNullException("typeNames");
        var fullLength = 1 + (outerNameParts != null ? outerNameParts.Length : 0);
        int idx = 0, result = -1;
        for (; symbol != null; symbol = symbol.ContainingSymbol) {
            var name = symbol.MetadataName;
            if (string.IsNullOrEmpty(name)) break;
            if (idx == fullLength) return -1;
            if (idx == 0) {
                for (var i = 0; i < typeNames.Length; i++) {
                    if (name == typeNames[i]) {
                        result = i;
                        break;
                    }
                }
                if (result == -1) return -1;
            }
            else {
                if (name != outerNameParts[idx - 1]) return -1;
            }
            idx++;
        }
        if (idx == fullLength) return result;
        return -1;
    }
    }
}
