using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль масона. Относится к <see cref="Teams.CITY">Городу</see>.
    /// Может общаться с другими масонами ночью.
    /// </summary>
    public class MasonRole : Role
    {
        public override void OnWasDied()
        {
            throw new NotImplementedException();
        }
    }
}