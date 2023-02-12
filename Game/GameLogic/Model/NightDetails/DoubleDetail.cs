namespace GameLogic.Model
{
    public class DoubleDetail : DetailDecorator
    {
        public Player SecondaryTarget { get; }

        public DoubleDetail(BaseDetail detail, Player secondaryTarged)
            : base(detail)
        {
            SecondaryTarget = secondaryTarged;
        }
    }
}