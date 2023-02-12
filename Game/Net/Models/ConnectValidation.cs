namespace Net.Models
{
    public enum ConnectValidation : byte
    {
        /// <summary>
        /// The client successfully passed validation.
        /// </summary>
        ACCEPTED,
        GAME_RUNNING,
        LOBBY_IS_FULL,
        CANNOT_CONNECT
    }
}