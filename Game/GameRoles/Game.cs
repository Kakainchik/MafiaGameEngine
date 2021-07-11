using GameLogic.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Game
    {
        private const string LACK_PLAYERS_ERROR = "Too few players. Minimum is 5.";

        private bool isGameStarted = false;
        private DailyMeeteng currentDay;
        private Night currentNight;

        /// <summary>
        /// Список ролей в данной игре.
        /// </summary>
        public List<Player> Players { get; }

        /// <summary>
        /// Количество живых игроков в данный момент.
        /// </summary>
        public int AlivePlayersNumber { get => Players.Count(p => p.IsAlive); }

        /// <summary>
        /// Номер хода в данный момент в игре.
        /// </summary>
        public int Day { get; private set; } = 0;

        /// <summary>
        /// Отображает период игры в данный момент. <c>True</c> - ночь, иначе день.
        /// </summary>
        public bool IsNightNow { get; private set; } = true;

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

        public DailyMeeteng DailyMeeteng { get => currentDay; }

        public Night CurrentNight { get => currentNight; }

        public Game(List<Player> players)
        {
            this.Players = players;
        }

        public event EventHandler GameWasStarted;
        public event EventHandler GameWasEnded;
        public event EventHandler DayStarted;
        public event EventHandler NightStarted;
        public event VoteStartEventHandler VotingStarted;
        public event VoteEventHandler VotingEnded;
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
                //Прибавляем ход, когда заканчивается ночь
                this.Day++;
                this.DayStarted?.Invoke(this, EventArgs.Empty);
                this.StartDay();
            }
            else
            {
                this.IsNightNow = true;
                this.NightStarted?.Invoke(this, EventArgs.Empty);
                this.StartNight();
            }
        }

        private void StartDay()
        {
            this.currentNight = null;
            //Первый день не голосуем
            if(this.Day == 1) return;

            //Исключаем всех мёртвых игроков
            List<Player> alivePlayers = Players.FindAll(p => p.IsAlive);
            this.currentDay = new DailyMeeteng(alivePlayers, Day, VotingStarted, VotingEnded);
        }

        private void StartNight()
        {
            this.currentDay = null;

            //Исключаем всех мёртвых игроков
            List<Player> alivePlayers = Players.FindAll(p => p.IsAlive);

        }
    }
}