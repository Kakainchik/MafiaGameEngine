using GameLogic.Model;

namespace GameLogic.Actions.ActionTemplates
{
    public abstract class ActionTemplate
    {
        protected IEnumerable<ActionLog> logs;

        public ActionTemplate(IEnumerable<ActionLog> logs)
        {
            this.logs = logs;
        }

        public abstract IEnumerable<BaseDetail> Convert();
    }
}