using Microsoft.EntityFrameworkCore;
using ServerBot.Entities;

namespace ServerBot
{
    public class ApplicationContext : DbContext
    {
        public DbSet<TgUserEntity> TgUsers { get; set; }
        public DbSet<Languages> Languages { get; set; }

        public ApplicationContext()
        {

        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                optionBuilder.UseNpgsql("Host=localhost;port=5432;Database=postgres;username=postgres;password=K7esKvAX");
                //optionBuilder.UseNpgsql(ConfigurationManager.AppSettings["ConnectionString"]);
                optionBuilder.EnableSensitiveDataLogging();
            }
        }
    }
}
