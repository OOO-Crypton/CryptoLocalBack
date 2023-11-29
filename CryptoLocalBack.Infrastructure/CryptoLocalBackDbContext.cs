using Microsoft.EntityFrameworkCore;
using CryptoLocalBack.Domain;

namespace CryptoLocalBack.Infrastructure
{
    public sealed class CryptoLocalBackDbContext : DbContext
    {
        public DbSet<Settings> Settings { get; set; } = null!;
        public DbSet<Videocard> Videocards { get; set; } = null!;
        public DbSet<Monitoring> Monitorings { get; set; } = null!;

        public CryptoLocalBackDbContext(DbContextOptions<CryptoLocalBackDbContext> options)
            : base(options)
        {
        }

        public CryptoLocalBackDbContext() : base() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}