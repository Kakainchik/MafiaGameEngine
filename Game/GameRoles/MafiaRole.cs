using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRoles
{
    public class MafiaRole : Role
    {
        public MafiaRole(string name) : base(name)
        {
        }

        public override void ExecuteAction(Role whom)
        {
            whom.Kill(this);
        }
    }
}
