namespace GameLogic.Roles
{
    /// <summary>
    /// Роль горожанина. Относится к <see cref="Teams.CITY">Городу</see>.
    /// Нет никаких способностей.
    /// </summary>
    public class CitizenRole : Role
    {
        public CitizenRole()
        {
            base.priority = 32; 
            base.Team = Teams.CITY;
        }

        public override void OnWasDied()
        {
            throw new System.NotImplementedException();
        }
    }
}