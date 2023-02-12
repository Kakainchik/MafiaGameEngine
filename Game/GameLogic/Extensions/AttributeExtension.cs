using GameLogic.Attributes;
using GameLogic.Model;
using GameLogic.Roles;
using System.Reflection;

namespace GameLogic.Extensions
{
    public static class AttributeExtension
    {
        public static Team GetTeam(this Role role)
        {
            var attrs = role.GetType().GetCustomAttributes<TeamAttribute>();
            return attrs.SingleOrDefault()?.ItsTeam ?? throw new InvalidOperationException();
        }

        public static RCategory GetRCategory(this Role role)
        {
            var attrs = role.GetType().GetCustomAttributes<CategoryAttribute>();
            return attrs.SingleOrDefault()?.Category ?? throw new InvalidOperationException();
        }

        public static ExecutorType GetExecutorType(this Role role)
        {
            var attrs = role.GetType().GetCustomAttributes<ExecutorAttribute>();
            return attrs.SingleOrDefault()?.EType ?? throw new InvalidOperationException();
        }

        public static ChatScopeAttribute GetChatScopeAttribute(this Role role)
        {
            var attrs = role.GetType().GetCustomAttributes<ChatScopeAttribute>();
            return attrs.SingleOrDefault() ?? throw new InvalidOperationException();
        }
    }
}