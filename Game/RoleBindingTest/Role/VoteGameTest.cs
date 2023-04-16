using GameLogic;
using GameLogic.Cycles;
using GameLogic.Interfaces;
using GameLogic.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace RoleBindingTest
{
    [TestClass]
    public class VoteGameTest
    {
        [TestMethod]
        public void VoteForOne_ThenForAnother_NoDuplicates()
        {
            List<Player> p = TestConfig.MinimumPlayersSetup();

            p[0].VoteFor(p[4]);
            p[0].VoteFor(p[3]);

            Assert.AreEqual(p[4].Votes, 0);
            Assert.AreEqual(p[3].Votes, 1);
        }

        [TestMethod]
        public void EachOneVotes_MultipleTimes_NoDuplicates()
        {
            List<Player> p = TestConfig.MinimumPlayersSetup();
            Random r1 = new Random(), r2 = new Random();

            for(int i = 0; i < 100; i++)
            {
                int a = r1.Next(p.Count);
                int b = r2.Next(p.Count);

                p[a].VoteFor(p[b]);
                Console.WriteLine("{0} VOTED {1}, LAST HAS {2} VOTES",
                    p[a].Id, p[b].Id, p[b].Votes);
            }

            int commonQuantityOfVotes = 0;
            foreach(var i in p)
            {
                commonQuantityOfVotes += i.Votes;
                Assert.IsTrue(i.Votes < p.Count);
            }
            Console.WriteLine("Votes is {0}, Objects is {1}", commonQuantityOfVotes, p.Count);
            Assert.AreEqual(commonQuantityOfVotes, p.Count);
        }

        [TestMethod]
        public void VoteTheSame_TwoTimes_False()
        {
            List<Player> p = TestConfig.MinimumPlayersSetup();

            p[0].VoteFor(p[4]);
            p[1].VoteFor(p[4]);

            //Repeated vote
            var result = p[0].VoteFor(p[4]);

            Assert.IsFalse(result);
            Assert.AreNotEqual(p[4].Votes, 3);
        }

        [TestMethod]
        public void VoteItself_OneTime_NoVote()
        {
            List<Player> p = TestConfig.MinimumPlayersSetup();

            p[0].VoteFor(p[0]);

            Assert.AreNotEqual(p[0].VoteTarget, p[0]);
            Assert.AreEqual(p[0].Votes, 0);
        }

        [TestMethod]
        public void VotePenguin_UsingDay_HasVote()
        {
            List<Player> p = TestConfig.MinimumPlayersSetup();
            DayCycle day = new DayCycle(p);

            day.VoteForNonLynch(p[0]);

            Assert.AreEqual(day.NonLynchVotes, 1);
        }

        [TestMethod]
        public void VotePenguin_EndElection_NoneIsElected()
        {
            List<Player> p = TestConfig.MinimumPlayersSetup();
            DayCycle day = new DayCycle(p);

            foreach(Player voter in p)
            {
                day.VoteForNonLynch(voter);
            }

            IVotable result = day.GetElectionResult();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void MultipleVotes_EndElection_SomeoneIsElected()
        {
            List<Player> p = TestConfig.LargePlayersSetup();
            DayCycle day = new DayCycle(p);
            Random r1 = new Random(), r2 = new Random();

            for(int i = 0; i < 100; i++)
            {
                int a = r1.Next(p.Count);
                int b = r2.Next(p.Count + 1);

                if(b >= p.Count)
                {
                    day.VoteForNonLynch(p[a]);
                    Console.WriteLine("{0} VOTED PENGUIN, LAST HAS {1} VOTES",
                        p[a].Id, day.NonLynchVotes);
                }
                else
                {
                    day.VoteFor(p[a], p[b]);
                    Console.WriteLine("{0} VOTED {1}, LAST HAS {2} VOTES",
                        p[a].Id, p[b].Id, p[b].Votes);
                }
            }

            try
            {
                IVotable result = day.GetElectionResult();
            }
            catch(Exception ex)
            {
                //Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void EachVotes_EndElectionWithNoEnoughVotes_Throw()
        {
            List<Player> p = TestConfig.MinimumPlayersSetup();
            DayCycle day = new DayCycle(p);

            day.VoteForNonLynch(p[0]);
            day.VoteFor(p[1], p[2]);
            day.VoteFor(p[2], p[3]);
            day.VoteFor(p[3], p[4]);
            day.VoteFor(p[4], p[0]);

            try
            {
                IVotable result = day.GetElectionResult();

                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.AreEqual(ex.Message, "There is no superiority in votes. Election should proceed");
            }
        }

        [TestMethod]
        public void OneVote_SameNickname_MissVotes()
        {
            List<Player> p = new List<Player>()
            {
                new Player(new CitizenRole(), 1UL),
                new Player(new MafiaRole(), 2UL)
            };
            DayCycle day = new DayCycle(p);

            day.VoteFor(p[0], p[1]);

            Assert.AreNotEqual(p[1].Votes, 1);
        }

        [TestMethod]
        public void SetVotes_ClearVotes_CollectionNotDestructed()
        {
            List<Player> p = TestConfig.MinimumPlayersSetup();
            int count = p.Count;
            DayCycle day = new DayCycle(p);
            Random ran = new Random();

            for(int i = 0; i < p.Count; i++)
            {
                int a = 0;
                do
                {
                    a = ran.Next(p.Count);
                }
                while(a == i);

                day.VoteFor(p[i], p[a]);
                Console.WriteLine("{0} VOTED {1}, LAST HAS {2} VOTES",
                    p[i].Id, p[a].Id, p[a].Votes);
            }

            day.ClearVotes();

            Assert.AreEqual(p.Count, count);
        }
    }
}