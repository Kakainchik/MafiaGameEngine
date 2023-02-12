using GameLogic.Roles;

namespace GameLogic.Interfaces
{
    public interface IDoubleExecutor : IExecutor
    {
        void SetSecondarySubject(Role secondary);
    }
}
