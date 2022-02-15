using System.Threading;
using Rst.Interfaces;
using Xunit.Abstractions;

namespace Rst.Tests.StateMachine.Button.Impl
{
    public class Off : State
    {
        private readonly ITestOutputHelper _output;

        public Off(ITestOutputHelper output)
        {
            _output = output;

            OnEntry += OnEntryLog;
            OnExit += OnExitLog;
        }

        private void OnEntryLog(IState from)
        {
            switch (from)
            {
                case On on:
                    Interlocked.Increment(ref on.State);
                    break;
                default:
                    _output.WriteLine("Unhandled state");
                    break;
            }

            _output.WriteLine(
                "{0} {1} From {2}",
                nameof(Off),
                nameof(OnEntryLog), from.GetType());
        }

        private void OnExitLog(IState to)
        {
            _output.WriteLine(
                "{0} {1} To {2}",
                nameof(Off),
                nameof(OnExitLog), to.GetType());
        }
    }
}