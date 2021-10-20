// using Tracer.ITracer;

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private TraceResult _traceResult = new TraceResult();
        // private ConcurrentStack<Stopwatch> _stopwatches = new ConcurrentStack<Stopwatch>();
        // private ConcurrentStack<TraceResult> _traceResults = new ConcurrentStack<TraceResult>();
        public void StartTrace()
        {
            var methodInfo = new MethodInfo()
            {
                MethodName = new StackTrace().GetFrame(1).GetMethod().Name
            };
            
            _traceResult.StartTrace(methodInfo);
        }

        public void StopTrace()
        {
            // _stopwatches.TryPop(out var stopwatch);
            // stopwatch.Stop();
            // // Console.WriteLine(stopwatch.ElapsedMilliseconds);
            // // getting caller method and class name
            // MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            // // Console.WriteLine(methodBase.Name);
            // // Console.WriteLine(methodBase.DeclaringType.Name);
            // TraceResult result = new TraceResult
            // {
            //     ElapsedTime = stopwatch.ElapsedMilliseconds,
            //     // Methodlevel = threadInfo[threadId].watches.Count,
            //     MethodName = methodBase.Name,
            //     ClassName = methodBase.DeclaringType.Name,
            //     ThreadID = Thread.CurrentThread.ManagedThreadId
            // };
            //
            
            _traceResult.StopTrace();
            
        }
        

        public TraceResult GetTraceResult()
        {
            // _traceResult.ElapsedTime = _stopwatch.Elapsed.Milliseconds;
            return _traceResult;
        }
    }
}