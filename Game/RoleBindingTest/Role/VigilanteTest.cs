using GameLogic;
using GameLogic.Cycles;
using GameLogic.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace RoleBindingTest
{
    [TestClass]
    public class VigilanteTest
    {
        [TestMethod]
        public void VigilanteKills_OneBullet_BulletsAreOver()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new MafiaRole(), 1UL),
                new Player(new CitizenRole(), 2UL),
                new Player(new VigilanteRole(), 3UL)
            };
            NightCycle night = new NightCycle(p);

            ((VigilanteRole)p[2].Role).Bullets = 1;

            //Act
            //p[2] is Vigilante
            night.ConfirmAction(p[2], p[1]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsFalse(((VigilanteRole)p[2].Role).HasBullets);
            Assert.IsFalse(p[1].IsAlive);
        }

        [TestMethod]
        public void VigilanteKills_ProstituteBlocks_BulletNotLost()
        {
            //Arrange
            List<Player> p = new List<Player>
            {
                new Player(new ProstituteRole(), 1UL),
                new Player(new CitizenRole(), 2UL),
                new Player(new VigilanteRole(), 3UL)
            };
            NightCycle night = new NightCycle(p);

            ((VigilanteRole)p[2].Role).Bullets = 1;

            //Act
            //p[2] is Vigilante
            night.ConfirmAction(p[2], p[1]);
            //p[0] is Prostitute
            night.ConfirmAction(p[0], p[2]);

            var states = night.ExecuteActions();

            //Assert
            Assert.IsTrue(((VigilanteRole)p[2].Role).HasBullets);
            Assert.IsTrue(p[1].IsAlive);
        }
    }
}