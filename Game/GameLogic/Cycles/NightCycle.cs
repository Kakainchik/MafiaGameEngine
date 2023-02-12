using GameLogic.Actions;
using GameLogic.Actions.ActionTemplates;
using GameLogic.Cycles.History;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Cycles
{
    public class NightCycle : GameCycle
    {
        private List<IAction> actions = new List<IAction>();
        private NightOrder order;

        public NightCycle(List<Player> alivePlayers) : base(alivePlayers)
        {
            
        }

        internal event EventHandler<NightMemento> NightEnded;

        public void ConfirmAction(Player executor, Player target)
        {
            IExecutor exRole = executor.Role as IExecutor;
            if(exRole == null)
                throw new Exception("This player cannot do any actions");

            //Save action for the future calculation
            var certainAction = exRole.PrepareAction(target.Role);
            actions.Add(certainAction);
        }

        public void ConfirmAction(Player executor, Player primary, Player secondary)
        {
            IDoubleExecutor exRole = executor.Role as IDoubleExecutor;
            if(exRole == null)
                throw new Exception("This player cannot do any double actions");

            //Save action for the future calculation
            exRole.SetSecondarySubject(secondary.Role);
            var certainAction = exRole.PrepareAction(primary.Role);
            actions.Add(certainAction);
        }

        public Queue<ActionLog> ExecuteActions()
        {
            order = new NightOrder(new TargetSwitcherPart(), actions);
            while(order.Part != null)
            {
                order.Proceed();
            }

            return order.Result;
        }

        public Queue<BaseDetail> ZipActionInfo(IEnumerable<ActionLog> logs)
        {
            TemplateState tstate = new TemplateState(logs);
            var result = tstate.ZipLogs();

            NightMemento memento = new NightMemento
            {
                revealedActions = logs,
                nightPlayer = base.alivePlayers
            };
            NightEnded?.Invoke(this, memento);

            return result;
        }

        /// <summary>
        /// Clear all actions of executors at this night.
        /// </summary>
        public void DisposeExecutors()
        {
            foreach(var a in alivePlayers)
            {
                if(a is IExecutor ai)
                    ai.DisposeAction();
            }
        }
    }
}