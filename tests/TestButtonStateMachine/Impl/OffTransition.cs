using Rst;

namespace TestButtonStateMachine.Impl
{
    public class OffTransition : Transition<On, Off>
    {
        public OffTransition()
        {
        }
        public OffTransition(On from, Off to) : base(from, to)
        {
        }

        public override void Triggered()
        {
            base.Triggered();
        }
    }
}