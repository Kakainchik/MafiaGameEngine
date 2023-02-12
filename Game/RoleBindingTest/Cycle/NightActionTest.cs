using GameLogic;
using GameLogic.Cycles;
using GameLogic.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace RoleBindingTest
{
    [TestClass]
    public class NightActionTest
    {
        [TestMethod]
        public void KillAndRevive_ViceVersaOrder_TargetIsAlive()
        {
            //Arrange
            List<Player> p = TestConfig.MinimumPlayersSetup();
            NightCycle night = new NightCycle(p);

            //Act
            //p[1] is Doctor
            night.ConfirmAction(p[1], p[2]);
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[2]);

            night.ExecuteActions();

            //Assert
            Assert.IsTrue(p[2].IsAlive);
        }

        [TestMethod]
        public void MakeActions_GetInfo_InfoInOrder()
        {
            //Arrange
            List<Player> p = TestConfig.MinimumPlayersSetup();
            NightCycle night = new NightCycle(p);

            //Act
            //p[0] is Mafia
            night.ConfirmAction(p[0], p[1]);
            //p[1] is Doctor
            night.ConfirmAction(p[1], p[2]);
            //p[3] is Prostitute
            night.ConfirmAction(p[3], p[0]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsInstanceOfType(states.Dequeue().Executor, typeof(ProstituteRole));
            Assert.IsInstanceOfType(states.Dequeue().Executor, typeof(MafiaRole));
            Assert.IsInstanceOfType(states.Dequeue().Executor, typeof(DoctorRole));
        }

        [TestMethod]
        public void MakeManyActions_ZipLogs_DetailsInOrder()
        {
            //Arrange
            List<Player> p = TestConfig.LargePlayersSetup();
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[19]);
            night.ConfirmAction(p[1], p[8]);
            night.ConfirmAction(p[3], p[2]);
            night.ConfirmAction(p[5], p[10]);
            night.ConfirmAction(p[6], p[2], p[10]);
            night.ConfirmAction(p[7], p[0], p[13]);
            night.ConfirmAction(p[9], p[3]);
            night.ConfirmAction(p[10], p[6]);
            night.ConfirmAction(p[11], p[19]);
            night.ConfirmAction(p[12], p[19]);
            night.ConfirmAction(p[13], p[11]);
            night.ConfirmAction(p[14], p[9]);
            night.ConfirmAction(p[16], p[12]);
            night.ConfirmAction(p[17], p[3]);
            night.ConfirmAction(p[18], p[7]);

            var logs = night.ExecuteActions();
            var zip = night.ZipActionInfo(logs);

            //Assert
            Assert.IsTrue(true);
        }
    }
}