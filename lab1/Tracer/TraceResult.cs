using System;

namespace Tracer
{
    public class TraceResult
    {
        // public string XmlTraceResult { get; set; }
        // public string JsonTraceResult { get; set; }

        public int ElapsedTime { get; set; }
        public string MethodName { get; set; }
        public Type ClassName { get; set; }
        
    }
}