using System;
using System.Collections.Generic;

namespace Rst.Interfaces
{
    public interface IStateMachine : IEnumerator<IState>
    {
        public IStateMachine AddTransition<TFrom, TTo>(
            ITransition<TFrom, TTo> transition)
            where TFrom : IState
            where TTo : IState;
        
        public IStateMachine AddTransition<TFrom, TTo>(ITransition<TFrom, TTo> transition, Action<ITransitionBuilder> action)
            where TFrom : IState
            where TTo : IState;

        public bool MoveNext<TFrom, TTo>(ITransition<TFrom, TTo> transition)
            where TFrom : IState
            where TTo : IState;

        public bool IsValid();
    }
}