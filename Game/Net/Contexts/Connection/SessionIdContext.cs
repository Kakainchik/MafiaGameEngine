using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts.Connection
{
    [Serializable]
    public class SessionIdContext : AuthorizationContext
    {
        public Guid Id { get; }

        [JsonConstructor]
        public SessionIdContext(Guid id) : base(ConnectValidation.ACCEPTED)
        {
            Id = id;
        }
    }
}