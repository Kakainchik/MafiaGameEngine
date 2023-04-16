using GameLogic;
using GameLogic.Cycles;
using GameLogic.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace RoleBindingTest
{
    [TestClass]
    public class ZombieTest
    {
        [TestMethod]
        public void MakeAction_ZombieRessurect_VictimIsZombie()
        {
            //Arrange
            List<Player> p = TestConfig.MinimumPlayersSetup();
            p.Add(new Player(new ZombieRole(), 1UL));
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[2]);
            //p[1] is Doctor
            night.ConfirmAction(p[1], p[2]);
            //p[3] is Prostitute
            night.ConfirmAction(p[3], p[1]);
            //p[5] is Zombie
            night.ConfirmAction(p[5], p[2]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsInstanceOfType(p[2].Role, typeof(ZombieRole));
        }

        [TestMethod]
        public void ZombieRevive_PoliceInvestigate_ResultIsCitizen()
        {
            //Arrange
            List<Player> p = TestConfig.MinimumPlayersSetup();
            p.Add(new Player(new ZombieRole(), 6UL));
            p.Add(new Player(new PolicemanRole(), 7UL));
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[2]);
            //p[5] is Zombie
            night.ConfirmAction(p[5], p[2]);
            //p[6] is Police
            night.ConfirmAction(p[6], p[2]);

            var states = night.ExecuteActions().ToArray();

            //Assert
            Assert.IsInstanceOfType(states[2].PrimaryTarget, typeof(CitizenRole));
        }

        [TestMethod]
        public void CitizenIsKilledTwice_TwoDoctorsRevive_CitizenIsResurrected()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new VigilanteRole() { Bullets = 1 }, 2UL),
                new Player(new DoctorRole(), 3UL),
                new Player(new DoctorRole(), 4UL),
                new Player(new CitizenRole(), 5UL),
                new Player(new ZombieRole(), 6UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[4]);
            //p[1] is Vigi
            night.ConfirmAction(p[1], p[4]);
            //p[2] and p[3] are Doctor
            night.ConfirmAction(p[2], p[4]);
            night.ConfirmAction(p[3], p[4]);
            //p[5] is Zombie
            night.ConfirmAction(p[5], p[4]);

            var state = night.ExecuteActions();

            //Assert
            Assert.IsTrue(p[4].IsAlive);
            Assert.IsInstanceOfType(p[4].Role, typeof(ZombieRole));
        }
    }
}