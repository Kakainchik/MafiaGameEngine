using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль проклятого. Временно относится к <see cref="Teams.CITY">Городу</see>.
    /// Если умрёт ночью, то превратится в <see cref="ZombieRole">Зомби</see>.
    /// </summary>
    public class CursedRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}