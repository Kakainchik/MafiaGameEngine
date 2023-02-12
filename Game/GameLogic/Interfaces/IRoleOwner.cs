using GameLogic.Roles;

namespace GameLogic.Interfaces
{
    public interface IRoleOwner
    {
        Role Role { get; }

        void ChangeRole(Role role);
        void SetDeathReason(IExecutor killer);
    }
}