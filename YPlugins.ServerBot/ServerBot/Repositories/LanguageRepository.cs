using ServerBot.Entities;

namespace ServerBot.Repositories
{
    public static class LanguageRepository
    {
        public static Languages GetLanguageByCode(string Code)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                Languages result = applicationContext.Languages.Where(u => u.Code == Code).FirstOrDefault();
                return result;
            }
        }
    }
}
