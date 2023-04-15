using System.Text.Json.Serialization;

namespace Net.Contexts.Intro
{
    [Serializable]
    public class NicknameContext : SessionContext
    {
        public string Nickname { get; }

        [JsonConstructor]
        public NicknameContext(string nickname)
        {
            Nickname = nickname;
        }
    }
}