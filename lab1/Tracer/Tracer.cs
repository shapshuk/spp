// using Tracer.ITracer;

using System;
using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private Stopwatch _stopwatch = new Stopwatch();
        private TraceResult _traceResult = new TraceResult();
        
        public void StartTrace()
        {
            _stopwatch.Start();
            
        }

        public void StopTrace()
        {
            _stopwatch.Stop();
            
        }
        

        public TraceResult GetTraceResult()
        {
            _traceResult.ElapsedTime = _stopwatch.Elapsed.Milliseconds;
            return _traceResult;
        }
    }
}