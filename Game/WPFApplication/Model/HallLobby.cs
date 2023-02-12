namespace WPFApplication.Model
{
    public record HallLobby(
        long Id,
        string Title,
        string Host,
        int Fullness,
        int MaxSeats,
        bool IsFull);
}