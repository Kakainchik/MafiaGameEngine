using GameLogic;
using GameLogic.Cycles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace RoleBindingTest
{
    [TestClass]
    public class MafiaTest
    {
        [TestMethod]
        public void MafiaKill_Citizen_IsNotAlive()
        {
            //Arrange
            List<Player> p = TestConfig.MinimumPlayersSetup();
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[1]);

            night.ExecuteActions();

            //Assert
            Assert.IsFalse(p[1].IsAlive);
        }
    }
}