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
        private readonly ConcurrentDictionary<IState, IList<ITransition<IState, IState>>> _transitions;
        public IWorkflow Workflow { get; private set; }

        public IState Current { get; private set; }

        public StateMachine(IState state)
        {
            Current = state;
            _transitions = new ConcurrentDictionary<IState, IList<ITransition<IState, IState>>>();
            Workflow = new Workflow(this);
        }

        public Transition<IState, IState> AddTransition<TFrom, TTo>(TFrom from, TTo to,
            Action<ITransitionBuilder> action)
            where TFrom : IState
            where TTo : IState
        {
            var t = new Transition<IState, IState>(from, to);

            var builder = new TransitionBuilder(t);
            action.Invoke(builder);

            if (!_transitions.ContainsKey(from))
            {
                _transitions.TryAdd(from, new List<ITransition<IState, IState>>());
            }

            _transitions[from].Add(t);


            return t;
        }

        public bool MoveNext<TFrom, TTo>(ITransition<TFrom, TTo> transition)
            where TFrom : IState
            where TTo : IState
        {
            Debug.Assert(Current is not null);

            if (!transition.From.Equals(Current))
                return false;

            var exist = _transitions.TryGetValue(Current, out var states);
            Debug.Assert(states != null);

            var state = states[states.IndexOf((ITransition<IState, IState>)transition)];

            if (!exist || state.From != Current)
                return false;

            state.Triggered();

            Current.Out();
            Current = state.To;

            Debug.Assert(Current is not null);

            Current.In();

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