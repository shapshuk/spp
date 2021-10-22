using System;
using System.Threading;

namespace TracerLib
{
    public class Program
    {
        static void Main(string[] args)
        {
            ITracer tracer = new Tracer();
            Foo foo = new Foo(tracer);
            foo.MyMethod();
            
        }
    }
}