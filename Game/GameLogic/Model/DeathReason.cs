namespace GameLogic.Model
{
    public class DeathDetail
    {
        public Player Victim { get; }
        public DeathReason Reason { get; }

        public DeathDetail(Player victim, DeathReason reason)
        {
            Victim = victim;
            Reason = reason;
        }
    }

    public enum DeathReason : byte
    {
        BY_MAFIA,
        BY_SERIAL_KILLER,
        BY_VIGILANTE,
        BY_DRIVER,
        BY_TERRORIST,
        SUICIDE
    }
}