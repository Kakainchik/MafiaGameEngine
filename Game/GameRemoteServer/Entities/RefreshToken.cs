using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GameRemoteServer.Entities
{
    [Owned]
    public class RefreshToken
    {
        [Key]
        public long Id { get; set; }

        public string Token { get; set; } = null!;

        public DateTime Created { get; set; }

        public DateTime Experies { get; set; }

        public DateTime? Revoked { get; set; }

        public string? ReasonRevoked { get; set; }

        public string? ReplacedByToken { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Experies;

        public bool IsRevoked => Revoked.HasValue;

        public bool IsActive => !IsRevoked && !IsExpired;
    }
}