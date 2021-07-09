using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль рекрута. Временно относится к <see cref="Teams.CITY">Городу</see>.
    /// Нет никаких способностей. Вербуется в <see cref="MafiaRole">мафиози</see>.
    /// </summary>
    public class RecruitRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}