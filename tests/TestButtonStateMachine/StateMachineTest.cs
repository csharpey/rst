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

        private readonly OffTransition _offTransition;
        private readonly OnTransition _onTransition;
        private readonly IStateMachine _machine;

        public StateMachineTest(ITestOutputHelper output)
        {
            _output = output;
            
            _on = new On(_output);
            _off = new Off(_output);

            _offTransition = new OffTransition(_on, _off);
            _onTransition = new OnTransition(_off, _on);
            
            _offTransition.OnTriggered += delegate
            {
                _output.WriteLine($"{nameof(OffTransition)} {nameof(OffTransition.Triggered)}");
            };
            _machine = new StateMachine(_on)
                .AddTransition(_offTransition, delegate { })
                .AddTransition(_onTransition);
        }
        
        [Fact]
        public void TestValidation()
        {
            Assert.True(_machine.IsValid());
        }

        [Fact]
        public void TestEnumerator()
        {
            Assert.True(_machine.MoveNext(_offTransition));
            Assert.Equal(_machine.Current, _off);
            Assert.True(_machine.MoveNext(_onTransition));
            Assert.Equal(_machine.Current, _on);
        }
    }
}