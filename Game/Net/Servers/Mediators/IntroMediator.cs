using GameLogic;
using Net.Contexts;
using Net.Contexts.Game;
using Net.Contexts.Intro;
using Net.Extensions;
using Net.Manager;
using Net.Manager.Intro;
using Net.Models;

namespace Net.Servers.Mediators
{
    public class IntroMediator : IMediator
    {
        private const string DEFAULT_CITY = "Minneapolis";

        private string city;
        private IntroManager introManager;
        private LANServer server;
        private IDictionary<ulong, SessionPLayer> players;
        private IEnumerable<Player> mixedPlayers;
        private bool disposedValue;

        public IntroMediator(LANServer server,
            IDictionary<ulong, LobbyPlayer> readyPlayers,
            IDictionary<RoleSignature, int> rolesDict,
            string? cityName)
        {
            this.server = server;
            city = string.IsNullOrWhiteSpace(cityName) ? DEFAULT_CITY : cityName;
            players = new Dictionary<ulong, SessionPLayer>(readyPlayers.Count);

            //Mix roles
            IList<RoleSignature> roles = new List<RoleSignature>();
            foreach(var pair in rolesDict)
                for(int i = 0; i < pair.Value; i++)
                    roles.Add(pair.Key);
            Random ran = new Random();
            mixedPlayers = readyPlayers.Keys.OrderBy(o => ran.Next())
                .Select((g, index) => new Player(roles[index].MakeGameRole(), g));

            //Factories
            IFactory<IntroManager> introFactory = new IntroFactory(this.server);

            //Managers
            introManager = introFactory.Create();

            introManager.HasEnded += IntroManager_HasEnded;
        }

        public void Accept(Context message)
        {
            switch(message)
            {
                case NicknameContext con:
                {
                    HandleNickname(con);
                    break;
                }
            }
        }

        public void StartIntro()
        {
            introManager.Initialize(city);
        }

        public GameRouterMediator RunGame()
        {
            var pairBundle = mixedPlayers.ToDictionary(
                key => key,
                session => players[session.Id]);
            var mediator = new GameRouterMediator(server, pairBundle);
            server.SessionMediator = mediator;
            return mediator;
        }

        private void HandleNickname(NicknameContext con)
        {
            //Receive nicknames on Intro from players as host
            //Bind nickname to id
            players[con.Presenter.Sender] = new(con.Nickname, ColorBank.GetRandomColor());

            if(players.Count == mixedPlayers.Count())
            {
                foreach(var p in mixedPlayers)
                {
                    //Send info to every player about own role
                    var msg = new IntroPlayerContext(p.Role.IntoSignature(),
                        players[p.Id].Nickname,
                        players[p.Id].NColor);
                    msg.Presenter.Receiver = p.Id;
                    server.SendSessionMessage(msg, p.Id);
                }

                //We got all nicknames, proceed
                introManager.StartIntroStep();
            }
        }

        //Event handler on intro ending
        private void IntroManager_HasEnded(object? sender, EventArgs e)
        {
            //Send to each their player state
            foreach(var p in mixedPlayers)
            {
                var smsg = new CommonPlayerStateContext(p.Role.IntoSignature(),
                        p.IsAlive);
                server.SendSessionMessage(smsg, p.Id);
            }

            //Intro ended, inform to prepare game screen
            var msg = new IntroRunGameContext();
            server.BroadcastSessionMessage(msg);
        }

        #region IDisposable Implementation

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    server?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}