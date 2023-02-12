using GameLogic.Roles;

namespace Net.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class RoleTypeAttribute : Attribute
    {
        private const string ARGUMENT_ERROR = "The type does not represent 'Role' type.";

        private readonly Type roleType;

        public Type RoleType => roleType;

        public RoleTypeAttribute(Type type)
        {
            if(type.IsAssignableFrom(typeof(Role)))
            {
                throw new ArgumentException(ARGUMENT_ERROR, nameof(type));
            }

            roleType = type;
        }
    }
}