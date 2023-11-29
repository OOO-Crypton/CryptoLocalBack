using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLocalBack.Infrastructure
{
    public class CryptoLacalBackFactory : IDesignTimeDbContextFactory<CryptoLocalBackDbContext>
    {
        public CryptoLocalBackDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CryptoLocalBackDbContext>();
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=cryptodb;User Id=postgres;Password=postgres;");
            //optionsBuilder.UseNpgsql("Server=172.18.0.2;Port=5432;Database=cryptodb;User Id=postgres;Password=postgres;");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            return new CryptoLocalBackDbContext(optionsBuilder.Options);
        }
    }
}
