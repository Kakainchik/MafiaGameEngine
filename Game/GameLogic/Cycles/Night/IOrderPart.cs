using GameLogic.Actions;

namespace GameLogic.Cycles
{
    public interface IOrderPart
    {
        void ExecuteActions(List<IAction> actions, NightOrder order);
    }
}