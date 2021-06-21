
namespace Rst.Interfaces
{
    public interface IState
    {
        public delegate void Enter();
        public delegate void Exit();

        public void In();
        public void Out();
    }
}