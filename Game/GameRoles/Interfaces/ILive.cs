namespace GameRoles.Interfaces
{
    interface ILive
    {
        void Die();
        void Die(string message);
        void Revive();
        void Execute();
    }
}
