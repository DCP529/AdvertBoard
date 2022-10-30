using Microsoft.EntityFrameworkCore;

namespace Models.ModelsDb
{
    public class AdvertBoardDbContext : DbContext
    {
        public DbSet<UserDb> Users { get; set; }

        public DbSet<AdvertDb> Adverts { get; set; }

        public AdvertBoardDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;" +
                "Port=5433;" +
                "Database=AdvertBoard;" +
                "Username=postgres;" +
                "Password=super200;");
        }
    }
}
