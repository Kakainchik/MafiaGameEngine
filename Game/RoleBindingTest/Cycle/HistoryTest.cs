using GameLogic;
using GameLogic.Cycles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace RoleBindingTest.Cycle
{
    [TestClass]
    public class HistoryTest
    {
        [TestMethod]
        public void CyclesPassed_ChronologyIsWritten_Backuped()
        {
            List<Player> p = TestConfig.MinimumPlayersSetup();
            Game game = new Game(p);

            game.Run();
            game.NextTurn();

            var night = game.CurrentCycle as NightCycle;
            night.ConfirmAction(p[0], p[2]);
            night.ConfirmAction(p[1], p[2]);
            night.ZipActionInfo(night.ExecuteActions());

            game.NextTurn();
            var morning = game.CurrentCycle as MorningCycle;
            morning.NoteDeaths();

            game.NextTurn();
            var day = game.CurrentCycle as DayCycle;
            day.VoteFor(p[0], p[4]);
            day.VoteFor(p[1], p[4]);
            day.ConfirmElectionResult(p[4]);

            game.NextTurn();
            var lynch = game.CurrentCycle as LynchCycle;
            lynch.Lynch("Hope");

            game.NextTurn();
            night = game.CurrentCycle as NightCycle;
            night.ConfirmAction(p[0], p[1]);
            night.ConfirmAction(p[1], p[3]);
            night.ZipActionInfo(night.ExecuteActions());

            game.NextTurn();
            morning = game.CurrentCycle as MorningCycle;
            morning.NoteDeaths();

            Assert.IsNotNull(game.Chronology);
        }
    }
}