using GameLogic;
using GameLogic.Cycles;
using GameLogic.Model;
using GameLogic.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace RoleBindingTest
{
    [TestClass]
    public class CycleGameTest
    {
        private List<Player> playersInstance;
        private Game game;

        [TestInitialize]
        public void TestInit()
        {
            playersInstance = TestConfig.MinimumPlayersSetup();
            game = new Game(playersInstance);
        }

        [TestCleanup]
        public void TestClean()
        {
            playersInstance = null;
            game = null;
        }

        [TestMethod]
        public void StartGame_NextTurn_CycleIsNight()
        {
            //Arrange

            //Act
            //Day started with initial set of players
            game.Run();

            //Jump to night - first lynch is passed
            game.NextTurn();

            //Assert
            Assert.IsInstanceOfType(game.CurrentCycle, typeof(NightCycle));
        }

        [TestMethod]
        public void StartGame_UntilMorning_CycleIsMorning()
        {
            //Arrange

            //Act
            //Day started with initial set of players
            game.Run();
            (game.CurrentCycle as DayCycle).ConfirmElectionResult(null);

            //Jump to night - first lynch is passed
            game.NextTurn();
            (game.CurrentCycle as NightCycle).ZipActionInfo(new List<ActionLog>());

            //Jump to morning
            game.NextTurn();

            //Assert
            Assert.IsInstanceOfType(game.CurrentCycle, typeof(MorningCycle));
        }
    }
}