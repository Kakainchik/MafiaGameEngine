using GameRemoteServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameRemoteServer
{
    public class ServerContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public ServerContext(DbContextOptions<ServerContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}