using Microsoft.EntityFrameworkCore;

namespace CryptoLocalBack.Extensions
{
    internal static class DbContextExtension
    {
        public static IServiceCollection AddNpgsqlDbContext<TDbContext>(
            this IServiceCollection services,
            string connectionString) where TDbContext : DbContext
                => services.AddDbContext<TDbContext>(options =>
                {
                    options.UseNpgsql(ConnectionStringExtension
                        .AppendApplicationName(connectionString));
                });
    }
}
