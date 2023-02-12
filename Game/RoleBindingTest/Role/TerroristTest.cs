using GameLogic;
using GameLogic.Cycles;
using GameLogic.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace RoleBindingTest
{
    [TestClass]
    public class TerroristTest
    {
        [TestMethod]
        public void TerroristBlow_TargetAndHimself_BothIsKilled()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new TerroristRole(), Guid.NewGuid()),
                new Player(new DoctorRole(), Guid.NewGuid())
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[1]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsFalse(p[0].IsAlive);
            Assert.IsFalse(p[1].IsAlive);
        }

        [TestMethod]
        public void TerroristBlow_DoctorHeals_TargetIsKilled()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new TerroristRole(), Guid.NewGuid()),
                new Player(new DoctorRole(), Guid.NewGuid()),
                new Player(new CitizenRole(), Guid.NewGuid())
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[2]);
            night.ConfirmAction(p[1], p[2]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsFalse(p[0].IsAlive);
            Assert.IsFalse(p[1].IsAlive);
            Assert.IsFalse(p[2].IsAlive);
        }

        [TestMethod]
        public void EveryoneVisitsBlowTarget_ExceptDoctor_EveryoneIsKilled()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new TerroristRole(), Guid.NewGuid()),
                new Player(new PolicemanRole(), Guid.NewGuid()),
                new Player(new MafiaRole(), Guid.NewGuid()),
                new Player(new WhoreRole(), Guid.NewGuid()),
                new Player(new DoctorRole(), Guid.NewGuid())
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[4]);
            night.ConfirmAction(p[1], p[4]);
            night.ConfirmAction(p[2], p[4]);
            night.ConfirmAction(p[3], p[4]);

            var logs = night.ExecuteActions();
            var zip = night.ZipActionInfo(logs);

            //Assert
            foreach(var player in p)
                Assert.IsFalse(player.IsAlive);
        }

        [TestMethod]
        public void TerroristBlow_WhoreBlocks_NobodyIsKilled()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new TerroristRole(), Guid.NewGuid()),
                new Player(new DoctorRole(), Guid.NewGuid()),
                new Player(new WhoreRole(), Guid.NewGuid())
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[1]);
            night.ConfirmAction(p[2], p[0]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsTrue(p[0].IsAlive);
            Assert.IsTrue(p[1].IsAlive);
        }

        [TestMethod]
        public void TerroristBlow_TargetLeftHome_OnlyTerroristIsKilled()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new TerroristRole(), Guid.NewGuid()),
                new Player(new DoctorRole(), Guid.NewGuid()),
                new Player(new CitizenRole(), Guid.NewGuid())
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[1]);
            night.ConfirmAction(p[1], p[2]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsFalse(p[0].IsAlive);
            Assert.IsTrue(p[1].IsAlive);
            Assert.IsTrue(p[2].IsAlive);
        }
    }
}