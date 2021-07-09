using GameLogic.Interfaces;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль стандартной мафии. Относится к <see cref="Teams.MAFIA">Мафии</see>.
    /// Каждую ночь должен решить, кого убить.
    /// </summary>
    public class MafiaRole : Role, IAction
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