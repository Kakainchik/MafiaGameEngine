using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль виджиланте. Относится к <see cref="Teams.CITY">Городу</see>.
    /// Каждую ночью может убить кого-то, пока не кончатся патроны.
    /// </summary>
    public class VigilanteRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}