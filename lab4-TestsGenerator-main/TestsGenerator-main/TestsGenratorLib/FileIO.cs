using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLib
{
    class FileIO
    {
        public async Task<string> ReadFileAsync(string source)
        {
            using (StreamReader reader = new StreamReader(source))
            {
                return await reader.ReadToEndAsync();
            }
        }
        public async Task  WriteFileAsync(string destination, List<TestFile> files)
        {
            foreach (var text in files)
            {
                using (StreamWriter writer = new StreamWriter(destination + @$"\{text.FileName}.cs", false))
                {
                    await writer.WriteAsync(text.TestCode);
                }
            }
        }
    }
}
