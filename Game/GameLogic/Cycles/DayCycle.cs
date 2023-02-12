using GameLogic.Cycles.History;
using GameLogic.Interfaces;

namespace GameLogic.Cycles
{
    public class DayCycle : GameCycle
    {
        private IVotable nonLynchObject;

        public int NonLynchVotes => nonLynchObject.Votes;
        public bool IsBallotBegan { get; set; }

        public DayCycle(List<Player> alivePlayers) : base(alivePlayers)
        {
            nonLynchObject = new Penguin();
        }

        internal event EventHandler<DayMemento> DayEnded;

        public bool VoteFor(Player voter, Player target)
        {
            if(!IsBallotBegan) return false;

            var v = alivePlayers.FirstOrDefault(a => voter.Id.Equals(a.Id));
            var t = alivePlayers.FirstOrDefault(a => target.Id.Equals(a.Id));
            if(t == null || v == null) throw new Exception("The player is dead or not exist");

            return v.VoteFor(t);
        }

        public bool VoteForNonLynch(Player voter)
        {
            if(!IsBallotBegan) return false;

            var v = alivePlayers.FirstOrDefault(a => voter.Id.Equals(a.Id));
            if(v == null) throw new Exception("The player is dead or not exist");

            return v.VoteFor(nonLynchObject);
        }

        public void Unvote(Player voter)
        {
            if(!IsBallotBegan) return;

            var v = alivePlayers.FirstOrDefault(a => voter.Id.Equals(a.Id));
            if(v == null) throw new Exception("The player is dead or not exist");

            v.Unvote();
        }

        public IVotable GetElectionResult()
        {
            int max = 0;
            foreach(IVotable p in alivePlayers)
            {
                if(p.Votes > max) max = p.Votes;
            }
            IVotable player = alivePlayers.First(p => p.Votes == max);

            if(NonLynchVotes > max) max = NonLynchVotes;

            const float P51 = 0.51F;
            float procent = (float)max / alivePlayers.Count;
            if(procent < P51)
                throw new Exception("There is no superiority in votes. Election should proceed");

            //Return null if penguin was elected
            if(NonLynchVotes > player.Votes) player = nonLynchObject;

            return player;
        }

        public bool TryGetElectionResult(out IVotable result)
        {
            int max = 0;
            foreach(IVotable p in alivePlayers)
            {
                if(p.Votes > max) max = p.Votes;
            }
            IVotable player = alivePlayers.First(p => p.Votes == max);

            if(NonLynchVotes > max) max = NonLynchVotes;

            const float P51 = 0.51F;
            float procent = (float)max / alivePlayers.Count;
            if(procent < P51)
            {
                result = null;
                return false;
            }

            //Return null if penguin was elected
            if(NonLynchVotes > player.Votes) player = nonLynchObject;

            result = player;
            return true;
        }

        public void ConfirmElectionResult(IVotable result)
        {
            DayMemento memento = new DayMemento
            {
                electedPlayer = result,
                gotVotes = result?.Votes ?? NonLynchVotes
            };

            DayEnded?.Invoke(this, memento);
        }

        /// <summary>
        /// Clears all election dependencies in this cycle.
        /// </summary>
        public void ClearVotes()
        {
            foreach(var p in alivePlayers)
            {
                p.Voters.Clear();
                p.VoteTarget = null;
            }
            nonLynchObject.Voters.Clear();
        }

        public bool IsItPenguin(IVotable votable) => votable is Penguin;

        /// <summary>
        /// Merely a friendly bird which is voted to avoid any death at current election.
        /// </summary>
        private sealed class Penguin : IVotable
        {
            public IList<IVoter> Voters { get; set; }

            public int Votes => Voters.Count;

            internal Penguin()
            {
                Voters = new List<IVoter>();
            }

            public bool AddVoteFrom(IVoter from)
            {
                //There is already such voter or the player is dead
                if(Voters.Contains(from)) return false;

                Voters.Add(from);
                return true;
            }

            public void RemoveVote(IVoter voter)
            {
                Voters.Remove(voter);
            }
        }
    }
}