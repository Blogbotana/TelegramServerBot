using Microsoft.EntityFrameworkCore;
using ServerBot.Entities;
using System.IO;

namespace ServerBot
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<LanguageEntity> Languages { get; set; }
        public DbSet<LicenseEntity> Licenses { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                optionBuilder.UseNpgsql(File.ReadAllText("server.txt"));
                optionBuilder.EnableSensitiveDataLogging();
            }
        }
    }
}
