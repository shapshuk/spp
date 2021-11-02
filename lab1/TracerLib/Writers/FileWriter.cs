using System.IO;
using TracerLib.Serializers;

namespace TracerLib.Writers
{
    public class FileWriter : IWriter
    {
        private readonly string _filePath;
        private readonly ISerializer _serializer;

        public FileWriter(string filePath, ISerializer serializer)
        {
            _filePath = filePath;
            _serializer = serializer;
        }
        
        public void Write(TraceResult traceResult)
        {
            File.WriteAllText(_filePath, _serializer.Serialize(traceResult));
        }
    }
}