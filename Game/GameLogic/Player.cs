using GameLogic.Interfaces;
using GameLogic.Roles;

namespace GameLogic
{
    public class Player : IVoter, IVotable, ILynch, IRoleOwner
    {
        #region Variables

        private Role role;

        #endregion

        #region Properties

        public Role Role => role;

        /// <summary>
        /// Unique id of player.
        /// </summary>
        public Guid Id { get; }
        public string LastMessage { get; set; }
        public string LastWill { get; set; }
        public IExecutor DeathReason { get; private set; }
        public bool IsAlive => Role.IsAlive;

        #region IVoter Properties

        public IVotable VoteTarget { get; set; }

        #endregion

        #region IVotable Properties

        public IList<IVoter> Voters { get; set; }
        public int Votes => Voters.Count;

        #endregion

        #endregion

        public Player(Role role, Guid id)
        {
            this.role = role;
            this.role.Owner = this;
            Id = id;
            Voters = new List<IVoter>();
        }

        #region Interface Implementation

        #region IVoter Implementation

        public bool VoteFor(IVotable whom)
        {
            //May not vote itself
            if(whom == this) return false;

            //Only alive player can vote
            if(!IsAlive) return false;

            //Remove vote from previous target
            if(VoteTarget != null && VoteTarget != whom)
            {
                VoteTarget.RemoveVote(this);
            }

            if(whom.AddVoteFrom(this))
            {
                VoteTarget = whom;
                return true;
            }
            else return false;
        }

        public void Unvote()
        {
            VoteTarget?.RemoveVote(this);
            VoteTarget = null;
        }

        #endregion

        #region IVotable Implementation

        public bool AddVoteFrom(IVoter from)
        {
            //There is already such voter or the player is dead
            if(Voters.Contains(from) || !IsAlive) return false;

            Voters.Add(from);
            return true;
        }

        public void RemoveVote(IVoter voter)
        {
            Voters.Remove(voter);
        }

        #endregion

        #region ILynch Implementation

        public void Lynch()
        {
            Role.IsAlive = false;
        }

        #endregion

        #region IRoleOwner Implementation

        public void ChangeRole(Role role)
        {
            this.role = role;
            this.role.Owner = this;
        }

        public void SetDeathReason(IExecutor killer)
        {
            DeathReason = killer;
        }

        #endregion

        #endregion

        public override string ToString()
        {
            return $"Id = {Id}, IsAlive = {IsAlive}";
        }
    }
}