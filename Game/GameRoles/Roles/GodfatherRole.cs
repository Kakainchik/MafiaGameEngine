using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль крёстного отца. Относится к <see cref="Teams.MAFIA">Мафии</see>.
    /// Каждую ночь приказывает <see cref="MafiaRole">мафиози</see>, кого им убить, также может вербовать <see cref="RecruitRole">рекрутов</see> и <see cref="ProstituteRole">проституток</see>.
    /// Не может быть убит ночью.
    /// </summary>
    public class GodfatherRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}