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

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                optionBuilder.UseNpgsql(File.ReadAllText("server.txt"));
                optionBuilder.EnableSensitiveDataLogging();
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LanguageEntity>()
                .HasData(GetAllLanguages());

            modelBuilder.Entity<LicenseEntity>()
                .HasData(GetAllLicenses());

        }

        private LanguageEntity[] GetAllLanguages()
        {
            return new LanguageEntity[] 
            { 
                new LanguageEntity { Id = 1, EnglishName = "English", IETF_LanguageTag = "en" },
                new LanguageEntity { Id = 2, EnglishName = "Russian", IETF_LanguageTag = "ru" },
                new LanguageEntity { Id = 3, EnglishName = "French", IETF_LanguageTag = "fr" },
                new LanguageEntity { Id = 4, EnglishName = "Nederlands", IETF_LanguageTag = "nl" },
                new LanguageEntity { Id = 5, EnglishName = "German", IETF_LanguageTag = "de" },
                new LanguageEntity { Id = 6, EnglishName = "Italian", IETF_LanguageTag = "it" },
                new LanguageEntity { Id = 7, EnglishName = "Spanish", IETF_LanguageTag = "es" },
                new LanguageEntity { Id = 8, EnglishName = "Japanese", IETF_LanguageTag = "ja" },
                new LanguageEntity { Id = 9, EnglishName = "Chinese", IETF_LanguageTag = "zh" },
                new LanguageEntity { Id = 10, EnglishName = "Czech", IETF_LanguageTag = "cs" },
                new LanguageEntity { Id = 11, EnglishName = "Portuguese", IETF_LanguageTag = "pt" },
                new LanguageEntity { Id = 12, EnglishName = "Hungarian", IETF_LanguageTag = "hu" },
                new LanguageEntity { Id = 13, EnglishName = "Polish", IETF_LanguageTag = "pl" },
            };

        }

        private LicenseEntity[] GetAllLicenses()
        {
            return new LicenseEntity[]
            {
                new LicenseEntity { Id = 1, Program = "Tekla", Name = "Specifications", TypeOfLicense = Enums.TypeOfLicenses.Demo  },
                new LicenseEntity { Id = 2, Program = "Tekla", Name = "Excel report generator", TypeOfLicense = Enums.TypeOfLicenses.Demo  },
                new LicenseEntity { Id = 3, Program = "Tekla", Name = "Profile chooser", TypeOfLicense = Enums.TypeOfLicenses.Demo  },
            };
        }
    }
}
