using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class EInvestigationInfoContext : NightInfoContext
    {
        public RoleSignature Info { get; }

        [JsonConstructor]
        public EInvestigationInfoContext(RoleSignature info)
        {
            Info = info;
        }
    }
}