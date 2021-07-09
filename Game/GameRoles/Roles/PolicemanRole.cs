using GameLogic.Interfaces;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль полицейского. Относится к <see cref="Teams.CITY">Городу</see>.
    /// Каждую ночь может проверить одного человека и узнать его принадлежность к какой-либо <see cref="Teams">стороне</see>, узнаёт на следующую ночь.
    /// Крёстного отца видит как Город.
    /// </summary>
    public class PolicemanRole : Role, IAction
    {
        public void DoAction(Role who)
        {
            
        }

        public override void OnWasDied()
        {
            throw new System.NotImplementedException();
        }
    }
}