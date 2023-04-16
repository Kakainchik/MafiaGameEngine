using GameLogic;
using GameLogic.Cycles;
using GameLogic.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoleBindingTest.Cycle
{
    [TestClass]
    public class MorningGameTest
    {
        [TestMethod]
        public void KillAtNight_GetDeaths_DadaIsOkay()
        {
            //Arrange
            List<Player> p = new List<Player>()
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new SerialKillerRole(), 2UL),
                new Player(new VigilanteRole() { Bullets = 1 }, 3UL),
                new Player(new CitizenRole(), 4UL),
                new Player(new CitizenRole(), 5UL),
                new Player(new CitizenRole(), 6UL),
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[3]);
            night.ConfirmAction(p[1], p[4]);
            night.ConfirmAction(p[2], p[5]);

            var logs = night.ExecuteActions();
            var zip = night.ZipActionInfo(logs);
            var alive = p.Where(p => p.IsAlive).ToList();

            MorningCycle morning = new MorningCycle(alive, p);
            var cases = morning.NoteDeaths();

            //Assert
            Assert.AreEqual(3, cases.Count());
        }

        [TestMethod]
        public void KillAtNight_Heal_NoDeath()
        {
            //Arrange
            List<Player> p = new List<Player>()
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new DoctorRole(), 2UL),
                new Player(new CitizenRole(), 3UL),
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[2]);
            night.ConfirmAction(p[1], p[2]);

            var logs = night.ExecuteActions();
            var zip = night.ZipActionInfo(logs);
            var alive = p.Where(p => p.IsAlive).ToList();

            MorningCycle morning = new MorningCycle(alive, p);
            var cases = morning.NoteDeaths();

            //Assert
            Assert.AreEqual(0, cases.Count());
        }

        [TestMethod]
        public void KillAtNight_HealAndKillAgain_Death()
        {
            //Arrange
            List<Player> p = new List<Player>()
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new SerialKillerRole(), 2UL),
                new Player(new CitizenRole(), 3UL),
                new Player(new DoctorRole(), 4UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[2]);
            night.ConfirmAction(p[1], p[2]);
            night.ConfirmAction(p[3], p[2]);

            var logs = night.ExecuteActions();
            var zip = night.ZipActionInfo(logs);
            var alive = p.Where(p => p.IsAlive).ToList();

            MorningCycle morning = new MorningCycle(alive, p);
            var cases = morning.NoteDeaths();

            //Assert
            Assert.AreEqual(1, cases.Count());
        }

        [TestMethod]
        public void BlowAtNight_SeveralTargets_SeveralDeath()
        {
            //Arrange
            List<Player> p = new List<Player>()
            {
                new Player(new TerroristRole(), 1UL),
                new Player(new SerialKillerRole(), 2UL),
                new Player(new CitizenRole(), 3UL),
                new Player(new DoctorRole(), 4UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[2]);
            night.ConfirmAction(p[1], p[2]);
            night.ConfirmAction(p[3], p[2]);

            var logs = night.ExecuteActions();
            var zip = night.ZipActionInfo(logs);
            var alive = p.Where(p => p.IsAlive).ToList();

            MorningCycle morning = new MorningCycle(alive, p);
            var cases = morning.NoteDeaths();

            //Assert
            Assert.AreEqual(3, cases.Count());
        }

        [TestMethod]
        public void BlowAtNightTwice_KillCursed_DataIsOkay()
        {
            //Arrange
            List<Player> p = new List<Player>()
            {
                new Player(new SerialKillerRole(), 1UL),
                new Player(new SerialKillerRole(), 2UL),
                new Player(new CursedRole(), 3UL),
                new Player(new TerroristRole(), 4UL),
                new Player(new TerroristRole(), 5UL),
                new Player(new DoctorRole(), 6UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[2]);
            night.ConfirmAction(p[1], p[0]);
            night.ConfirmAction(p[3], p[1]);
            night.ConfirmAction(p[4], p[3]);
            night.ConfirmAction(p[5], p[2]);

            var logs = night.ExecuteActions();
            var zip = night.ZipActionInfo(logs);
            var alive = p.Where(p => p.IsAlive).ToList();

            MorningCycle morning = new MorningCycle(alive, p);
            var cases = morning.NoteDeaths();

            //Assert
            Assert.AreEqual(3, cases.Count());
        }

        [TestMethod]
        public void KillAtNight_KillCursed_NoDeathCases()
        {
            //Arrange
            List<Player> p = new List<Player>()
            {
                new Player(new SerialKillerRole(), 1UL),
                new Player(new CursedRole(), 2UL),
                new Player(new DoctorRole(), 3UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[1]);

            var logs = night.ExecuteActions();
            var zip = night.ZipActionInfo(logs);
            var alive = p.Where(p => p.IsAlive).ToList();

            MorningCycle morning = new MorningCycle(alive, p);
            var cases = morning.NoteDeaths();

            //Assert
            Assert.AreEqual(0, cases.Count());
        }

        [TestMethod]
        public void KillAtNight_KillCursedTwice_DeathCase()
        {
            //Arrange
            List<Player> p = new List<Player>()
            {
                new Player(new SerialKillerRole(), 1UL),
                new Player(new SerialKillerRole(), 2UL),
                new Player(new CursedRole(), 3UL),
                new Player(new DoctorRole(), 4UL)
            };
            NightCycle night = new NightCycle(p);

            //Act
            night.ConfirmAction(p[0], p[2]);
            night.ConfirmAction(p[0], p[2]);

            var logs = night.ExecuteActions();
            var zip = night.ZipActionInfo(logs);
            var alive = p.Where(p => p.IsAlive).ToList();

            MorningCycle morning = new MorningCycle(alive, p);
            var cases = morning.NoteDeaths();

            //Assert
            Assert.AreEqual(1, cases.Count());
        }
    }
}