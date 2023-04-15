using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts
{
    [Serializable]
    public class AuthorizationContext : Context
    {
        public ConnectValidation Validation { get; }

        [JsonConstructor]
        public AuthorizationContext(ConnectValidation validation)
        {
            Validation = validation;
        }
    }
}