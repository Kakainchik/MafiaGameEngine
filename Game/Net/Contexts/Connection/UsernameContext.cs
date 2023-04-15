using System.Text.Json.Serialization;

namespace Net.Contexts.Connection
{
    [Serializable]
    public class UsernameContext : SessionContext
    {
        public string Username { get; }

        [JsonConstructor]
        public UsernameContext(string username)
        {
            Username = username;
        }
    }
}