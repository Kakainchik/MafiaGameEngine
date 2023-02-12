namespace GameLogic.Model
{
    public class BaseDetail
    {
        public ActionType Type { get; }
        public Player Executor { get; }
        public Player PrimaryTarget { get; }

        public BaseDetail(ActionType type, Player executor, Player primaryTarget)
        {
            Type = type;
            Executor = executor;
            PrimaryTarget = primaryTarget;
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}