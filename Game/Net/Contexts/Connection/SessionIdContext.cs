using Net.Models;

namespace Net.Contexts.Connection
{
    [Serializable]
    public class SessionIdContext : AuthorizationContext
    {
        public Guid Id { get; }

        public SessionIdContext(Guid id) : base(ConnectValidation.ACCEPTED)
        {
            Id = id;
        }
    }
}