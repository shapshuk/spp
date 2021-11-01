using System.Text.Json;

namespace TracerLib.Serializers
{
    public class JSONSerializer : ISerializer
    {
        private static readonly JsonSerializerOptions Options = new ()
        {
            WriteIndented = true
        };
        
        public string Serialize(TraceResult traceResult)
        {
            return JsonSerializer.Serialize(traceResult, Options);
        }
    }
}