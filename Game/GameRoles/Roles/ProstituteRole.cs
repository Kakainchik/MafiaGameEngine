using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль проститутки. Временно относится к <see cref="Teams.CITY">Городу</see>.
    /// Каждую ночь может заблокировать действия другого человека. Вербуется в <see cref="WhoreRole">шлюху</see>.
    /// </summary>
    public class ProstituteRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}