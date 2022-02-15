using Rst.Interfaces;
using Rst.Tests.StateMachine.Button.Impl;
using Xunit;
using Xunit.Abstractions;

namespace Rst.Tests.StateMachine.Button
{
    public class ButtonTest
    {
        private readonly ITestOutputHelper _output;

        private readonly On _on;
        private readonly Off _off;

        private readonly IStateMachine _machine;
        private readonly IWorkflow _workflow;

        public ButtonTest(ITestOutputHelper output)
        {
            _output = output;

            _on = new On(_output);
            _off = new Off(_output);

            _machine = new Rst.StateMachine(_on);
            _workflow = new Workflow(_machine);
            
            var off = _machine.AddTransition(_on, _off, delegate { });
            var on = _machine.AddTransition(_off, _on, delegate { });
            
            _workflow.Add(off);
            _workflow.Add(on);
            
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
            Assert.True(_workflow.MoveNext());
            Assert.Equal(_machine.Current, _off);
            Assert.True(_workflow.MoveNext());
            Assert.Equal(_machine.Current, _on);
        }
    }
}