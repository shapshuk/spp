using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TracerLib
{
    public class TraceResult
    {
        private readonly ConcurrentDictionary<int, ThreadInfo> _threads = new ConcurrentDictionary<int, ThreadInfo>();
        
        public IReadOnlyCollection<ThreadInfo> Threads => _threads.Values.ToList();
        
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