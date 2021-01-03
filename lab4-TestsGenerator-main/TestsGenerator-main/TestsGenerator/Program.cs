using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TestsGeneratorLib;

namespace TestsGenerator 
{ 
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Tests();
            string dirPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string dest = @$"{dirPath}\TestGeneratorClassTest";
            var sourceList = new List<string>() 
            { 
                @$"{dirPath}\TestClasses\Class1.cs",
                @$"{dirPath}\TestClasses\Class2.cs",
                @$"{dirPath}\TestClasses\Class3.txt",
                @$"{dirPath}\TestClasses\Class3.cs"
            }; 
            test.Generate(sourceList, dest, 5).Wait();
        }
    }
}
