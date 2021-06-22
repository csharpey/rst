using Rst;
using Xunit.Abstractions;

namespace TestButtonStateMachine.Impl
{
    public class On : State
    {
        private readonly ITestOutputHelper _output;

        public On(ITestOutputHelper output)
        {
            _output = output;

            OnEntry += OnEntryLog;
            OnExit += OnExitLog;
        }

        private void OnEntryLog()
        {
            _output.WriteLine(
                "{0} {1}", 
                nameof(On), 
                nameof(OnEntryLog));
        }

        private void OnExitLog()
        {
            _output.WriteLine(
                "{0} {1}", 
                nameof(On), 
                nameof(OnExitLog));
        }
        
    }
}