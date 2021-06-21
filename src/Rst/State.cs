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
        
        public void In()
        {
            Debug.Assert(OnEntry is not null);
            
            OnEntry.Invoke();
        }

        public void Out()
        {
            Debug.Assert(OnExit is not null);
            
            OnExit.Invoke();
        }
    }
}
