using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль шлюхи. Относится к <see cref="Teams.MAFIA">Мафии</see>.
    /// Каждую ночь может заблокировать действия другого человека.
    /// </summary>
    public class WhoreRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}