using System;
using GameRoles.Interfaces;

namespace GameRoles.Roles
{
    public class MafiaRole : Role
    {
        private IAction Kill;

        public MafiaRole() : base()
        {

        }

        public override void DoAction()
        {
            Kill.Execute(Targets.Peek());
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
