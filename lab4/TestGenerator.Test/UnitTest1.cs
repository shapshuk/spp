using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using TestsGeneratorLib;

namespace TestGenerator.Test
{
    public class Tests
    {
        private TestsGeneratorLib.Tests test;
        private TestsGeneratorLib.TestGenerator gen;
        private string dirPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        [SetUp]
        public void Setup()
        {
            test = new TestsGeneratorLib.Tests();
            gen = new TestsGeneratorLib.TestGenerator();
        }
  
        [Test]
        public void SplitMultipleClassesIntoDifferentFiles()
        {
            var files = new List<string>() { Directory.GetCurrentDirectory() + @"\Code\Code.cs",  };
            test.Generate(files, Directory.GetCurrentDirectory() + @"\Test", 5).Wait();
            int expected = 2;
            int actual = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Test").Length;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void CompareTestCode()
        {
            var code = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Code\Code.cs");
            List<TestsGeneratorLib.TestFile> result = gen.CreateTest(code);

            string expected = File.ReadAllText(dirPath + "\\TestGenerator.Test\\testcode.txt");
            string actual = result[0].TestCode;
            Assert.AreEqual(expected, actual);
        }



    }
}