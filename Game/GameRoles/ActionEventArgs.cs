using GameLogic.Interfaces;
using GameLogic.Roles;

namespace GameLogic
{
    public delegate void ActionHandler(object sender, ActionEventArgs e);

    public class ActionEventArgs
    {
        public string Log { get; }
        public IAction Action { get; }

        public ActionEventArgs(string message, IAction action)
        {
            this.Log = message;
            this.Action = action;
        }
    }
}