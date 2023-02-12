namespace GameLogic.Interfaces
{
    /// <summary>
    /// Represents an object who can vote any <see cref="IVotable"/> instances.
    /// </summary>
    public interface IVoter
    {
        IVotable VoteTarget { get; set; }

        bool VoteFor(IVotable whom);
        void Unvote();
    }
}