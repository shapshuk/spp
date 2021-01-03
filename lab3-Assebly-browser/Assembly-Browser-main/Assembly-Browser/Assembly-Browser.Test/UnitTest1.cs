using Assembly_BrowserLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Browser.Test
{
    [TestClass]
    public class UnitTest1
    {
        public List<int> A;

        [TestMethod]
        public void MethodNameTest()
        {
            string expected = "Method: Void MethodNameTest()";

            string path = Directory.GetCurrentDirectory();
            path += @"\Assembly-Browser.Test.dll";

            var asm = new AssemblyBrowser().GetAssemblyInfo(path);

            string actual = asm[1].Classes[0].Methods[0].Name;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void NamespaceTest()
        {
            string expected = "Browser.Test";
            string path = Directory.GetCurrentDirectory();
            path += @"\Assembly-Browser.Test.dll";

            var asm = new AssemblyBrowser().GetAssemblyInfo(path);

            string actual = asm[1].Name;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ClassNameTest()
        {
            string expected = "UnitTest1";

            string path = Directory.GetCurrentDirectory();
            path += @"\Assembly-Browser.Test.dll";

            var asm = new AssemblyBrowser().GetAssemblyInfo(path);

            string actual = asm[1].Classes[0].Name;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GenericNameTest()
        {
            string expected = "Field: List<Int32> A";

            string path = Directory.GetCurrentDirectory();
            path += @"\Assembly-Browser.Test.dll";

            var asm = new AssemblyBrowser().GetAssemblyInfo(path);

            string actual = asm[1].Classes[0].Fields[0].Name;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BadFilePathTest()
        {
            string path = "";
            var asm = new AssemblyBrowser().GetAssemblyInfo(path);

            Assert.AreEqual(0, asm.Count);
        }
    }
}
