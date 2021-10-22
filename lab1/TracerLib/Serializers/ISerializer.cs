namespace TracerLib.Serializers
{
    public interface ISerializer
    {
        string Serialize(TraceResult traceResult);
    }
}