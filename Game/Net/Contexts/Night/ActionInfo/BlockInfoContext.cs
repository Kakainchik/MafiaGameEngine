using GameLogic.Actions.ActionTemplates;
using System.Text.Json.Serialization;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class BlockInfoContext : NightInfoContext
    {
        public BlockInfo Info { get; }

        [JsonConstructor]
        public BlockInfoContext(BlockInfo info)
        {
            //Show info as is
            Info = info;
        }
    }
}