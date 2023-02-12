namespace GameLogic.Model
{
    public abstract class DetailDecorator : BaseDetail
    {
        protected BaseDetail detail;

        public DetailDecorator(BaseDetail detail) 
            : base(detail.Type, detail.Executor, detail.PrimaryTarget)
        {
            this.detail = detail;
        }
    }
}