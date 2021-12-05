using System;
using System.Collections.Generic;
using System.Text;

namespace Assembly_BrowserLib
{
    public class NamespaceInfo
    {
        public string Name { get; set; }
        public List<TypeInfo> Classes { get; } = new List<TypeInfo>();
    }
}
