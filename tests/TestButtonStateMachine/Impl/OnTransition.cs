using Rst;

namespace TestButtonStateMachine.Impl
{
    public class OnTransition : Transition<Off, On>
    {
        public OnTransition()
        {
        }
        
        public OnTransition(Off from, On to) : base(from, to)
        {
        }

        public override void Triggered()
        {
            base.Triggered();
        }
    }
}