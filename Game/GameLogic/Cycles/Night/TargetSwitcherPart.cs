using GameLogic.Actions;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Cycles
{
    public class TargetSwitcherPart : IOrderPart
    {
        public void ExecuteActions(List<IAction> actions, NightOrder order)
        {
            var byPriority = actions.Where(a => a.Priority == APriority.B1).ToList();

            //Get Witcher actions
            var controlA = byPriority.OfType<ControlAction>().ToList();
            foreach(var a in controlA)
            {
                //Find the action of primary control target from the general list
                IAction primaryA = actions.SingleOrDefault(e => e.Executor == a.PrimaryTarget);
                
                if(primaryA == null)
                {
                    //Primary target is not executor or does not have an action at this night
                    //therefore create the action

                    //Do nothing cause Witch cannot create DoubleAction
                    if(a.PrimaryTarget is IDoubleExecutor) continue;

                    primaryA = (a.PrimaryTarget as IExecutor)?.PrepareAction(a.SecondaryTarget);
                    //Add new action to the list if primary target is executor
                    //otherwise do nothing
                    if(primaryA != null) actions.Add(primaryA);
                }
                else
                {
                    //Primary target has own action
                    //therefore change it
                    primaryA.PrimaryTarget = a.SecondaryTarget;
                }

                //Book action and remove to prevent dublicate
                ActionLog state = a.AccomplishAction();
                order.Result.Enqueue(state);

                byPriority.Remove(a);
            }

            //Get Driver actions
            var driveA = byPriority.OfType<DriveAction>().ToList();
            foreach(var a in driveA)
            {
                //Find all actions where their target is driver primary one
                var executorsOnP = actions.Where(
                    e => e.PrimaryTarget == a.PrimaryTarget && e != a).ToList();

                //Find all actions where their target is driver secondary one
                var executorsOnS = actions.Where(
                    e => e.PrimaryTarget == a.SecondaryTarget && e != a).ToList();

                //Swap P target onto S
                foreach(IAction p in executorsOnP)
                {
                    p.PrimaryTarget = a.SecondaryTarget;
                    //Check if this executor also has secondary target
                    if(p is IDoubleAction pd && pd.SecondaryTarget == a.SecondaryTarget)
                        pd.SecondaryTarget = a.PrimaryTarget;
                }

                //Swap S target onto P
                foreach(IAction s in executorsOnS)
                {
                    s.PrimaryTarget = a.PrimaryTarget;
                    //Check if this executor also has secondary target
                    if(s is IDoubleAction sd && sd.SecondaryTarget == a.PrimaryTarget)
                        sd.SecondaryTarget = a.SecondaryTarget;
                }

                //Book action and remove to prevent dublicate
                ActionLog state = a.AccomplishAction();
                order.Result.Enqueue(state);

                byPriority.Remove(a);
            }
            
            //Execute actions and put them to result
            foreach(var a in byPriority)
            {
                ActionLog state = a.AccomplishAction();
                order.Result.Enqueue(state);
            }

            order.Part = new BlockerPart();
        }
    }
}