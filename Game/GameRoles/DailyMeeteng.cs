using GameLogic.Roles;
using System.Collections.Generic;

namespace GameLogic
{
    public delegate void VoteStartEventHandler(object sender, List<Player> players);
    public delegate void VoteEventHandler(object sender, Player selectedPlayer);

    public class DailyMeeteng
    {
        private VoteStartEventHandler voteStartHandler;
        private VoteEventHandler voteEndHandler;

        public int Day { get; }
        public int NonLynchVotes { get; set; } = 0;
        public List<Player> AlivePlayers { get; }

        public DailyMeeteng(List<Player> alivePlayers, int day, VoteStartEventHandler voteStartHandler, VoteEventHandler voteEndHandler)
        {
            this.Day = day;
            this.voteStartHandler = voteStartHandler;
            this.voteEndHandler = voteEndHandler;
            this.AlivePlayers = alivePlayers;
            
            foreach(var p in AlivePlayers) p.MadeVote += Player_MadeVote;
        }

        private void Player_MadeVote(object sender, Player selectedPlayer)
        {
            if(selectedPlayer != null) selectedPlayer.AddVote();
            else NonLynchVotes++;
        }

        public void StartVoting()
        {
            this.voteStartHandler?.Invoke(this, AlivePlayers);
        }

        public void EndVoting(Player selected)
        {
            foreach(var p in AlivePlayers) p.MadeVote -= Player_MadeVote;

            this.voteEndHandler?.Invoke(this, selected);
            selected?.Lynch();
        }
    }
}