using Rst.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace Rst.Tests.StateMachine.Bot
{
    public class StateMachineTest
    {
        private readonly ITestOutputHelper _output;

        private readonly Idle _idle;
        private readonly Publish _publish;
        private readonly Compute _compute;

        private readonly IStateMachine _machine;
        private readonly IWorkflow _softWorkflow;
        private readonly IWorkflow _hardWorkflow;

        public StateMachineTest(ITestOutputHelper output)
        {
            _output = output;
            
            _idle = new Idle(_output);
            _compute = new Compute(_output);
            _publish = new Publish(_output);

            _machine = new Rst.StateMachine(_idle);
            _softWorkflow = new Workflow(_machine);
            _hardWorkflow = new Workflow(_machine);
            
            var soft = _machine.AddTransition(_idle, _compute);
            var hard = _machine.AddTransition(_idle, _compute);
            var reset = _machine.AddTransition(_publish, _idle);
            var publish = _machine.AddTransition(_compute, _publish);
            var help = _machine.AddTransition(_idle, _idle, builder =>
            {
                builder.SetLimit(5);
            });
            
            _softWorkflow.Add(help);
            _softWorkflow.Add(soft);
            _softWorkflow.Add(publish);
            _softWorkflow.Add(reset);
            
            _hardWorkflow.Add(help);
            _hardWorkflow.Add(hard);
            _hardWorkflow.Add(publish);
            
            help.OnTriggered += delegate
            {
                _output.WriteLine("Help called | Usage");
            };
        }

        [Fact]
        public void TestValidation()
        {
            Assert.True(_machine.IsValid());
        }

        [Fact]
        public void TestSoft()
        {
            while (_softWorkflow.MoveNext())
            {
                _output.WriteLine("Current : {0}", _machine.Current);
                Assert.True(true);
            }
        }

        [Fact]
        public void TestHard()
        {
            while (_hardWorkflow.MoveNext())
            {
                _output.WriteLine("Current : {0}", _machine.Current);
                Assert.True(true);
            }
        }
    }
}