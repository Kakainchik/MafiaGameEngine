using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль лидера культа. Относится к <see cref="Teams.CULTUS">Культу</see>.
    /// Каждую ночь может завербовать кого-то в культ, не может разговаривать с члена культа, но может слышать их.
    /// Не может завербовать крёстного отца.
    /// </summary>
    public class CultusLeaderRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}