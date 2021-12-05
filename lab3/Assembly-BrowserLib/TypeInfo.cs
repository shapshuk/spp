using System;
using System.Collections.Generic;
using System.Text;

namespace Assembly_BrowserLib
{
    public class TypeInfo
    {
        public string Name {get; set;}
        public List<MethodInfo> Methods { get; } = new List<MethodInfo>();
        public List<FieldInfo> Fields { get; } = new List<FieldInfo>();
        public List<PropertyInfo> Properties { get; } = new List<PropertyInfo>();
        public List<ConstructorInfo> Constructors { get; } = new List<ConstructorInfo>();
    }
}
