using GameLogic;
using GameLogic.Cycles;
using GameLogic.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace RoleBindingTest
{
    [TestClass]
    public class CultusTest
    {
        [TestMethod]
        public void CultusLeaderRecruit_Citizen_CultistAppears()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new CultusLeaderRole(), 1UL),
                new Player(new CitizenRole(), 2UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[1]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsInstanceOfType(p[1].Role, typeof(CultistRole));
        }

        [TestMethod]
        public void CultusLeaderRecruit_Cultist_ActionIsNotSuccess()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new CultusLeaderRole(), 1UL),
                new Player(new CultistRole(), 2UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[1]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsInstanceOfType(p[1].Role, typeof(CultistRole));
            Assert.IsFalse(states.Peek().Success);
        }

        [TestMethod]
        public void CultusLeaderRecruit_Godfather_ActionIsNotSuccess()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new CultusLeaderRole(), 1UL),
                new Player(new GodfatherRole(), 2UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[1]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsInstanceOfType(p[1].Role, typeof(GodfatherRole));
            Assert.IsFalse(states.Peek().Success);
        }
    }
}