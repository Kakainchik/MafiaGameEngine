using GameLogic.Interfaces;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль доктора. Относится к <see cref="Teams.CITY">Городу</see>.
    /// Каждую ночь может защитить одного человека от смерти в течение данной ночи.
    /// </summary>
    public class DoctorRole : Role, IAction
    {
        public void DoAction(Role who)
        {
            throw new System.NotImplementedException();
        }

        public override void OnWasDied()
        {
            throw new System.NotImplementedException();
        }
    }
}