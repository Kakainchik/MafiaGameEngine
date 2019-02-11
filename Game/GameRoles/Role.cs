using GameRoles.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameRoles
{
    public abstract class Role
    {
        #region Variables
        
        protected bool isAlive = true;
        protected string name;
        protected Stack<Role> targets;
        public readonly byte Priority;

        #endregion

        #region Properties

        public bool IsAlive { get => isAlive; protected set => isAlive = value; }
        public string Name { get => name; protected set => name = value; }
        public Stack<Role> Targets { get => targets; protected set => targets = value; }

        #endregion

        #region Events

        public event ActionHandler WasKilled;
        public event ActionHandler WasRevived;

        #endregion

        public Role()
        {
        }

        #region Abstract methods

        public abstract void DoAction();
        public abstract void Undo();

        #endregion

        #region Methods

        public virtual bool AddTarget(Role who)
        {
            if(who.IsAlive)
            {
                this.Targets.Push(who);
                return true;
            }
            else return false;
        }

        public void ClearTargets(Role who)
        {
            this.Targets.Clear();
        }

        public override string ToString()
        {
            return $"{Name} is {(IsAlive?"Alive":"Dead")};";
        }

        #endregion

        #region Handlers

        private void CallEvent(ActionHandler handler, ActionEventArgs e)
        {
            if(handler != null && e != null) handler(this, e);
        }

        protected virtual void OnDied(ActionEventArgs e) => this.CallEvent(WasKilled, e);
        protected virtual void OnRevived(ActionEventArgs e) => this.CallEvent(WasRevived, e);

        #endregion
    }
}