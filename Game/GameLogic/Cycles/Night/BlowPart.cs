using GameLogic.Actions;
using GameLogic.Model;
using GameLogic.Roles;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic.Cycles
{
    public class BlowPart : IOrderPart
    {
        public void ExecuteActions(List<IAction> actions, NightOrder order)
        {
            var byPriority = actions.Where(a => a.Priority == APriority.B32).ToList();

            foreach(var a in byPriority)
            {
                ActionLog bState;

                //If target stays at house then blow it
                if(!actions.Any(t => a.PrimaryTarget == t.Executor))
                {
                    //BUG with doctors whose actions are removed so they 'stay' at home
                    bState = a.AccomplishAction();
                }
                else
                {
                    //If target left the house
                    bState = new ActionLog
                    {
                        Success = false,
                        IsBlocked = a.Executor.IsBlocked,
                        PrimaryTarget = a.PrimaryTarget,
                        Executor = a.Executor
                    };
                }

                order.Result.Enqueue(bState);

                //If blow executor has been blocked then everybody is saved
                if(bState.IsBlocked) continue;

                Queue<IAction> blowActions = new Queue<IAction>();

                //Check executed actions
                foreach(var old in order.Result)
                {
                    //If someone visited blow target
                    if(old.PrimaryTarget == a.PrimaryTarget && old.Executor.IsAlive)
                    {
                        //Kill visiter of blow target
                        BlowAction blow = new BlowAction(old.Executor, a.Executor);

                        blowActions.Enqueue(blow);
                    }
                    else if(old.Executor is DriverRole)
                    {
                        //Check also secondary target of driver
                        //And prevent dublicate if driver swaps the target itself
                        var dold = (DoubleActionLog)old;
                        if(dold.SecondaryTarget == a.PrimaryTarget
                            && dold.PrimaryTarget != dold.SecondaryTarget)
                        {
                            BlowAction blow = new BlowAction(old.Executor, a.Executor);

                            blowActions.Enqueue(blow);
                        }
                    }
                }

                //Transport result from temprorary list
                foreach(var b in blowActions)
                {
                    ActionLog state = b.AccomplishAction();

                    order.Result.Enqueue(state);
                }
            }

            order.Part = new RecruitPart();
        }
    }
}