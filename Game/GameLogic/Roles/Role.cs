using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Обобщающий класс для всех ролей. Содержит основные свойства.
    /// </summary>
    public abstract class Role
    {
        public Player Owner { get; set; }
        public bool IsAlive { get; set; } = true;

        public bool IsBlocked { get; set; }

        /// <summary>
        /// Determines if this role is unique in the entire game.
        /// </summary>
        public abstract bool IsUnique { get; }

        public virtual ActionLog Block()
        {
            //Almost always blockable
            IsBlocked = true;
            return new ActionLog
            {
                Success = true,
                PrimaryTarget = this
            };
        }

        public virtual ActionLog Kill(IExecutor killer)
        {
            if(IsAlive) Owner.SetDeathReason(killer);
            IsAlive = false;
            return new ActionLog
            {
                Success = true,
                PrimaryTarget = this
            };
        }

        public virtual ActionLog Heal()
        {
            if(!IsAlive)
            {
                IsAlive = true;
                //Clear killer
                Owner.SetDeathReason(null);
                return new ActionLog
                {
                    Success = true,
                    PrimaryTarget = this
                };
            }
            else return new ActionLog
            {
                Success = false,
                PrimaryTarget = this
            };
        }

        public virtual ActionLog Ressurect()
        {
            if(!IsAlive)
            {
                Owner.ChangeRole(new ZombieRole());
                //Clear killer
                Owner.SetDeathReason(null);
                return new ActionLog
                {
                    Success = true,
                    PrimaryTarget = this
                };
            }
            else return new ActionLog
            {
                Success = false,
                PrimaryTarget = this
            };
        }

        public virtual ActionLog RecruitCultus()
        {
            //Almost everyone can be recruit to cultus
            if(IsAlive)
            {
                Owner.ChangeRole(new CultistRole());
                return new ActionLog
                {
                    Success = true,
                    PrimaryTarget = this
                };
            }
            else return new ActionLog
            {
                Success = false,
                PrimaryTarget = this
            };
        }

        public virtual ActionLog RecruitMafia()
        {
            //Only specific roles can be recruited
            return new ActionLog
            {
                Success = false,
                PrimaryTarget = this
            };
        }
    }
}