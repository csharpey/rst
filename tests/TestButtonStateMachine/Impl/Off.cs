using Rst;
using Xunit.Abstractions;

namespace TestButtonStateMachine.Impl
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

        private void OnEntryLog()
        {
            _output.WriteLine(
                "{0} {1}", 
                nameof(Off), 
                nameof(OnEntryLog));
        }

        private void OnExitLog()
        {
            _output.WriteLine(
                "{0} {1}", 
                nameof(Off), 
                nameof(OnExitLog));
        }
    }
}