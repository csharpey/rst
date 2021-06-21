using Rst;

namespace TestButtonStateMachine
{
    public class OffTransition : Transition<On, Off>
    {
        public OffTransition(On from, Off to) : base(from, to)
        {
        }
    }
}