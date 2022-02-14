using System.Reflection.Metadata;
using Rst.Interfaces;

namespace Rst
{
    public interface IHandler<in TFrom, in TTo>
        where TFrom : IState
        where TTo : IState
    {
        public void Handle(TFrom from, TTo to);
    }
}