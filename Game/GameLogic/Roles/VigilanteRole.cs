using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль виджиланте. Относится к <see cref="Team.TOWN">Городу</see>.
    /// Каждую ночью может убить кого-то, пока не кончатся патроны.
    /// </summary>
    [Executor(ExecutorType.TARGET)]
    [Category(RCategory.KILLING)]
    [Team(Team.TOWN)]
    public class VigilanteRole : Role, IExecutor
    {
        private IAction killAction;
        private int bullets;

        public override bool IsUnique => false;

        public bool HasBullets => bullets > 0;

        public int Bullets
        {
            get => bullets;
            set => bullets = value;
        }

        public VigilanteRole()
        {
            
        }

        public IAction PrepareAction(Role subject)
        {
            killAction = new KillAction(subject, this);

            return killAction;
        }

        public void DisposeAction()
        {
            killAction = null;
        }

        public void LoadGun() => bullets--;

        public override string ToString()
        {
            return $"{killAction.Priority}: {typeof(VigilanteRole).Name}";
        }
    }
}