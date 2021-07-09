using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameLogic;
using GameLogic.Roles;
using System.Collections.Generic;
using System.Linq;

namespace RoleBindingTest
{
    [TestClass]
    public class StartGameTest
    {
        List<Player> players = new List<Player>
        {
            new Player(new MafiaRole(), "Alice"),
            new Player(new CitizenRole(), "Bob"),
            new Player(new CitizenRole(), "Clark"),
            new Player(new CitizenRole(), "Dad"),
            new Player(new CitizenRole(), "Enigma"),
            new Player(new CitizenRole(), "Fox")
        };

        Game game;

        [TestMethod]
        public void StraitVote()
        {
            game = new Game(players);
            game.Run();

            game.VotingStarted += Game_StraitVotingStarted;
            game.VotingEnded += Game_VotingEnded;

            game.NextTurn();
            game.NextTurn();
            game.NextTurn();
        }

        private void Game_VotingEnded(object sender, Player votedPlayer)
        {
            game.NextTurn();
        }

        [TestMethod]
        public void EqualVote()
        {
            game = new Game(players);
            game.Run();

            game.NextTurn();

            game.VotingStarted += Game_EqualVotingStarted;
        }

        private void Game_StraitVotingStarted(object sender, List<Player> players)
        {
            foreach(var p in players)
            {
                p.Vote(players[0]);
            }

            int maxVotes = players.Max(p => p.Votes);
            var votedPlayer = players.First(p => p.Votes == maxVotes);
            game.EndVote(votedPlayer);
        }

        private void Game_EqualVotingStarted(object sender, List<Player> players)
        {
            int i;
            for(i = 0; i < players.Count / 2; i++)
            {
                players[i].Vote(players[0]);
            }
            for(; i < players.Count; i++)
            {
                players[i].Vote(players[5]);
            }

            int maxVotes = players.Max(p => p.Votes);
            List<Player> votedPlayers = players.Where(p => p.Votes == maxVotes).ToList();

            
        }
    }
}