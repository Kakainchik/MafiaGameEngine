using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions
{
    public interface IAction
    {
        APriority Priority { get; }
        Role Executor { get; }
        Role PrimaryTarget { get; set; }

        ActionLog AccomplishAction();
    }
}