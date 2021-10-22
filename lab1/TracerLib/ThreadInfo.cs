using System.Collections.Generic;

namespace TracerLib
{
    public class ThreadInfo
    {
        private Stack<MethodInfo> _methodsStack = new Stack<MethodInfo>();
        private List<MethodInfo> _methods = new List<MethodInfo>();
        
        public int ThreadId { get; set; }
        
        public IReadOnlyCollection<MethodInfo> Methods => _methods;

        internal void StartTrace(MethodInfo methodInfo)
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
        
        internal void StopTrace()
        {
            _methodsStack.Pop().StopTrace();
        }
    }
}