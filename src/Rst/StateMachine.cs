using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Rst.Interfaces;

namespace Rst
{
    public class StateMachine : IStateMachine, IEnumerator<IState>
    {
        private readonly ConcurrentDictionary<IState, ITransition<IState, IState>> _transitions;
        
        public IState Current { get; private set; }

        public StateMachine(IState state)
        {
            Current = state;
            _transitions = new ConcurrentDictionary<IState, ITransition<IState, IState>>();
        }

        public StateMachine AddTransition<TFrom, TTo>(ITransition<TFrom, TTo> transition)
            where TFrom : IState 
            where TTo : IState
        {
            Debug.Assert(transition is ITransition<IState, IState>);
            
            if(!_transitions.TryAdd(transition.From, (ITransition<IState, IState>) transition))
                throw new InvalidOperationException();
            
            return this;
        }

        public bool MoveNext(ITransition<IState, IState> transition)
        {
            Debug.Assert(Current is not null);

            var exist = _transitions.TryGetValue(Current, out var state);
            
            if (!exist || state.From != Current) 
                return false;
            
            Current.Out();
            Current = state.To;
            
            Debug.Assert(Current is not null);
            
            Current.In();
            
            return true;
        }

        public bool MoveNext()
        {
            return false;
        }

        public void Reset()
        {
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}
