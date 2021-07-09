using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль водителя. Относится к <see cref="Teams.CITY">Городу</see>.
    /// Каждую ночь может поменять местами двух людей, вследствие чего действия, направленные ночью на них, поменяются между ними.
    /// </summary>
    public class DriverRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}