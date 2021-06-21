namespace Rst.Interfaces
{
    public interface IStateMachine
    {
        public StateMachine AddTransition<TFrom, TTo>(ITransition<TFrom, TTo> transition)
            where TFrom : IState
            where TTo : IState;

        public bool MoveNext(ITransition<IState, IState> transition);
    }
}