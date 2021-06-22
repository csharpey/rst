using System;

namespace Rst.Interfaces
{
    public interface ITransition<out TFrom, out TTo>
        where TFrom : IState
        where TTo : IState
    {
        public delegate void Trigger();
        
        public TFrom From { get; }
        public TTo To { get; }
        public void Triggered();
    }
}