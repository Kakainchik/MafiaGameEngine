namespace GameLogic.Roles
{
    /// <summary>
    /// Роль террориста. Самостоятельный и относится к <see cref="Teams.NEUTRAL"/>.
    /// Может ночью один раз совершить суицидальный взрыв в доме какого-либо человека, тем самым убивая себя и всех, кто находился в этом доме в данную ночь.
    /// </summary>
    public class TerroristRole : Role
    {
        public override void OnWasDied()
        {
            throw new System.NotImplementedException();
        }
    }
}