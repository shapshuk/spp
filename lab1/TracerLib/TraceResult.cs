using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml.Serialization;

namespace TracerLib
{
    [Serializable]
    [DataContract]
    public class TraceResult
    {
        private ConcurrentDictionary<int, ThreadInfo> _threads = new ConcurrentDictionary<int, ThreadInfo>();

        [DataMember]
        public List<ThreadInfo> Threads
        {
            get => _threads.Values.ToList();
            set => _threads = new ConcurrentDictionary<int, ThreadInfo>(value.ToDictionary(x => x.ThreadId));
        }

        internal void StartTrace(MethodInfo methodInfo)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            ThreadInfo threadInfo;
            if (_threads.ContainsKey(threadId))
            {
                threadInfo = _threads[threadId];
            }
            else
            {
                threadInfo = new ThreadInfo
                {
                    ThreadId = threadId
                };
                _threads.TryAdd(threadId, threadInfo);
            }
            
            threadInfo.StartTrace(methodInfo);
        }
        
        internal void StopTrace()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            if (_threads.TryGetValue(threadId, out var threadInfo))
            {
                threadInfo.StopTrace();
                return;
            }

            throw new Exception("StopTrace called before StartTrace");
        }

    }
}