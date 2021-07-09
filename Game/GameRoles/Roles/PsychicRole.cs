using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль медиума. Относится к <see cref="Teams.UNDEAD">Нежити</see>.
    /// Ночью может обращаться к мёртвым.
    /// </summary>
    public class PsychicRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}