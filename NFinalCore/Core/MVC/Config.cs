using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace NFinal.MVC
{
    public class Config
    {
        public void ConfigMVC(string[] assemblyNames)
        {
            Assembly assem= Assembly.GetEntryAssembly();
            Module[] modules= assem.GetModules();
            Type[] type= modules[0].GetTypes();
            if (assemblyNames?.Length > 0)
            {
                foreach (string assemblyName in assemblyNames)
                {
                    Assembly plug= Assembly.Load(new AssemblyName(assemblyName));
                    RegistControllers(plug);
                }
            }
        }
        public static Dictionary<string, Delegate> actionDic = new Dictionary<string, Delegate>(StringComparer.Ordinal);
        public void RegistControllers(Assembly assem)
        {
            var OwinActions= assem.GetTypes().Where(m => m.GetTypeInfo().IsSubclassOf(typeof(OwinAction<,>)));
            foreach (var action in OwinActions)
            {
                MethodInfo methodInfo= action.GetMethod("Execute");
                if (methodInfo != null)
                {
                    Delegate ExecuteMethodDelegate = methodInfo.CreateDelegate(action);
                    string[] actionUrls = (string[])action.GetField("actionUrls").GetValue(null);
                    foreach (string actionUrl in actionUrls)
                    {
                        if (actionUrl != null)
                        {
                            actionDic.Add(actionUrl, ExecuteMethodDelegate);
                        }
                    }
                }
            }
        }
    }
}
