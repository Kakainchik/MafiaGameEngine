using System;
using GameRoles.Interfaces;

namespace GameRoles.Roles
{
    public class Citizen : Role
    {
        public Citizen(string name) : base(name)
        {

        }

        public override void ExecuteAction()
        {
            return;
        }
    }
}