using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль культиста. Относится к <see cref="Teams.CULTUS">Культу</see>.
    /// Ночью может разговаривать с другими культистами, но личность Лидера остаётся тайной.
    /// </summary>
    public class CultistRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}