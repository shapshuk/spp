using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TestsGeneratorLib
{
    public class Tests
    {
        private FileIO fileIO;
        private TestGenerator generator;
        public Tests()
        {
            fileIO = new FileIO();
            generator = new TestGenerator();
        }
        public Task Generate(List<string> source, string destination, int maxParallelism)
        {
            var executionOptions = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = maxParallelism };
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            var openFile = new TransformBlock<string, string>(async path =>
                await fileIO.ReadFileAsync(path),
                executionOptions);

            var generateTest = new TransformBlock<string, List<TestFile>>(text =>
            {
                return generator.CreateTest(text);
            }, executionOptions);

            var saveTestFile = new ActionBlock<List<TestFile>>(async text =>
            {
                await fileIO.WriteFileAsync(destination, text);
            }, executionOptions);

            openFile.LinkTo(generateTest, linkOptions);
            generateTest.LinkTo(saveTestFile, linkOptions);

            foreach (var file in source)
            {
                openFile.Post(file);
            }

            openFile.Complete();
            return saveTestFile.Completion;
        }
    }
}
