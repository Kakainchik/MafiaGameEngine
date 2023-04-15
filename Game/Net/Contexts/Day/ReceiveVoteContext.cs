using System.Text.Json.Serialization;

namespace Net.Contexts.Day
{
    [Serializable]
    public class ReceiveVoteContext : SessionContext
    {
        public Guid VoterId { get; }
        public VoteTarget? CurrentT { get; }
        public VoteTarget? PreviousT { get; }

        public ReceiveVoteContext(Guid voterId,
            Guid targetId,
            int votes,
            VoteTarget? previousT = null)
        {
            VoterId = voterId;
            CurrentT = new VoteTarget(targetId, votes);
            PreviousT = previousT;
        }

        [JsonConstructor]
        public ReceiveVoteContext(Guid voterId,
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
        public Guid Id { get; }
        public int Votes { get; }

        [JsonConstructor]
        public VoteTarget(Guid id, int votes)
        {
            Id = id;
            Votes = votes;
        }
    }
}