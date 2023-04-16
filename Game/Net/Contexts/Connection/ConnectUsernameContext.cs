using System.Text.Json.Serialization;

namespace Net.Contexts.Connection
{
    [Serializable]
    public class ConnectUsernameContext : SessionContext
    {
        public string Username { get; }

        [JsonConstructor]
        public ConnectUsernameContext(string username)
        {
            Username = username;
        }
    }
}