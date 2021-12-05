using Assembly_BrowserLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Browser.Model
{
    class DataModel
    {
        public List<NamespaceInfo> GetAsmData(string path)
        {
            return new AssemblyBrowser().GetAssemblyInfo(path);
        }
    }
}
