using GameLogic.Actions.ActionTemplates;

namespace Net.Contexts.Night.ActionInfo
{
    [Serializable]
    public class EDetectInfoContext : NightInfoContext
    {
        public DetectInfo Info { get; }

        public EDetectInfoContext(DetectInfo info)
        {
            Info = info;
        }
    }
}