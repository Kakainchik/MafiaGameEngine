using System.Text.Json.Serialization;

namespace Net.Contexts.Morning
{
    [Serializable]
    public class MorningContext : SessionContext
    {
        public byte Deaths { get; }
        public byte RTown { get; }
        public byte RMafia { get; }
        public byte RCultus { get; }
        public byte RUndead { get; }
        public byte RNeutral { get; }

        [JsonConstructor]
        public MorningContext(byte deaths,
            byte rtown = 0,
            byte rmafia = 0,
            byte rcultus = 0,
            byte rundead = 0,
            byte rneutral = 0)
        {
            Deaths = deaths;
            RTown = rtown;
            RMafia = rmafia;
            RCultus = rcultus;
            RUndead = rundead;
            RNeutral = rneutral;
        }
    }
}