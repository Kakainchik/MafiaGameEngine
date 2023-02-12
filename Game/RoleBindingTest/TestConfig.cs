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
                new Player(new MafiaRole(), Guid.NewGuid()),//0
                new Player(new DoctorRole(), Guid.NewGuid()),//1
                new Player(new CitizenRole(), Guid.NewGuid()),//2
                new Player(new ProstituteRole(), Guid.NewGuid()),//3
                new Player(new CitizenRole(), Guid.NewGuid()),//4
            };
        }

        public static List<Player> SmallPlayersSetup()
        {
            return new List<Player>
            {
                new Player(new MafiaRole(), Guid.NewGuid()),
                new Player(new CitizenRole(), Guid.NewGuid()),
                new Player(new CitizenRole(), Guid.NewGuid()),
                new Player(new CitizenRole(), Guid.NewGuid()),
                new Player(new CitizenRole(), Guid.NewGuid()),
            };
        }

        public static List<Player> LargePlayersSetup()
        {
            return new List<Player>(MinimumPlayersSetup())
            {
                new Player(new MafiaRole(), Guid.NewGuid()),//5
                new Player(new WitchRole(), Guid.NewGuid()),//6
                new Player(new DriverRole(), Guid.NewGuid()),//7
                new Player(new RecruitRole(), Guid.NewGuid()),//8
                new Player(new GodfatherRole(), Guid.NewGuid()),//9
                new Player(new WhoreRole(), Guid.NewGuid()),//10
                new Player(new VigilanteRole(), Guid.NewGuid()),//11
                new Player(new SurgeonRole(), Guid.NewGuid()),//12
                new Player(new PolicemanRole(), Guid.NewGuid()),//13
                new Player(new DetectiveRole(), Guid.NewGuid()),//14
                new Player(new CultistRole(), Guid.NewGuid()),//15
                new Player(new CultusLeaderRole(), Guid.NewGuid()),//16
                new Player(new SerialKillerRole(), Guid.NewGuid()),//17
                new Player(new ZombieRole(), Guid.NewGuid()),//18
                new Player(new CursedRole(), Guid.NewGuid())//19
            };
        }
    }
}