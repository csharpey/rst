using Rst;

namespace TestButtonStateMachine
{
    public class OnTransition : Transition<Off, On>
    {
        public OnTransition(Off from, On to) : base(from, to)
        {
        }
    }
}