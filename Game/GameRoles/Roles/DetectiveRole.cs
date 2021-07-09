using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль детектива. Относится к <see cref="Teams.CITY">Городу</see>.
    /// Каждую ночь может изучить одного человека, вследствие чего узнать полностью его роль, узнаёт сразу же.
    /// Крёстного отца видит как мафиози. Лидера культа видит как культиста.
    /// </summary>
    public class DetectiveRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}