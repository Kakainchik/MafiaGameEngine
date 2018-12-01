using System;

namespace GameRoles
{
    public delegate void ActionHandler(object sender, ActionEventArgs e);

    public class ActionEventArgs
    {
        public string Log { get; }
        public Role Who { get; }

        public ActionEventArgs(string mes, Role role)
        {
            this.Log = mes;
            this.Who = role;
        }
    }
}
