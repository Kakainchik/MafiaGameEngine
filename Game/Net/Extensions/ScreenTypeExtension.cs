using GameLogic.Cycles;
using Net.Models;

namespace Net.Extensions
{
    internal static class ScreenTypeExtension
    {
        internal static ScreenType IntoScreen(this GameCycle cycle)
        {
            switch(cycle)
            {
                case DayCycle _:
                    return ScreenType.DAY;
                case LynchCycle _:
                    return ScreenType.LYNCH;
                case NightCycle _:
                    return ScreenType.NIGHT;
                case MorningCycle _:
                    return ScreenType.MORNING;
                default:
                    throw new ArgumentException("Such cycle is not valid", nameof(cycle));
            }
        }
    }
}