using GameLogic;
using GameLogic.Cycles;
using GameLogic.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace RoleBindingTest
{
    [TestClass]
    public class DriverTest
    {
        [TestMethod]
        public void DriversSwap_SecondaryTargets_DriverIsKilled()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new CitizenRole(), 2UL),
                new Player(new DoctorRole(), 3UL),
                new Player(new PolicemanRole(), 4UL),
                new Player(new DriverRole(), 5UL),
                new Player(new DriverRole(), 6UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[2]);
            //p[4] and [5] are Driver
            //                                       0 1  2--3  4 5
            night.ConfirmAction(p[4], p[2], p[3]); //0 1  3- 2 -4 5
            night.ConfirmAction(p[5], p[2], p[4]); //0 1 [4] 2  3 5

            var states = night.ExecuteActions();

            //Assert
            Assert.IsFalse(p[4].IsAlive);
        }

        [TestMethod]
        public void DriversSwap_SameTargets_DoctorIsKilledAnyway()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new CitizenRole(), 2UL),
                new Player(new DoctorRole(), 3UL),
                new Player(new PolicemanRole(), 4UL),
                new Player(new DriverRole(), 5UL),
                new Player(new DriverRole(), 6UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[2]);
            //p[4] and [5] are Driver
            //                                       0 1  2--3  4 5
            night.ConfirmAction(p[4], p[2], p[3]); //0 1  3--2  4 5
            night.ConfirmAction(p[5], p[3], p[2]); //0 1 [2] 3  4 5

            var states = night.ExecuteActions();

            //Assert
            Assert.IsFalse(p[2].IsAlive);
        }

        [TestMethod]
        public void DriversSpawTwice_DoctorIsTarget_PolicemanIsKilled()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new CitizenRole(), 2UL),
                new Player(new DoctorRole(), 3UL),
                new Player(new PolicemanRole(), 4UL),
                new Player(new DriverRole(), 6UL),
                new Player(new DriverRole(), 7UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[2]);
            //p[4] and [5] are Driver
            //                                       0 1--2  3 4 5
            night.ConfirmAction(p[4], p[1], p[2]); //0 2  1--3 4 5
            night.ConfirmAction(p[5], p[2], p[3]); //0 2 [3] 1 4 5
            night.ConfirmAction(p[2], p[1]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsFalse(p[3].IsAlive);
        }

        [TestMethod]
        public void DriverSpaws_SwapMafiaWithHisTarget_MafiaKillsHimself()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new CitizenRole(), 2UL),
                new Player(new DriverRole(), 3UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[1]);
            //p[2] is Driver
            //                                       0--1  2
            night.ConfirmAction(p[2], p[0], p[1]); //1 [0] 2

            var states = night.ExecuteActions();

            //Assert
            Assert.IsFalse(p[0].IsAlive);
        }

        [TestMethod]
        public void DriverSpaws_SwapSerialWithHisTarget_MafiaKillsHimself()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new SerialKillerRole(), 1UL),
                new Player(new CitizenRole(), 2UL),
                new Player(new DriverRole(), 3UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[1]);
            //p[2] is Driver
            //                                       0--1  2
            night.ConfirmAction(p[2], p[1], p[0]); //1 [0] 2

            var logs = night.ExecuteActions();
            var states = night.ZipActionInfo(logs);

            //Assert
            Assert.IsFalse(p[0].IsAlive);
        }

        [TestMethod]
        public void DriverSwap_WitcherControlMafia_DoctorIsKilled()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new DriverRole(), 2UL),
                new Player(new DoctorRole(), 3UL),
                new Player(new CitizenRole(), 4UL),
                new Player(new WitchRole(), 5UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[2]);
            //p[1] is Driver
            night.ConfirmAction(p[1], p[2], p[3]);// 0 1 [2]-3 (4)
            //p[4] is Witch                          0 1 [3] 2 (4)
            night.ConfirmAction(p[4], p[0], p[3]);// 0 1  3 [2] (4)

            var states = night.ExecuteActions();

            //Assert
            Assert.IsFalse(p[2].IsAlive);
        }

        [TestMethod]
        public void DriverSwap_PoliceActs_PoliceInvestigatesOther()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new CitizenRole(), 2UL),
                new Player(new DriverRole(), 3UL),
                new Player(new PolicemanRole(), 4UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[2], p[0], p[1]);
            night.ConfirmAction(p[2], p[0], p[3]);
            night.ConfirmAction(p[3], p[1]);

            var states = night.ExecuteActions();

            //Assert

        }
    }
}