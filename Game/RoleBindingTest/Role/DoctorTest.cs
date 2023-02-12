using GameLogic;
using GameLogic.Cycles;
using GameLogic.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace RoleBindingTest
{
    [TestClass]
    public class DoctorTest
    {
        [TestMethod]
        public void MafiaKill_DoctorRevive_TargetIsAlive()
        {
            //Arrange
            List<Player> p = TestConfig.MinimumPlayersSetup();
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[2]);
            //p[1] is Doctor
            night.ConfirmAction(p[1], p[2]);

            night.ExecuteActions();

            //Assert
            Assert.IsTrue(p[2].IsAlive);
        }

        [TestMethod]
        public void CitizenIsKilledTwice_TwoDoctorsRevive_CitizenIsKilledAnyway()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new MafiaRole(), Guid.NewGuid()),
                new Player(new VigilanteRole() { Bullets = 1 }, Guid.NewGuid()),
                new Player(new DoctorRole(), Guid.NewGuid()),
                new Player(new DoctorRole(), Guid.NewGuid()),
                new Player(new CitizenRole(), Guid.NewGuid())
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

            var state = night.ExecuteActions();

            //Assert
            Assert.IsFalse(p[4].IsAlive);
        }
    }
}