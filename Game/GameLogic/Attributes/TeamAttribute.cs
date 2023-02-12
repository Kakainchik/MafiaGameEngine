using GameLogic.Model;

namespace GameLogic.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TeamAttribute : Attribute
    {
        private readonly Team team;

        public TeamAttribute(Team team)
        {
            this.team = team;
        }

        public Team ItsTeam => team;
    }
}