using System;
using System.Diagnostics;
using Rst.Interfaces;

namespace Rst
{
    public class State : IState
    {
        public event IState.Enter OnEntry;
        public event IState.Exit OnExit;

        public State()
        {
            OnEntry = delegate {  };
            OnExit = delegate {  };
        }

        public static bool Connect(IState emitter, Func<int, int> f, IState receiver, Action<int> callback)
        {

            return false;
        }
        
        public void In(IState from)
        {
            Debug.Assert(OnEntry is not null);
            
            OnEntry.Invoke(from);
        }

        public void Out(IState to)
        {
            Debug.Assert(OnExit is not null);
            
            OnExit.Invoke(to);
        }
    }
}
