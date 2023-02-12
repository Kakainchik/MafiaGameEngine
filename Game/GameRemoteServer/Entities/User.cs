using System.ComponentModel.DataAnnotations;

namespace GameRemoteServer.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [MinLength(5)]
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        public string Hash { get; set; } = null!;

        public List<RefreshToken> RefreshTokens { get; set; } = null!;
    }
}