using System;
using Rst.Interfaces;

namespace Rst
{
    public class Transition<TFrom, TTo> : ITransition<TFrom, TTo> 
        where TTo : IState
        where TFrom : IState
    {
        public TFrom From { get; }
        public TTo To { get; }

        protected Transition(TFrom from, TTo to)
        {
            From = from;
            To = to;
        }
    }
}