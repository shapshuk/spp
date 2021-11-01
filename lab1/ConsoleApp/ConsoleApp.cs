using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TracerLib;
using TracerLib.Serializers;
using TracerLib.Writers;

namespace ConsoleApp
{
    public class ConsoleApp
    {
        private static Tracer tracerTest = new Tracer();
        public static int ThreadCount = 3;
        public static int SleepMiliseconds = 100;
        
        
        static void Main(string[] args)
        {
            var testThreads = new List<Thread>();
            for (var i = 0; i < ThreadCount; i++)
            {
                var thread = new Thread(MultipleThreads);
                testThreads.Add(thread);
                thread.Start();
            }

            foreach (var thread in testThreads)
            {
                thread.Join();
            }

            var jsonSerializer = new JSONSerializer();
            var xmlSerializer = new XMLSerializer();

            var xmlWriter = new FileWriter("TraceResults\\xmlSerialization.xml", xmlSerializer);
            xmlWriter.Write(tracerTest.GetTraceResult());
            
            var jsonWriter = new FileWriter("TraceResults\\jsonSerialization.json", jsonSerializer);
            jsonWriter.Write(tracerTest.GetTraceResult());

            var xmlConsoleWriter = new ConsoleWriter(xmlSerializer);
            xmlConsoleWriter.Write(tracerTest.GetTraceResult());
            
            var jsonConsoleWriter = new ConsoleWriter(jsonSerializer);
            jsonConsoleWriter.Write(tracerTest.GetTraceResult());
        }

        private static void CallMethod()
        {
            tracerTest.StartTrace();
            Thread.Sleep(SleepMiliseconds);
            tracerTest.StopTrace();
        }
            
        private static void MultipleThreads()
        {
            tracerTest.StartTrace();
            List<Thread> threadsInMethod = new List<Thread>();
            for (int i = 0; i < ThreadCount; i++)
            {
                Thread thread = new Thread(CallMethod);
                threadsInMethod.Add(thread);
                thread.Start();
            }
            foreach (var thread in threadsInMethod)
            {
                thread.Join();
            }
            CallMethod();
            Thread.Sleep(SleepMiliseconds);
            tracerTest.StopTrace();
            
        }
    }
}