using GameLogic.Roles;
using System.Collections.Generic;

namespace GameLogic
{
    public class DailyMeeteng
    {
        public int Day { get; }
        public List<Role> AlivePlayers { get; }

        public DailyMeeteng(List<Role> alivePlayers, int day)
        {
            this.Day = day;
            this.AlivePlayers = alivePlayers;
        }

        public void StartVoting()
        {

        }
    }
}