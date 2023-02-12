using GameLogic.Actions.ActionTemplates;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class BlockInfoContext : NightInfoContext
    {
        public BlockInfo Info { get; }

        public BlockInfoContext(BlockInfo info)
        {
            //Show info as is
            Info = info;
        }
    }
}