
namespace Rst.Interfaces
{
    public interface IState
    {
        public delegate void Enter(IState from);
        public delegate void Exit(IState to);

        public void In(IState from);
        public void Out(IState to);
    }
}