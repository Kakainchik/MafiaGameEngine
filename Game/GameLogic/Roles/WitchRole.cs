using GameLogic.Actions;
using GameLogic.Attributes;
using GameLogic.Interfaces;
using GameLogic.Model;

namespace GameLogic.Roles
{
    /// <summary>
    /// Роль ведьмы. Самостоятельный и относится к <see cref="Team.NEUTRAL"/>.
    /// Каждую ночь может заставить кого-то выполнить действие своей роли на цель,
    /// которую выберет, даже если тот пропустил ход.
    /// </summary>
    [Executor(ExecutorType.EXECUTOR_TARGER)]
    [Category(RCategory.GOVERMENT)]
    [Team(Team.WITCH)]
    public class WitchRole : Role, IDoubleExecutor
    {
        private IAction controlAction;
        private Role secondary;

        public override bool IsUnique => false;

        public WitchRole()
        {
            
        }

        public IAction PrepareAction(Role subject)
        {
            controlAction = new ControlAction(subject, secondary, this);

            return controlAction;
        }

        public void SetSecondarySubject(Role secondary)
        {
            this.secondary = secondary;
        }

        public void DisposeAction()
        {
            controlAction = null;
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
            return $"{controlAction.Priority}: {typeof(WitchRole).Name}";
        }
    }
}