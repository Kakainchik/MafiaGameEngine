using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameLogic;
using GameLogic.Roles;
using System.Collections.Generic;
using System.Linq;
using System;

namespace RoleBindingTest
{
    [TestClass]
    public class VoteGameTest
    {
        List<Player> playersInstance = new List<Player>
        {
            new Player(new MafiaRole(), "Alice"),
            new Player(new CitizenRole(), "Bob"),
            new Player(new CitizenRole(), "Clark"),
            new Player(new CitizenRole(), "Dad"),
            new Player(new CitizenRole(), "Enigma"),
            new Player(new CitizenRole(), "Fox")
        };
        Game game;

        public void JumpToSecondNight(Game currentGame)
        {
            //? ???? - ?????????? ????????
            game.NextTurn();
            //? ???? - ????????
            game.NextTurn();
            //??????? ?????? ???????????
            //??????? ????????? ???????????
            game.DailyMeeteng.StartVoting();
            //? ???? - ?????? ????????? ????
            game.NextTurn();
        }

        [TestMethod]
        public void StraitVoteWithDeath()
        {
            //Arrange
            List<Player> players = new List<Player>(playersInstance);
            game = new Game(players);
            game.Run();

            //??????? ?????? ???????????
            game.VotingStarted += (sender, alivePlayers) =>
            {
                //??? ???????? ?? ??????
                foreach(var p in players) p.Vote(players[0]);

                int maxVotes = players.Max(p => p.Votes);
                var votedPlayer = players.First(p => p.Votes == maxVotes);
                game.DailyMeeteng.EndVoting(votedPlayer);
            };

            //??????? ????? ???????????
            game.VotingEnded += (sender, votedPlayer) =>
            {
                Console.WriteLine("Voting has been ended! {0} was picked.", votedPlayer.Nickname);
            };

            //Act
            JumpToSecondNight(game);

            //Assert
            Assert.AreEqual(2, game.Day);
            Assert.IsTrue(game.IsNightNow);
            Assert.IsFalse(players[0].IsAlive);
            Assert.AreEqual(players.Count - 1, players.Count(p => p.IsAlive));
        }

        [TestMethod]
        public void EqualVoteWithDeath()
        {
            //Arrange
            List<Player> players = new List<Player>(playersInstance);
            game = new Game(players);
            game.Run();

            //??????? ?????? ???????????
            game.VotingStarted += (sender, alivePlayers) =>
            {
                int votingTurn = 0;
                //???????? ???????
                Func<List<Player>, List<Player>> voting1 = delegate (List<Player> remainPlayers)
                {
                    int i = 0;
                    for(; i < alivePlayers.Count / 2; i++)
                    {
                        alivePlayers[i].Vote(remainPlayers[0]);
                    }
                    for(; i < alivePlayers.Count; i++)
                    {
                        alivePlayers[i].Vote(remainPlayers[1]);
                    }

                    votingTurn++;
                    return remainPlayers;
                };

                //???????? ?? ?????? ?? ??????????
                Func<List<Player>, List<Player>> voting2 = delegate (List<Player> remainPlayers)
                {
                    foreach(var p in alivePlayers)
                    {
                        p.Vote(remainPlayers[0]);
                    }

                    votingTurn++;
                    return remainPlayers;
                };

                List<Player> votedPlayers = voting1(alivePlayers);

                while(votingTurn < 3)
                {
                    int maxVotes = alivePlayers.Max(p => p.Votes);
                    votedPlayers = alivePlayers.Where(p => p.Votes == maxVotes).ToList();

                    //???? ?? ????? ??????????? ???????? ????????? ??????? ? ?????????? ??????????? ???????
                    if(votedPlayers.Count > 1)
                    {
                        Console.WriteLine("Voting will be repeated once more! {0} were picked.", string.Join(';', votedPlayers));
                        alivePlayers.ForEach(p => p.ClearVotes());
                        votedPlayers = voting2(votedPlayers);
                    }
                    else
                    {
                        game.DailyMeeteng.EndVoting(votedPlayers[0]);
                        break;
                    }
                }
            };

            //??????? ????? ???????????
            game.VotingEnded += (sender, votedPlayer) =>
            {
                Console.WriteLine("Voting has been ended! {0} was picked.", votedPlayer.Nickname);
            };

            //Act
            JumpToSecondNight(game);

            //Assert
            Assert.AreEqual(2, game.Day);
            Assert.IsTrue(game.IsNightNow);
            Assert.IsFalse(players[0].IsAlive);
            Assert.AreEqual(players.Count - 1, players.Count(p => p.IsAlive));
        }

        [TestMethod]
        public void NoneVoteTest()
        {
            //Arrange
            List<Player> players = new List<Player>(playersInstance);
            game = new Game(players);
            game.Run();

            game.VotingStarted += (sender, alivePlayers) =>
            {
                foreach(var p in alivePlayers) p.Vote(null);
            };

            game.VotingEnded += (sender, votedPlayer) =>
            {
                if(votedPlayer is null)
                    Console.WriteLine("Voting has been ended! None was lynched.");
            };

            //Act
            JumpToSecondNight(game);

            int maxVotes = players.Max(p => p.Votes);
            int nonLynchVotes = game.DailyMeeteng.NonLynchVotes;

            if(nonLynchVotes >= maxVotes) game.DailyMeeteng.EndVoting(null);

            //Assert

        }
    }
}