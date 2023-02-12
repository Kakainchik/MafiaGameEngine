namespace GameLogic.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class CategoryAttribute : Attribute
    {
        private readonly RCategory type;

        public RCategory Category => type;

        public CategoryAttribute(RCategory type)
        {
            this.type = type;
        }
    }

    public enum RCategory : byte
    {
        GOVERMENT,
        KILLING,
        SUPPORT,
        INVESTIGATE
    }
}