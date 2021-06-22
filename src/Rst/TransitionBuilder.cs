using Rst.Interfaces;

namespace Rst
{
    public class TransitionBuilder : ITransitionBuilder
    {
        private ITransition<IState, IState> _transition;

        public TransitionBuilder(ITransition<IState, IState> transition)
        {
            _transition = transition;
        }

        public ITransitionBuilder SetLimit(int limit)
        {
            return this;
        }
    }
}