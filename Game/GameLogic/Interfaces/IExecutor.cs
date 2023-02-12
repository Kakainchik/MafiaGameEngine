using GameLogic.Actions;
using GameLogic.Roles;

namespace GameLogic.Interfaces
{
    public interface IExecutor
    {
        IAction PrepareAction(Role subject);
        void DisposeAction();
    }
}