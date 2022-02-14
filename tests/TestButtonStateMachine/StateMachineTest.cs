using Rst;
using Rst.Interfaces;
using TestButtonStateMachine.Impl;
using Xunit;
using Xunit.Abstractions;

namespace TestButtonStateMachine
{
    public class StateMachineTest
    {
        private readonly ITestOutputHelper _output;

        private readonly On _on;
        private readonly Off _off;

        private readonly IStateMachine _machine;

        public StateMachineTest(ITestOutputHelper output)
        {
            _output = output;

            _on = new On(_output);
            _off = new Off(_output);

            _machine = new StateMachine(_on);
            
            var off = _machine.AddTransition(_on, _off, delegate { });
            var on = _machine.AddTransition(_off, _on, delegate { });
            
            _machine.Workflow.Add(off);
            _machine.Workflow.Add(on);
            
            off.OnTriggered += delegate
            {
                _output.WriteLine($"{nameof(off)} {nameof(off.Triggered)}");
            };
        }

        [Fact]
        public void TestValidation()
        {
            Assert.True(_machine.IsValid());
        }

        [Fact]
        public void TestEnumerator()
        {
            Assert.True(_machine.Workflow.MoveNext());
            Assert.Equal(_machine.Current, _off);
            Assert.True(_machine.Workflow.MoveNext());
            Assert.Equal(_machine.Current, _on);
        }
    }
}