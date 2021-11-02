using System;
using TracerLib.Serializers;

namespace TracerLib.Writers
{
    public class ConsoleWriter : IWriter
    {
        private readonly ISerializer _serializer;

        public ConsoleWriter(ISerializer serializer)
        {
            _serializer = serializer;
        }
        
        public void Write(TraceResult traceResult)
        {
            Console.Write(_serializer.Serialize(traceResult));
        }
    }
}