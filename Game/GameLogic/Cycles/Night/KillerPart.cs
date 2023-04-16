using GameLogic.Actions;
using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Cycles
{
    public class KillerPart : IOrderPart
    {
        public void ExecuteActions(List<IAction> actions, NightOrder order)
        {
            var byPriority = actions.Where(a => a.Priority == APriority.B4).ToList();

            //If there is mafia
            var mafs = byPriority.Where(a => a.Executor is MafiaRole);
            if(mafs.Any())
            {
                //Just one mafia can kill per night
                //Randomize the choise
                Random ran = new Random();
                int i = ran.Next(mafs.Count());

                IAction sample = mafs.ElementAt(i);

                //Exclude mafs
                byPriority.RemoveAll(a => a.Executor is MafiaRole);
                byPriority.Add(sample);
            }

            //If there is vigilante
            var vigs = byPriority.Where(a => a.Executor is VigilanteRole).ToList();
            foreach(IAction va in vigs)
            {
                VigilanteRole v = (VigilanteRole)va.Executor;
                if(!v.HasBullets)
                {
                    //If Witch forced Vigi to kill without bullets
                    //Delete action
                    byPriority.Remove(va);
                    continue;
                }
                if(!v.IsBlocked) v.LoadGun();
            }

            //Add other actions to queue
            foreach(var a in byPriority)
            {
                ActionLog state = a.AccomplishAction();

                order.Result.Enqueue(state);

                if(state.Success)
                {
                    //Find doctor after success killing except killed one
                    var docsA = actions.OfType<HealAction>().Where(
                        d => d.PrimaryTarget == state.PrimaryTarget
                        && state.PrimaryTarget != d.Executor);
                    if(docsA.Any())
                    {
                        //If one or many doctors heal killed target
                        foreach(var d in docsA)
                        {
                            //Doctors can save only once and cannot prevent second kill
                            ActionLog dState = d.AccomplishAction();

                            order.Result.Enqueue(dState);
                        }

                        //Remove doctor actions
                        actions.RemoveAll(r => r.PrimaryTarget == state.PrimaryTarget && r is HealAction);
                    }
                }
            }

            order.Part = new HealerPart();
        }
    }
}