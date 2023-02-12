using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль водителя. Относится к <see cref="Team.TOWN">Городу</see>.
    /// Каждую ночь может поменять местами двух людей, вследствие чего действия,
    /// направленные ночью на них, поменяются между ними.
    /// </summary>
    [Executor(ExecutorType.TARGET_TARGET)]
    [Category(RCategory.SUPPORT)]
    [Team(Team.TOWN)]
    public class DriverRole : Role, IDoubleExecutor
    {
        private IAction driveAction;
        private Role secondary;

        public override bool IsUnique => false;

        public DriverRole()
        {
            
        }

        public IAction PrepareAction(Role subject)
        {
            driveAction = new DriveAction(subject, secondary, this);

            return driveAction;
        }

        public void SetSecondarySubject(Role secondary)
        {
            this.secondary = secondary;
        }

        public void DisposeAction()
        {
            driveAction = null;
            secondary = null;
        }

        public override ActionLog Block()
        {
            //Non-blockable due to priority
            return new ActionLog
            {
                Success = false,
                PrimaryTarget = this
            };
        }

        public override string ToString()
        {
            return $"{driveAction.Priority}: {typeof(DriverRole).Name}";
        }
    }
}