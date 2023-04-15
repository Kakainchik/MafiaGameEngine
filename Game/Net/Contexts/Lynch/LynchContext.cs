using System.Text.Json.Serialization;

namespace Net.Contexts.Lynch
{
    [Serializable]
    public class LynchContext : SessionContext
    {
        public LynchStep Step { get; }

        [JsonConstructor]
        public LynchContext(LynchStep step)
        {
            Step = step;
        }
    }

    public enum LynchStep : byte
    {
        QUESTION,
        LAST_MESSAGE,
        PREPARE_EXECUTE,
        EXECUTE,
        SHOW_ROLE,
        END
    }
}