using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetTest
{
    [TestClass]
    public class HallHubTest
    {
        private const string CONNECTION_URL = "http://localhost:7262/LobbyHub";

        [TestMethod]
        public async void User_ReceiveLobbies_ListOfLobbies()
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(CONNECTION_URL)
                .Build();

            await connection.StartAsync();

        }
    }
}