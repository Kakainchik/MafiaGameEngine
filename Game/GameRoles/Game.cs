using GameLogic.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public delegate void VoteStartEventHandler(object sender, List<Player> players);
    public delegate void VoteEndEventHandler(object sender, Player votedPlayer);

    public class Game
    {
        private const string LACK_PLAYERS_ERROR = "Too few players. Minimum is 5.";

        private bool isGameStarted = false;

        /// <summary>
        /// Номер хода в данный момент в игре.
        /// </summary>
        public int Day { get; private set; } = 0;
        public int NonLynchVotes { get; set; } = 0;

        /// <summary>
        /// Отображает период игры в данный момент. <c>True</c> - ночь, иначе день.
        /// </summary>
        public bool IsNightNow { get; private set; } = false;

        /// <summary>
        /// Отображает, начата ли в данный момент игра.
        /// </summary>
        public bool IsGameStarted
        {
            get => isGameStarted;
            private set
            {
                if(value) this.GameWasStarted?.Invoke(this, EventArgs.Empty);
                else this.GameWasEnded?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Список ролей в данной игре.
        /// </summary>
        public List<Player> Players { get; }

        public Game(List<Player> players)
        {
            this.Players = players;
        }

        public event EventHandler GameWasStarted;
        public event EventHandler GameWasEnded;
        public event EventHandler DayStarted;
        public event EventHandler NightStarted;
        public event VoteStartEventHandler VotingStarted;
        public event VoteEndEventHandler VotingEnded;
        /// <summary>
        /// Запускает игру с загруженными в ранее конструктор ролями.
        /// </summary>
        public void Run()
        {
            if(Players.Count < 5)
            {
                throw new InitializingGameException(LACK_PLAYERS_ERROR);
            }

            //Оповещаем о начале игры
            IsGameStarted = true;

            //Запускаем первый ход - дневное собрание.
            this.NextTurn();
        }

        public void NextTurn()
        {
            //Обрабатываем перед началом нового хода

            if(IsNightNow)
            {
                this.IsNightNow = false;
                this.StartDay();
            }
            else
            {
                this.IsNightNow = true;
                this.StartNight();
            }
        }

        public void EndVote(Player selected)
        {
            selected.Lynch();
            this.VotingStarted.
            this.VotingEnded?.Invoke(this, selected);
        }

        public void RepeatVote(List<Player> remainedPlayers)
        {
            this.Players.ForEach(p => p.ClearVotes());
            this.VotingStarted?.Invoke(this, remainedPlayers);
        }

        private void StartDay()
        {
            this.DayStarted?.Invoke(this, EventArgs.Empty);

            //Прибавляем ход, когда заканчивается ночь
            this.Day++;

            //Первый день не голосуем
            if(this.Day == 1) return;

            //Исключаем всех мёртвых игроков
            List<Player> alivePlayers = Players.FindAll(p => p.IsAlive);
            this.VotingStarted?.Invoke(this, alivePlayers);
        }

        private void StartNight()
        {

        }
    }
}