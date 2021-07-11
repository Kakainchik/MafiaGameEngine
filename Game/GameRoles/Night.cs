using GameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Night
    {
        public List<IAction> Actions { get; } = new List<IAction>();

    }
}