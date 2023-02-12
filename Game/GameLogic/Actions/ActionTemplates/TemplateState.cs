using GameLogic.Model;

namespace GameLogic.Actions.ActionTemplates
{
    public class TemplateState
    {
        private IEnumerable<ActionLog> executedActions;
        private ActionTemplate template;

        public TemplateState(IEnumerable<ActionLog> executedActions)
        {
            this.executedActions = executedActions;
        }

        /// <summary>
        /// Process all logs into compact and ordered format for providing information.
        /// </summary>
        /// <returns>Ordered information by type to show.</returns>
        public Queue<BaseDetail> ZipLogs()
        {
            Queue<BaseDetail> queue = new Queue<BaseDetail>();

            //Witch info first
            template = new WitchTemplate(executedActions);
            foreach(var z in template.Convert())
                queue.Enqueue(z);

            //Then driver info
            template = new DriveTemplate(executedActions);
            foreach(var z in template.Convert())
                queue.Enqueue(z);

            //Then blocking info
            template = new BlockTemplate(executedActions);
            foreach(var z in template.Convert())
                queue.Enqueue(z);

            //Then recruit info
            template = new RecruitTemplate(executedActions);
            foreach(var z in template.Convert())
                queue.Enqueue(z);

            //Then killing with doctor info
            template = new KillAndHealTemplate(executedActions);
            foreach(var z in template.Convert())
                queue.Enqueue(z);

            //Then investigator info
            template = new InvestigateTemplate(executedActions);
            foreach(var z in template.Convert())
                queue.Enqueue(z);

            //Then police info
            template = new PolicemanTemplate(executedActions);
            foreach(var z in template.Convert())
                queue.Enqueue(z);

            //Then blow info
            template = new BlowTemplate(executedActions);
            foreach(var z in template.Convert())
                queue.Enqueue(z);

            //Then end-night ressurect info
            template = new RessurectTemplate(executedActions);
            foreach(var z in template.Convert())
                queue.Enqueue(z);

            return queue;
        }
    }
}