using GameLogic.Roles;

namespace GameLogic.Model
{
    public class LogBuilder
    {
        private ActionLog result;

        public LogBuilder()
        {
            result = new ActionLog();
        }

        public LogBuilder(ActionLog current)
        {
            result = current;
        }

        public LogBuilder BindExecutor(Role executor)
        {
            result.Executor = executor;
            return this;
        }

        public LogBuilder BindTarget(Role target)
        {
            result.PrimaryTarget = target;
            return this;
        }

        public LogBuilder IsSuccess()
        {
            result.Success = true;
            return this;
        }

        public LogBuilder IsBlocked()
        {
            result.IsBlocked = true;
            return this;
        }

        public ActionLog Build()
        {
            return result;
        }
    }
}