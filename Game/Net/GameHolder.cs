using GameLogic;
using GameLogic.Cycles.History;
using GameLogic.Model;
using Net.Contexts.Day;
using Net.Contexts.Game;
using Net.Contexts.Night;
using Net.Extensions;
using Net.Manager;
using Net.Manager.Day;
using Net.Manager.Lynch;
using Net.Manager.Morning;
using Net.Manager.Night;
using Net.Models;
using Net.Servers;

namespace Net
{
    /// <summary>
    /// Represents an object that takes control over initialization 
    /// and manipulation of game logic part.
    /// </summary>
    public class GameHolder : IDisposable
    {
        private DayManager dayManager;
        private LynchManager lynchManager;
        private NightManager nightManager;
        private MorningManager morningManager;
        private Game game;
        private LANServer server;
        private IDictionary<Player, SessionPLayer> players;

        public DayManager DayManager => dayManager;
        public LynchManager LynchManager => lynchManager;
        public NightManager NightManager => nightManager;

        public GameHolder(LANServer server, IDictionary<Player, SessionPLayer> players)
        {
            this.server = server;
            this.players = players;
            game = new Game(players.Keys.ToList());

            //Factories
            IFactory<DayManager> dayFactory = new DayFactory(server,
                game,
                game.Players);
            IFactory<LynchManager> lynchFactory = new LynchFactory(server,
                game,
                players);
            IFactory<NightManager> nightFactory = new NightFactory(server, game, game.Players);
            IFactory<MorningManager> morningFactory = new MorningFactory(server,
                game,
                players);

            //Managers
            dayManager = dayFactory.Create();
            lynchManager = lynchFactory.Create();
            nightManager = nightFactory.Create();
            morningManager = morningFactory.Create();

            dayManager.HasEnded += Manager_HasEnded;
            lynchManager.HasEnded += Manager_HasEnded;
            nightManager.HasEnded += Manager_HasEnded;
            morningManager.HasEnded += Manager_HasEnded;
            game.GameEnded += Game_GameEnded;
        }

        public void RunGame()
        {
            game.Run();

            var msg = new ScreenContext(game.CurrentCycle.IntoScreen());
            server.BroadcastSessionMessage(msg);

            BeginCycle();
        }

        private void EndCycle()
        {
            game.NextTurn();
            //If game is still running
            if(game.IsGameStarted)
            {
                var msg = new ScreenContext(game.CurrentCycle.IntoScreen());
                server.BroadcastSessionMessage(msg);

                BeginCycle();
            }
        }

        private void BeginCycle()
        {
            switch(game.CurrentCycle.IntoScreen())
            {
                case ScreenType.MORNING:
                {
                    //Send each morning info who is alive and his role
                    foreach(var p in players)
                    {
                        var smsg = new CommonPlayerStateContext(p.Key.Role.IntoSignature(),
                                p.Key.IsAlive);
                        server.SendSessionMessage(smsg, p.Key.Id);
                    }

                    morningManager.StartMorning();
                    break;
                }
                case ScreenType.DAY:
                {
                    //Send data about each player
                    foreach(var pair in players)
                    {
                        var message = new DayPlayerStateContext(pair.Key.Id,
                            pair.Value.Nickname,
                            pair.Value.NColor,
                            pair.Key.IsAlive);
                        server.BroadcastSessionMessage(message);
                    }

                    dayManager.StartDay();
                    break;
                }
                case ScreenType.LYNCH:
                {
                    lynchManager.StartLynch();
                    break;
                }
                case ScreenType.NIGHT:
                {
                    //Send data about each player
                    foreach(var p in players)
                    {
                        var message = new NightPlayerStateContext(p.Key.Id,
                            p.Value.Nickname,
                            p.Value.NColor,
                            p.Key.IsAlive);
                        //Except owner
                        server.BroadcastSessionMessage(message, p.Key.Id);

                        //Now send to owner
                        var owmsg = new NightPlayerStateContext(p.Key.Id,
                            p.Value.Nickname,
                            p.Value.NColor,
                            p.Key.IsAlive,
                            true);
                        server.SendSessionMessage(owmsg, p.Key.Id);
                    }

                    nightManager.StartReminderStep();
                    break;
                }
            }
        }

        //Event handler on manager process ending
        private void Manager_HasEnded(object? sender, EventArgs e)
        {
            EndCycle();
        }

        private void Game_GameEnded(object? sender, Team? e)
        {
            var msg = new ScreenContext(ScreenType.WIN);
            server.BroadcastSessionMessage(msg);

            EndGamePlayerState[] egps = new EndGamePlayerState[game.Players.Count];
            for(int i = 0; i < egps.Length; i++)
            {
                Player p = game.Players[i];
                egps[i] = new EndGamePlayerState(
                    p.Id,
                    players[p].Nickname,
                    players[p].NColor,
                    p.Role.IntoSignature(),
                    p.IsAlive);
            }

            EndGameHistory[] egh = new EndGameHistory[game.Chronology.History.Count];
            for(int i = 0; i < egh.Length; i++)
            {
                CycleMemento cm = game.Chronology.History.Pop();

                EndGameNightH[] egnh = new EndGameNightH[cm.Night.RevealedActions.Count()];
                for(int j = 0; j < egnh.Length; j++)
                {
                    ActionLog log = cm.Night.RevealedActions.ElementAt(j);
                    if(log is DoubleActionLog dlog)
                        egnh[j] = new EndGameNightH(
                            players[log.Executor.Owner].Nickname,
                            log.Executor.IntoSignature(),
                            players[log.PrimaryTarget.Owner].Nickname,
                            log.Success,
                            players[dlog.SecondaryTarget.Owner].Nickname);
                    else egnh[j] = new EndGameNightH(
                            players[log.Executor.Owner].Nickname,
                            log.Executor.IntoSignature(),
                            players[log.PrimaryTarget.Owner].Nickname,
                            log.Success);
                }

                string[] mdu = new string[cm.Morning.Deaths.Count()];
                for(int j = 0; j < mdu.Length; j++)
                {
                    //Deaths can be only players
                    mdu[j] = players[cm.Morning.Deaths.ElementAt(j).Victim].Nickname;
                }

                var elected = cm.Day.ElectedPlayer as Player;
                string? eUsername = null;
                if(elected != null) eUsername = players[elected].Nickname;

                egh[i] = new EndGameHistory(
                    cm.Turn,
                    eUsername,
                    cm.Day.GotVotes,
                    cm.Lynch.LastMessage,
                    egnh,
                    mdu);
            }

            var emsg = new EndGameContext(e, egps, egh);
            server.BroadcastSessionMessage(emsg);
        }

        #region IDisposable
#nullable disable warnings

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    //Dispose managed state (managed objects)
                    dayManager?.Dispose();
                    lynchManager?.Dispose();
                    nightManager?.Dispose();
                    morningManager?.Dispose();
                }

                //Free unmanaged resources (unmanaged objects) and override finalizer
                //Set large fields to null
                dayManager = null;
                lynchManager = null;
                nightManager = null;
                morningManager = null;
                server = null;

                disposedValue = true;
            }
        }

        ~GameHolder()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            //Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

#nullable restore warnings
        #endregion
    }
}