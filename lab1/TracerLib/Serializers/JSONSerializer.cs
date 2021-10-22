using System.Text.Json;

namespace TracerLib.Serializers
{
    public class JSONSerializer : ISerializer
    {
        private readonly JsonSerializerOptions _options = new ()
        {
            WriteIndented = true
        };
        
        public string Serialize(TraceResult traceResult)
        {
            return JsonSerializer.Serialize(traceResult, _options);
        }
    }
}