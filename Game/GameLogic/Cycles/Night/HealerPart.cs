﻿using GameLogic.Actions;
using GameLogic.Model;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic.Cycles
{
    public class HealerPart : IOrderPart
    {
        public void ExecuteActions(List<IAction> actions, NightOrder order)
        {
            var byPriority = actions.Where(a => a.Priority == APriority.B8).ToList();

            foreach(var a in byPriority)
            {
                ActionLog state = a.AccomplishAction();

                order.Result.Enqueue(state);
            }

            order.Part = new InvestigatePart();
        }
    }
}