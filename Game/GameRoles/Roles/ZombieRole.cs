using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль зомби. Относится к <see cref="Teams.UNDEAD">Нежити</see>.
    /// Каждую ночь решает, кого возродить из мёртвых.
    /// </summary>
    public class ZombieRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}