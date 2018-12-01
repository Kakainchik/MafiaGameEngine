using System;
using GameRoles.Interfaces;

namespace GameRoles.Roles
{
    public class CitizenRole : Role
    {
        public CitizenRole(string name) : base(name)
        {

        }

        public override void ExecuteAction(Role whom)
        {
            return;
        }
    }
}