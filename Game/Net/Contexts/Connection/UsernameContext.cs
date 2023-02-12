namespace Net.Contexts.Connection
{
    [Serializable]
    public class UsernameContext : SessionContext
    {
        public string Username { get; }

        public UsernameContext(string username)
        {
            Username = username;
        }
    }
}