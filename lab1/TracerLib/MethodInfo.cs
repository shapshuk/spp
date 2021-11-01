using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TracerLib
{
    [Serializable]
    [DataContract]
    public class MethodInfo
    {
        private Stopwatch _stopwatch = new Stopwatch();
        private List<MethodInfo> _innerMethods = new List<MethodInfo>();

        [DataMember]
        public List<MethodInfo> InnerMethods
        {
            get => _innerMethods;
            set => _innerMethods = value.ToList();
        }
        
        [DataMember]
        public long ExecutionTime { get; private set; }
        [DataMember]
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