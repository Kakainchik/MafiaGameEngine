using GameLogic;
using GameLogic.Roles;
using System;
using System.Collections.Generic;

namespace RoleBindingTest
{
    public static class TestConfig
    {
        public static List<Player> MinimumPlayersSetup()
        {
            return new List<Player>
            {
                new Player(new MafiaRole(), 1UL),//0
                new Player(new DoctorRole(), 2UL),//1
                new Player(new CitizenRole(), 3UL),//2
                new Player(new ProstituteRole(), 4UL),//3
                new Player(new CitizenRole(), 5UL),//4
            };
        }

        public static List<Player> SmallPlayersSetup()
        {
            return new List<Player>
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new CitizenRole(), 2UL),
                new Player(new CitizenRole(), 3UL),
                new Player(new CitizenRole(), 4UL),
                new Player(new CitizenRole(), 5UL),
            };
        }

        public static List<Player> LargePlayersSetup()
        {
            return new List<Player>(MinimumPlayersSetup())
            {
                new Player(new MafiaRole(), 6UL),//5
                new Player(new WitchRole(), 7UL),//6
                new Player(new DriverRole(), 8UL),//7
                new Player(new RecruitRole(), 9UL),//8
                new Player(new GodfatherRole(), 10UL),//9
                new Player(new WhoreRole(), 11UL),//10
                new Player(new VigilanteRole(), 12UL),//11
                new Player(new SurgeonRole(), 13UL),//12
                new Player(new PolicemanRole(), 14UL),//13
                new Player(new DetectiveRole(), 15UL),//14
                new Player(new CultistRole(), 16UL),//15
                new Player(new CultusLeaderRole(), 17UL),//16
                new Player(new SerialKillerRole(), 18UL),//17
                new Player(new ZombieRole(), 19UL),//18
                new Player(new CursedRole(), 20UL)//19
            };
        }
    }
}