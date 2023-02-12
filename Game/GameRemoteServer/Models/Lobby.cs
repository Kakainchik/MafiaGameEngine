using GameRemoteServer.Entities;
using Net.Models;
using System.ComponentModel.DataAnnotations;

namespace GameRemoteServer.Models
{
    public class LobbyDomain
    {
        private static long SharedId;

        public long Id { get; } = ++SharedId;

        public User Host { get; init; } = null!;
        public string Title { get; init; } = null!;
        public DateTime Created { get; }
        public DateTime Expires { get; }
        public IDictionary<long, LobbyPlayer> Players { get; init; } = null!;

        public string? Description { get; set; }
        [Range(5, 50)]
        public int MaxSeats { get; set; }

        public int Fullness => Players.Count;
        public bool IsFull => Fullness == MaxSeats;
        public bool IsValid => Expires - DateTime.Now > TimeSpan.Zero;
    }

    public record LobbyDTO(
        long Id,
        string Title,
        string Host,
        int Fullness,
        int MaxSeats,
        bool IsFull);

    public record class ReqCreateLobbyDTO(
        string Title,
        string? Description,
        [Range(5, 50)] int MaxSeats);

    public record class ResCreateLobbyDTO(long Id);
}