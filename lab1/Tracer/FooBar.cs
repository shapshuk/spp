using System.Threading;
using System.Threading.Tasks;
using Tracer;

public class Foo{
    private Bar _bar;
    private ITracer _tracer;

    internal Foo(ITracer tracer)
    {
        _tracer = tracer;
        _bar = new Bar(_tracer);
    }

    public void MyMethod()
    {
        _tracer.StartTrace();
        
        Thread.Sleep(1000);
        _bar.InnerMethod();
            
        _tracer.StopTrace();
        
        
        _tracer.StartTrace();
        
        Thread.Sleep(2000);
            
        _tracer.StopTrace();
    }
}

public class Bar{
    private ITracer _tracer;

    internal Bar(ITracer tracer)
    {
        _tracer = tracer;
    }

    public void InnerMethod()
    {
        _tracer.StartTrace(); 
        
        Thread.Sleep(4000);

        _tracer.StopTrace();
    }
}
