using System.Text.Json.Serialization;

namespace Net.Contexts.Day
{
    [Serializable]
    public class ReceiveVoteContext : SessionContext
    {
        public ulong VoterId { get; }
        public VoteTarget? CurrentT { get; }
        public VoteTarget? PreviousT { get; }

        public ReceiveVoteContext(ulong voterId,
            ulong targetId,
            int votes,
            VoteTarget? previousT = null)
        {
            VoterId = voterId;
            CurrentT = new VoteTarget(targetId, votes);
            PreviousT = previousT;
        }

        [JsonConstructor]
        public ReceiveVoteContext(ulong voterId,
            VoteTarget? currentT,
            VoteTarget? previousT = null)
        {
            VoterId = voterId;
            CurrentT = currentT;
            PreviousT = previousT;
        }
    }

    [Serializable]
    public class VoteTarget
    {
        public ulong? Id { get; }
        public int Votes { get; }

        [JsonConstructor]
        public VoteTarget(ulong? id, int votes)
        {
            Id = id;
            Votes = votes;
        }
    }
}