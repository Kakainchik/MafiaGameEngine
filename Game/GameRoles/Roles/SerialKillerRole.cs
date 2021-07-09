using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль серийного убийцы. Самостоятельный и относится к <see cref="Teams.NEUTRAL"/>.
    /// Каждую ночь может убить кого-то.
    /// </summary>
    public class SerialKillerRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}