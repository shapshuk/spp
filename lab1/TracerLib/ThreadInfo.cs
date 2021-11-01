using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TracerLib
{
    [Serializable]
    [DataContract]
    public class ThreadInfo
    {
        private Stack<MethodInfo> _methodsStack = new Stack<MethodInfo>();
        private List<MethodInfo> _methods = new List<MethodInfo>();
        [DataMember]
        public int ThreadId { get; set; }

        [DataMember]
        public List<MethodInfo> Methods
        {
            get => _methods;
            set => _methods = value.ToList();
        }

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