using GameLogic.Actions;
using GameLogic.Model;
using System.Collections.Generic;

namespace GameLogic.Cycles
{
    public interface IOrderPart
    {
        void ExecuteActions(List<IAction> actions, NightOrder order);
    }
}