using System.Collections.Generic;

namespace Rst.Interfaces
{
    public interface IWorkflow : IEnumerator<ITransition<IState, IState>>
    {
        public void Add(ITransition<IState, IState> t);

    }
}