using System.Diagnostics;

namespace TracerLib.Writers
{
    public interface IWriter
    {
        void Write(TraceResult traceResult);
    }
}