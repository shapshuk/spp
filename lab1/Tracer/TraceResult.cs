using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Tracer
{
    public class TraceResult
    {
        // public string XmlTraceResult { get; set; }
        // public string JsonTraceResult { get; set; }

        // public long ElapsedTime { get; set; }
        // public string MethodName { get; set; }
        // public string ClassName { get; set; }
        // public int ThreadID { get; set; }
        //
        // public List<TraceResult> TraceResultList { get; set; } = new List<TraceResult>();

        private Stack<MethodInfo> _methodsStack = new Stack<MethodInfo>();
        private List<MethodInfo> _methods = new List<MethodInfo>();
        
        public IReadOnlyCollection<MethodInfo> Methods => _methods;

        public void StartTrace(MethodInfo methodInfo)
        {
            if (_methodsStack.Count == 0)
            { 
                _methods.Add(methodInfo);
            }
            else
            {
                _methodsStack.Peek().AddInnerMethod(methodInfo);
            }
            _methodsStack.Push(methodInfo);
            methodInfo.StartTrace();
        }
        
        public void StopTrace()
        {
            _methodsStack.Pop().StopTrace();
        }
    }
}