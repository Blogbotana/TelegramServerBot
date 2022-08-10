using ServerBot.Entities;
using System.Globalization;

namespace ServerBot.Repositories
{
    public static class LanguageRepository
    {
        public static LanguageEntity GetLanguageOfUser(int userId)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var user = context.Users.Where(u => u.Id == userId).FirstOrDefault();
                
                return user.Language;
            }

        }

        public static LanguageEntity GetLanguageByCode(string langCode)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var language = context.Languages.Where(u => u.IETF_LanguageTag == langCode).FirstOrDefault();
                if (language == null)
                    return GetDefaultLanguage();
                else
                    return language;
            }
        }

        public static LanguageEntity GetDefaultLanguage()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var language = context.Languages.Where(u => u.Id == 1).FirstOrDefault();
                if (language == null)
                    throw new Exception("Default language didn't sing");
                else
                    return language;
            }
        }
    }
}
