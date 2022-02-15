using System.Threading;
using Xunit.Abstractions;

namespace Rst.Tests.StateMachine.Bot;

public class Publish : State
{
    public Publish(ITestOutputHelper output)
    {
        OnEntry += from =>
        {
            output.WriteLine("Publish stage : {0}", 1000);
            Thread.Sleep(1000);
        };
    }
}