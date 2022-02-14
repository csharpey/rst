using Rst.Interfaces;

namespace Rst
{
    public sealed class Transition<TFrom, TTo> : ITransition<TFrom, TTo>
        where TTo : IState
        where TFrom : IState
    {
        public event ITransition<TFrom, TTo>.Trigger OnTriggered;

        public TFrom From { get; }
        public TTo To { get; }

        private Transition()
        {
            From = default;
            To = default;
        }

        public Transition(TFrom from, TTo to)
        {
            From = from;
            To = to;
        }

        public void Triggered()
        {
            OnTriggered?.Invoke();
        }
    }
}