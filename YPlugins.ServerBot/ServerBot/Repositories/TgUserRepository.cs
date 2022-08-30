using Microsoft.EntityFrameworkCore;
using ServerBot.DTO;
using ServerBot.Entities;
using System;

namespace ServerBot.Repositories
{
    public static class TgUserRepository
    {
        /// <summary>
        /// Create user to database
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns>The number of state entries written to the database.</returns>
        /// <exception cref="Exception"></exception>
        public static int CreateUser(UserEntity userEntity)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var lang = applicationContext.Languages.FirstOrDefault(l =>
                l.Id == userEntity.Language.Id);

                if (lang == null)
                    lang = LanguageRepository.GetDefaultLanguage();

                var user = applicationContext.Users.Where(u=>u.TgId == userEntity.TgId).FirstOrDefault();
                if (user != null)
                    return 0;

                userEntity.Language = lang;
                applicationContext.Users.Add(userEntity);
                return applicationContext.SaveChanges();
            }
        }

        public static UserEntity? GetUserByTgId(long userTgId)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                UserEntity user = context.Users.Where(u => u.TgId == userTgId).FirstOrDefault();

                if(user == null)
                    return null;

                context.Entry(user).Reference(t => t.Language).Load();
                context.Entry(user).Collection(c => c.Licenses).Load();
                return user;
            }
        }

        public static UserEntity? GetUserByEmail(string email)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                UserEntity user = context.Users.Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefault();

                if (user == null)
                    return null;

                context.Entry(user).Reference(t => t.Language).Load();
                context.Entry(user).Collection(c => c.Licenses).Load();
                return user;
            }
        }

        public static bool EditUser(UserEntity userEntity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var user = context.Users.Where(u => u.Id == userEntity.Id).FirstOrDefault();
                if (user == null)
                    return false;
                //I don't know is it ok or no
                user.Email = userEntity.Email;
                user.ComputerInformation = userEntity.ComputerInformation;
                user.Licenses = userEntity.Licenses;
                user.Language = userEntity.Language;
                user.LastLogin = userEntity.LastLogin;
                user.FirstName = userEntity.FirstName;
                user.LastName = userEntity.LastName;
                user.TgId = userEntity.TgId;

                context.SaveChanges();
                return true;
            }
        }

        public static void SetThisLanguageForUser(UserEntity userEntity, LanguageEntity languageEntity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var user = context.Users.Where(u => u.Id == userEntity.Id).FirstOrDefault();
                if (user == null)
                    return;

                var language = context.Languages.Where(l => l.IETF_LanguageTag == languageEntity.IETF_LanguageTag).FirstOrDefault();
                if (language == null)
                    language = LanguageRepository.GetDefaultLanguage();

                user.Language = language;
                context.SaveChanges();
            }
        }

        internal static void SetThisLicenseForUser(UserEntity userEntity, LicenseEntity licenseEntity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var user = context.Users.Where(u => u.Id == userEntity.Id).FirstOrDefault();
                if (user == null)
                    return;

                if(user.Licenses == null)
                    user.Licenses = new List<LicenseEntity>();

                if (user.Licenses.Contains(licenseEntity))
                {
                    //TODO understand how to work with it if user already have a license
                }
                else
                {

                }
                user.Licenses.ToList().Add(licenseEntity);

                context.SaveChanges();
            }
        }
    }
}
