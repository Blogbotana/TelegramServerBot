using ServerBot.Entities;

namespace ServerBot.Repositories
{
    public static class LanguageRepository
    {
        public static LanguageEntity GetLanguageOfUser(UserEntity userEntity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var user = context.Users.Where(u => u.Id == userEntity.Id).FirstOrDefault();
                if (user == null)
                    throw new Exception("Не удалось получить язык, потому что не нашли пользователя в базе данных");

                return user.Language;
            }

        }
    }
}
