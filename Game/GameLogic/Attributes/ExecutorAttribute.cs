namespace GameLogic.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ExecutorAttribute : Attribute
    {
        private readonly ExecutorType executorType;

        public ExecutorType EType => executorType;

        public ExecutorAttribute(ExecutorType executorType)
        {
            this.executorType = executorType;
        }
    }

    public enum ExecutorType
    {
        NONE,
        TARGET,
        TARGET_TARGET,
        EXECUTOR_TARGER
    }
}