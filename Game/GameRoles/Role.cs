using GameRoles.Interfaces;

namespace GameRoles
{
    public abstract class Role
    {
        #region Variables

        protected bool isAlive = true;
        protected string name;

        #endregion

        #region Properties

        public bool IsAlive { get => isAlive; protected set => isAlive = value; }
        public string Name { get => name; protected set => name = value; }

        #endregion

        #region Events

        public event ActionHandler WasDied;
        public event ActionHandler WasRevived;

        #endregion

        public Role(string name)
        {
            this.Name = name;
        }

        public abstract void ExecuteAction();

        public void Kill(Role who)
        {
            this.IsAlive = false;
            OnDied(new ActionEventArgs($"This person was killed by {who.Name}", who));
        }

        public void Revive(Role who)
        {
            this.IsAlive = true;
            OnRevived(new ActionEventArgs($"This person was revived by {who.Name}", who));
        }

        private void CallEvent(ActionHandler handler, ActionEventArgs e)
        {
            if(handler != null && e != null) handler(this, e);
        }

        protected virtual void OnDied(ActionEventArgs e) => this.CallEvent(WasDied, e);

        protected virtual void OnRevived(ActionEventArgs e) => this.CallEvent(WasRevived, e);
    }
}