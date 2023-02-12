using Net.Models;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class EInvestigationInfoContext : NightInfoContext
    {
        public RoleSignature Info { get; }

        public EInvestigationInfoContext(RoleSignature info)
        {
            Info = info;
        }
    }
}