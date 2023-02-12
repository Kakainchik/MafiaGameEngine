namespace GameLogic.Interfaces
{
    /// <summary>
    /// Represents an object which can be voted by <see cref="IVoter"/>
    /// </summary>
    public interface IVotable
    {
        IList<IVoter> Voters { get; set; }
        int Votes { get; }

        bool AddVoteFrom(IVoter from);
        void RemoveVote(IVoter voter);
    }
}