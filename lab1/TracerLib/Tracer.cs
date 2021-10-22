// using Tracer.ITracer;

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace TracerLib
{
    public class Tracer : ITracer
    {


        private TraceResult _traceResult = new TraceResult();
        
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
            _traceResult.StopTrace();
        }
        

        public TraceResult GetTraceResult()
        {
            // _traceResult.ElapsedTime = _stopwatch.Elapsed.Milliseconds;
            return _traceResult;
            
        }
    }
}