using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts.Connection
{
    [Serializable]
    public class ConnectClientIdContext : AuthorizationContext
    {
        public ulong ClientId { get; }

        [JsonConstructor]
        public ConnectClientIdContext(ulong clientId) : base(ConnectValidation.ACCEPTED)
        {
            ClientId = clientId;
        }
    }
}