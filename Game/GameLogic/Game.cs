using GameLogic.Cycles;
using GameLogic.Cycles.History;
using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic
{
    public class Game
    {
        private const string LACK_PLAYERS_ERROR = "Too few players. Minimum is 5.";
        private const string GAME_RUNNED_ERROR = "The game is already running.";
        private const string UNIQUE_ROLES_ERROR = "There are more than one certain type unique role.";

        public const int MIN_PLAYERS = 5;

        private bool isGameStarted = false;
        private GameCycle currentCycle;
        private GameHistory chronology;

        private List<Player> GetAlivePlayers => Players.FindAll(p => p.IsAlive);

        /// <summary>
        /// List of the roles in this game.
        /// </summary>
        public List<Player> Players { get; }

        /// <summary>
        /// The number of current alive players.
        /// </summary>
        public int AlivePlayersNumber => GetAlivePlayers.Count;

        public GameHistory Chronology => chronology;

        /// <summary>
        /// The number of the current day in the game.
        /// </summary>
        public int Day { get; private set; } = 0;

        /// <summary>
        /// Indicates if the game is started.
        /// </summary>
        public bool IsGameStarted
        {
            get => isGameStarted;
            private set
            {
                isGameStarted = value;
            }
        }

        public GameCycle CurrentCycle => currentCycle;

        public Game(List<Player> players)
        {
            //Check on unique roles
            var uniqueRoles = players.Select<Player, Role>(p => p.Role)
                .Where<Role>(r => r.IsUnique);

            if(uniqueRoles.OfType<GodfatherRole>().Count() > 1)
                throw new InitializingGameException(UNIQUE_ROLES_ERROR);
            if(uniqueRoles.OfType<PsychicRole>().Count() > 1)
                throw new InitializingGameException(UNIQUE_ROLES_ERROR);
            if(uniqueRoles.OfType<CultusLeaderRole>().Count() > 1)
                throw new InitializingGameException(UNIQUE_ROLES_ERROR);

            Players = players;
            SetupNumerableItems();

            chronology = new GameHistory();
        }

        public event EventHandler<Team?> GameEnded;

        /// <summary>
        /// Runs the game with uploaded roles.
        /// </summary>
        public void Run()
        {
            if(Players.Count < MIN_PLAYERS)
            {
                //throw new InitializingGameException(LACK_PLAYERS_ERROR);
            }

            if(IsGameStarted)
            {
                throw new InitializingGameException(GAME_RUNNED_ERROR);
            }

            //Inform about starting the game
            IsGameStarted = true;

            //Run the first turn - day gathering
            StartDay();
        }

        public void NextTurn()
        {
            //Handle before begining of each turn
            if(currentCycle is MorningCycle)
            {
                if(((MorningCycle)currentCycle).TryResolveWinner(out Team? winner))
                {
                    OnEndGame(winner);
                }
                else StartDay();
            }
            else if(currentCycle is DayCycle)
            {
                //First day, no lynch
                if(Day == 1) StartNight();
                //Nobody was elected at the gathering, no lynch
                else if(!(Chronology.tempCycle.day.electedPlayer is Player)) StartNight();
                else StartLynch();
            }
            else if(currentCycle is LynchCycle)
            {
                StartNight();
            }
            else if(currentCycle is NightCycle)
            {
                StartMorning();
            }
        }

        #region Cycles Methods

        private void StartMorning()
        {
            currentCycle = new MorningCycle(GetAlivePlayers,
                Chronology.tempCycle.night.nightPlayer);
            (currentCycle as MorningCycle).MorningEnded += Game_MorningEnded;
        }

        private void StartDay()
        {
            //Save whole day in chronology
            Chronology.SetTurn(++Day);

            currentCycle = new DayCycle(GetAlivePlayers);
            (currentCycle as DayCycle).DayEnded += Game_DayEnded;
        }

        private void StartLynch()
        {
            currentCycle = new LynchCycle(GetAlivePlayers,
                Chronology.tempCycle.day.electedPlayer);
            (currentCycle as LynchCycle).LynchEnded += Game_LynchEnded;
        }

        private void StartNight()
        {
            currentCycle = new NightCycle(GetAlivePlayers);
            (currentCycle as NightCycle).NightEnded += Game_NightEnded;
        }

        private void OnEndGame(Team? winner)
        {
            IsGameStarted = false;
            GameEnded?.Invoke(this, winner);
        }

        #endregion

        private void SetupNumerableItems()
        {
            //Set bullets to vigs
            int bullets = (int)Math.Round(Math.Sqrt(Players.Count));
            var vigs = Players.Where(p => p.Role is VigilanteRole);
            foreach(var v in vigs)
                (v.Role as VigilanteRole).Bullets = bullets;
        }

        #region Event Hadlers

        private void Game_MorningEnded(object sender, MorningMemento e)
        {
            Chronology.SaveMorning(e);
            Chronology.MakeTurnBackup();
        }

        private void Game_DayEnded(object sender, DayMemento e)
        {
            Chronology.SaveDay(e);
        }

        private void Game_NightEnded(object sender, NightMemento e)
        {
            Chronology.SaveNight(e);
        }

        private void Game_LynchEnded(object sender, LynchMemento e)
        {
            Chronology.SaveLynch(e);
        }

        #endregion
    }
}