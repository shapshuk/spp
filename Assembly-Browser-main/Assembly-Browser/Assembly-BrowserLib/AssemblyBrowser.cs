using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Assembly_BrowserLib
{
    public class AssemblyBrowser
    {
        public List<NamespaceInfo> GetAssemblyInfo(string path)
        {
            var assemblies = new List<NamespaceInfo>();

            try
            {
                Assembly asm = Assembly.LoadFrom(path);

                foreach (var type in asm.GetTypes())
                {
                    if (Attribute.GetCustomAttribute(type, typeof(CompilerGeneratedAttribute)) == null)
                    {
                        var typeinfo = new TypeInfo();
                        typeinfo.Name = type.Name;

                        foreach (var method in type.GetMethods())
                            if (Attribute.GetCustomAttribute(method, typeof(CompilerGeneratedAttribute)) == null)
                            {
                                var methodInfo = new MethodInfo();
                                methodInfo.SetName(method);
                                typeinfo.Methods.Add(methodInfo);
                            }
                                

                        foreach (var field in type.GetFields())
                            if (Attribute.GetCustomAttribute(field, typeof(CompilerGeneratedAttribute)) == null)
                            {
                                var fieldInfo = new FieldInfo();
                                fieldInfo.SetName(field);
                                typeinfo.Fields.Add(fieldInfo);
                            }

                        foreach (var property in type.GetProperties())
                            if (Attribute.GetCustomAttribute(property, typeof(CompilerGeneratedAttribute)) == null)
                            {
                                var propetryInfo = new PropertyInfo();
                                propetryInfo.SetName(property);
                                typeinfo.Properties.Add(propetryInfo);
                            }

                        foreach (var constructor in type.GetConstructors())
                            if (Attribute.GetCustomAttribute(constructor, typeof(CompilerGeneratedAttribute)) == null)
                            {
                                var constructorInfo = new ConstructorInfo();
                                constructorInfo.SetName(constructor);
                                typeinfo.Constructors.Add(constructorInfo);
                            }

                        NamespaceInfo ns = assemblies.Find(item => item.Name == type.Namespace);
                        if(ns != null)
                        {
                            ns.Classes.Add(typeinfo);
                        }
                        else
                        {
                            ns = new NamespaceInfo();
                            ns.Name = type.Namespace;
                            ns.Classes.Add(typeinfo);
                            assemblies.Add(ns);
                        }
                        
                    }
                }
            } 
            catch
            {

            }
            return assemblies;
        }
    }
}
