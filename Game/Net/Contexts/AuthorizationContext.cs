using Net.Models;

namespace Net.Contexts
{
    [Serializable]
    public class AuthorizationContext : Context
    {
        public ConnectValidation Validation { get; }

        public AuthorizationContext(ConnectValidation validation)
        {
            Validation = validation;
        }
    }
}