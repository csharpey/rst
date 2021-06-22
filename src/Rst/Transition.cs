using Rst.Interfaces;

namespace Rst
{
    public class Transition<TFrom, TTo> : ITransition<TFrom, TTo> 
        where TTo : IState
        where TFrom : IState
    {
        public event ITransition<TFrom, TTo>.Trigger OnTriggered;
        
        public TFrom From { get; }
        public TTo To { get; }

        protected Transition()
        {
            From = default;
            To = default;
        }

        public Transition(TFrom from, TTo to)
        {
            From = from;
            To = to;
        }

        public virtual void Triggered()
        {
            OnTriggered?.Invoke();
        }
    }
}