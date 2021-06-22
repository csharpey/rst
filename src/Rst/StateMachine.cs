using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Rst.Interfaces;

namespace Rst
{
    public class StateMachine : IStateMachine
    {
        private readonly ConcurrentDictionary<IState, ITransition<IState, IState>> _transitions;
        
        public IState Current { get; private set; }

        public StateMachine(IState state)
        {
            Current = state;
            _transitions = new ConcurrentDictionary<IState, ITransition<IState, IState>>();
        }

        public IStateMachine AddTransition<TFrom, TTo>(ITransition<TFrom, TTo> transition) 
            where TFrom : IState 
            where TTo : IState
        {
            return AddTransition(transition, delegate { });
        }

        public IStateMachine AddTransition<TFrom, TTo>(
            ITransition<TFrom, TTo> transition, 
            Action<ITransitionBuilder> action)
            where TFrom : IState
            where TTo : IState
        {
            Debug.Assert(transition is ITransition<IState, IState>);
            var t = (ITransition<IState, IState>) transition;
            var builder = new TransitionBuilder(t);
            action.Invoke(builder);
            
            if(!_transitions.TryAdd(transition.From, t))
                throw new InvalidOperationException();
            
            return this;
        }
        public bool MoveNext<TFrom, TTo>(ITransition<TFrom, TTo> transition)
            where TFrom : IState 
            where TTo : IState
        {
            Debug.Assert(Current is not null);

            if (!transition.From.Equals(Current))
                return false;

            var exist = _transitions.TryGetValue(Current, out var state);
            
            if (!exist || state.From != Current) 
                return false;
            
            state.Triggered();
            
            Current.Out();
            Current = state.To;
            
            Debug.Assert(Current is not null);
            
            Current.In();
            
            return true;
        }

        public bool MoveNext<T>(T t = default)
            where T : IState
        {
            return MoveNext(typeof(T));
        }

        public bool MoveNext(Type type)
        {
            return true;
        }

        public bool IsValid()
        {
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
