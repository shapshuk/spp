using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using TracerLib;

namespace TracerTests
{
    public class UnitTest1
    {
        public Tracer tracer;
        private readonly List<Thread> threads = new List<Thread>();
        int ThreadsCount = 3;
        int methodCount = 5;
        int sleepMiliseconds = 100;

        private void CallMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(sleepMiliseconds);
            tracer.StopTrace();
        }

        private void CallMethodSecond()
        {
            tracer.StartTrace();
            Thread.Sleep(sleepMiliseconds*3);
            tracer.StopTrace();
        }

        public void InnerMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(sleepMiliseconds);
            CallMethod();
            tracer.StopTrace();
        }

        public void NestedMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(sleepMiliseconds);
            InnerMethod();
            tracer.StopTrace();

        }

        public void MultipleThreads()
        {
            tracer.StartTrace();
            List<Thread> threadsInMethod = new List<Thread>();
            for (int i = 0; i < ThreadsCount; i++)
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
            Thread.Sleep(sleepMiliseconds);
            tracer.StopTrace();

        }

        //Проверка структуры ThreadStructure и работы класса TraceResult
        [Test]
        public void ThreadCount()
        {
            tracer = new Tracer();

            for (int i = 0; i< ThreadsCount;i++ )
            {
                threads.Add(new Thread(CallMethod));
            }

            foreach(Thread threa in threads)
            {
                threa.Start();
                threa.Join();
            }

            TraceResult traceResult = tracer.GetTraceResult();
            Assert.AreEqual(ThreadsCount, traceResult.Threads.Count);
        }

        //Проверка структуры MethodStructure и работы класса TraceResult
        [Test]
        public void MethodCount()
        {
            tracer = new Tracer();
            for (int i =0; i < methodCount; i++)
            {
                CallMethod();
            }
            TraceResult traceResult = tracer.GetTraceResult();
            Assert.AreEqual(methodCount, traceResult.Threads[0].Methods.Count);
        }

        // Проверка работы программы при данных: один метод в одном потоке
        [Test]
        public void SingleMethodSingleThread()
        {
            tracer = new Tracer();
            CallMethod();
            TraceResult traceResult = tracer.GetTraceResult();
            
            Assert.AreEqual(1, traceResult.Threads.Count);
            Assert.AreEqual(1, traceResult.Threads[0].Methods.Count);
            Assert.AreEqual(nameof(CallMethod), traceResult.Threads[0].Methods[0].MethodName);
        }

        // Проверка работы программы при данных: два метода в одном потоке
        [Test]
        public void TwoMethodsSingleThread()
        {

            tracer = new Tracer();
            CallMethod();
            CallMethodSecond();
            TraceResult traceResult = tracer.GetTraceResult();
            Assert.AreEqual(1, traceResult.Threads.Count);
            Assert.AreEqual(2, traceResult.Threads[0].Methods.Count);
            Assert.AreEqual(nameof(CallMethod), traceResult.Threads[0].Methods[0].MethodName);
            Assert.AreEqual(nameof(CallMethodSecond), traceResult.Threads[0].Methods[1].MethodName);
        }

        // Проверка работы программы при данных: несколько вложенных методов в одном потоке
        [Test]
        public void NestedMethodsSingleThread()
        {
            tracer = new Tracer();
            NestedMethod();
            TraceResult traceResult = tracer.GetTraceResult();
            Assert.AreEqual(1, traceResult.Threads.Count);
            Assert.AreEqual(1, traceResult.Threads[0].Methods.Count);
            Assert.AreEqual(1, traceResult.Threads[0].Methods[0].InnerMethods.Count);
            Assert.AreEqual(1, traceResult.Threads[0].Methods[0].InnerMethods[0].InnerMethods.Count);
            Assert.AreEqual(0, traceResult.Threads[0].Methods[0].InnerMethods[0].InnerMethods[0].InnerMethods.Count);
            Assert.AreEqual(nameof(NestedMethod), traceResult.Threads[0].Methods[0].MethodName);
            Assert.AreEqual(nameof(InnerMethod), traceResult.Threads[0].Methods[0].InnerMethods[0].MethodName);
            Assert.AreEqual(nameof(CallMethod), traceResult.Threads[0].Methods[0].InnerMethods[0].InnerMethods[0].MethodName);

        }

        // Проверка работы программы при данных: несколько вложенных методов в разных потоках
        [Test]
        public void NestedThreads()
        {
            tracer = new Tracer();
            int singleMethods = 0;
            int nestedMethods = 0;
            List<Thread> testThreads = new List<Thread>();
            for(int i = 0; i < ThreadsCount; i++)
            {
                Thread thread = new Thread(MultipleThreads);
                testThreads.Add(thread);
                thread.Start();

            }
            foreach (var thread in testThreads)
                thread.Join();

            TraceResult traceResult = tracer.GetTraceResult();
            Assert.AreEqual(ThreadsCount * ThreadsCount + ThreadsCount, traceResult.Threads.Count);
          
            for (int i = 0; i < traceResult.Threads.Count; i++)
            {
                Assert.AreEqual(1, traceResult.Threads[i].Methods.Count);
                if (traceResult.Threads[i].Methods[0].InnerMethods.Count != 0)
                {
                    nestedMethods++;
                    Assert.AreEqual(nameof(CallMethod), traceResult.Threads[i].Methods[0].InnerMethods[0].MethodName);
                }
                else
                    singleMethods++;
            }
            Assert.AreEqual(ThreadsCount, nestedMethods);
            Assert.AreEqual(ThreadsCount * ThreadsCount, singleMethods);
        }


    }
}