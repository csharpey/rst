using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using Rst.Interfaces;

namespace Rst
{
    public class Workflow : IWorkflow
    {
        private readonly ConcurrentQueue<ITransition<IState, IState>> _transitions;
        private readonly IStateMachine _machine;

        public Workflow(IStateMachine machine)
        {
            _machine = machine;
            _transitions = new ConcurrentQueue<ITransition<IState, IState>>();
        }

        public void Add(ITransition<IState, IState> t)
        {
            _transitions.Enqueue(t);
        }

        public bool MoveNext()
        {
            var success = _transitions.TryDequeue(out var transition);
            if (!success) return false;
            
            Current = transition;
            _machine.MoveNext(transition);

            return true;
        }

        public void Reset()
        {
        }

        public ITransition<IState, IState> Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}