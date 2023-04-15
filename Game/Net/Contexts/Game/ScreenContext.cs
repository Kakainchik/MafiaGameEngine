using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts.Game
{
    [Serializable]
    public class ScreenContext : SessionContext
    {
        public ScreenType Screen { get; }

        [JsonConstructor]
        public ScreenContext(ScreenType screen)
        {
            Screen = screen;
        }
    }
}