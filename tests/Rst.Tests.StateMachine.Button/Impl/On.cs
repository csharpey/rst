using Rst.Interfaces;
using Xunit.Abstractions;

namespace Rst.Tests.StateMachine.Button.Impl
{
    public class On : State
    {
        private readonly ITestOutputHelper _output;
        public int State = 0;

        public On(ITestOutputHelper output)
        {
            _output = output;

            OnEntry += OnEntryLog;
            OnExit += OnExitLog;
        }

        private void OnEntryLog(IState from)
        {
            _output.WriteLine(
                "{0} {1} State {2}", 
                nameof(On), 
                nameof(OnEntryLog), State);
        }

        
        private void OnExitLog(IState to)
        {
            _output.WriteLine(
                "{0} {1}", 
                nameof(On), 
                nameof(OnExitLog));
        }
        
    }
}