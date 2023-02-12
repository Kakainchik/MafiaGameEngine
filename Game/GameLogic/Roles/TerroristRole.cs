using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль террориста. Самостоятельный и относится к <see cref="Team.NEUTRAL"/>.
    /// Может ночью один раз совершить суицидальный взрыв в доме какого-либо человека, 
    /// тем самым убивая себя и всех, кто находился в этом доме в данную ночь.
    /// </summary>
    [Executor(ExecutorType.TARGET)]
    [Category(RCategory.GOVERMENT)]//REMARK
    [Team(Team.TERRORIST)]
    public class TerroristRole : Role, IExecutor
    {
        private IAction blowAction;

        public override bool IsUnique => false;

        public TerroristRole()
        {
            
        }

        public IAction PrepareAction(Role subject)
        {
            blowAction = new BlowAction(subject, this);

            return blowAction;
        }

        public void DisposeAction()
        {
            blowAction = null;
        }

        public override string ToString()
        {
            return $"{blowAction.Priority}: {typeof(TerroristRole).Name}";
        }
    }
}