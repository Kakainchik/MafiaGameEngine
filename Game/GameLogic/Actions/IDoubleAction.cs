using GameLogic.Roles;

namespace GameLogic.Actions
{
    public interface IDoubleAction : IAction
    {
        Role SecondaryTarget { get; set; }
    }
}