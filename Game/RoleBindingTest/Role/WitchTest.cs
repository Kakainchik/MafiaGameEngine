using GameLogic;
using GameLogic.Cycles;
using GameLogic.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace RoleBindingTest
{
    [TestClass]
    public class WitchTest
    {
        [TestMethod]
        public void WitchControlMafia_MafiaNotActing_CitizenIsKilled()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new CitizenRole(), 2UL),
                new Player(new CitizenRole(), 3UL),
                new Player(new WitchRole(), 4UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            //p[3] is Witch
            //                                       0 1  2  (3)
            night.ConfirmAction(p[3], p[0], p[2]);// 0 1 [2] (3)

            var states = night.ExecuteActions();

            //Assert
            Assert.IsFalse(p[2].IsAlive);
        }

        [TestMethod]
        public void WitchControlMafia_MafiaKillOther_CitizenIsKilled()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new CitizenRole(), 2UL),
                new Player(new CitizenRole(), 3UL),
                new Player(new WitchRole(), 4UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[1]);
            //p[3] is Witch
            // 0 [1]  2  (3)
            night.ConfirmAction(p[3], p[0], p[2]);// 0  1  [2] (3)

            var states = night.ExecuteActions();

            //Assert
            Assert.IsTrue(p[1].IsAlive);
            Assert.IsFalse(p[2].IsAlive);
        }

        [TestMethod]
        public void WitchControlMafia_TwoMafia_RandomTargerAnyway()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new MafiaRole(), 2UL),
                new Player(new CitizenRole(), 3UL),
                new Player(new CitizenRole(), 4UL),
                new Player(new WitchRole(), 5UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] and p[1] are Mafia
            night.ConfirmAction(p[0], p[2]);
            night.ConfirmAction(p[1], p[2]);
            //p[4] is Witch
            //                                       0  1  [2] 3 (4)
            night.ConfirmAction(p[4], p[0], p[1]);// 0 [1] [2] 3 (4)

            var states = night.ExecuteActions();

            //Assert
            Assert.IsTrue(p[1].IsAlive || p[2].IsAlive);
        }

        [TestMethod]
        public void WitchControlMafia_ProstituteBlock_KillNotExecuted()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new CitizenRole(), 2UL),
                new Player(new ProstituteRole(), 3UL),
                new Player(new WitchRole(), 4UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[1]);
            //p[2] is Prostitute
            night.ConfirmAction(p[2], p[0]);
            //p[3] is Witch
            //                                        0  [1] 2  (3)
            night.ConfirmAction(p[3], p[0], p[2]);// <0>  1 [2] (3)

            var states = night.ExecuteActions();

            //Assert
            Assert.IsTrue(p[0].Role.IsBlocked);
            Assert.IsTrue(p[1].IsAlive);
        }

        [TestMethod]
        public void WitchActs_ProstituteBlocksWitch_NoBloking()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new CitizenRole(), 2UL),
                new Player(new CitizenRole(), 3UL),
                new Player(new WitchRole(), 4UL)
            };
            NightCycle night = new NightCycle(p);

            //Act

            var states = night.ExecuteActions();

            //Assert

        }
    }
}