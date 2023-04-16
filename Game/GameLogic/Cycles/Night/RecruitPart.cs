using GameLogic.Actions;
using GameLogic.Model;

namespace GameLogic.Cycles
{
    public class RecruitPart : IOrderPart
    {
        public void ExecuteActions(List<IAction> actions, NightOrder order)
        {
            var byPriority = actions.Where(a => a.Priority == APriority.B128).ToList();

            foreach(var a in byPriority)
            {
                ActionLog state = a.AccomplishAction();

                order.Result.Enqueue(state);
            }

            order.Part = null;
        }
    }
}