namespace GameLogic.Model
{
    public class BlowDetail : BaseDetail
    {
        public Player[] Victims { get; }

        public BlowDetail(
            ActionType type,
            Player executor,
            Player primaryTarget,
            params Player[] victims)
            : base(type, executor, primaryTarget)
        {
            Victims = victims;
        }
    }
}