namespace Net.Contexts.Intro
{
    [Serializable]
    public class NicknameContext : SessionContext
    {
        public string Nickname { get; }

        public NicknameContext(string nickname)
        {
            Nickname = nickname;
        }
    }
}