using GameLogic;
using GameLogic.Cycles;
using GameLogic.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace RoleBindingTest
{
    [TestClass]
    public class RecruitTest
    {
        [TestMethod]
        public void GodfatherRecruit_Recuiter_ConvertToMafia()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new GodfatherRole(), 1UL),
                new Player(new RecruitRole(), 2UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[1]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsInstanceOfType(p[1].Role, typeof(MafiaRole));
        }

        [TestMethod]
        public void GodfatherRecruit_Prostitute_ConvertToWhore()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new GodfatherRole(), 1UL),
                new Player(new ProstituteRole(), 2UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[1]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsInstanceOfType(p[1].Role, typeof(WhoreRole));
        }

        [TestMethod]
        public void GodfatherRecruit_ProstituteBlocks_CannotConvert()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new GodfatherRole(), 1UL),
                new Player(new ProstituteRole(), 2UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[1]);
            night.ConfirmAction(p[1], p[0]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsInstanceOfType(p[1].Role, typeof(ProstituteRole));
        }
    }
}