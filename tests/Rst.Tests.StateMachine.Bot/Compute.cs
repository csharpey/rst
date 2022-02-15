using System.Threading;
using Xunit.Abstractions;

namespace Rst.Tests.StateMachine.Bot;

public class Compute : State
{
    public Compute(ITestOutputHelper output)
    {
        OnEntry += from =>
        {
            output.WriteLine("Current thread {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
        };
    }
}