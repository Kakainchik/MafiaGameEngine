namespace GameLogic.Roles
{
    /// <summary>
    /// Роль ведьмы. Самостоятельный и относится к <see cref="Teams.NEUTRAL"/>.
    /// Каждую ночь может заставить кого-то выполнить действие своей роли на цель, которую выберет, даже если тот пропустил ход.
    /// </summary>
    public class WitchRole : Role
    {
        public override void OnWasDied()
        {
            throw new System.NotImplementedException();
        }
    }
}