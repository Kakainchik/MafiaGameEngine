using GameLogic.Actions;
using GameLogic.Model;

namespace GameLogic.Cycles
{
    public class NightOrder
    {
        private List<IAction> actions;
        private Queue<ActionLog> resultStates;

        public IOrderPart Part { get; set; }
        public Queue<ActionLog> Result => resultStates;

        public NightOrder(IOrderPart part, List<IAction> actions)
        {
            this.actions = actions;
            Part = part;
            resultStates = new Queue<ActionLog>();
        }

        public void Proceed()
        {
            Part.ExecuteActions(actions, this);
        }
    }
}