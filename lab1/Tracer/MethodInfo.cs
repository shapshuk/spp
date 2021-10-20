using System.Collections.Generic;
using System.Diagnostics;

namespace Tracer
{
    public class MethodInfo
    {
        private Stopwatch _stopwatch = new Stopwatch();
        private List<MethodInfo> _innerMethods = new List<MethodInfo>();

        public IReadOnlyCollection<MethodInfo> InnerMethods => _innerMethods;
        
        public long ExecutionTime { get; private set; }
        
        public string MethodName { get; set; }

        public void AddInnerMethod(MethodInfo methodInfo)
        {
            _innerMethods.Add(methodInfo);
        }        
        
        public void StartTrace()
        {
            _stopwatch.Start();
        }

        public void StopTrace()
        {
            _stopwatch.Stop();
            ExecutionTime = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Reset();
        }
    }
}