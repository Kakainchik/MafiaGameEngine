using Net.Models;

namespace Net.Contexts.Game
{
    [Serializable]
    public class ScreenContext : SessionContext
    {
        public ScreenType Screen { get; }

        public ScreenContext(ScreenType screen)
        {
            Screen = screen;
        }
    }
}