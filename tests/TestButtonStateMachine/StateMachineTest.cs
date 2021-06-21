using Rst;
using Xunit;
using Xunit.Abstractions;

namespace TestButtonStateMachine
{
    public class StateMachineTest
    {
        private readonly ITestOutputHelper _output;

        public StateMachineTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void TestInit()
        {
            var on = new On(_output);
            var off = new Off(_output);

            var toOff = new OffTransition(on, off);
            var toOn = new OnTransition(off, on);
            
            var m = new StateMachine(on)
                .AddTransition(toOff)
                .AddTransition(toOn);
            
            Assert.True(m.MoveNext(toOff));
            
            Assert.Equal(m.Current, off);
            
            Assert.True(m.MoveNext(toOn));
            
            Assert.Equal(m.Current, on);
        }
    }
}