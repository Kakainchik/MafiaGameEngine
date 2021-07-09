using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль советника. Относится к <see cref="Teams.MAFIA">Мафии</see>.
    /// Каждую ночь может изучить одного человека, вследствие чего узнать его роль.
    /// </summary>
    public class CounselorRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}